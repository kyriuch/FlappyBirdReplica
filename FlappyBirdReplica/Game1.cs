using FlappyBirdReplica.MyClasses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace FlappyBirdReplica
{

	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		Entity bird;
		List<Obstacle> obstacles;

		SpriteFont spriteFont;

		private int score = 0;

		public static bool gameOver = false;

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
		}

		protected override void Initialize()
		{
			Physics.Gravity = 9.81f;
			Physics.Friction = 0f;

			obstacles = new List<Obstacle>();

			base.Initialize();
		}


		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);

			Texture2D birdTexture = Content.Load<Texture2D>("bird");
			Texture2D obstacleTexture = Content.Load<Texture2D>("obstacle");

			spriteFont = Content.Load<SpriteFont>("Score");

			bird = new Bird
			{
				Position = new Vector2(30, 100),
				Scale = new Vector2(0.07f, 0.07f),
				Texture2D = birdTexture,
				SimulatePhysics = true,
				JumpForce = 3f,
				Colliders = new List<Collider>()
			};

			bird.Colliders.Add(new RectangleCollider
			{
				rectangle = new Rectangle(0, 0,
				(int)(bird.Texture2D.Width * bird.Scale.X),
				(int)(bird.Texture2D.Height * bird.Scale.Y))
			});

			Random random = new Random();

			for (int i = 0; i < 4; i++)
			{
				obstacles.Add(new Obstacle
				{
					Position = new Vector2(GraphicsDevice.PresentationParameters.Bounds.Width / 2 * i + GraphicsDevice.PresentationParameters.Bounds.Width, random.Next(-250, -50)),
					Scale = Vector2.One * 3f,
					Texture2D = obstacleTexture,
					SimulatePhysics = false,
					MoveSpeed = 200f,
					Colliders = new List<Collider>()
				});

				obstacles[i].Colliders.Add(new RectangleCollider
				{
					rectangle = new Rectangle(0, 0,
				(int)(obstacles[i].Texture2D.Width * obstacles[i].Scale.X),
				(int)(115 * obstacles[i].Scale.Y)),
					ShouldPoint = false
				});

				obstacles[i].Colliders.Add(new RectangleCollider
				{
					rectangle = new Rectangle(0, (int)(143 * obstacles[i].Scale.Y),
				(int)(obstacles[i].Texture2D.Width * obstacles[i].Scale.X),
				(int)(115 * obstacles[i].Scale.Y)),
					ShouldPoint = false
				});

				obstacles[i].Colliders.Add(new RectangleCollider
				{
					rectangle = new Rectangle(0, 0,
					(int)(obstacles[i].Texture2D.Width * obstacles[i].Scale.X),
					(int)(obstacles[i].Texture2D.Height * obstacles[i].Scale.Y)),
					ShouldPoint = true
				});
			}

			obstacles[0].OnScreenLeft += () => obstacles[0].Position = new Vector2(obstacles[3].Position.X + GraphicsDevice.PresentationParameters.Bounds.Width / 2, random.Next(-250, -50));
			obstacles[1].OnScreenLeft += () => obstacles[1].Position = new Vector2(obstacles[0].Position.X + GraphicsDevice.PresentationParameters.Bounds.Width / 2, random.Next(-250, -50));
			obstacles[2].OnScreenLeft += () => obstacles[2].Position = new Vector2(obstacles[1].Position.X + GraphicsDevice.PresentationParameters.Bounds.Width / 2, random.Next(-250, -50));
			obstacles[3].OnScreenLeft += () => obstacles[3].Position = new Vector2(obstacles[2].Position.X + GraphicsDevice.PresentationParameters.Bounds.Width / 2, random.Next(-250, -50));
		}

		protected override void UnloadContent()
		{

		}

		protected override void Update(GameTime gameTime)
		{
			Time.UpdateTime(gameTime);

			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			if(Keyboard.GetState().IsKeyDown(Keys.Space) && gameOver)
			{
				//lecisz z gra

				foreach (Obstacle obstacle in obstacles)
				{
					obstacle.Position = new Vector2(obstacle.Position.X + 1200, obstacle.Position.Y);
					obstacle.MoveSpeed = 200;

					foreach (RectangleCollider collider in obstacle.Colliders)
						collider.AlreadyPointed = false;
				}

				score = 0;
				bird.SimulatePhysics = true;
				gameOver = false;
			}

			bird.SelfCalculations();
			bird.Update();

			Rectangle birdRectangle = ((RectangleCollider)bird.Colliders[0]).rectangle;
			birdRectangle.X += (int)bird.Position.X;
			birdRectangle.Y += (int)bird.Position.Y;

			foreach (Entity obstacle in obstacles)
			{
				obstacle.SelfCalculations();
				obstacle.Update();

				foreach (RectangleCollider collider in obstacle.Colliders)
				{
					Rectangle rectangle = collider.rectangle;

					rectangle.X += (int)(obstacle.Position.X);
					rectangle.Y += (int)(obstacle.Position.Y);

					if (rectangle.Intersects(birdRectangle))
					{
						if (collider.ShouldPoint &&
							!collider.AlreadyPointed &&
							bird.Position.X > rectangle.Location.X)
						{
							score++;
							collider.AlreadyPointed = true;
						}
						else if (!collider.ShouldPoint)
						{
							gameOver = true;

							foreach (Obstacle obst in obstacles)
								obst.MoveSpeed = 0;
						}
					}
				}
			}

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			spriteBatch.Begin();

			foreach (Entity entity in obstacles)
				spriteBatch.Draw(entity.Texture2D,
					new Rectangle((int)entity.Position.X, (int)entity.Position.Y,
					(int)(entity.Texture2D.Width * entity.Scale.X),
					(int)(entity.Texture2D.Height * entity.Scale.Y)),
					Color.White);

			spriteBatch.Draw(bird.Texture2D,
					new Rectangle((int)bird.Position.X, (int)bird.Position.Y,
					(int)(bird.Texture2D.Width * bird.Scale.X),
					(int)(bird.Texture2D.Height * bird.Scale.Y)),
					Color.White);

			spriteBatch.DrawString(spriteFont, score.ToString(),
				new Vector2(GraphicsDevice.PresentationParameters.Bounds.Width / 2 - (spriteFont.MeasureString(score.ToString()).X / 2), 50),
				Color.White);

			spriteBatch.End();


			base.Draw(gameTime);
		}
	}
}
