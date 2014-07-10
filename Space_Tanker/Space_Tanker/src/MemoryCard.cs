using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.IsolatedStorage;
using Microsoft.Xna.Framework;

namespace Space_Tanker.src
{
    internal class MemoryCard
    {

        internal int score;
        internal int level;
        internal int mission;
        internal int missionUnlocked;

        //player spaceship atributes
        internal int speed; // vai de 0.3 até 3
        internal int acceleration; // vai de 0.3 até 3
        internal int agility; // vai de 0.003 até 0.03
        internal int armor;
        internal int shieldPower;
        internal int shieldRecharge;

        //Energy
        internal int energy; // Energy equiped

        //Ammo equiped
        internal Dictionary<string, Ammo> ammo;

        //Ship HardPoints
        internal Dictionary<string, HardPoint> hardPoints;

        //Aux
        internal int sectorIndex;
        public int shipIndex;

        internal MemoryCard()
        {
            hardPoints = new Dictionary<string, HardPoint>();
            hardPoints.Add("l1", new HardPoint("l1", new Vector2(276, 43), "l"));
            hardPoints.Add("c1", new HardPoint("c1", new Vector2(422, 43), "c"));
            hardPoints.Add("r1", new HardPoint("r1", new Vector2(568, 43), "r"));
            hardPoints.Add("l2", new HardPoint("l2", new Vector2(276, 189), "l"));
            hardPoints.Add("c2", new HardPoint("c2", new Vector2(422, 189), "c"));
            hardPoints.Add("r2", new HardPoint("r2", new Vector2(568, 189), "r"));

            ammo = new Dictionary<string, Ammo>();
            ammo.Add("autocannonx1Ammo", new Ammo(0));
            ammo.Add("autocannonx2Ammo", new Ammo(0));
            ammo.Add("autocannonx3Ammo", new Ammo(0));
            ammo.Add("missileAmmo", new Ammo(0));
        }


        internal void newGame(int playerShip)
        {
            shipIndex = playerShip;
            score = 10000;
            level = 1;
            mission = 0;
            missionUnlocked = 1;

            speed = 10 + Game1.players.playerTypes[playerShip].speedBonus;
            agility = 10 + Game1.players.playerTypes[playerShip].agilityBonus;
            acceleration = 10 + Game1.players.playerTypes[playerShip].accelerationBonus;
            armor = 10 + Game1.players.playerTypes[playerShip].armorBonus;
            shieldPower = 10 + Game1.players.playerTypes[playerShip].shieldPowerBonus;
            shieldRecharge = 10 + Game1.players.playerTypes[playerShip].shieldRechargeBonus;

            sectorIndex = 0;

            foreach (KeyValuePair<string, HardPoint> item in hardPoints)
            {
                Game1.config.shopWeapons[Game1.config.shopWeapons.First().Value.weaponName].inventoryAmount++;
                item.Value.addItem(Game1.config.shopWeapons.First().Value);
            }
        }

        internal void saveGame()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("shipIndex=" + shipIndex);
            sb.AppendLine("score=" + score);
            sb.AppendLine("level=" + level);
            sb.AppendLine("mission=" + mission);
            sb.AppendLine("missionUnlocked=" + missionUnlocked);

            sb.AppendLine("speed=" + speed);
            sb.AppendLine("acceleration=" + acceleration);
            sb.AppendLine("agility=" + agility);
            sb.AppendLine("armor=" + armor);
            sb.AppendLine("shieldPower=" + shieldPower);
            sb.AppendLine("shieldRecharge=" + shieldRecharge);

            sb.AppendLine("energy=" + energy);

            foreach (var item in ammo)
            {
                sb.AppendLine(item.Key + "=" + item.Value.count);
            }

            sb.AppendLine("sectorIndex=" + sectorIndex);

            foreach (KeyValuePair<string, HardPoint> slot in hardPoints)
            {
                sb.AppendLine(slot.Key + "=" + slot.Value.weaponName);
                sb.AppendLine("" + slot.Value.amountEquiped);
            }

