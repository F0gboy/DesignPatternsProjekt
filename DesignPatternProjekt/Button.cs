using DesignPatternProjekt.ComponentPatterns;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternProjekt
{
    internal class Button : UIComponent
    {
        private bool isHovering;
        private MouseState currentMouse;
        private MouseState previousMouse;
        private Texture2D texture;
        private SpriteFont font;

        public Rectangle rect;
        private Point edgeSize;

        public ICommand Command
        {
            private get;
            set;
        }

        public string text;

        public Button()
        {
            this.rect = rect;
            this.texture = GameWorld.Instance.sprites["NineSlice"];
            this.font = GameWorld.font;
            edgeSize = new Point(8, 8);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var color = isHovering ? Color.Gray : Color.White;
            var textColor = Color.Black;
            int layerDepth = 0;

            //Calculate scale
            var scale = new Vector2(
                (float)(rect.Width - 2 * edgeSize.X) / (texture.Width - 2 * edgeSize.X),
                (float)(rect.Height - 2 * edgeSize.Y) / (texture.Height - 2 * edgeSize.Y)
            );

            //Corners
            spriteBatch.Draw(
                texture,
                new Vector2(rect.X, rect.Y),
                new Rectangle(Point.Zero, edgeSize),
                color, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, layerDepth
            );
            spriteBatch.Draw(
                texture,
                new Vector2(rect.X + rect.Width - edgeSize.X, rect.Y),
                new Rectangle(new Point(texture.Width - edgeSize.X, 0), edgeSize),
                color, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, layerDepth
            );
            spriteBatch.Draw(
                texture,
                new Vector2(rect.X, rect.Y + rect.Height - edgeSize.Y),
                new Rectangle(new Point(0, texture.Height - edgeSize.Y), edgeSize),
                color, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, layerDepth
            );
            spriteBatch.Draw(
                texture,
                new Vector2(rect.X + rect.Width - edgeSize.X, rect.Y + rect.Height - edgeSize.Y),
                new Rectangle(new Point(texture.Width - edgeSize.X, texture.Height - edgeSize.Y), edgeSize),
                color, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, layerDepth
            );
    
            //Edges
            spriteBatch.Draw(
                texture,
                new Vector2(rect.X + edgeSize.X, rect.Y),
                new Rectangle(edgeSize.X, 0, texture.Width - 2 * edgeSize.X, edgeSize.Y),
                color, 0, Vector2.Zero, new Vector2(scale.X, 1), SpriteEffects.None, layerDepth
            );
            spriteBatch.Draw(
                texture,
                new Vector2(rect.X + edgeSize.X, rect.Y + rect.Height - edgeSize.Y),
                new Rectangle(edgeSize.X, texture.Height - edgeSize.Y, texture.Width - 2 * edgeSize.X, edgeSize.Y),
                color, 0, Vector2.Zero, new Vector2(scale.X, 1), SpriteEffects.None, layerDepth
            );
            spriteBatch.Draw(
                texture,
                new Vector2(rect.X, rect.Y + edgeSize.Y),
                new Rectangle(0, edgeSize.Y, edgeSize.X, texture.Height - 2 * edgeSize.Y),
                color, 0, Vector2.Zero, new Vector2(1, scale.Y), SpriteEffects.None, layerDepth
            );
            spriteBatch.Draw(
                texture,
                new Vector2(rect.X + rect.Width - edgeSize.Y, rect.Y + edgeSize.Y),
                new Rectangle(texture.Width - edgeSize.X, edgeSize.Y, edgeSize.X, texture.Height - 2 * edgeSize.Y),
                color, 0, Vector2.Zero, new Vector2(1, scale.Y), SpriteEffects.None, layerDepth
            );

            //Fill/Center
            spriteBatch.Draw(
                texture,
                new Vector2(rect.X + edgeSize.X, rect.Y + edgeSize.Y),
                new Rectangle(edgeSize.X, edgeSize.Y, texture.Width - 2 * edgeSize.X, texture.Height - 2 * edgeSize.Y),
                color, 0, Vector2.Zero, scale, SpriteEffects.None, layerDepth
            );

            spriteBatch.DrawString(font, text, new Vector2(rect.X, rect.Y), textColor);
        }
        public void Update(GameTime gameTime)
        {
            previousMouse = currentMouse;
            currentMouse = Mouse.GetState();

            isHovering = rect.Contains(currentMouse.X, currentMouse.Y);
            if ( isHovering && currentMouse.LeftButton == ButtonState.Pressed && previousMouse.LeftButton == ButtonState.Released)
            {
                InputHandler.Instance.ExecuteCommand(Command);
            }
        } 
    }
}
