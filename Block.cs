using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;

namespace ld28_2
{
	class Block
	{
		public int CollideFlag;
		public IntRect Texture;

		public Block(int CollideFlag, IntRect Texture)
		{
			this.CollideFlag = CollideFlag;
			this.Texture = Texture;
		}

		public static Block Wood = new Block(1, new IntRect(0, 0, 16, 16));
		public static Block Bricks = new Block(0, new IntRect(16, 0, 16, 16));
		public static Block MossBricks = new Block(0, new IntRect(32, 0, 16, 16));
		public static Block CrackBricks = new Block(0, new IntRect(48, 0, 16, 16));
		public static Block DestroyedBricks = new Block(1, new IntRect(0, 16, 16, 16));
		public static Block WetWood = new Block(1, new IntRect(16, 16, 16, 16));
		public static Block NyanGun = new Block(4, new IntRect(32, 16, 16, 16));
		public static Block B0 = new Block(0, new IntRect(48, 16, 16, 16));
		public static Block B1 = new Block(0, new IntRect(0, 32, 16, 16));
		public static Block B2 = new Block(0, new IntRect(16, 32, 16, 16));
		public static Block B3 = new Block(0, new IntRect(0, 48, 16, 16));
		public static Block B4 = new Block(0, new IntRect(16, 48, 16, 16));
		public static Block B5 = new Block(1, new IntRect(32, 32, 16, 16));
		public static Block B6 = new Block(1, new IntRect(48, 32, 16, 16));
		public static Block B7 = new Block(1, new IntRect(32, 48, 16, 16));
		public static Block B8 = new Block(1, new IntRect(48, 48, 16, 16));

		public static Block Finish = new Block(3, new IntRect(0, 0, 16, 16));
		public static Block EntityDestroyer = new Block(2, new IntRect(0, 0, 16, 16));
		public static Block Monolog = new Block(5, new IntRect(0, 0, 16, 16));
		public static Block OfficeEnter = new Block(3, new IntRect(0, 0, 16, 16));
	}
}
