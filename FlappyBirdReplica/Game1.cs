using FlappyBirdReplica.MyClasses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace FlappyBirdReplica
{

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

		Entity bird;
		List<Obstacle> obstacles;

		public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
			// TODO: Add your initialization logic here
			Physics.Gravity = 9.81f;
			Physics.Friction = 0f;

			obstacles = new List<Obstacle>();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

			Texture2D birdTexture = Content.Load<Texture2D>("bird");
			Texture2D obstacleTexture = Content.Load<Texture2D>("obstacle");

			bird = new Bird
			{
				Position = new Vector2(30, 100),
				Scale = new Vector2(0.1f, 0.1f),
				Texture2D = birdTexture,
				SimulatePhysics = true,
				JumpForce = 3f
			};

			Random random = new Random();

			for (int i = 0; i < 4; i++)
				obstacles.Add(new Obstacle
				{
					Position = new Vector2(GraphicsDevice.PresentationParameters.Bounds.Width / 2 * i + GraphicsDevice.PresentationParameters.Bounds.Width, random.Next(-250, -50)),
					Scale = Vector2.One * 3f,
					Texture2D = obstacleTexture,
					SimulatePhysics = false,
					MoveSpeed = 200f
				});

			obstacles[0].OnScreenLeft += () => obstacles[0].Position = new Vector2(obstacles[3].Position.X + GraphicsDevice.PresentationParameters.Bounds.Width / 2, random.Next(-250, -50));
			obstacles[1].OnScreenLeft += () => obstacles[1].Position = new Vector2(obstacles[0].Position.X + GraphicsDevice.PresentationParameters.Bounds.Width / 2, random.Next(-250, -50));
			obstacles[2].OnScreenLeft += () => obstacles[2].Position = new Vector2(obstacles[1].Position.X + GraphicsDevice.PresentationParameters.Bounds.Width / 2, random.Next(-250, -50));
			obstacles[3].OnScreenLeft += () => obstacles[3].Position = new Vector2(obstacles[2].Position.X + GraphicsDevice.PresentationParameters.Bounds.Width / 2, random.Next(-250, -50));
			// TODO: use this.Content to load your game content here
		}

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
			Time.UpdateTime(gameTime);

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

			// TODO: Add your update logic here

			bird.SelfCalculations();
			bird.Update();

			foreach(Entity obstacle in obstacles)
			{
				obstacle.SelfCalculations();
				obstacle.Update();
			}

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

			spriteBatch.Begin();

			foreach (Entity entity in obstacles)
				spriteBatch.Draw(entity.Texture2D,
					new Rectangle((int) entity.Position.X, (int) entity.Position.Y,
					(int) (entity.Texture2D.Width * entity.Scale.X),
					(int) (entity.Texture2D.Height * entity.Scale.Y)), 
					Color.White);

			spriteBatch.Draw(bird.Texture2D,
					new Rectangle((int)bird.Position.X, (int)bird.Position.Y,
					(int)(bird.Texture2D.Width * bird.Scale.X),
					(int)(bird.Texture2D.Height * bird.Scale.Y)),
					Color.White);

			spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
