using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlappyBirdReplica.MyClasses
{
	class Entity
	{
		public float X { get; set; }
		public float Y { get; set; }
		public Vector2 Scale { get; set; }
		public Vector2 Velocity { get; set; }
		public Vector2 Acceleration { get; set; }
		public Texture2D Texture2D { get; set; }

		public void CalculateVelocity()
		{
			Velocity = new Vector2(Velocity.X + Acceleration.X, 
				Velocity.Y + Acceleration.Y);
		}

		public void CalculatePosition()
		{
			X += Velocity.X;
			Y += Velocity.Y;
		}
	}
}
