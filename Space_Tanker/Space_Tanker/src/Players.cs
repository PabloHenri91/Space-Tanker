using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Space_Tanker.src
{
    class Players
    {
        internal Dictionary<int, PlayerType> playerTypes;
        PlayerType playerType;

        internal Players()
        {
            playerTypes = new Dictionary<int, PlayerType>();
        }

        internal void loadPlayers()
        {
            playerTypes.Clear();
            int i = 1;

            playerType = new PlayerType(i++, 10, 10, 0, 0, 0, 0);
            playerTypes.Add(playerType.type, playerType);

            playerType = new PlayerType(i++, 0, 0, 0, 0, 10, 10);
            playerTypes.Add(playerType.type, playerType);

            playerType = new PlayerType(i++, 0, 0, 0, 10, 10, 0);
            playerTypes.Add(playerType.type, playerType);

            playerType = new PlayerType(i++, 0, 0, 0, 20, 0, 0);
            playerTypes.Add(playerType.type, playerType);

            playerType = new PlayerType(i++, 0, 0, 5, 5, 5, 5);
            playerTypes.Add(playerType.type, playerType);

            playerType = new PlayerType(i++, 5, 5, 5, 5, 0, 0);
            playerTypes.Add(playerType.type, playerType);
        }
    }
}
