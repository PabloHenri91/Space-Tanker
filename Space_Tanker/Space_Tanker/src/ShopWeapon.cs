using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Space_Tanker.src
{
    internal class ShopWeapon
    {
        internal string weaponName;
        internal string textWeaponName;
        internal string textDescription;
        internal string textureReference;
        internal Vector2 position;
        internal int inventoryAmount;

        internal int demage;
        internal int weight;
        internal int energy;
        internal int reloadTime;
        internal int price;
        internal int ammoWeight;
        internal int shootsPerRound;
        internal bool isEnergyWeapon;
        internal string ammoKey;


        internal ShopWeapon(string weaponName, string textWeaponName, string textDescription, string textureReference, int demage, int weight, int energy, int reloadTime, int price, int ammoWeight, int shootsPerRound, bool isEnergyWeapon, string ammoKey)
        {
            this.weaponName = weaponName;
            this.textWeaponName = textWeaponName;
            this.textDescription = textDescription;
            this.textureReference = textureReference;

            this.demage = demage;
            this.weight = weight;
            this.energy = energy;
            this.reloadTime = reloadTime;
            this.price = price;
            this.ammoWeight = ammoWeight;
            this.shootsPerRound = shootsPerRound;
            this.isEnergyWeapon = isEnergyWeapon;
            this.ammoKey = ammoKey;
        }

        internal ShopWeapon(ShopWeapon weapon)
        {
            this.textWeaponName = weapon.textWeaponName;
            this.weaponName = weapon.weaponName;
            this.textDescription = weapon.textDescription;
            this.textureReference = weapon.textureReference;
            this.price = weapon.price;
            this.inventoryAmount = weapon.inventoryAmount;
        }

        internal void sell()
        {
            if (inventoryAmount > 0)
            {
                inventoryAmount--;
                Game1.memoryCard.score += price / 2;
            }
        }

        internal void buy()
        {
            if (Game1.memoryCard.score - price >= 0)
            {
                inventoryAmount++;
                Game1.memoryCard.score -= price;
            }
        }
    }
}
