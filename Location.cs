using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Window;

namespace ld28_2
{
	struct Location
	{
		public Vector2i Vec2i
		{
			get
			{
				return new Vector2i((int)X, (int)Y);
			}
			set
			{
				X = value.X;
				Y = value.Y;
			}
		}
		public Vector2f Vec2f
		{
			get
			{
				return new Vector2f(X, Y);
			}
			set
			{
				X = value.X;
				Y = value.Y;
			}
		}

		public float X { get; set; }
		public float Y { get; set; }

		public static Location operator *(Location a, float b)
		{
			return new Location() { X = a.X * b, Y = a.Y * b };
		}

		public static Location operator *(Location a, Location b)
		{
			return new Location() { X = a.X * b.X, Y = a.Y * b.Y };
		}

		public static Location operator +(Location a, float b)
		{
			return new Location() { X = a.X + b, Y = a.Y + b };
		}

		public static Location operator +(Location a, Location b)
		{
			return new Location() { X = a.X + b.X, Y = a.Y + b.Y };
		}

		public static Location operator -(Location a, Location b)
		{
			return new Location() { X = a.X - b.X, Y = a.Y - b.Y };
		}

		public float DistSquared(Location b)
		{
			return (b.X - X) * (b.X - X) + (b.Y - Y) * (b.Y - Y);
		}
	}
}
