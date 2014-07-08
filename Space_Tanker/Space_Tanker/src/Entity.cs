using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Space_Tanker.src
{
    internal class Entity
    {
        internal Vector2 position;
        internal float rotation;
        internal int width;
        internal int height;
        internal int biggerSide;

        internal Entity(int width, int height, int x, int y)
        {
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
            this.position = new Vector2((float)x, (float)y);
            rotation = 0;
            //rectangle = new Rectangle(x - width / 2, y + height / 2, width, height);
        }

        internal void draw(Sprite sprite)
        {
            sprite.draw((int)position.X, (int)position.Y);
        }

        internal void drawTile(Tileset tileset, int texCoordx, int texCoordy)
        {
            tileset.drawTile((int)position.X, (int)position.Y, texCoordx, texCoordy);
        }

        internal void setPosition(float x, float y)
        {
            Game1.needToDraw = true;
            this.position = new Vector2(x, y);
            //setRectanglePosition(x, y);
        }

        internal void setPosition(float x, float y, int rotation)
        {
            Game1.needToDraw = true;
            this.position = new Vector2(x, y);
            this.rotation = rotation;
            //setRectanglePosition(x, y);
        }

        //internal void setRectanglePosition(float x, float y)
        //{
        //    rectangle.X = (int)x - width / 2;
        //    rectangle.Y = (int)y + height / 2;
        //}

        //internal bool collidesWith(Rectangle other)
        //{
        //    return rectangle.Intersects(other);
        //}
    }
}
