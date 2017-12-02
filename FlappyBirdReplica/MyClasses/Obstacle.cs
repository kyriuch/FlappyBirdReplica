using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlappyBirdReplica.MyClasses
{
	class Obstacle : Entity
	{
		public float MoveSpeed { get; set; }
		public Action OnScreenLeft { get; set; }

		public override void Update()
		{
			Position = new Vector2(Position.X - MoveSpeed * Time.DeltaTime, Position.Y);

			if (Position.X < -80)
			{
				if (OnScreenLeft != null)
					OnScreenLeft();

				foreach (RectangleCollider collider in Colliders)
					collider.AlreadyPointed = false;
			}	
		}
	}
}
