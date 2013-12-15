using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

using SFML.Window;
using SFML.Graphics;

namespace ld28_2
{
	class Level
	{
		public Block[,] Blocks;
		public uint Width;
		public uint Height;
		public Block OutterBlock;
		public Location Start;
		public List<Entity> Entities;
		public List<Location> Lights;
		public Texture Tiles;
		public Texture Sitting;
		public RectangleShape Tile;
		public Random random;
		public Location offset;
		public Location orginalOffset;
		public bool Office;
		public bool shaking;

		public Level()
		{
			Entities = new List<Entity>();
			Tiles = new Texture("Content/tiles.png");
			Tiles.Smooth = false;
			Sitting = new Texture("Content/sitting.png");
			Sitting.Smooth = false;
			Tile = new RectangleShape();
			Tile.Size = new Vector2f(32, 32);
			Tile.Texture = Tiles;
			Tile.TextureRect = new IntRect(16, 0, 16, 16);

			random = new Random();
		}

		public void AddEntity(Location l)
		{
			Entity e = new Entity(new Texture("Content/enemy.png"));
			e.Location = l * 32 + 16;
			Entities.Add(e);
		}

		public void RemoveGun()
		{
			for(int x = 0; x < Width; x++)
			{
				for (int y = 0; y < Height; y++)
				{
					if(Blocks[x,y] == Block.NyanGun) Blocks[x, y] = Block.Wood;
				}
			}
		}

		public void ShakeScreen(float amount, int shakes = 10)
		{
			if (!shaking)
			{
				shaking = true;
				new Thread(() =>
				{
					orginalOffset = offset;
					for (int i = 0; i < shakes; i++)
					{
						offset += new Location() { X = amount * 10, Y = 0 };
						Thread.Sleep(100);
						offset += new Location() { X = 0, Y = amount * 7 };
						Thread.Sleep(100);
						offset -= new Location() { X = amount * 10, Y = 0 };
						Thread.Sleep(100);
						offset -= new Location() { X = 0, Y = amount * 7 };
						Thread.Sleep(100);
					}
					shaking = false;
				}).Start();
			}
		}

		public void Load(string file)
		{
			Office = false;
			Entities.Clear();
			Lights = new List<Location>();
			using (StreamReader r = new StreamReader(file))
			{
				string level = "";
				int lineN = 0;
				string line;
				while ((line = r.ReadLine()) != null)
				{
					if (lineN == 0)
					{
						if (!uint.TryParse(line, out Width))
						{
							Width = 16;
							Console.Error.WriteLine("Can't Read Level Width");
						}
					}
					else if (lineN == 1)
					{
						if (!uint.TryParse(line, out Height))
						{
							Height = 16;
							Console.Error.WriteLine("Can't Read Level Height");
						}
					}
					else if (lineN == 2)
					{
						char b = line[0];
						switch (b)
						{
							case 'b': OutterBlock = Block.Bricks; break;
							case ' ': OutterBlock = Block.Wood; break;
							case 'd': OutterBlock = Block.DestroyedBricks; break;
							default: Console.Error.WriteLine("Can't Read Outter Level Block"); OutterBlock = Block.Bricks; break;
						}
					}
					else
					{
						level += line;
					}
					lineN++;
				}
				Blocks = new Block[Width, Height];
				for (int i = 0; i < level.Length; i++)
				{
					char b = level[i];
					switch (b)
					{
						case 'b': Blocks[i % Width, i / Width] = Block.Bricks; break;
						case ' ': Blocks[i % Width, i / Width] = Block.Wood; break;
						case 'd': Blocks[i % Width, i / Width] = Block.DestroyedBricks; break;
						case 'S': Blocks[i % Width, i / Width] = Block.Wood; Start = new Location() { X = i % Width, Y = i / Width }; break;
						case 'F': Blocks[i % Width, i / Width] = Block.Finish; break;
						case 'E': Blocks[i % Width, i / Width] = Block.Wood; AddEntity(new Location() { X = i % Width, Y = i / Width }); break;
						case 'D': Blocks[i % Width, i / Width] = Block.EntityDestroyer; break;
						case 'L': Blocks[i % Width, i / Width] = Block.Wood; Lights.Add(new Location() { X = i % Width, Y = i / Width }); break;
						case 'G': Blocks[i % Width, i / Width] = Block.NyanGun; break;
						case 'M': Blocks[i % Width, i / Width] = Block.Monolog; break;
						case 'T': Blocks[i % Width, i / Width] = Block.OfficeEnter; break;
						case '0': Blocks[i % Width, i / Width] = Block.B0; break;
						case '1': Blocks[i % Width, i / Width] = Block.B1; break;
						case '2': Blocks[i % Width, i / Width] = Block.B2; break;
						case '3': Blocks[i % Width, i / Width] = Block.B3; break;
						case '4': Blocks[i % Width, i / Width] = Block.B4; break;
						case '5': Blocks[i % Width, i / Width] = Block.B5; break;
						case '6': Blocks[i % Width, i / Width] = Block.B6; break;
						case '7': Blocks[i % Width, i / Width] = Block.B7; break;
						case '8': Blocks[i % Width, i / Width] = Block.B8; break;
						default: Console.Error.WriteLine("Can't Read Block: " + b); break;
					}
				}
			}
			DestroySome();
			int ww = (int)Width * 32;
			int hh = (int)Height * 32;
			int ox = ((int)Game.Width - ww) / 2;
			int oy = ((int)Game.Height - hh) / 2;
			offset = new Location() { X = ox, Y = oy };
		}

