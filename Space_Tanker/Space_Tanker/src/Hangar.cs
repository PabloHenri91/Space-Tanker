using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace Space_Tanker.src
{
    class Hangar : State
    {
        enum states { loading, hangar, supplyRoom, equipment, shop, chooseSector, chooseMisson, saveGame };
        states state;
        states nextState;

        //Hangar
        private float radians2 = MathHelper.ToRadians(2f);
        private float angle;
        private Vector2 shipPosition;
        private int health;
        private int energyShield;

        private int maximumWeight;
        private int currentWeight;
        private int availableWeight;

        //Shop
        private int itenIndex;

        //Equipment
        bool getItem;
        ShopWeapon mouseItem;
        ShopWeapon mouseItemRef;
        private bool drawPlusButtons;
        private int bonus;

        SoundEffect music;
        SoundEffectInstance musicInstance;

        internal Hangar()
            : base()
        {
            state = states.loading;
            nextState = states.hangar;

            //Hangar
            textures2Dlocations.Add("hangarBackground");
            textures2Dlocations.Add("hangarBox");
            textures2Dlocations.Add("playMission");
            textures2Dlocations.Add("playMissionPressed");
            textures2Dlocations.Add("supplyRoom");
            textures2Dlocations.Add("supplyRoomPressed");
            textures2Dlocations.Add("saveGame");
            textures2Dlocations.Add("saveGamePressed");
            textures2Dlocations.Add("levelUp");
            textures2Dlocations.Add("levelUpPressed");
            textures2Dlocations.Add("minus");
            textures2Dlocations.Add("minusPressed");
            textures2Dlocations.Add("plus");
            textures2Dlocations.Add("plusPressed");
            textures2Dlocations.Add("backButton");
            textures2Dlocations.Add("backButtonPressed");

            textures2Dlocations.Add("player" + Game1.memoryCard.shipIndex);
            textures2Dlocations.Add("upgradeWeapon");
            textures2Dlocations.Add("upgradeWeaponPressed");
            textures2Dlocations.Add("rightWeapon");
            textures2Dlocations.Add("rightWeaponPressed");
            textures2Dlocations.Add("leftWeapon");
            textures2Dlocations.Add("leftWeaponPressed");
            textures2Dlocations.Add("sell");
            textures2Dlocations.Add("sellPressed");
            textures2Dlocations.Add("buy");
            textures2Dlocations.Add("buyPressed");
            textures2Dlocations.Add("weaponSlotAmmo");
            textures2Dlocations.Add("weaponSlot");
            textures2Dlocations.Add("weaponSlotLocked");
            textures2Dlocations.Add("equipWeaponButton");
            textures2Dlocations.Add("equipWeaponButtonPressed");
            textures2Dlocations.Add("moreAmmoButton");
            textures2Dlocations.Add("lessAmmoButton");
            textures2Dlocations.Add("moreAmmoButtonPressed");
            textures2Dlocations.Add("lessAmmoButtonPressed");

            //Supply Room
            textures2Dlocations.Add("supplyRoomBackground");
            foreach (KeyValuePair<string, ShopWeapon> shopWeapon in Game1.config.shopWeapons)
            {
                textures2Dlocations.Add(shopWeapon.Value.textureReference);
            }

            //Choose Sector
            textures2Dlocations.Add("sectorBox");
            textures2Dlocations.Add("sectorBoxLocked");

            //Choose Mission
            textures2Dlocations.Add("missionBox");
            textures2Dlocations.Add("missionBoxLocked");

            //Save Game
            textures2Dlocations.Add("saveGameBackground");
            textures2Dlocations.Add("yes");
            textures2Dlocations.Add("no");
            textures2Dlocations.Add("yesPressed");
            textures2Dlocations.Add("noPressed");

            textures2DlocationsCount = textures2Dlocations.Count;

#if MUSIC
            music = Game1.myContentManager.Load<SoundEffect>("hanger");
            musicInstance = music.CreateInstance();
            musicInstance.IsLooped = true;
            music.Play();
#endif
        }

        internal bool load()
        {
            if (textures2DlocationsCount > 0)
            {
                string reference = textures2Dlocations[textures2DlocationsCount - 1];
                textures2D.Add(reference, new Sprite(reference));
                updatePercentLoaded();
                return false;
            }

            //Hangar
            textures2D["playMission"].setPosition(93, 99);
            textures2D["supplyRoom"].setPosition(93, 159);
            textures2D["saveGame"].setPosition(93, 219);
            textures2D["levelUp"].setPosition(273, 99);
            textures2D["playMissionPressed"].setPosition(93, 99);
            textures2D["supplyRoomPressed"].setPosition(93, 159);
            textures2D["saveGamePressed"].setPosition(93, 219);
            textures2D["levelUpPressed"].setPosition(273, 99);
            textures2D["backButton"].setPosition(25, 405);
            textures2D["backButtonPressed"].setPosition(25, 405);
            
            Game1.config.vectors2.Clear();
            Game1.config.vectors2.Add("speedMinus", new Vector2(568, 100));
            Game1.config.vectors2.Add("accelerationMinus", new Vector2(568, 148));
            Game1.config.vectors2.Add("agilityMinus", new Vector2(568, 196));
            Game1.config.vectors2.Add("armorMinus", new Vector2(568, 245));
            Game1.config.vectors2.Add("shieldPowerMinus", new Vector2(568, 293));
            Game1.config.vectors2.Add("shieldRechargeMinus", new Vector2(568, 341));
            Game1.config.vectors2.Add("speedPlus", new Vector2(668, 100));
            Game1.config.vectors2.Add("accelerationPlus", new Vector2(668, 148));
            Game1.config.vectors2.Add("agilityPlus", new Vector2(668, 196));
            Game1.config.vectors2.Add("armorPlus", new Vector2(668, 245));
            Game1.config.vectors2.Add("shieldPowerPlus", new Vector2(668, 293));
            Game1.config.vectors2.Add("shieldRechargePlus", new Vector2(668, 341));

            Game1.config.vectors2.Add("speedText", new Vector2(624, 112));
            Game1.config.vectors2.Add("accelerationText", new Vector2(624, 160));
            Game1.config.vectors2.Add("agilityText", new Vector2(624, 208));
            Game1.config.vectors2.Add("armorText", new Vector2(624, 257));
            Game1.config.vectors2.Add("shieldPowerText", new Vector2(624, 305));
            Game1.config.vectors2.Add("shieldRechargeText", new Vector2(624, 353));
            Game1.config.vectors2.Add("reqPointsText", new Vector2(318, 167));
            Game1.config.vectors2.Add("levelText", new Vector2(313, 208));
            Game1.config.vectors2.Add("scoreText", new Vector2(115, 59));

            Game1.config.vectors2.Add("maximumWeightText", new Vector2(90, 296));
            Game1.config.vectors2.Add("currentWeightText", new Vector2(90, 333));
            Game1.config.vectors2.Add("availableWeightText", new Vector2(90, 369));

            //Supply Room
            Game1.config.vectors2.Add("itemNameText", new Vector2(97, 349));
            Game1.config.vectors2.Add("itemDescriptionText", new Vector2(310, 110));
            Game1.config.vectors2.Add("itemPriceText", new Vector2(506, 357));
            Game1.config.vectors2.Add("itemSellPriceText", new Vector2(506, 400));
            Game1.config.vectors2.Add("itemAmountText", new Vector2(112, 448));
            Game1.config.vectors2.Add("itemTexture", new Vector2(87, 339));

            Game1.config.vectors2.Add("weaponDemageBar", new Vector2(349, 357));
            Game1.config.vectors2.Add("weaponWeightBar", new Vector2(349, 376));
            Game1.config.vectors2.Add("weaponEnergyBar", new Vector2(349, 396));
            Game1.config.vectors2.Add("weaponReloadTimeBar", new Vector2(349, 415));
            Game1.config.vectors2.Add("weaponAmmoWeightBar", new Vector2(349, 435));
            Game1.config.vectors2.Add("weaponAmmoPerMagBar", new Vector2(349, 454));

            textures2D["upgradeWeapon"].setPosition(592, 349);
            textures2D["upgradeWeaponPressed"].setPosition(592, 349);
            textures2D["buy"].setPosition(592, 349);
            textures2D["buyPressed"].setPosition(592, 349);
            textures2D["sell"].setPosition(592, 392);
            textures2D["sellPressed"].setPosition(592, 392);
            textures2D["rightWeapon"].setPosition(592, 435);
            textures2D["rightWeaponPressed"].setPosition(592, 435);
            textures2D["leftWeapon"].setPosition(482, 435);
            textures2D["leftWeaponPressed"].setPosition(482, 435);

            //Choose Sector
            for (int i = 0; i < 10; i++)
            {
                Game1.config.vectors2.Add("sectorBox" + i, new Vector2(250 + (400 * i) + ((Game1.memoryCard.sectorIndex) * -400), 90));
            }

            //Choose Mission
            for (int x = 0; x <= 9; x++)
            {
                for (int y = 0; y <= 9; y++)
                {
                    if (y < 5)
                    {
                        Game1.config.vectors2.Add("missionBox" + x + "" + y, new Vector2(45 + (150 * y), 110));
                    }
                    else
                    {
                        Game1.config.vectors2.Add("missionBox" + x + "" + y, new Vector2(45 + (150 * (y - 5)), 265));
                    }
                }
            }
            
            

            //Save Game
            textures2D["yes"].setPosition(275, 260);
            textures2D["yesPressed"].setPosition(275, 260);
            textures2D["no"].setPosition(429, 260);
            textures2D["noPressed"].setPosition(429, 260);

            return true;
        }

        internal void doLogic()
        {
            if (state == nextState)
            {
                switch (state)
                {
                    //case states.hangar:
                    #region
                    case states.hangar:
                        {
                            textures2D["player" + Game1.memoryCard.shipIndex].setAngle(angle += radians2);
                            if (Game1.input.click0)
                            {
                                if (textures2D["backButton"].intersectsWithMouseClick())
                                {
                                    Game1.nextState = Game1.states.mainMenu;
                                    return;
                                }

                                if (textures2D["playMission"].intersectsWithMouseClick())
                                {
                                    nextState = states.chooseSector;
                                    return;
                                }

                                if (textures2D["supplyRoom"].intersectsWithMouseClick())
                                {
                                    nextState = states.supplyRoom;
                                    return;
                                }

                                if (textures2D["saveGame"].intersectsWithMouseClick())
                                {
                                    nextState = states.saveGame;
                                    return;
                                }

                                if (textures2D["levelUp"].intersectsWithMouseClick())
                                {
                                    if (Game1.memoryCard.score >= Game1.config.level[Game1.memoryCard.level + 1])
                                    {
                                        Game1.memoryCard.level++;
                                        setShipAtributes();
                                        Game1.memoryCard.score -= Game1.config.level[Game1.memoryCard.level + 1];
                                        return;
                                    }
                                }

                                if (drawPlusButtons)
                                {
                                    if (Game1.memoryCard.speed < 100)
                                    if (textures2D["plus"].intersectsWithMouseClick(Game1.config.vectors2["speedPlus"]))
                                    {
                                        Game1.memoryCard.speed++;
                                        setShipAtributes();
                                        return;
                                    }
                                    if (Game1.memoryCard.acceleration < 100)
                                    if (textures2D["plus"].intersectsWithMouseClick(Game1.config.vectors2["accelerationPlus"]))
                                    {
                                        Game1.memoryCard.acceleration++;
                                        setShipAtributes();
                                        return;
                                    }
                                    if (Game1.memoryCard.agility < 100)
                                    if (textures2D["plus"].intersectsWithMouseClick(Game1.config.vectors2["agilityPlus"]))
                                    {
                                        Game1.memoryCard.agility++;
                                        setShipAtributes();
                                        return;
                                    }
                                    if (Game1.memoryCard.armor < 100)
                                    if (textures2D["plus"].intersectsWithMouseClick(Game1.config.vectors2["armorPlus"]))
                                    {
                                        Game1.memoryCard.armor++;
                                        setShipAtributes();
                                        return;
                                    }
                                    if (Game1.memoryCard.shieldPower < 100)
                                    if (textures2D["plus"].intersectsWithMouseClick(Game1.config.vectors2["shieldPowerPlus"]))
                                    {
                                        Game1.memoryCard.shieldPower++;
                                        setShipAtributes();
                                        return;
                                    }
                                    if (Game1.memoryCard.shieldRecharge < 100)
                                    if (textures2D["plus"].intersectsWithMouseClick(Game1.config.vectors2["shieldRechargePlus"]))
                                    {
                                        Game1.memoryCard.shieldRecharge++;
                                        setShipAtributes();
                                        return;
                                    }
                                }

                                if (Game1.memoryCard.speed > 10 + Game1.players.playerTypes[Game1.memoryCard.shipIndex].speedBonus)
                                {
                                    if (textures2D["minus"].intersectsWithMouseClick(Game1.config.vectors2["speedMinus"]))
                                    {
                                        Game1.memoryCard.speed--;
                                        setShipAtributes();
                                        return;
                                    }
                                }

                                if (Game1.memoryCard.acceleration > 10 + Game1.players.playerTypes[Game1.memoryCard.shipIndex].accelerationBonus)
                                {
                                    if (textures2D["minus"].intersectsWithMouseClick(Game1.config.vectors2["accelerationMinus"]))
                                    {
                                        Game1.memoryCard.acceleration--;
                                        setShipAtributes();
                                        return;
                                    }
                                }

                                if (Game1.memoryCard.agility > 10 + Game1.players.playerTypes[Game1.memoryCard.shipIndex].agilityBonus)
                                {
                                    if (textures2D["minus"].intersectsWithMouseClick(Game1.config.vectors2["agilityMinus"]))
                                    {
                                        Game1.memoryCard.agility--;
                                        setShipAtributes();
                                        return;
                                    }
                                }

                                if (Game1.memoryCard.armor > 10 + Game1.players.playerTypes[Game1.memoryCard.shipIndex].armorBonus)
                                {
                                    if (textures2D["minus"].intersectsWithMouseClick(Game1.config.vectors2["armorMinus"]))
                                    {
                                        Game1.memoryCard.armor--;
                                        setShipAtributes();
                                        return;
                                    }
                                }

                                if (Game1.memoryCard.shieldPower > 10 + Game1.players.playerTypes[Game1.memoryCard.shipIndex].shieldPowerBonus)
                                {
                                    if (textures2D["minus"].intersectsWithMouseClick(Game1.config.vectors2["shieldPowerMinus"]))
                                    {
                                        Game1.memoryCard.shieldPower--;
                                        setShipAtributes();
                                        return;
                                    }
                                }

                                if (Game1.memoryCard.shieldRecharge > 10 + Game1.players.playerTypes[Game1.memoryCard.shipIndex].shieldRechargeBonus)
                                {
                                    if (textures2D["minus"].intersectsWithMouseClick(Game1.config.vectors2["shieldRechargeMinus"]))
                                    {
                                        Game1.memoryCard.shieldRecharge--;
                                        setShipAtributes();
                                        return;
                                    }
                                }
                            }
                        }
                        break;
                    #endregion
                    //case states.supplyRoom:
                    #region
                    case states.supplyRoom:
                        {
                            textures2D["player" + Game1.memoryCard.shipIndex].setAngle(angle += radians2);
                            if (Game1.input.click0)
                            {
                                if (textures2D["backButton"].intersectsWithMouseClick())
                                {
                                    nextState = states.hangar;
                                    return;
                                }

                                //Shop
                                if (textures2D["leftWeapon"].intersectsWithMouseClick())
                                {
                                    if (itenIndex > 0)
                                    {
                                        itenIndex--;
                                    }
                                    return;
                                }
                                if (textures2D["rightWeapon"].intersectsWithMouseClick())
                                {
                                    if (itenIndex < Game1.config.shopWeapons.Count - 1)
                                    {
                                        itenIndex++;
                                    }
                                    return;
                                }
                                if (textures2D["buy"].intersectsWithMouseClick())
                                {
                                    Game1.config.shopWeapons.Values.ElementAt(itenIndex).buy();
                                    return;
                                }
                                if (textures2D["sell"].intersectsWithMouseClick())
                                {
                                    Game1.config.shopWeapons.Values.ElementAt(itenIndex).sell();
                                    return;
                                }

                                
                                foreach (KeyValuePair<string, HardPoint> weaponSlot in Game1.memoryCard.hardPoints)
                                {
                                    if (textures2D["weaponSlot"].intersectsWithMouseClick(weaponSlot.Value.position))
                                    {
                                        if (weaponSlot.Value.isEmpty)
                                        {
                                            int aux = Game1.config.shopWeapons.Values.ElementAt(itenIndex).inventoryAmount;
                                            for (int i = 0; i < aux; i++)
                                            {
                                                weaponSlot.Value.addItem(Game1.config.shopWeapons.Values.ElementAt(itenIndex));
                                            }
                                        }
                                        else
                                        {
                                            weaponSlot.Value.removeItem();
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    #endregion
                    //case states.equipment:
                    #region
                    case states.equipment:
                        {
                            if (Game1.input.click0)
                            {
                                if (textures2D["backButton"].intersectsWithMouseClick())
                                {
                                    nextState = states.supplyRoom;
                                }

                                foreach (KeyValuePair<string, HardPoint> weaponSlot in Game1.memoryCard.hardPoints)
                                {
                                    if (!weaponSlot.Value.isEmpty)
                                    {
                                        if (textures2D["weaponSlot"].intersectsWithMouseClick(weaponSlot.Value.position))
                                        {
                                            weaponSlot.Value.removeItem();
                                            resetItensPositions();
                                            break;
                                        }
                                    }
                                }
                            }

                            if (getItem)
                            {
                                if (!Game1.input.mouse0)
                                {
                                    foreach (KeyValuePair<string, HardPoint> weaponSlot in Game1.memoryCard.hardPoints)
                                    {
                                        if (textures2D["weaponSlot"].intersectsWithMouseClick(weaponSlot.Value.position))
                                        {
                                            if (weaponSlot.Value.addItem(mouseItem))
                                            {
                                                getItem = false;
                                                mouseItem = null;
                                                break;
                                            }
                                        }
                                    }

                                    if (mouseItem != null)
                                    {
                                        getItem = false;
                                        mouseItemRef.inventoryAmount++;
                                        mouseItem = null;
                                    }
                                }
                                else
                                {
                                    mouseItem.position = new Vector2(Game1.input.onScreenMouseX - textures2D[mouseItem.textureReference].width / 2, Game1.input.onScreenMouseY - textures2D[mouseItem.textureReference].height / 2);
                                }
                            }
                            else
                            {
                                if (Math.Abs(Game1.input.totalDy) > 10)
                                {
                                    if (Game1.input.dy < 0)
                                    {
                                        //mooving up
                                        try
                                        {
                                            if (Game1.config.shopWeapons.Last(item => item.Value.inventoryAmount > 0).Value.position.Y + (Game1.input.dy * 2) > 173)
                                            {
                                                foreach (KeyValuePair<string, ShopWeapon> item in Game1.config.shopWeapons)
                                                {
                                                    if (item.Value.inventoryAmount > 0)
                                                    {
                                                        item.Value.position.Y = item.Value.position.Y + (Game1.input.dy * 2);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                int auxDx = (int)(173 - Game1.config.shopWeapons.Last(item => item.Value.inventoryAmount > 0).Value.position.Y);

                                                foreach (KeyValuePair<string, ShopWeapon> item in Game1.config.shopWeapons)
                                                {
                                                    if (item.Value.inventoryAmount > 0)
                                                    {
                                                        item.Value.position.Y = item.Value.position.Y + auxDx;
                                                    }
                                                }
                                            }
                                        }
                                        catch (InvalidOperationException) { }
                                    }
                                    else
                                    {
                                        //mooving down
                                        //Game1.input.dy > 0
                                        try
                                        {
                                            if (Game1.config.shopWeapons.First(item => item.Value.inventoryAmount > 0).Value.position.Y + Game1.input.dy * 2 < 173)
                                            {
                                                foreach (KeyValuePair<string, ShopWeapon> item in Game1.config.shopWeapons)
                                                {
                                                    if (item.Value.inventoryAmount > 0)
                                                    {
                                                        item.Value.position.Y = item.Value.position.Y + Game1.input.dy * 2;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                int auxDx = (int)(173 - Game1.config.shopWeapons.First(item => item.Value.inventoryAmount > 0).Value.position.Y);

                                                foreach (KeyValuePair<string, ShopWeapon> item in Game1.config.shopWeapons)
                                                {
                                                    if (item.Value.inventoryAmount > 0)
                                                    {
                                                        item.Value.position.Y = item.Value.position.Y + auxDx;
                                                    }
                                                }
                                            }
                                        }
                                        catch (InvalidOperationException) { }
                                    }
                                }
                                else if (Math.Abs(Game1.input.totalDx) > 20)
                                {
                                    foreach (KeyValuePair<string, ShopWeapon> item in Game1.config.shopWeapons)
                                    {
                                        if (item.Value.inventoryAmount > 0)
                                        {
                                            if (textures2D[item.Value.textureReference].intersectsWithMouseClick(item.Value.position))
                                            {
                                                getItem = true;

                                                mouseItem = new ShopWeapon(item.Value);
                                                mouseItem.position = new Vector2(Game1.input.onScreenMouseX - textures2D[mouseItem.textureReference].width / 2, Game1.input.onScreenMouseY - textures2D[mouseItem.textureReference].height / 2);

                                                mouseItemRef = item.Value;
                                                mouseItemRef.inventoryAmount--;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    #endregion
                    //case states.shop:
                    #region
                    case states.shop:
                        {
                            if (Game1.input.click0)
                            {
                                if (textures2D["backButton"].intersectsWithMouseClick())
                                {
                                    nextState = states.supplyRoom;
                                }
                                else if (textures2D["left"].intersectsWithMouseClick())
                                {
                                    if (itenIndex > 0)
                                    {
                                        itenIndex--;
                                    }
                                }
                                else if (textures2D["right"].intersectsWithMouseClick())
                                {
                                    if (itenIndex < Game1.config.shopWeapons.Count - 1)
                                    {
                                        itenIndex++;
                                    }
                                }
                                else if (textures2D["buy"].intersectsWithMouseClick())
                                {
                                    Game1.config.shopWeapons.Values.ElementAt(itenIndex).buy();
                                }
                                else if (textures2D["sell"].intersectsWithMouseClick())
                                {
                                    Game1.config.shopWeapons.Values.ElementAt(itenIndex).sell();
                                }
                            }
                        }
                        break;
                    #endregion
                    //case states.chooseSector:
                    #region
                    case states.chooseSector:
                        {
                            if (Math.Abs(Game1.input.totalDx) > 10)
                            {
                                if (Game1.input.dx < 0)
                                {
                                    if (Game1.config.vectors2["sectorBox" + 9].X > 250 + (400 * 9) + ((9) * -400))
                                    {
                                        for (int j = 0; j < 10; j++)
                                        {
                                            Game1.config.vectors2["sectorBox" + j] = new Vector2(Game1.config.vectors2["sectorBox" + j].X + Game1.input.dx * 2, Game1.config.vectors2["sectorBox" + j].Y);
                                        }
                                    }
                                }
                                else
                                {
                                    //Game1.input.dx > 0
                                    if (Game1.config.vectors2["sectorBox" + 0].X < 250 + (400 * 1) + (1 * -400))
                                    {
                                        for (int j = 0; j < 10; j++)
                                        {
                                            Game1.config.vectors2["sectorBox" + j] = new Vector2(Game1.config.vectors2["sectorBox" + j].X + Game1.input.dx * 2, Game1.config.vectors2["sectorBox" + j].Y);
                                        }
                                    }
                                }
                            }

                            if (Game1.input.click0)
                            {
                                if (textures2D["backButton"].intersectsWithMouseClick())
                                {
                                    nextState = states.hangar;
                                }

                                for (int i = 0; i < 10; i++)
                                {
                                    if (textures2D["sectorBox"].intersectsWithMouseClick(Game1.config.vectors2["sectorBox" + i]))
                                    {
                                        if ((Game1.memoryCard.missionUnlocked - 1) / 10 >= i)
                                        {
                                            nextState = states.chooseMisson;
                                            Game1.memoryCard.sectorIndex = i;
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    #endregion
                    //case states.chooseMisson:
                    #region
                    case states.chooseMisson:
                        {
                            if (Game1.input.click0)
                            {
                                if (textures2D["backButton"].intersectsWithMouseClick())
                                {
                                    nextState = states.chooseSector;
                                }

                                for (int y = 0; y < 10; y++)
                                {
                                    if (y < 5)
                                    {
                                        if (textures2D["missionBox"].intersectsWithMouseClick(Game1.config.vectors2["missionBox" + (Game1.memoryCard.sectorIndex) + "" + y]))
                                        {
                                            if (Game1.memoryCard.missionUnlocked > Convert.ToInt32((Game1.memoryCard.sectorIndex) + "" + y))
                                            {
                                                Game1.memoryCard.mission = Convert.ToInt32((Game1.memoryCard.sectorIndex) + "" + y);
                                                Game1.nextState = Game1.states.mission;
                                            }
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (textures2D["missionBox"].intersectsWithMouseClick(Game1.config.vectors2["missionBox" + (Game1.memoryCard.sectorIndex) + "" + y]))
                                        {
                                            if (Game1.memoryCard.missionUnlocked > Convert.ToInt32((Game1.memoryCard.sectorIndex) + "" + y))
                                            {
                                                Game1.memoryCard.mission = Convert.ToInt32((Game1.memoryCard.sectorIndex) + "" + y);
                                                Game1.nextState = Game1.states.mission;
                                            }
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    #endregion
                    //case states.saveGame:
                    #region
                    case states.saveGame:
                        {
                            if (Game1.input.click0)
                            {
                                if (textures2D["yes"].intersectsWithMouseClick())
                                {
                                    Game1.memoryCard.saveGame();
                                    nextState = states.hangar;
                                }
                                else if (textures2D["no"].intersectsWithMouseClick())
                                {
                                    nextState = states.hangar;
                                }
                            }
                        }
                        break;
                    #endregion
                }
            }
            else
            {
                //reloadNextState
                switch (nextState)
                {
                    //case states.hangar:
                    #region
                    case states.hangar:
                        {
                            angle = 0;
                            textures2D["player" + Game1.memoryCard.shipIndex].setPosition(347, 323, angle);
                            shipPosition = new Vector2(347, 323);//Health Bar
                            setShipAtributes();

                            Game1.config.vectors2["maximumWeightText"] = new Vector2(90, 296);
                            Game1.config.vectors2["currentWeightText"] = new Vector2(90, 333);
                            Game1.config.vectors2["availableWeightText"] = new Vector2(90, 369);

                            Game1.config.vectors2["scoreText"] = new Vector2(115, 59);
                        }
                        break;
                    #endregion
                    //case states.supplyRoom:
                    #region
                    case states.supplyRoom:
                        {
                            angle = 0;
                            textures2D["player" + Game1.memoryCard.shipIndex].setPosition(186, 132, angle);
                            shipPosition = new Vector2(186, 132);//Health Bar

                            Game1.config.vectors2["maximumWeightText"] = new Vector2(105, 230);
                            Game1.config.vectors2["currentWeightText"] = new Vector2(105, 267);
                            Game1.config.vectors2["availableWeightText"] = new Vector2(105, 303);

                            Game1.config.vectors2["scoreText"] = new Vector2(130, 13);
                        }
                        break;
                    #endregion
                    //case states.shop:
                    #region
                    case states.shop:
                        {
                        }
                        break;
                    #endregion
                    //case states.equipment:
                    #region
                    case states.equipment:
                        {
                            resetItensPositions();
                            textures2D["player" + Game1.memoryCard.shipIndex].setPosition(515, 240);
                        }
                        break;
                    #endregion
                    //case states.chooseSector:
                    #region
                    case states.chooseSector:
                        {
                        }
                        break;
                    #endregion
                    //case states.chooseMisson:
                    #region
                    case states.chooseMisson:
                        {
                        }
                        break;
                    #endregion
                    //case states.saveGame:
                    #region
                    case states.saveGame:
                        {
                        }
                        break;
                    #endregion
                }
                state = nextState;
                Game1.needToDraw = true;
            }
        }

        internal void draw()
        {
            textures2D["hangarBackground"].drawOnScreen();

            switch (state)
            {
                //case states.hangar:
                #region
                case states.hangar:
                    {
                        textures2D["hangarBox"].drawOnScreen();
                        textures2D["playMission"].drawOnScreen();
                        textures2D["supplyRoom"].drawOnScreen();
                        textures2D["saveGame"].drawOnScreen();
                        textures2D["levelUp"].drawOnScreen();
                        textures2D["player" + Game1.memoryCard.shipIndex].drawOnScreen2();
                        drawHealthBar();

                        textures2D["backButton"].drawOnScreen();

                        Game1.spriteBatch.DrawString(Game1.Verdana12, "$" + Game1.memoryCard.score, Game1.config.vectors2["scoreText"], Color.White);

                        Game1.spriteBatch.DrawString(Game1.Verdana12, "Maximum Weight: " + maximumWeight, Game1.config.vectors2["maximumWeightText"], Color.White);
                        Game1.spriteBatch.DrawString(Game1.Verdana12, "  Current Weight: " + currentWeight, Game1.config.vectors2["currentWeightText"], Color.White);
                        Game1.spriteBatch.DrawString(Game1.Verdana12, "Available Weight: " + availableWeight, Game1.config.vectors2["availableWeightText"], Color.White);


                        if (Game1.config.level[Game1.memoryCard.level + 1] <= Game1.memoryCard.score)
                        {
                            Game1.spriteBatch.DrawString(Game1.Verdana12, "$" + Game1.config.level[Game1.memoryCard.level + 1], Game1.config.vectors2["reqPointsText"], Color.Lime);
                        }
                        else
                        {
                            Game1.spriteBatch.DrawString(Game1.Verdana12, "$" + Game1.config.level[Game1.memoryCard.level + 1], Game1.config.vectors2["reqPointsText"], Color.Red);
                        }

                        Game1.spriteBatch.DrawString(Game1.Verdana12, "Level: " + Game1.memoryCard.level, Game1.config.vectors2["levelText"], Color.White);
                        Game1.spriteBatch.DrawString(Game1.Verdana12, "" + Game1.memoryCard.speed, Game1.config.vectors2["speedText"], Game1.memoryCard.speed == 100 ? Color.Gold : Color.White);
                        Game1.spriteBatch.DrawString(Game1.Verdana12, "" + Game1.memoryCard.acceleration, Game1.config.vectors2["accelerationText"], Game1.memoryCard.acceleration == 100 ? Color.Gold : Color.White);
                        Game1.spriteBatch.DrawString(Game1.Verdana12, "" + Game1.memoryCard.agility, Game1.config.vectors2["agilityText"], Game1.memoryCard.agility == 100 ? Color.Gold : Color.White);
                        Game1.spriteBatch.DrawString(Game1.Verdana12, "" + Game1.memoryCard.armor, Game1.config.vectors2["armorText"], Game1.memoryCard.armor == 100 ? Color.Gold : Color.White);
                        Game1.spriteBatch.DrawString(Game1.Verdana12, "" + Game1.memoryCard.shieldPower, Game1.config.vectors2["shieldPowerText"], Game1.memoryCard.shieldPower == 100 ? Color.Gold : Color.White);
                        Game1.spriteBatch.DrawString(Game1.Verdana12, "" + Game1.memoryCard.shieldRecharge, Game1.config.vectors2["shieldRechargeText"], Game1.memoryCard.shieldRecharge == 100 ? Color.Gold : Color.White);

                        //drawMinusButtons
                        {
                            if (Game1.memoryCard.speed > 10 + Game1.players.playerTypes[Game1.memoryCard.shipIndex].speedBonus)
                            {
                                textures2D["minus"].drawOnScreen(Game1.config.vectors2["speedMinus"]);
                            }

                            if (Game1.memoryCard.acceleration > 10 + Game1.players.playerTypes[Game1.memoryCard.shipIndex].accelerationBonus)
                            {
                                textures2D["minus"].drawOnScreen(Game1.config.vectors2["accelerationMinus"]);
                            }

                            if (Game1.memoryCard.agility > 10 + Game1.players.playerTypes[Game1.memoryCard.shipIndex].agilityBonus)
                            {
                                textures2D["minus"].drawOnScreen(Game1.config.vectors2["agilityMinus"]);
                            }

                            if (Game1.memoryCard.armor > 10 + Game1.players.playerTypes[Game1.memoryCard.shipIndex].armorBonus)
                            {
                                textures2D["minus"].drawOnScreen(Game1.config.vectors2["armorMinus"]);
                            }

                            if (Game1.memoryCard.shieldPower > 10 + Game1.players.playerTypes[Game1.memoryCard.shipIndex].shieldPowerBonus)
                            {
                                textures2D["minus"].drawOnScreen(Game1.config.vectors2["shieldPowerMinus"]);
                            }

                            if (Game1.memoryCard.shieldRecharge > 10 + Game1.players.playerTypes[Game1.memoryCard.shipIndex].shieldRechargeBonus)
                            {
                                textures2D["minus"].drawOnScreen(Game1.config.vectors2["shieldRechargeMinus"]);
                            }
                        }

                        if (drawPlusButtons)
                        {
                            if (Game1.memoryCard.speed < 100)
                            textures2D["plus"].drawOnScreen(Game1.config.vectors2["speedPlus"]);
                            if (Game1.memoryCard.acceleration < 100)
                            textures2D["plus"].drawOnScreen(Game1.config.vectors2["accelerationPlus"]);
                            if (Game1.memoryCard.agility < 100)
                            textures2D["plus"].drawOnScreen(Game1.config.vectors2["agilityPlus"]);
                            if (Game1.memoryCard.armor < 100)
                            textures2D["plus"].drawOnScreen(Game1.config.vectors2["armorPlus"]);
                            if (Game1.memoryCard.shieldPower < 100)
                            textures2D["plus"].drawOnScreen(Game1.config.vectors2["shieldPowerPlus"]);
                            if (Game1.memoryCard.shieldRecharge < 100)
                            textures2D["plus"].drawOnScreen(Game1.config.vectors2["shieldRechargePlus"]);
                        }

                        if (Game1.input.mouse0)
                        {
                            if (textures2D["supplyRoom"].intersectsWithMouseClick())
                            {
                                textures2D["supplyRoomPressed"].drawOnScreen();
                                return;
                            }
                            if (textures2D["playMission"].intersectsWithMouseClick())
                            {
                                textures2D["playMissionPressed"].drawOnScreen();
                                return;
                            }
                            if (textures2D["saveGame"].intersectsWithMouseClick())
                            {
                                textures2D["saveGamePressed"].drawOnScreen();
                                return;
                            }
                            if (textures2D["levelUp"].intersectsWithMouseClick())
                            {
                                textures2D["levelUpPressed"].drawOnScreen();
                                return;
                            }
                            if (textures2D["backButton"].intersectsWithMouseClick())
                            {
                                textures2D["backButtonPressed"].drawOnScreen();
                                return;
                            }

                            if (Game1.memoryCard.speed > 10 + Game1.players.playerTypes[Game1.memoryCard.shipIndex].speedBonus)
                            {
                                if (textures2D["minus"].intersectsWithMouseClick(Game1.config.vectors2["speedMinus"]))
                                {
                                    textures2D["minusPressed"].drawOnScreen(Game1.config.vectors2["speedMinus"]);
                                    return;
                                }
                            }

                            if (Game1.memoryCard.acceleration > 10 + Game1.players.playerTypes[Game1.memoryCard.shipIndex].accelerationBonus)
                            {
                                if (textures2D["minus"].intersectsWithMouseClick(Game1.config.vectors2["accelerationMinus"]))
                                {
                                    textures2D["minusPressed"].drawOnScreen(Game1.config.vectors2["accelerationMinus"]);
                                    return;
                                }
                            }

                            if (Game1.memoryCard.agility > 10 + Game1.players.playerTypes[Game1.memoryCard.shipIndex].agilityBonus)
                            {
                                if (textures2D["minus"].intersectsWithMouseClick(Game1.config.vectors2["agilityMinus"]))
                                {
                                    textures2D["minusPressed"].drawOnScreen(Game1.config.vectors2["agilityMinus"]);
                                    return;
                                }
                            }

                            if (Game1.memoryCard.armor > 10 + Game1.players.playerTypes[Game1.memoryCard.shipIndex].armorBonus)
                            {
                                if (textures2D["minus"].intersectsWithMouseClick(Game1.config.vectors2["armorMinus"]))
                                {
                                    textures2D["minusPressed"].drawOnScreen(Game1.config.vectors2["armorMinus"]);
                                    return;
                                }
                            }

                            if (Game1.memoryCard.shieldPower > 10 + Game1.players.playerTypes[Game1.memoryCard.shipIndex].shieldPowerBonus)
                            {
                                if (textures2D["minus"].intersectsWithMouseClick(Game1.config.vectors2["shieldPowerMinus"]))
                                {
                                    textures2D["minusPressed"].drawOnScreen(Game1.config.vectors2["shieldPowerMinus"]);
                                    return;
                                }
                            }

                            if (Game1.memoryCard.shieldRecharge > 10 + Game1.players.playerTypes[Game1.memoryCard.shipIndex].shieldRechargeBonus)
                            {
                                if (textures2D["minus"].intersectsWithMouseClick(Game1.config.vectors2["shieldRechargeMinus"]))
                                {
                                    textures2D["minusPressed"].drawOnScreen(Game1.config.vectors2["shieldRechargeMinus"]);
                                    return;
                                }
                            }

                            if (drawPlusButtons)
                            {
                                if (textures2D["plus"].intersectsWithMouseClick(Game1.config.vectors2["speedPlus"]))
                                {
                                    if (Game1.memoryCard.speed < 100)
                                    textures2D["plusPressed"].drawOnScreen(Game1.config.vectors2["speedPlus"]);
                                    return;
                                }
                                if (textures2D["plus"].intersectsWithMouseClick(Game1.config.vectors2["accelerationPlus"]))
                                {
                                    if (Game1.memoryCard.acceleration < 100)
                                    textures2D["plusPressed"].drawOnScreen(Game1.config.vectors2["accelerationPlus"]);
                                    return;
                                }
                                if (textures2D["plus"].intersectsWithMouseClick(Game1.config.vectors2["agilityPlus"]))
                                {
                                    if (Game1.memoryCard.agility < 100)
                                    textures2D["plusPressed"].drawOnScreen(Game1.config.vectors2["agilityPlus"]);
                                    return;
                                }
                                
                                if (textures2D["plus"].intersectsWithMouseClick(Game1.config.vectors2["armorPlus"]))
                                {
                                    if (Game1.memoryCard.armor < 100)
                                    textures2D["plusPressed"].drawOnScreen(Game1.config.vectors2["armorPlus"]);
                                    return;
                                }
                                if (textures2D["plus"].intersectsWithMouseClick(Game1.config.vectors2["shieldPowerPlus"]))
                                {
                                    if (Game1.memoryCard.shieldPower < 100)
                                    textures2D["plusPressed"].drawOnScreen(Game1.config.vectors2["shieldPowerPlus"]);
                                    return;
                                }
                                if (textures2D["plus"].intersectsWithMouseClick(Game1.config.vectors2["shieldRechargePlus"]))
                                {
                                    if (Game1.memoryCard.shieldRecharge < 100)
                                    textures2D["plusPressed"].drawOnScreen(Game1.config.vectors2["shieldRechargePlus"]);
                                    return;
                                }
                            }
                        }
                    }
                    break;
                #endregion
                //case states.supplyRoom:
                #region
                case states.supplyRoom:
                    {
                        textures2D["supplyRoomBackground"].drawOnScreen();
                        textures2D["backButton"].drawOnScreen();

                        Game1.spriteBatch.DrawString(Game1.Verdana12, "Maximum Weight: " + maximumWeight, Game1.config.vectors2["maximumWeightText"], Color.White);
                        Game1.spriteBatch.DrawString(Game1.Verdana12, "  Current Weight: " + currentWeight, Game1.config.vectors2["currentWeightText"], Color.White);
                        Game1.spriteBatch.DrawString(Game1.Verdana12, "Available Weight: " + availableWeight, Game1.config.vectors2["availableWeightText"], Color.White);

                        foreach (KeyValuePair<string, HardPoint> weaponSlot in Game1.memoryCard.hardPoints)
                        {
                            if (weaponSlot.Value.isEmpty)
                            {
                                textures2D["weaponSlotLocked"].drawOnScreen(weaponSlot.Value.position);
                            }
                            else
                            {
                                textures2D["weaponSlot"].drawOnScreen(weaponSlot.Value.position);
                                textures2D[weaponSlot.Value.textureReference].drawOnScreen(weaponSlot.Value.position);
                                Game1.spriteBatch.DrawString(Game1.Verdana12, "" + Game1.config.shopWeapons[weaponSlot.Value.weaponName].textWeaponName, new Vector2(weaponSlot.Value.position.X + 10, weaponSlot.Value.position.Y + 10), Color.White);
                                Game1.spriteBatch.DrawString(Game1.Verdana12, "" + weaponSlot.Value.amountEquiped, new Vector2(weaponSlot.Value.position.X + 10, weaponSlot.Value.position.Y + 110), Color.White);
                            }
                        }

                        textures2D["player" + Game1.memoryCard.shipIndex].drawOnScreen2();
                        drawHealthBar();



                        Game1.spriteBatch.DrawString(Game1.Verdana12, "$" + Game1.memoryCard.score, Game1.config.vectors2["scoreText"], Color.White);

                        //ShopWeapon
                        textures2D[Game1.config.shopWeapons.Values.ElementAt(itenIndex).textureReference].drawOnScreen(Game1.config.vectors2["itemTexture"]);
                        Game1.spriteBatch.DrawString(Game1.Verdana12, Game1.config.shopWeapons.Values.ElementAt(itenIndex).textWeaponName, Game1.config.vectors2["itemNameText"], Color.White);

                        if (Game1.config.shopWeapons.Values.ElementAt(itenIndex).price <= Game1.memoryCard.score)
                        {
                            Game1.spriteBatch.DrawString(Game1.Verdana12, "$" + Game1.config.shopWeapons.Values.ElementAt(itenIndex).price, Game1.config.vectors2["itemPriceText"], Color.Lime);
                        }
                        else
                        {
                            Game1.spriteBatch.DrawString(Game1.Verdana12, "$" + Game1.config.shopWeapons.Values.ElementAt(itenIndex).price, Game1.config.vectors2["itemPriceText"], Color.Red);
                        }

                        Game1.spriteBatch.DrawString(Game1.Verdana12, "$" + (Game1.config.shopWeapons.Values.ElementAt(itenIndex).price / 2), Game1.config.vectors2["itemSellPriceText"], Color.White);
                        Game1.spriteBatch.DrawString(Game1.Verdana12, "$" + Game1.memoryCard.score, Game1.config.vectors2["scoreText"], Color.White);
                        Game1.spriteBatch.DrawString(Game1.Verdana12, "You have: " + Game1.config.shopWeapons.Values.ElementAt(itenIndex).inventoryAmount, Game1.config.vectors2["itemAmountText"], Color.White);

                        drawAtributeBar(Game1.config.vectors2["weaponDemageBar"], Game1.config.shopWeapons.Values.ElementAt(itenIndex).demage, Game1.config.maxDemage, "Lime");
                        drawAtributeBar(Game1.config.vectors2["weaponWeightBar"], Game1.config.shopWeapons.Values.ElementAt(itenIndex).weight, Game1.config.maxWeight, "Red");
                        drawAtributeBar(Game1.config.vectors2["weaponEnergyBar"], Game1.config.shopWeapons.Values.ElementAt(itenIndex).energy, Game1.config.maxEnergy, "Red");
                        drawAtributeBar(Game1.config.vectors2["weaponReloadTimeBar"], Game1.config.shopWeapons.Values.ElementAt(itenIndex).reloadTime, Game1.config.maxReloadTime, "Red");
                        drawAtributeBar(Game1.config.vectors2["weaponAmmoWeightBar"], Game1.config.shopWeapons.Values.ElementAt(itenIndex).ammoWeight, Game1.config.maxAmmoWeight, "Red");
                        drawAtributeBar(Game1.config.vectors2["weaponAmmoPerMagBar"], Game1.config.shopWeapons.Values.ElementAt(itenIndex).shootsPerRound, Game1.config.maxShootsPerRound, "Lime");

                        textures2D["leftWeapon"].drawOnScreen();
                        textures2D["rightWeapon"].drawOnScreen();
                        textures2D["buy"].drawOnScreen();
                        textures2D["sell"].drawOnScreen();

                        if (Game1.input.mouse0)
                        {
                            if (textures2D["backButton"].intersectsWithMouseClick())
                            {
                                textures2D["backButtonPressed"].drawOnScreen();
                                return;
                            }

                            //Shop
                            if (textures2D["leftWeapon"].intersectsWithMouseClick())
                            {
                                textures2D["leftWeaponPressed"].drawOnScreen();
                                return;
                            }
                            if (textures2D["rightWeapon"].intersectsWithMouseClick())
                            {
                                textures2D["rightWeaponPressed"].drawOnScreen();
                                return;
                            }
                            if (textures2D["buy"].intersectsWithMouseClick())
                            {
                                textures2D["buyPressed"].drawOnScreen();
                                return;
                            }
                            if (textures2D["sell"].intersectsWithMouseClick())
                            {
                                textures2D["sellPressed"].drawOnScreen();
                                return;
                            }
                        }
                    }
                    break;
                #endregion
                //case states.equipment:
                #region
                case states.equipment:
                    {
                        textures2D["weaponsBackground"].drawOnScreen();
                        textures2D["backButton"].drawOnScreen();

                        if (Game1.input.mouse0)
                        {
                            if (textures2D["backButton"].intersectsWithMouseClick())
                            {
                                textures2D["backButtonPressed"].drawOnScreen();
                            }
                        }

                        foreach (KeyValuePair<string, ShopWeapon> shopWeapon in Game1.config.shopWeapons)
                        {
                            if (shopWeapon.Value.inventoryAmount > 0)
                            {
                                textures2D[shopWeapon.Value.textureReference].drawOnScreen(shopWeapon.Value.position);
                                Game1.spriteBatch.DrawString(Game1.Verdana12, "" + shopWeapon.Value.textWeaponName, new Vector2(shopWeapon.Value.position.X + 10, shopWeapon.Value.position.Y + 10), Color.White);
                                Game1.spriteBatch.DrawString(Game1.Verdana12, "" + shopWeapon.Value.inventoryAmount, new Vector2(shopWeapon.Value.position.X + 10, shopWeapon.Value.position.Y + 110), Color.White);
                            }
                        }

                        textures2D["player" + Game1.memoryCard.shipIndex].drawOnScreen();

                        foreach (KeyValuePair<string, HardPoint> weaponSlot in Game1.memoryCard.hardPoints)
                        {
                            if (weaponSlot.Value.isEmpty)
                            {
                                textures2D["weaponSlotLocked"].drawOnScreen(weaponSlot.Value.position);
                            }
                            else
                            {
                                textures2D["weaponSlot"].drawOnScreen(weaponSlot.Value.position);
                                textures2D[weaponSlot.Value.textureReference].drawOnScreen(weaponSlot.Value.position);
                                Game1.spriteBatch.DrawString(Game1.Verdana12, "" + Game1.config.shopWeapons[weaponSlot.Value.weaponName].textWeaponName, new Vector2(weaponSlot.Value.position.X + 10, weaponSlot.Value.position.Y + 10), Color.White);
                                Game1.spriteBatch.DrawString(Game1.Verdana12, "" + weaponSlot.Value.amountEquiped, new Vector2(weaponSlot.Value.position.X + 10, weaponSlot.Value.position.Y + 110), Color.White);
                            }
                        }

                        if (mouseItem != null)
                        {
                            textures2D[mouseItem.textureReference].drawOnScreen(mouseItem.position);
                            Game1.spriteBatch.DrawString(Game1.Verdana12, "" + mouseItem.textWeaponName, new Vector2(mouseItem.position.X + 10, mouseItem.position.Y + 10), Color.White);
                        }
                    }
                    break;
                #endregion
                //case states.shop:
                #region
                case states.shop:
                    {
                        textures2D["shopBackground"].drawOnScreen();
                        textures2D["backButton"].drawOnScreen();
                        textures2D["left"].drawOnScreen();
                        textures2D["right"].drawOnScreen();
                        textures2D["buy"].drawOnScreen();
                        textures2D["sell"].drawOnScreen();

                        if (Game1.input.mouse0)
                        {
                            if (textures2D["backButton"].intersectsWithMouseClick())
                            {
                                textures2D["backButtonPressed"].drawOnScreen();
                            }
                            else if (textures2D["left"].intersectsWithMouseClick())
                            {
                                textures2D["leftPressed"].drawOnScreen();
                            }
                            else if (textures2D["right"].intersectsWithMouseClick())
                            {
                                textures2D["rightPressed"].drawOnScreen();
                            }
                            else if (textures2D["buy"].intersectsWithMouseClick())
                            {
                                textures2D["buyPressed"].drawOnScreen();
                            }
                            else if (textures2D["sell"].intersectsWithMouseClick())
                            {
                                textures2D["sellPressed"].drawOnScreen();
                            }
                        }

                        //ShopWeapon
                        Game1.spriteBatch.DrawString(Game1.Verdana12, Game1.config.shopWeapons.Values.ElementAt(itenIndex).textWeaponName, Game1.config.vectors2["itemNameText"], Color.White);
                        Game1.spriteBatch.DrawString(Game1.Verdana12, "$" + Game1.config.shopWeapons.Values.ElementAt(itenIndex).price, Game1.config.vectors2["itemPriceText"], Color.White);
                        Game1.spriteBatch.DrawString(Game1.Verdana12, "$" + Game1.memoryCard.score, Game1.config.vectors2["scoreText"], Color.White);
                        Game1.spriteBatch.DrawString(Game1.Verdana12, "You have: " + Game1.config.shopWeapons.Values.ElementAt(itenIndex).inventoryAmount, Game1.config.vectors2["itemAmountText"], Color.White);
                        textures2D[Game1.config.shopWeapons.Values.ElementAt(itenIndex).textureReference].drawOnScreen(Game1.config.vectors2["itemTexture"]);
                    }
                    break;
                #endregion
                //case states.chooseSector:
                #region
                case states.chooseSector:
                    {
                        textures2D["backButton"].drawOnScreen();

                        for (int i = 0; i < 10; i++)
                        {
                            if ((Game1.memoryCard.missionUnlocked - 1) / 10 >= i)
                            {
                                textures2D["sectorBox"].drawOnScreen(Game1.config.vectors2["sectorBox" + i]);
                                Game1.spriteBatch.DrawString(Game1.quartzMS20, Game1.config.sectors[i + 1], new Vector2(Game1.config.vectors2["sectorBox" + i].X + 10, Game1.config.vectors2["sectorBox" + i].Y + 10), Color.White);
                            }
                            else
                            {
                                textures2D["sectorBoxLocked"].drawOnScreen(Game1.config.vectors2["sectorBox" + i]);
                                Game1.spriteBatch.DrawString(Game1.quartzMS20, Game1.config.sectors[i + 1], new Vector2(Game1.config.vectors2["sectorBox" + i].X + 10, Game1.config.vectors2["sectorBox" + i].Y + 10), Color.Black);
                            }
                        }

                        if (Game1.input.mouse0)
                        {
                            if (textures2D["backButton"].intersectsWithMouseClick())
                            {
                                textures2D["backButtonPressed"].drawOnScreen();
                            }
                        }
                    }
                    break;
                #endregion
                //case states.chooseMisson:
                #region
                case states.chooseMisson:
                    {
                        textures2D["backButton"].drawOnScreen();

                        if (Game1.input.mouse0)
                        {
                            if (textures2D["backButton"].intersectsWithMouseClick())
                            {
                                textures2D["backButtonPressed"].drawOnScreen();
                            }
                        }

                        for (int y = 0; y < 10; y++)
                        {
                            if (y < 5)
                            {
                                if (Game1.memoryCard.missionUnlocked > Convert.ToInt32(Game1.memoryCard.sectorIndex + "" + y))
                                {
                                    textures2D["missionBox"].drawOnScreen(Game1.config.vectors2["missionBox" + (Game1.memoryCard.sectorIndex) + "" + y]);
                                    Game1.spriteBatch.DrawString(Game1.quartzMS20, "" + ((Game1.memoryCard.sectorIndex) * 10 + y + 1), new Vector2(Game1.config.vectors2["missionBox" + (Game1.memoryCard.sectorIndex) + "" + y].X + 10, Game1.config.vectors2["missionBox" + (Game1.memoryCard.sectorIndex) + "" + y].Y + 10), Color.White);
                                }
                                else
                                {
                                    textures2D["missionBoxLocked"].drawOnScreen(Game1.config.vectors2["missionBox" + (Game1.memoryCard.sectorIndex) + "" + y]);
                                    Game1.spriteBatch.DrawString(Game1.quartzMS20, "" + ((Game1.memoryCard.sectorIndex) * 10 + y + 1), new Vector2(Game1.config.vectors2["missionBox" + (Game1.memoryCard.sectorIndex) + "" + y].X + 10, Game1.config.vectors2["missionBox" + (Game1.memoryCard.sectorIndex) + "" + y].Y + 10), Color.Black);
                                }
                            }
                            else
                            {
                                if (Game1.memoryCard.missionUnlocked > Convert.ToInt32(Game1.memoryCard.sectorIndex + "" + y))
                                {
                                    textures2D["missionBox"].drawOnScreen(Game1.config.vectors2["missionBox" + (Game1.memoryCard.sectorIndex) + "" + y]);
                                    Game1.spriteBatch.DrawString(Game1.quartzMS20, "" + ((Game1.memoryCard.sectorIndex) * 10 + y + 1), new Vector2(Game1.config.vectors2["missionBox" + (Game1.memoryCard.sectorIndex) + "" + y].X + 10, Game1.config.vectors2["missionBox" + (Game1.memoryCard.sectorIndex) + "" + y].Y + 10), Color.White);
                                }
                                else
                                {
                                    textures2D["missionBoxLocked"].drawOnScreen(Game1.config.vectors2["missionBox" + (Game1.memoryCard.sectorIndex) + "" + y]);
                                    Game1.spriteBatch.DrawString(Game1.quartzMS20, "" + ((Game1.memoryCard.sectorIndex) * 10 + y + 1), new Vector2(Game1.config.vectors2["missionBox" + (Game1.memoryCard.sectorIndex) + "" + y].X + 10, Game1.config.vectors2["missionBox" + (Game1.memoryCard.sectorIndex) + "" + y].Y + 10), Color.Black);
                                }
                            }
                        }
                    }
                    break;
                #endregion
                //case states.saveGame:
                #region
                case states.saveGame:
                    {
                        textures2D["saveGameBackground"].drawOnScreen();
                        textures2D["yes"].drawOnScreen();
                        textures2D["no"].drawOnScreen();
                        if (Game1.input.mouse0)
                        {
                            if (textures2D["yes"].intersectsWithMouseClick())
                            {
                                textures2D["yesPressed"].drawOnScreen();
                            }
                            else if (textures2D["no"].intersectsWithMouseClick())
                            {
                                textures2D["noPressed"].drawOnScreen();
                            }
                        }
                    }
                    break;
                #endregion
            }
        }

        //Hangar
        #region
        private void drawHealthBar()
        {
            Game1.spriteBatch.DrawString(Game1.Verdana12, "" + health, new Vector2((int)(shipPosition.X) - textures2D["player" + Game1.memoryCard.shipIndex].biggerSide / 2, (int)(shipPosition.Y) - textures2D["player" + Game1.memoryCard.shipIndex].biggerSide / 2 - 35), Color.Lime);
            Game1.spriteBatch.DrawString(Game1.Verdana12, "" + energyShield, new Vector2((int)(shipPosition.X) + textures2D["player" + Game1.memoryCard.shipIndex].biggerSide / 4, (int)(shipPosition.Y) - textures2D["player" + Game1.memoryCard.shipIndex].biggerSide / 2 - 35), Color.CornflowerBlue);

            Game1.spriteBatch.Draw(Game1.voidTexture, new Rectangle((int)(shipPosition.X) - textures2D["player" + Game1.memoryCard.shipIndex].biggerSide / 2 - 1, (int)(shipPosition.Y) - textures2D["player" + Game1.memoryCard.shipIndex].biggerSide / 2 - 16, textures2D["player" + Game1.memoryCard.shipIndex].biggerSide + 2, 4), Color.White);
            Game1.spriteBatch.Draw(Game1.voidTexture, new Rectangle((int)(shipPosition.X) - textures2D["player" + Game1.memoryCard.shipIndex].biggerSide / 2, (int)(shipPosition.Y) - textures2D["player" + Game1.memoryCard.shipIndex].biggerSide / 2 - 15, textures2D["player" + Game1.memoryCard.shipIndex].biggerSide, 2), Color.CornflowerBlue);
            Game1.spriteBatch.Draw(Game1.voidTexture, new Rectangle((int)(shipPosition.X) - textures2D["player" + Game1.memoryCard.shipIndex].biggerSide / 2 - 1, (int)(shipPosition.Y) - textures2D["player" + Game1.memoryCard.shipIndex].biggerSide / 2 - 11, textures2D["player" + Game1.memoryCard.shipIndex].biggerSide + 2, 4), Color.White);
            Game1.spriteBatch.Draw(Game1.voidTexture, new Rectangle((int)(shipPosition.X) - textures2D["player" + Game1.memoryCard.shipIndex].biggerSide / 2, (int)(shipPosition.Y) - textures2D["player" + Game1.memoryCard.shipIndex].biggerSide / 2 - 10, textures2D["player" + Game1.memoryCard.shipIndex].biggerSide, 2), Color.Lime);
        }

        private void setShipAtributes()
        {
            health = (Game1.memoryCard.level * Game1.config.hpPerLevel) + ((Game1.memoryCard.armor - 10) * Game1.config.hpPerArmor);
            energyShield = (Game1.memoryCard.level * Game1.config.spPerLevel) + ((Game1.memoryCard.shieldPower - 9) * Game1.config.spPerPower);

            bonus = 60;
            bonus += Game1.players.playerTypes[Game1.memoryCard.shipIndex].speedBonus;
            bonus += Game1.players.playerTypes[Game1.memoryCard.shipIndex].accelerationBonus;
            bonus += Game1.players.playerTypes[Game1.memoryCard.shipIndex].agilityBonus;
            bonus += Game1.players.playerTypes[Game1.memoryCard.shipIndex].armorBonus;
            bonus += Game1.players.playerTypes[Game1.memoryCard.shipIndex].shieldPowerBonus;
            bonus += Game1.players.playerTypes[Game1.memoryCard.shipIndex].shieldRechargeBonus;

            maximumWeight = Game1.config.baseWeitght + (Game1.memoryCard.level * 100);
            currentWeight = ((textures2D["player" + Game1.memoryCard.shipIndex].width - 1) * (textures2D["player" + Game1.memoryCard.shipIndex].height - 1)) / 10;
            currentWeight += ((Game1.memoryCard.speed + Game1.memoryCard.acceleration + Game1.memoryCard.agility + Game1.memoryCard.armor + Game1.memoryCard.shieldPower + Game1.memoryCard.shieldRecharge) - bonus) * 100;
            for (int i = 0; i < 10; i++)
            {
                //TODO: calcular o peso das armas
            }
            availableWeight = maximumWeight - currentWeight;
            drawPlusButtons = availableWeight >= 100;
        }
        #endregion

        //Equipment
        #region
        private void drawAtributeBar(Vector2 position, int health, int maxHealth, string color)
        {
            int biggerSide = 100;
            Color healthColor = new Color();
            switch (color)
            {
                case "Lime":
                    healthColor = new Color(500 - 500 * health / maxHealth, 500 * health / maxHealth, 0);
                    break;
                case "Red":
                    healthColor = new Color(500 - 500 * (maxHealth - health) / maxHealth, 500 *  (maxHealth - health) / maxHealth, 0);
                    break;
            }
            

            Game1.spriteBatch.Draw(Game1.voidTexture, new Rectangle((int)(position.X) - 1, (int)(position.Y) - 1, biggerSide + 2, 4), Color.White);
            Game1.spriteBatch.Draw(Game1.voidTexture, new Rectangle((int)(position.X), (int)(position.Y), biggerSide, 2), Color.Black);
            Game1.spriteBatch.Draw(Game1.voidTexture, new Rectangle((int)(position.X), (int)(position.Y), biggerSide * health / maxHealth, 2), healthColor);
        }

        private void resetItensPositions()
        {
            int i = 0;
            foreach (KeyValuePair<string, ShopWeapon> shopWeapon in Game1.config.shopWeapons)
            {
                if (shopWeapon.Value.inventoryAmount > 0)
                {
                    shopWeapon.Value.position = new Vector2(102, 173 + i++ * 145);
                }
            }
        }

        private void resetItensPositions(int i)
        {
            i = -i;
            foreach (KeyValuePair<string, ShopWeapon> item in Game1.config.shopWeapons)
            {
                if (item.Value.inventoryAmount > 0)
                {
                    item.Value.position = new Vector2(102, 173 + i++ * 145);
                }
            }
        }
        #endregion
    }
}
