using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Space_Tanker.src
{
    internal class EnemyType
    {
        internal int type;
        internal int level;

        //Vida e Escudo de Energia
        internal int health;
        internal int energyShield;
        internal int shieldRechargeInterval;

        //Física
        internal float angularImpulse;
        internal float maxAngularVelocity;
        internal float maxLinearVelocity;
        internal float force;

        //Weapons
        internal Dictionary<string, EnemyHardPoint> hardPoints;

        internal EnemyType(int type, int speed, int acceleration, int agility, int armor, int shieldPower, int shieldRecharge)
        {
            hardPoints = new Dictionary<string, EnemyHardPoint>();
            hardPoints.Add("l", new EnemyHardPoint("l"));
            hardPoints.Add("c", new EnemyHardPoint("c"));
            hardPoints.Add("r", new EnemyHardPoint("r"));

            this.type = type;
            level = speed + acceleration + agility + armor + shieldPower + shieldRecharge - 59;

            health = (level * Game1.config.hpPerLevel) + ((armor - 10) * Game1.config.hpPerArmor);
            energyShield = (level * Game1.config.spPerLevel) + ((shieldPower - 9) * Game1.config.spPerPower);
            shieldRechargeInterval = 100 - shieldRecharge;

            angularImpulse = (agility / 100f) * 0.03f;
            maxAngularVelocity = agility / 10f;
            maxLinearVelocity = (speed / 100f) * 3f;
            force = (acceleration / 100f) * 3f;
        }
    }
}
