using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace FlappyBirdReplica.MyClasses
{
	public abstract class Entity
	{
		public Vector2 Position { get; set; }
		public Vector2 Scale { get; set; }
		public Texture2D Texture2D { get; set; }
		public bool SimulatePhysics { get; set; }
		public List<Collider> Colliders { get; set; }

		protected Vector2 Velocity { get; set; }

		public void SelfCalculations()
		{
			if(SimulatePhysics)
			{
				Velocity = Vector2.Clamp(new Vector2(Velocity.X - (Physics.Friction * Time.DeltaTime), Velocity.Y - (Physics.Gravity * Time.DeltaTime)),
				new Vector2(0, -Physics.Gravity), Vector2.One * int.MaxValue);

				Position = new Vector2(Position.X + Velocity.X, Position.Y - Velocity.Y);
			}
		}

		public abstract void Update();
	}
}
