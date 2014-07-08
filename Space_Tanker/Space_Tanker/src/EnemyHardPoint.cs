using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Space_Tanker.src
{
    internal class EnemyHardPoint
    {
        internal bool isEmpty;
        internal string slotName;
        internal string weaponName;
        internal int amountEquiped;

        //Equiped Weapon
        internal int lastFire;
        internal int ammoLoaded;
        internal int maxAmmoLoaded;
        internal bool needToLoad;
        internal int lastReloaded;
        internal int shootsPerRound;

        internal EnemyHardPoint(string slotName)
        {
            this.slotName = slotName;

            isEmpty = true;

            ammoLoaded = 0;
            maxAmmoLoaded = 0;
            needToLoad = false;
        }

        internal void removeItem()
        {
            amountEquiped--;
            maxAmmoLoaded -= shootsPerRound;
            if (amountEquiped == 0)
            {
                isEmpty = true;
                weaponName = "";
                shootsPerRound = 0;
            }
        }

        internal bool addItem(ShopWeapon shopWeapon)
        {
            if (isEmpty)
            {
                isEmpty = false;
                weaponName = shopWeapon.weaponName;
                shootsPerRound = shopWeapon.shootsPerRound;
                amountEquiped = 1;
                maxAmmoLoaded = shootsPerRound;
            }
            else
            {
                if (weaponName == shopWeapon.weaponName)
                {
                    amountEquiped++;
                    maxAmmoLoaded += shopWeapon.shootsPerRound;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
    }
}
