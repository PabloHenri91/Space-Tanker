using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Space_Tanker.src
{
    internal class Tileset : Sprite
    {
        internal int tileWidth, tileHeight;
        internal int tilesPerRow;
        internal int tilesPerColumn;
        private Rectangle source;
        internal int tilesCount;

        internal Tileset(String reference, int tileSize)
            : base(reference)
        {
            this.tileWidth = tileSize;
            this.tileHeight = tileSize;
            this.tilesPerRow = texture.Width / tileSize;
            this.tilesPerColumn = texture.Height / tileSize;
            this.tilesCount = tilesPerRow * tilesPerColumn;
            this.source = new Rectangle(0, 0, tileSize, tileSize);
        }

        internal Tileset(String reference, int tileWidth, int tileHeight)
            : base(reference, tileWidth, tileHeight)
        {
            this.tileWidth = tileWidth;
            this.tileHeight = tileHeight;
            this.tilesPerRow = texture.Width / tileWidth;
            this.tilesPerColumn = texture.Height / tileHeight;
            this.tilesCount = tilesPerRow * tilesPerColumn;
            this.source = new Rectangle(0, 0, tileWidth, tileHeight);
        }

        internal void drawTile(int x, int y, int texCoordx, int texCoordy)
        {
            source.X = texCoordx * tileWidth;
            source.Y = texCoordy * tileHeight;
            Game1.spriteBatch.Draw(texture, new Vector2(x + Game1.matrix.X - tileWidth / 2f, -1f * (y + Game1.matrix.Y + tileHeight / 2f)), source, Color.White);
        }
    }
}
