using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Space_Tanker.src
{
    class Players
    {
        internal Dictionary<int, PlayerType> playerTypes;
        PlayerShip playerType;

        internal Players()
        {
            playerTypes = new Dictionary<int, PlayerType>();
        }

        internal void loadPlayers()
        {
            throw new NotImplementedException();
        }
    }
}
