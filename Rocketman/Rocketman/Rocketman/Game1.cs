using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Rocketman
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D rocket;
        Texture2D barrier1;
        Texture2D barrier2;
        Boolean movbar1, movbar2;
        Rectangle rocketrec = Rectangle.Empty;
        Rectangle bar1rec = Rectangle.Empty;
        Rectangle bar2rec = Rectangle.Empty;
        Vector2 winrec = Vector2.Zero;
        Rectangle win2rec = Rectangle.Empty;
    
        //Vector2 rocpos = Vector2.Zero;
        //Vector2 bar1pos = Vector2.Zero;
        //Vector2 bar2pos = Vector2.Zero;
        SpriteFont spriteFont;


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
            rocket = Content.Load<Texture2D>("Rocket");
            barrier1 = Content.Load<Texture2D>("Barrier1");
            barrier2 = Content.Load<Texture2D>("Barrier2");
            movbar1 = true;
            movbar2 = true;
            rocketrec = new Rectangle(0, 0, 100, 50);
            bar1rec = new Rectangle((graphics.GraphicsDevice.Viewport.Width / 4), 0, 192, 64);
            bar2rec = new Rectangle((graphics.GraphicsDevice.Viewport.Width * 3 / 4) - (barrier1.Width / 2), graphics.GraphicsDevice.Viewport.Height - 64, 192, 64);
            winrec = new Vector2((graphics.GraphicsDevice.Viewport.Width-100),graphics.GraphicsDevice.Viewport.Height-100);
            win2rec = new Rectangle((graphics.GraphicsDevice.Viewport.Width - 100), graphics.GraphicsDevice.Viewport.Height - 100, 100, 100);


            //bar1pos = new Vector2((graphics.GraphicsDevice.Viewport.Width/ 4) - (barrier1.Width / 2), 0);
            //bar2pos = new Vector2((graphics.GraphicsDevice.Viewport.Width*3/4) - (barrier1.Width / 2), graphics.GraphicsDevice.Viewport.Height - 64);

            spriteFont = Content.Load<SpriteFont>("SpriteFont1");
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();


            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Left) && rocketrec.X >=0)
            {
                rocketrec.X -= 3;
            }
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Right) && rocketrec.X <=(graphics.GraphicsDevice.Viewport.Width - 100))
            {
                rocketrec.X += 3;
            }
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Up) && rocketrec.Y >=0)
            {
                rocketrec.Y -= 3;
            }
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Down) && rocketrec.Y <=(graphics.GraphicsDevice.Viewport.Height - 50))
            {
                rocketrec.Y += 3;
            }

            if (movbar1)
            {
                bar1rec.Y += 3;
            }
            else
            {
                bar1rec.Y -= 3;
            }

            if (movbar2)
            {
                bar2rec.Y -= 3;
            }
            else
            {
                bar2rec.Y += 3;
            }

            if (bar1rec.Y > (graphics.GraphicsDevice.Viewport.Height -64) || bar1rec.Y < 0)
            {
                movbar1 = !movbar1;
            }

            if (bar2rec.Y > (graphics.GraphicsDevice.Viewport.Height - 64) || bar2rec.Y < 0)
            {
                movbar2 = !movbar2;
            }
            
           
            //if ((bar2rec.X <= rocketrec.X && (bar2rec.X + 192) >= rocketrec.X) && (bar2rec.Y <= rocketrec.Y && (bar2rec.Y+ 64) >= rocketrec.Y))
            if (rocketrec.Intersects(bar2rec))
            {
                rocketrec.X = 0;
                rocketrec.Y = 0;
            }

            //if ((bar1rec.X <= rocketrec.X && (bar1rec.X + 192) >= rocketrec.X) && (bar1rec.Y <= rocketrec.Y && (bar1rec.Y + 64) >= rocketrec.Y))
            if(rocketrec.Intersects(bar1rec))
            {
                rocketrec.X = 0;
                rocketrec.Y = 0;
            }
            if(rocketrec.Intersects(win2rec))
            {
                
                
                //spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                
               // spriteBatch.End();
                this.Exit();

            }
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            spriteBatch.Draw(rocket, rocketrec, Color.White);
           spriteBatch.Draw(barrier1, bar1rec, Color.White);
           spriteBatch.Draw(barrier2, bar2rec, Color.White);
           spriteBatch.DrawString(spriteFont, "End Game", winrec, Color.White);
            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
