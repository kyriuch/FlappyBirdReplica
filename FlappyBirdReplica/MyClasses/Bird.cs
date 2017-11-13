using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace FlappyBirdReplica.MyClasses
{
	class Bird : Entity
	{
		public float JumpForce { get; set; }

		public override void Update()
		{
			if(Keyboard.GetState().IsKeyDown(Keys.Space))
			{
				Velocity = new Vector2(0, JumpForce);
			}
		}
	}
}
