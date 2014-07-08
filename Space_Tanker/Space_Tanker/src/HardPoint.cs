using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Space_Tanker.src
{
    internal class HardPoint
    {
        internal bool isEmpty;
        internal string slotName;
        internal string weaponName;
        internal Vector2 position;
        internal int amountEquiped;
        internal string textureReference;
        internal string shipPosition;

        //Equiped Weapon
        internal int lastFire;
        internal int ammoLoaded;
        internal int maxAmmoLoaded;
        internal bool needToLoad;
        internal int lastReloaded;

        internal HardPoint(string slotName, Vector2 position, string shipPosition)
        {
            this.slotName = slotName;
            this.position = position;
            this.shipPosition = shipPosition;

            isEmpty = true;
            ammoLoaded = 0;
            maxAmmoLoaded = 0;
            needToLoad = false;
        }

        internal void removeItem()
        {
            amountEquiped--;
            Game1.config.shopWeapons[weaponName].inventoryAmount++;

            if (amountEquiped == 0)
            {
                isEmpty = true;
                weaponName = "";
            }
        }

        internal bool addItem(ShopWeapon shopWeapon)
        {
            if (isEmpty)
            {
                isEmpty = false;
                weaponName = shopWeapon.weaponName;
                amountEquiped = 1;
                Game1.config.shopWeapons[weaponName].inventoryAmount--;
                textureReference = shopWeapon.textureReference;
            }
            else
            {
                if (weaponName == shopWeapon.weaponName)
                {
                    amountEquiped++;
                    Game1.config.shopWeapons[weaponName].inventoryAmount--;
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
