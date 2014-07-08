using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Space_Tanker.src
{
    class PlayerType
    {
        internal int type;
        internal int speedBonus;
        internal int accelerationBonus;
        internal int agilityBonus;
        internal int armorBonus;
        internal int shieldPowerBonus;
        internal int shieldRechargeBonus;

        internal PlayerType(int type, int speed, int acceleration, int agility, int armor, int shieldPower, int shieldRecharge)
        {
            this.type = type;
            this.speedBonus = speed;
            this.accelerationBonus = acceleration;
            this.agilityBonus = agility;
            this.armorBonus = armor;
            this.shieldPowerBonus = shieldPower;
            this.shieldRechargeBonus = shieldRecharge;
        }
    }
}
