using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;
using SFML.Window;

namespace ld28_2
{
	class Entity
	{
		public Location Location = new Location();
		public float xa, ya;
		public float axa, aya;
		public float Rotation;
		public float RadRotation;
		public int Health = 1000;
		public float radius = 16;

		protected RectangleShape rect;
		protected Texture texture;

		public Entity(Texture tex)
		{
			texture = tex;
			rect = new RectangleShape();
			tex.Smooth = false;
			rect.TextureRect = new IntRect(0, 0, 16, 16);
			rect.Size = new Vector2f(32, 32);
			rect.Origin = new Vector2f(16, 16);
		}

		public void Draw(RenderTarget window, Location offset)
		{
			rect.Rotation = -Rotation - 90;
			rect.Position = Location.Vec2f + offset.Vec2f;
			rect.Texture = texture;
			window.Draw(rect);
		}

		public void Stuck()
		{
			xa = ya = axa = aya = 0;
		}

		public Location PreUpdate()
		{
			Location Location = this.Location;

			Location.X += (float)Math.Sin(RadRotation) * xa;
			Location.Y += (float)Math.Cos(RadRotation) * xa;
			Location.X += (float)Math.Cos(RadRotation) * ya;
			Location.Y -= (float)Math.Sin(RadRotation) * ya;

			Location.X += axa;
			Location.Y += aya;

			return Location;
		}

		public virtual void Update()
		{
			Location.X += (float)Math.Sin(RadRotation) * xa;
			Location.Y += (float)Math.Cos(RadRotation) * xa;
			Location.X += (float)Math.Cos(RadRotation) * ya;
			Location.Y -= (float)Math.Sin(RadRotation) * ya;

			Location.X += axa;
			Location.Y += aya;

			xa *= 0.8f;
			ya *= 0.8f;
			axa *= 0.8f;
			aya *= 0.8f;
		}

		public void Rotate(Vector2i pos)
		{
			RadRotation = (float)Math.Atan2(Location.X - pos.X, Location.Y - pos.Y);
			Rotation = (float)(RadRotation * 180 / Math.PI);
		}

		public void Move(float x, float y)
		{
			xa += x * 0.5f;
			ya += y * 0.5f;
		}

		public void MoveAbsolute(float x, float y)
		{
			axa += x * 0.5f;
			aya += y * 0.5f;
		}
	}
}
