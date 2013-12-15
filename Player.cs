using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;

namespace ld28_2
{
	delegate bool Shoot(Location l, float r);

	class Player : Entity
	{
		public bool Armed = false;
		public DateTime last = DateTime.Now;

		public bool canShoot = false;
		public event Shoot OnShoot;

		public Player()
			: base(new Texture("Content/player.png"))
		{
		}

		public void Arm()
		{
			Armed = true;
			canShoot = true;
		}

		public void Shoot()
		{
			if (Armed && canShoot)
			{
				if (Math.Abs((DateTime.Now - last).TotalMilliseconds) > 500)
				{
					if (OnShoot != null)
					{
						if (OnShoot(Location, RadRotation))
						{
							canShoot = false;
							last = DateTime.Now;
						}
					}
				}
			}
		}
	}
}
