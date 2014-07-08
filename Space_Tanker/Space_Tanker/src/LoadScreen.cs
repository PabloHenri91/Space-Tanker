using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace Space_Tanker.src
{
    internal class LoadScreen : State
    {
        internal float percentLoaded;
        private Rectangle destination;
        private Color color;
        private int width, height, y, x;

        internal LoadScreen()
            : base()
        {
            textures2Dlocations.Add("loadBackground");
            x = 25;
            y = 430;
            width = 700;
            height = 25;
            percentLoaded = 0;
        }

        internal bool load(ContentManager contentManager)
        {
            foreach (string reference in textures2Dlocations)
            {
                textures2D.Add(reference, new Sprite(reference, contentManager));
            }

            return true;
        }

        internal void draw()
        {
            color = new Color(2f - 2f * percentLoaded / 100f, 2f * percentLoaded / 100f, 0f);
            destination = new Rectangle(x, y, (int)(width * percentLoaded / 100f), height);

            textures2D["loadBackground"].drawOnScreen();
            Game1.spriteBatch.DrawString(Game1.quartzMS20, (int)percentLoaded + "%", new Vector2(722 + 10, 429), color);
            Game1.spriteBatch.Draw(Game1.voidTexture, destination, color);
            if ((int)percentLoaded == 100)
            {
                percentLoaded = 0;
            }
        }
    }
}
