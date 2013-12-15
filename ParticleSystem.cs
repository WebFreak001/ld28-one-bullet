using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Window;
using SFML.Graphics;

namespace ld28_2
{
	class Particle
	{
		public Location location { get; set; }
		public float time { get; set; }
	}

	class ParticleSystem
	{
		Texture texture;
		List<Particle> particles;
		Sprite particle;
		Random random;

		public ParticleSystem(Texture tex)
		{
			particles = new List<Particle>();
			texture = tex;
			particle = new Sprite();
			particle.Texture = texture;
			random = new Random();
		}

		public void Emit(Location l)
		{
			particles.Add(new Particle() { location = l + new Location() { X = (float)random.NextDouble() * 10 - 5, Y = (float)random.NextDouble() * 10 - 5 }, time = 1 });
		}

		public void Draw(RenderTarget t, Location offset)
		{
			List<Particle> toRemove = new List<Particle>();
			for(int i = 0; i < particles.Count; i++)
			{
				particles[i].time -= 0.01f;
				if(particles[i].time <= 0) toRemove.Add(particles[i]);
				else {
					particle.Position = particles[i].location.Vec2f + offset.Vec2f;
					particle.Color = new Color(255, 255, 255, (byte)(particles[i].time * 255));
					t.Draw(particle);
				}
			}
			for(int i = 0; i < toRemove.Count; i++) particles.Remove(toRemove[i]);
			toRemove.Clear();
		}
	}
}
