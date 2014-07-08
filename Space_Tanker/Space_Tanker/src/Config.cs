using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Space_Tanker.src
{
    internal class Config
    {
        //Spaceships
        internal int baseWeitght = ((64 * 64) / 10) + 100;
        internal int hpPerArmor = 5;
        internal int hpPerLevel = 2;

        internal int spPerPower = 5;
        internal int spPerLevel = 1;

        internal int shootingInterval = 6;
        internal float shotsMaxSpeed = 6f;

        //Iuput
        internal short touchInterval = 10;

        //Mission
        internal int spawningZone = 2;
        internal short spawningInterval = 30 * 2;

        //shopWeapons
        #region
        internal int maxDemage = 0;
        internal int maxWeight = 0;
        internal int maxEnergy = 0;
        internal int maxReloadTime = 0;
        internal int maxAmmoWeight = 0;
        internal int maxShootsPerRound = 0;

        internal short smallLaserDemage = 1;
        internal short smallLaserWeight = 100;
        internal short smallLaserEnergy = 1;
        internal short smallLaserReloadTime = 30;
        internal short smallLaserPrice = 1000;

        internal short mediumLaserDemage = 5;
        internal short mediumLaserWeight = 200;
        internal short mediumLaserEnergy = 6;
        internal short mediumLaserReloadTime = 90;
        internal short mediumLaserPrice = 5000;

        internal short largeLaserDemage = 25;
        internal short largeLaserWeight = 1000;
        internal short largeLaserEnergy = 12;
        internal short largeLaserReloadTime = 150;
        internal short largeLaserPrice = 25000;

        internal short autocannonx1Demage = 5;
        internal short autocannonx1Weight = 1600;
        internal short autocannonx1Energy = 4;
        internal short autocannonx1ReloadTime = 60;
        internal short autocannonx1Price = 5000;
        internal short autocannonx1AmmoWeight = 2;

        internal short autocannonx2Demage = 15;
        internal short autocannonx2Weight = 2600;
        internal short autocannonx2Energy = 6;
        internal short autocannonx2ReloadTime = 120;
        internal short autocannonx2Price = 30000;
        internal short autocannonx2AmmoWeight = 5;

        internal short autocannonx3Demage = 20;
        internal short autocannonx3Weight = 3200;
        internal short autocannonx3Energy = 12;
        internal short autocannonx3ReloadTime = 180;
        internal ushort autocannonx3Price = 60000;
        internal short autocannonx3AmmoWeight = 10;

        internal short missileReloadTime = 180;
        internal short missileDemage = 15;
        internal short missileAmmoWeight = 20;

        internal short missilex1Weight = 600;
        internal short missilex1Energy = 24;
        internal short missilex1Price = 15000;

        internal short missilex2Weight = 1200;
        internal short missilex2Energy = 24;
        internal short missilex2Price = 30000;

        internal short missilex3Weight = 1600;
        internal short missilex3Energy = 20;
        internal ushort missilex3Price = 45000;

        internal short missilex4Weight = 2200;
        internal short missilex4Energy = 18;
        internal ushort missilex4Price = 60000;

        #endregion

        //ReqXP vectors2 Dictionary
        internal Dictionary<int, int> level;

        //Weapons Dictionary
        internal Dictionary<string, ShopWeapon> shopWeapons;

        //HardPoint Dictionary
        //internal Dictionary<int, HardPoint> hardPoints;

        //Vectors2
        internal Dictionary<string, Vector2> vectors2;

        //Sectors
        internal Dictionary<int, string> sectors;

        internal Config()
        {
            //ReqXP vectors2
            level = new Dictionary<int, int>();
            for (int i = 1; i < 600; i++)
            {
                level.Add(i, i * 100);
            }

            //Shop Weapons
            shopWeapons = new Dictionary<string, ShopWeapon>();
            shopWeapons.Add("smallLasers", new ShopWeapon("smallLasers", "Small Laser", "", "smallLaserBig", smallLaserDemage, smallLaserWeight ,smallLaserEnergy, smallLaserReloadTime, smallLaserPrice, 0, 1, true, ""));
            shopWeapons.Add("mediumLasers", new ShopWeapon("mediumLasers", "Medium Laser", "", "mediumLaserBig", mediumLaserDemage, mediumLaserWeight, mediumLaserEnergy, mediumLaserReloadTime, mediumLaserPrice, 0, 1, true, ""));
            shopWeapons.Add("largeLasers", new ShopWeapon("largeLasers", "Large Laser", "", "largeLaserBig", largeLaserDemage, largeLaserWeight, largeLaserEnergy, largeLaserReloadTime, largeLaserPrice, 0, 1, true, ""));
            shopWeapons.Add("autocannonsx1", new ShopWeapon("autocannonsx1", "Autocannon", "", "autocannonBig", autocannonx1Demage, autocannonx1Weight,autocannonx1Energy, autocannonx1ReloadTime, autocannonx1Price, autocannonx1AmmoWeight, 1, false, "autocannonx1Ammo"));
            shopWeapons.Add("autocannonsx2", new ShopWeapon("autocannonsx2", "Autocannon x2", "", "autocannonx2Big", autocannonx2Demage, autocannonx2Weight, autocannonx2Energy, autocannonx2ReloadTime, autocannonx2Price, autocannonx2AmmoWeight, 2, false, "autocannonx2Ammo"));
            shopWeapons.Add("autocannonsx3", new ShopWeapon("autocannonsx3", "Autocannon x3", "", "autocannonx3Big", autocannonx3Demage, autocannonx3Weight, autocannonx3Energy, autocannonx3ReloadTime, autocannonx3Price, autocannonx3AmmoWeight, 3, false, "autocannonx3Ammo"));
            shopWeapons.Add("missilesx1", new ShopWeapon("missilesx1", "Missile", "", "missileBig", missileDemage, missilex1Weight, missilex1Energy, missileReloadTime, missilex1Price, missileAmmoWeight, 1, false, "missileAmmo"));
            shopWeapons.Add("missilesx2", new ShopWeapon("missilesx2", "Missile x2", "", "missilex2Big", missileDemage, missilex2Weight, missilex2Energy, missileReloadTime, missilex2Price, missileAmmoWeight, 2, false, "missileAmmo"));
            shopWeapons.Add("missilesx3", new ShopWeapon("missilesx3", "Missile x3", "", "missilex3Big", missileDemage, missilex3Weight, missilex3Energy, missileReloadTime, missilex3Price, missileAmmoWeight, 3, false, "missileAmmo"));
            shopWeapons.Add("missilesx4", new ShopWeapon("missilesx4", "Missile x4", "", "missilex4Big", missileDemage, missilex4Weight, missilex4Energy, missileReloadTime, missilex4Price, missileAmmoWeight, 4, false, "missileAmmo"));

            foreach (KeyValuePair<string, ShopWeapon> shopWeapon in shopWeapons)
            {
                if (maxDemage < shopWeapon.Value.demage) maxDemage = shopWeapon.Value.demage;
                if (maxWeight < shopWeapon.Value.weight) maxWeight = shopWeapon.Value.weight;
                if (maxEnergy < shopWeapon.Value.energy) maxEnergy = shopWeapon.Value.energy;
                if (maxReloadTime < shopWeapon.Value.reloadTime) maxReloadTime = shopWeapon.Value.reloadTime;
                if (maxAmmoWeight < shopWeapon.Value.ammoWeight) maxAmmoWeight = shopWeapon.Value.ammoWeight;
                if (maxShootsPerRound < shopWeapon.Value.shootsPerRound) maxShootsPerRound = shopWeapon.Value.shootsPerRound;
            }

            //Points
            vectors2 = new Dictionary<string, Vector2>();

            //Sectors
            sectors = new Dictionary<int, string>();
            sectors.Add(1, "1. Sector A");
            sectors.Add(2, "2. Sector B");
            sectors.Add(3, "3. Sector C");
            sectors.Add(4, "4. Sector D");
            sectors.Add(5, "5. Sector E");
            sectors.Add(6, "6. Sector F");
            sectors.Add(7, "7. Sector G");
            sectors.Add(8, "8. Sector H");
            sectors.Add(9, "9. Sector I");
            sectors.Add(10, "10. Sector J");
        }
    }
}

