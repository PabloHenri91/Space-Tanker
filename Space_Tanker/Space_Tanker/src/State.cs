using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Space_Tanker.src
{
    internal class State
    {
        internal Dictionary<string, Sprite> textures2D;
        protected List<string> textures2Dlocations;
        protected int textures2DlocationsCount;

        internal State()
        {
            textures2D = new Dictionary<string, Sprite>();
            textures2Dlocations = new List<string>();
        }

        protected void updatePercentLoaded()
        {
            Game1.loadScreen.percentLoaded = 100f / (float)(textures2Dlocations.Count) * (float)(textures2Dlocations.Count - --textures2DlocationsCount);
        }
    }
}
