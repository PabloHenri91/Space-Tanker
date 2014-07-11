using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Space_Tanker.src
{
    internal class Enemies
    {
        internal Dictionary<int, EnemyType> enemyTypes;
        EnemyType enemyType;

        internal Enemies()
        {
            enemyTypes = new Dictionary<int, EnemyType>();
        }

        internal void loadMission()
        {
            enemyTypes.Clear();
            for (int i = 1; i <= Game1.memoryCard.mission + 1; i++)
            {

                switch (i)
                {   
                    case 1:
                        {
                            enemyType = new EnemyType(i, 10, 10, 10, 10, 10, 10);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(0).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 2:
                        {
                            enemyType = new EnemyType(i, 15, 10, 10, 10, 10, 10);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(0).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(0).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 3:
                        {
                            enemyType = new EnemyType(i, 15, 15, 10, 10, 10, 10);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(0).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(0).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(0).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 4:
                        {
                            enemyType = new EnemyType(i, 15, 15, 15, 10, 10, 10);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(0).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(1).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(0).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 5:
                        {
                            enemyType = new EnemyType(i, 15, 15, 15, 15, 10, 10);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(1).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(1).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(0).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 6:
                        {
                            enemyType = new EnemyType(i, 15, 15, 15, 15, 15, 10);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(1).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(1).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(1).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 7:
                        {
                            enemyType = new EnemyType(i, 15, 15, 15, 15, 15, 15);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(1).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(2).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(1).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 8:
                        {
                            enemyType = new EnemyType(i, 20, 15, 15, 15, 15, 15);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(2).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(2).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(1).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 9:
                        {
                            enemyType = new EnemyType(i, 20, 20, 15, 15, 15, 15);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(2).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(2).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(2).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 10:
                        {
                            enemyType = new EnemyType(i, 20, 20, 20, 15, 15, 15);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(2).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(3).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(2).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 11:
                        {
                            enemyType = new EnemyType(i, 20, 20, 20, 20, 15, 15);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(3).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(3).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(2).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 12:
                        {
                            enemyType = new EnemyType(i, 20, 20, 20, 20, 20, 15);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(3).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(3).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(3).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 13:
                        {
                            enemyType = new EnemyType(i, 20, 20, 20, 20, 20, 20);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(3).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(4).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(3).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 14:
                        {
                            enemyType = new EnemyType(i, 25, 20, 20, 20, 20, 20);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(4).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(4).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(3).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 15:
                        {
                            enemyType = new EnemyType(i, 25, 25, 20, 20, 20, 20);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(4).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(4).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(4).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 16:
                        {
                            enemyType = new EnemyType(i, 25, 25, 25, 20, 20, 20);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(4).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(5).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(4).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 17:
                        {
                            enemyType = new EnemyType(i, 25, 25, 25, 25, 20, 20);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(5).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(5).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(4).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 18:
                        {
                            enemyType = new EnemyType(i, 25, 25, 25, 25, 25, 20);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(5).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(5).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(5).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 19:
                        {
                            enemyType = new EnemyType(i, 25, 25, 25, 25, 25, 25);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(5).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(6).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(5).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 20:
                        {
                            enemyType = new EnemyType(i, 30, 25, 25, 25, 25, 25);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(6).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(6).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(5).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 21:
                        {
                            enemyType = new EnemyType(i, 30, 30, 25, 25, 25, 25);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(6).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(6).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(6).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 22:
                        {
                            enemyType = new EnemyType(i, 30, 30, 30, 25, 25, 25);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(6).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(7).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(6).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 23:
                        {
                            enemyType = new EnemyType(i, 30, 30, 30, 30, 25, 25);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(7).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(7).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(6).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 24:
                        {
                            enemyType = new EnemyType(i, 30, 30, 30, 30, 30, 25);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(7).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(7).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(7).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 25:
                        {
                            enemyType = new EnemyType(i, 30, 30, 30, 30, 30, 30);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(7).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(8).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(7).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 26:
                        {
                            enemyType = new EnemyType(i, 35, 30, 30, 30, 30, 30);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(8).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(8).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(7).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 27:
                        {
                            enemyType = new EnemyType(i, 35, 35, 30, 30, 30, 30);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(8).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(8).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(8).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 28:
                        {
                            enemyType = new EnemyType(i, 35, 35, 35, 30, 30, 30);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(8).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(9).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(8).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 29:
                        {
                            enemyType = new EnemyType(i, 35, 35, 35, 35, 30, 30);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(9).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(9).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(8).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 30:
                        {
                            enemyType = new EnemyType(i, 35, 35, 35, 35, 35, 30);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(9).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(9).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(9).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    /*
                    case 31:
                        {
                            enemyType = new EnemyType(i, 35, 35, 35, 35, 35, 35);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(9).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(10).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(9).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 32:
                        {
                            enemyType = new EnemyType(i, 15, 15, 15, 15, 15, 15);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(10).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(10).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(9).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 33:
                        {
                            enemyType = new EnemyType(i, 20, 15, 15, 15, 15, 15);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(10).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(10).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(10).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 34:
                        {
                            enemyType = new EnemyType(i, 20, 20, 15, 15, 15, 15);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(10).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(11).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(10).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 35:
                        {
                            enemyType = new EnemyType(i, 20, 20, 20, 15, 15, 15);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(11).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(11).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(10).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 36:
                        {
                            enemyType = new EnemyType(i, 20, 20, 20, 20, 15, 15);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(11).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(11).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(11).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 37:
                        {
                            enemyType = new EnemyType(i, 20, 20, 20, 20, 20, 15);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(11).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(12).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(11).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 38:
                        {
                            enemyType = new EnemyType(i, 20, 20, 20, 20, 20, 20);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(12).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(12).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(11).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 39:
                        {
                            enemyType = new EnemyType(i, 25, 20, 20, 20, 20, 20);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(12).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(12).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(12).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 40:
                        {
                            enemyType = new EnemyType(i, 25, 25, 20, 20, 20, 20);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(12).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(13).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(12).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 41:
                        {
                            enemyType = new EnemyType(i, 25, 25, 25, 20, 20, 20);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(13).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(13).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(12).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 42:
                        {
                            enemyType = new EnemyType(i, 25, 25, 25, 25, 20, 20);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(13).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(13).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(13).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 43:
                        {
                            enemyType = new EnemyType(i, 25, 25, 25, 25, 25, 20);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(13).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(14).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(13).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 44:
                        {
                            enemyType = new EnemyType(i, 25, 25, 25, 25, 25, 25);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(14).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(14).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(13).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 45:
                        {
                            enemyType = new EnemyType(i, 30, 25, 25, 25, 25, 25);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(14).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(14).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(14).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 46:
                        {
                            enemyType = new EnemyType(i, 30, 30, 25, 25, 25, 25);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(14).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(15).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(14).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 47:
                        {
                            enemyType = new EnemyType(i, 30, 30, 30, 25, 25, 25);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(15).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(15).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(14).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 48:
                        {
                            enemyType = new EnemyType(i, 30, 30, 30, 30, 25, 25);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(15).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(15).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(15).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 49:
                        {
                            enemyType = new EnemyType(i, 30, 30, 30, 30, 25, 25);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(15).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(16).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(15).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 50:
                        {
                            enemyType = new EnemyType(i, 30, 30, 30, 30, 30, 25);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(16).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(16).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(15).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 51:
                        {
                            enemyType = new EnemyType(i, 10, 10, 10, 10, 10, 10);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(16).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(16).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(16).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 52:
                        {
                            enemyType = new EnemyType(i, 15, 10, 10, 10, 10, 10);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(16).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(17).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(16).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 53:
                        {
                            enemyType = new EnemyType(i, 15, 15, 10, 10, 10, 10);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(17).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(17).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(16).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 54:
                        {
                            enemyType = new EnemyType(i, 15, 15, 15, 10, 10, 10);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(17).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(17).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(17).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 55:
                        {
                            enemyType = new EnemyType(i, 15, 15, 15, 15, 10, 10);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(17).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(18).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(17).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 56:
                        {
                            enemyType = new EnemyType(i, 15, 15, 15, 15, 15, 10);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(18).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(18).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(17).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 57:
                        {
                            enemyType = new EnemyType(i, 15, 15, 15, 15, 15, 15);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(18).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(18).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(18).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 58:
                        {
                            enemyType = new EnemyType(i, 20, 15, 15, 15, 15, 15);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(18).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(19).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(18).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 59:
                        {
                            enemyType = new EnemyType(i, 20, 20, 15, 15, 15, 15);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(19).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(19).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(18).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 60:
                        {
                            enemyType = new EnemyType(i, 20, 20, 20, 15, 15, 15);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(19).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(19).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(19).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 61:
                        {
                            enemyType = new EnemyType(i, 20, 20, 20, 20, 15, 15);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(3).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(3).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(2).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 62:
                        {
                            enemyType = new EnemyType(i, 20, 20, 20, 20, 20, 15);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(3).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(3).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(3).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 63:
                        {
                            enemyType = new EnemyType(i, 20, 20, 20, 20, 20, 20);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(3).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(4).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(3).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 64:
                        {
                            enemyType = new EnemyType(i, 25, 20, 20, 20, 20, 20);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(4).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(4).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(3).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 65:
                        {
                            enemyType = new EnemyType(i, 25, 25, 20, 20, 20, 20);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(4).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(4).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(4).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 66:
                        {
                            enemyType = new EnemyType(i, 25, 25, 25, 20, 20, 20);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(4).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(5).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(4).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 67:
                        {
                            enemyType = new EnemyType(i, 25, 25, 25, 25, 20, 20);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(5).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(5).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(4).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 68:
                        {
                            enemyType = new EnemyType(i, 25, 25, 25, 25, 25, 20);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(5).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(5).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(5).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 69:
                        {
                            enemyType = new EnemyType(i, 25, 25, 25, 25, 25, 25);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(5).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(6).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(5).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 70:
                        {
                            enemyType = new EnemyType(i, 30, 25, 25, 25, 25, 25);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(6).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(6).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(5).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 71:
                        {
                            enemyType = new EnemyType(i, 30, 30, 25, 25, 25, 25);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(6).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(6).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(6).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 72:
                        {
                            enemyType = new EnemyType(i, 30, 30, 30, 25, 25, 25);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(6).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(7).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(6).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 73:
                        {
                            enemyType = new EnemyType(i, 30, 30, 30, 30, 25, 25);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(7).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(7).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(6).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 74:
                        {
                            enemyType = new EnemyType(i, 30, 30, 30, 30, 25, 25);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(7).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(7).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(7).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 75:
                        {
                            enemyType = new EnemyType(i, 30, 30, 30, 30, 30, 25);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(7).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(8).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(7).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 76:
                        {
                            enemyType = new EnemyType(i, 10, 10, 10, 10, 10, 10);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(0).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 77:
                        {
                            enemyType = new EnemyType(i, 15, 10, 10, 10, 10, 10);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(0).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(0).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 78:
                        {
                            enemyType = new EnemyType(i, 15, 15, 10, 10, 10, 10);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(0).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(0).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(0).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 79:
                        {
                            enemyType = new EnemyType(i, 15, 15, 15, 10, 10, 10);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(0).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(1).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(0).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 80:
                        {
                            enemyType = new EnemyType(i, 15, 15, 15, 15, 10, 10);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(1).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(1).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(0).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 81:
                        {
                            enemyType = new EnemyType(i, 15, 15, 15, 15, 15, 10);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(1).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(1).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(1).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 82:
                        {
                            enemyType = new EnemyType(i, 15, 15, 15, 15, 15, 15);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(1).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(2).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(1).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 83:
                        {
                            enemyType = new EnemyType(i, 20, 15, 15, 15, 15, 15);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(2).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(2).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(1).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 84:
                        {
                            enemyType = new EnemyType(i, 20, 20, 15, 15, 15, 15);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(2).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(2).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(2).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 85:
                        {
                            enemyType = new EnemyType(i, 20, 20, 20, 15, 15, 15);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(2).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(3).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(2).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 86:
                        {
                            enemyType = new EnemyType(i, 20, 20, 20, 20, 15, 15);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(3).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(3).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(2).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 87:
                        {
                            enemyType = new EnemyType(i, 20, 20, 20, 20, 20, 15);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(3).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(3).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(3).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 88:
                        {
                            enemyType = new EnemyType(i, 20, 20, 20, 20, 20, 20);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(3).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(4).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(3).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 89:
                        {
                            enemyType = new EnemyType(i, 25, 20, 20, 20, 20, 20);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(4).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(4).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(3).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 90:
                        {
                            enemyType = new EnemyType(i, 25, 25, 20, 20, 20, 20);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(4).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(4).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(4).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 91:
                        {
                            enemyType = new EnemyType(i, 25, 25, 20, 20, 20, 20);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(4).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(4).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(4).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 92:
                        {
                            enemyType = new EnemyType(i, 25, 25, 25, 20, 20, 20);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(4).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(5).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(4).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 93:
                        {
                            enemyType = new EnemyType(i, 25, 25, 25, 25, 20, 20);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(5).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(5).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(4).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 94:
                        {
                            enemyType = new EnemyType(i, 25, 25, 25, 25, 25, 20);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(5).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(5).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(5).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 95:
                        {
                            enemyType = new EnemyType(i, 25, 25, 25, 25, 25, 25);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(5).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(6).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(5).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 96:
                        {
                            enemyType = new EnemyType(i, 30, 25, 25, 25, 25, 25);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(6).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(6).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(5).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 97:
                        {
                            enemyType = new EnemyType(i, 30, 30, 25, 25, 25, 25);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(6).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(6).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(6).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 98:
                        {
                            enemyType = new EnemyType(i, 30, 30, 30, 25, 25, 25);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(6).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(7).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(6).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 99:
                        {
                            enemyType = new EnemyType(i, 30, 30, 30, 30, 25, 25);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(7).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(7).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(6).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                    case 100:
                        {
                            enemyType = new EnemyType(i, 30, 30, 30, 30, 25, 25);
                            enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(7).Value);
                            enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(7).Value);
                            enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(7).Value);
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                     */
                    default:
                        {
                            //Criar um monstro =P
                            enemyType = new EnemyType(i, Game1.random.Next(10, 50), Game1.random.Next(10, 100), Game1.random.Next(10, 100), Game1.random.Next(10, 100), Game1.random.Next(10, 100), Game1.random.Next(10, 100));
                            int length = Game1.random.Next(1, 10);
                            int weaponIndex = Game1.random.Next(0, Game1.config.shopWeapons.Count - 1);
                            for (int j = 0; j < length; j++)
                            {
                                enemyType.hardPoints["l"].addItem(Game1.config.shopWeapons.ElementAt(weaponIndex).Value);
                            }

                            length = Game1.random.Next(1, 10);
                            weaponIndex = Game1.random.Next(0, Game1.config.shopWeapons.Count - 1);
                            for (int j = 0; j < length; j++)
                            {
                                enemyType.hardPoints["c"].addItem(Game1.config.shopWeapons.ElementAt(weaponIndex).Value);
                            }

                            length = Game1.random.Next(1, 10);
                            weaponIndex = Game1.random.Next(0, Game1.config.shopWeapons.Count - 1);
                            for (int j = 0; j < length; j++)
                            {
                                enemyType.hardPoints["r"].addItem(Game1.config.shopWeapons.ElementAt(weaponIndex).Value);
                            }
                            enemyTypes.Add(i, enemyType);
                        }
                        break;
                }
            }
        }
    }
}

