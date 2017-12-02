using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FlappyBirdReplica.MyClasses
{
	class Bird : Entity
	{
		public float JumpForce { get; set; }

		private bool jumpPressed = false;

		public override void Update()
		{
			if(Keyboard.GetState().IsKeyDown(Keys.Space) && !jumpPressed && !Game1.gameOver)
			{
				jumpPressed = true;
				Velocity = new Vector2(0, JumpForce);
				
			}

			if (Game1.gameOver && Position.Y > GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height)
			{
				Position = new Vector2(30, 100);
				SimulatePhysics = false;
			}
			if (Keyboard.GetState().IsKeyUp(Keys.Space))
			{
				jumpPressed = false;
			}
		}
	}
}
