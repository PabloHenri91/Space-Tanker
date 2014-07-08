using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Space_Tanker.src
{
    internal class Sprite
    {
        internal int width;
        internal int height;
        internal int biggerSide;

        internal Texture2D texture;
        internal int x = 0;
        internal int y = 0;
        internal float rotation = 0;

        internal Sprite(String reference)
        {
            texture = Game1.myContentManager.Load<Texture2D>(reference);
            width = texture.Width;
            height = texture.Height;
            if (height > width)
            {
                biggerSide = height;
            }
            else
            {
                biggerSide = width;
            }
        }

        internal Sprite(String reference, ContentManager contentManager)
        {
            texture = contentManager.Load<Texture2D>(reference);
            width = texture.Width;
            height = texture.Height;
            if (height > width)
            {
                biggerSide = height;
            }
            else
            {
                biggerSide = width;
            }
        }

        internal Sprite(String reference, int width, int height)
        {
            texture = Game1.myContentManager.Load<Texture2D>(reference);
            this.width = width;
            this.height = height;
            if (height > width)
            {
                biggerSide = height;
            }
            else
            {
                biggerSide = width;
            }
        }

        internal void setAngle(float rotation)
        {
            Game1.needToDraw = true;
            this.rotation = rotation;
        }

        internal void setPosition(int x, int y)
        {
            Game1.needToDraw = true;
            this.x = x;
            this.y = y;
        }

        internal void setPosition(int x, int y, float rotation)
        {
            Game1.needToDraw = true;
            this.x = x;
            this.y = y;
            this.rotation = rotation;
        }

        internal void draw()
        {
            Game1.spriteBatch.Draw(texture, new Vector2(x + Game1.matrix.X - width / 2f, -1f * (y + Game1.matrix.Y + height / 2f)), Color.White);
        }

        internal void drawOnScreen()
        {
            Game1.spriteBatch.Draw(texture, new Vector2(x, y), Color.White);
        }
        internal void drawOnScreen2()
        {
            Game1.spriteBatch.Draw(texture, new Rectangle(x, y, width, height), null, Color.White, rotation, new Vector2(width / 2f, height / 2f), SpriteEffects.None, 0f);
        }

        internal void draw(int x, int y)
        {
            Game1.spriteBatch.Draw(texture, new Vector2(x + Game1.matrix.X - width / 2f, -1f * (y + Game1.matrix.Y + height / 2f)), Color.White);
        }

        internal void draw(Vector2 vector2)
        {
            Game1.spriteBatch.Draw(texture, new Vector2(vector2.X + Game1.matrix.X - width / 2f, -1f * (vector2.Y + Game1.matrix.Y + height / 2f)), Color.White);
        }

        internal void drawOnScreen(Vector2 vector2)
        {
            Game1.spriteBatch.Draw(texture, vector2, Color.White);
        }

        internal void draw(int x, int y, float rotation)
        {
            Game1.spriteBatch.Draw(texture, new Rectangle((int)(x + Game1.matrix.X), (int)(y - Game1.matrix.Y), width, height), null, Color.White, rotation, new Vector2(width / 2f, height / 2f), SpriteEffects.None, 0f);
        }

        internal void drawOnScreen(int x, int y, float rotation)
        {
            Game1.spriteBatch.Draw(texture, new Rectangle(x, y, width, height), null, Color.White, rotation, new Vector2(width / 2f, height / 2f), SpriteEffects.None, 0f);
        }

        internal void drawStars(int sourceX, int sourceY)
        {
            Game1.spriteBatch.Draw(texture, new Rectangle(Game1.display.displayWidthOver2, Game1.display.displayHeightOver2,                Game1.display.displayWidth, Game1.display.displayHeight), new Rectangle(sourceX, sourceY, width, height), Color.White, 0, new Vector2(width / 2f, height / 2f), SpriteEffects.None, 0f);
        }

        internal bool intersectsWithMouseClick()
        {
            Rectangle me = new Rectangle(x, y, width, height);
            Rectangle him = new Rectangle(Game1.input.onScreenMouseX, Game1.input.onScreenMouseY, 1, 1);
            return him.Intersects(me);
        }

        internal bool intersectsWithMouseClick(int x, int y)
        {
            Rectangle me = new Rectangle(x, y, width, height);
            Rectangle him = new Rectangle(Game1.input.onScreenMouseX, Game1.input.onScreenMouseY, 1, 1);
            return him.Intersects(me);
        }

        internal bool intersectsWithMouseClick(Vector2 vector2)
        {
            Rectangle me = new Rectangle((int)vector2.X, (int)vector2.Y, width, height);
            Rectangle him = new Rectangle(Game1.input.onScreenMouseX, Game1.input.onScreenMouseY, 1, 1);
            return him.Intersects(me);
        }
    }
}
