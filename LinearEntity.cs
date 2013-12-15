using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;

namespace ld28_2
{
	class LinearEntity : Entity
	{
		public float x, y;

		public LinearEntity(Location start, float rad, float speed) : base(new Texture("bullet.png"))
		{
			Location = start;
			rad += (float)Math.PI * 0.5f;
			x = (float)Math.Sin(rad) * speed;
			y = (float)Math.Cos(rad) * speed;
			rect.Size = new SFML.Window.Vector2f(6, 6);
			rect.TextureRect = new IntRect(0, 0, 12, 12);
			rect.Origin = new SFML.Window.Vector2f(3, 3);
			radius = 3;
		}

		public override void Update()
		{
			Move(-x, y);
			base.Update();
		}
	}
}