            foreach (KeyValuePair<string, ShopWeapon> shopWeapon in Game1.config.shopWeapons)
            {
                sb.AppendLine(shopWeapon.Key + "=" + shopWeapon.Value.inventoryAmount);
            }

#if WINDOWS_PHONE
            IsolatedStorageFile isolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication();
            using (StreamWriter sw = new StreamWriter(new IsolatedStorageFileStream("data", FileMode.OpenOrCreate, isolatedStorageFile)))
#endif

#if WINDOWS|| LINUX
            using (StreamWriter sw = new StreamWriter("data"))
#endif

            {
                sw.Write(sb);
            }
        }

        internal bool loadGame()
        {
            try
            {
#if WINDOWS_PHONE
            IsolatedStorageFile isolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication();
            using (StreamReader sr = new StreamReader(new IsolatedStorageFileStream("data", FileMode.Open, isolatedStorageFile)))
#endif

#if WINDOWS || LINUX
                using (StreamReader sr = new StreamReader("data"))
#endif
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();

                        if (line.StartsWith("shipIndex="))
                        {
                            shipIndex = Convert.ToInt32(line.Split('=')[1]);
                        }
                        else if (line.StartsWith("score="))
                        {
                            score = Convert.ToInt32(line.Split('=')[1]);
                        }
                        else if (line.StartsWith("level="))
                        {
                            level = Convert.ToInt32(line.Split('=')[1]);
                        }
                        else if (line.StartsWith("mission="))
                        {
                            mission = Convert.ToInt32(line.Split('=')[1]);
                        }
                        else if (line.StartsWith("missionUnlocked="))
                        {
                            missionUnlocked = Convert.ToInt32(line.Split('=')[1]);
                        }
                        else if (line.StartsWith("speed="))
                        {
                            speed = Convert.ToInt32(line.Split('=')[1]);
                        }
                        else if (line.StartsWith("acceleration="))
                        {
                            acceleration = Convert.ToInt32(line.Split('=')[1]);
                        }
                        else if (line.StartsWith("agility="))
                        {
                            agility = Convert.ToInt32(line.Split('=')[1]);
                        }
                        else if (line.StartsWith("armor="))
                        {
                            armor = Convert.ToInt32(line.Split('=')[1]);
                        }
                        else if (line.StartsWith("shieldPower="))
                        {
                            shieldPower = Convert.ToInt32(line.Split('=')[1]);
                        }
                        else if (line.StartsWith("shieldRecharge="))
                        {
                            shieldRecharge = Convert.ToInt32(line.Split('=')[1]);
                        }
                        else if (line.StartsWith("energy="))
                        {
                            energy = Convert.ToInt32(line.Split('=')[1]);
                        }
                        else if (line.StartsWith("sectorIndex="))
                        {
                            sectorIndex = Convert.ToInt32(line.Split('=')[1]);
                        }
                        else
                        {
                            bool foundString = false;

                            if (!foundString)
                                foreach (string ammoKey in ammo.Keys)
                                {
                                    if (line.StartsWith(ammoKey + "="))
                                    {
                                        foundString = true;
                                        ammo[ammoKey].count = Convert.ToInt32(line.Split('=')[1]);
                                    }
                                }

                            if (!foundString)
                                foreach (string hardPointKey in hardPoints.Keys)
                                {
                                    if (line.StartsWith(hardPointKey + "="))
                                    {
                                        foundString = true;
                                        if (line.Split('=')[1] != "")
                                        {
                                            hardPoints[hardPointKey].weaponName = line.Split('=')[1]; line = sr.ReadLine();
                                            hardPoints[hardPointKey].amountEquiped = Convert.ToInt32(line);
                                            hardPoints[hardPointKey].isEmpty = false;
                                            hardPoints[hardPointKey].textureReference = Game1.config.shopWeapons[hardPoints[hardPointKey].weaponName].textureReference;
                                        }
                                        break;
                                    }
                                }

                            if (!foundString)
                                foreach (KeyValuePair<string, ShopWeapon> shopWeapon in Game1.config.shopWeapons)
                                {
                                    if (line.StartsWith(shopWeapon.Key + "="))
                                    {
                                        foundString = true;
                                        shopWeapon.Value.inventoryAmount = Convert.ToInt32(line.Split('=')[1]);
                                        break;
                                    }
                                }
                        }
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        internal void newGame(short shipIndex, int speed, int acceleration, int agility, int armor, int shieldPower, int shieldRecharge)
        {
            throw new NotImplementedException();
        }
    }
}