		public Location RandomLocation()
		{
			return new Location() { X = (int)random.Next((int)Width), Y = (int)random.Next((int)Height) };
		}

		public void Mutate(Location l)
		{
			try
			{
				Block b = Blocks[(int)l.X, (int)l.Y];
				if (b == Block.Wood) Blocks[(int)l.X, (int)l.Y] = Block.WetWood;
				if (b == Block.Bricks) Blocks[(int)l.X, (int)l.Y] = Block.MossBricks;
			}
			catch { }
		}

		public void DestroySome()
		{
			int populators = random.Next(9) + 1;
			for (int i = 0; i < populators; i++)
			{
				Location l = RandomLocation();
				int steps = 4 + random.Next(5);
				for (int j = 0; j < steps; j++)
				{
					int side = random.Next(4);
					if (side == 0)
					{
						l.X++;
					}
					if (side == 1)
					{
						l.Y++;
					}
					if (side == 2)
					{
						l.X--;
					}
					if (side == 3)
					{
						l.Y--;
					}
					Mutate(l);
				}
			}
		}

		public bool Intersect(Entity entity)
		{
			foreach (Entity e in Entities)
			{
				if (e != entity)
				{
					if (entity.Location.DistSquared(e.Location) <= e.radius * e.radius + entity.radius * entity.radius)
					{
						return true;
					}
				}
			}
			return false;
		}

		public bool Intersect(Entity entity, Location l, float radius)
		{
			if (entity.Location.DistSquared(l) <= radius * radius + entity.radius * entity.radius)
			{
				return true;
			}
			return false;
		}

		public int Move(Location l, float size = 32)
		{
			int tx = (int)(l.X / 32.0f);
			int ty = (int)(l.Y / 32.0f);

			try
			{
				if (tx < 0 || tx >= Width || ty < 0 || ty >= Height) return 0;
				Block b = Blocks[tx, ty];
				return b.CollideFlag;
			}
			catch { Console.Error.WriteLine("How?"); return 0; }
		}

		public void Draw(RenderTarget window, Player player)
		{
			for (int x = 0; x < Width; x++)
			{
				for (int y = 0; y < Height; y++)
				{
					Block b = Blocks[x, y];
					Tile.Texture = Tiles;
					Tile.TextureRect = b.Texture;
					Tile.Position = new Vector2f(x * 32, y * 32) + offset.Vec2f;
					window.Draw(Tile);
				}
			}
			if (Office)
			{
				Tile.Texture = Sitting;
				Tile.TextureRect = new IntRect(0, 0, 16, 16);
				Tile.Position = new Vector2f(3 * 32 + 6, 2 * 32 - 12) + offset.Vec2f;
				window.Draw(Tile);
			}
			List<Entity> toRemove = new List<Entity>();
			for (int i = 0; i < Entities.Count; i++)
			{
				Entities[i].Rotate(player.Location.Vec2i);
				if (Entities[i].Location.DistSquared(player.Location) <= 20 * 32 * 32)
				{
					Entities[i].Move(-0.8f, 0);
				}
				int r = Move(Entities[i].PreUpdate());
				if (r == 1)
				{
					Entities[i].Update();
				}
				else if (r == 2) toRemove.Add(Entities[i]);
				else Entities[i].Stuck();
				Entities[i].Draw(window, offset);
			}
			foreach (Entity e in toRemove)
			{
				Entities.Remove(e);
			}
			toRemove.Clear();
		}
	}
}
