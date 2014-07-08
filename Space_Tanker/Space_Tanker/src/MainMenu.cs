using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace Space_Tanker.src
{
    class MainMenu : State
    {
        internal enum states { mainMenu, loading, newGame, loadGame, options, help, credits };
        states state;
        states nextState;

        //New Game
        private float radians2 = MathHelper.ToRadians(2f);
        private Vector2 shipPosition;
        private int health;
        private int energyShield;
        private float angle;
        private short shipIndex;
        internal int speed;
        internal int acceleration;
        internal int agility;
        internal int armor;
        internal int shieldPower;
        internal int shieldRecharge;
        internal int maximumWeight;
        internal int currentWeight;
        internal int availableWeight;

        SoundEffect music;
        SoundEffectInstance musicInstance;

        internal MainMenu()
            : base()
        {
            state = states.loading;
            nextState = states.mainMenu;

            //Main Menu
            textures2Dlocations.Add("mainMenuBackground");
            textures2Dlocations.Add("mainMenuTitle");
            textures2Dlocations.Add("mainMenuBox");
            textures2Dlocations.Add("newGame");
            textures2Dlocations.Add("newGamePressed");
            textures2Dlocations.Add("loadGame");
            textures2Dlocations.Add("loadGamePressed");
            textures2Dlocations.Add("options");
            textures2Dlocations.Add("optionsPressed");
            textures2Dlocations.Add("help");
            textures2Dlocations.Add("helpPressed");
            textures2Dlocations.Add("credits");
            textures2Dlocations.Add("creditsPressed");
            textures2Dlocations.Add("quit");
            textures2Dlocations.Add("quitPressed");

            //New Game
            textures2Dlocations.Add("newGameBackground");
            textures2Dlocations.Add("cancel");
            textures2Dlocations.Add("cancelPressed");
            textures2Dlocations.Add("left");
            textures2Dlocations.Add("leftPressed");
            textures2Dlocations.Add("right");
            textures2Dlocations.Add("rightPressed");
            textures2Dlocations.Add("backButton");
            textures2Dlocations.Add("backButtonPressed");

            for (int i = 1; i <= 6; i++)
            {
                textures2Dlocations.Add("player" + i);
            }

            //Credits
            textures2Dlocations.Add("creditsBackground");

            textures2DlocationsCount = textures2Dlocations.Count;

#if MUSIC
            music = Game1.myContentManager.Load<SoundEffect>("mainmenu");
            musicInstance = music.CreateInstance();
            musicInstance.IsLooped = true;
            musicInstance.Play();
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

            //Main Menu
            textures2D["mainMenuTitle"].setPosition(180, 26);
            textures2D["mainMenuBox"].setPosition(305, 135);
            textures2D["loadGame"].setPosition(325, 215);
            textures2D["loadGamePressed"].setPosition(325, 215);
            textures2D["optionsPressed"].setPosition(325, 275);
            textures2D["options"].setPosition(325, 275);
            textures2D["help"].setPosition(325, 335);
            textures2D["helpPressed"].setPosition(325, 335);
            textures2D["credits"].setPosition(325, 395);
            textures2D["creditsPressed"].setPosition(325, 395);
            textures2D["quit"].setPosition(25, 405);
            textures2D["quitPressed"].setPosition(25, 405);

            //New Game
            textures2D["cancel"].setPosition(158, 234);
            textures2D["cancelPressed"].setPosition(158, 234);
            textures2D["left"].setPosition(325, 154);
            textures2D["leftPressed"].setPosition(325, 154);
            textures2D["right"].setPosition(398, 154);
            textures2D["rightPressed"].setPosition(398, 154);
            textures2D["backButton"].setPosition(25, 405);
            textures2D["backButtonPressed"].setPosition(25, 405);

            resetConfigVectors2();

            return true;
        }

        internal void doLogic()
        {
            if (state == nextState)
            {
                switch (state)
                {
                    //case states.mainMenu:
                    #region
                    case states.mainMenu:
                        {
                            if (Game1.input.click0)
                            {
                                if (textures2D["newGame"].intersectsWithMouseClick())
                                {
                                    nextState = states.newGame;
                                }
                                else if (textures2D["loadGame"].intersectsWithMouseClick())
                                {
                                    nextState = states.loadGame;
                                }
                                else if (textures2D["options"].intersectsWithMouseClick())
                                {
                                    nextState = states.options;
                                }
                                else if (textures2D["help"].intersectsWithMouseClick())
                                {
                                    nextState = states.help;
                                }
                                else if (textures2D["credits"].intersectsWithMouseClick())
                                {
                                    nextState = states.credits;
                                }
                                else if (textures2D["quit"].intersectsWithMouseClick())
                                {
                                    Game1.nextState = Game1.states.quit;
                                }
                            }
                        }
                        break;
                    #endregion
                    //case states.newGame:
                    #region
                    case states.newGame:
                        {
                            angle += radians2;
                            textures2D["player" + shipIndex].setAngle(angle);
                            if (Game1.input.click0)
                            {
                                if (textures2D["backButton"].intersectsWithMouseClick())
                                {
                                    nextState = states.mainMenu;
                                    return;
                                }
                                if (textures2D["newGame"].intersectsWithMouseClick())
                                {
                                    Game1.config = new Config();
                                    Game1.memoryCard = new MemoryCard();
                                    Game1.memoryCard.newGame(shipIndex);
                                    Game1.nextState = Game1.states.hangar;
                                    return;
                                }
                                if (textures2D["cancel"].intersectsWithMouseClick())
                                {
                                    nextState = states.mainMenu;
                                    return;
                                }
                                if (textures2D["right"].intersectsWithMouseClick())
                                {
                                    shipIndex++;
                                    if (shipIndex > 6)
                                    {
                                        shipIndex = 1;
                                    }
                                    setNewShipAtributes(shipIndex);
                                    textures2D["player" + shipIndex].setAngle(angle);
                                    return;
                                }
                                if (textures2D["left"].intersectsWithMouseClick())
                                {
                                    shipIndex--;
                                    if (shipIndex < 1)
                                    {
                                        shipIndex = 6;
                                    }
                                    setNewShipAtributes(shipIndex);
                                    textures2D["player" + shipIndex].setAngle(angle);
                                    return;
                                }
                            }
                        }
                        break;
                    #endregion
                    //case states.loadGame:
                    #region
                    case states.loadGame:
                        {
                            if (Game1.memoryCard.loadGame())
                            {
                                Game1.nextState = Game1.states.hangar;
                            }
                            else
                            {
                                nextState = states.newGame;
                            }
                        }
                        break;
                    #endregion
                    //case states.options:
                    #region
                    case states.options:
                        if (Game1.input.click0)
                        {
                            nextState = states.mainMenu;
                        }
                        break;
                    #endregion
                    //case states.help:
                    #region
                    case states.help:
                        if (Game1.input.click0)
                        {
                            nextState = states.mainMenu;
                        }
                        break;
                    #endregion
                    //case states.credits:
                    #region
                    case states.credits:
                        if (Game1.input.click0)
                        {
                            nextState = states.mainMenu;
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
                    //case states.mainMenu:
                    #region
                    case states.mainMenu:
                        {
                            textures2D["newGame"].setPosition(325, 155);
                            textures2D["newGamePressed"].setPosition(325, 155);
                        }
                        break;
                    #endregion
                    //case states.newGame:
                    #region
                    case states.newGame:
                        {
                            angle = 0;
                            textures2D["newGame"].setPosition(158, 154);
                            textures2D["newGamePressed"].setPosition(158, 154);
                            for (int i = 1; i <= 6; i++)
                            {
                                textures2D["player" + i].setPosition(398, 377, angle);
                            }
                            shipIndex = 1;
                            setNewShipAtributes(shipIndex);
                            shipPosition = new Vector2(398, 377);
                        }
                        break;
                    #endregion
                    //case states.loadGame:
                    #region
                    case states.loadGame:
                        {
                            resetConfigVectors2();
                        }
                        break;
                    #endregion
                    //case states.options:
                    #region
                    case states.options:
                        {
                        }
                        break;
                    #endregion
                    //case states.help:
                    #region
                    case states.help:
                        {
                        }
                        break;
                    #endregion
                    //case states.credits:
                    #region
                    case states.credits:
                        {
                        }
                        break;
                    #endregion
                }
                state = nextState;
                Game1.needToDraw = true;
            }
        }

        private static void resetConfigVectors2()
        {
            Game1.config = new Config();
            Game1.memoryCard = new MemoryCard();

            Game1.config.vectors2.Clear();
            Game1.config.vectors2.Add("speed", new Vector2(608, 166));
            Game1.config.vectors2.Add("acceleration", new Vector2(608, 213));
            Game1.config.vectors2.Add("agility", new Vector2(608, 261));
            Game1.config.vectors2.Add("armor", new Vector2(608, 310));
            Game1.config.vectors2.Add("shieldPower", new Vector2(608, 358));
            Game1.config.vectors2.Add("shieldRecharge", new Vector2(608, 406));

            Game1.config.vectors2.Add("maximumWeight", new Vector2(155, 316));
            Game1.config.vectors2.Add("currentWeight", new Vector2(155, 369));
            Game1.config.vectors2.Add("availableWeight", new Vector2(155, 422));

            Game1.config.vectors2.Add("score", new Vector2(360, 221));
            Game1.config.vectors2.Add("level", new Vector2(360, 263));
        }

        internal void draw()
        {
            textures2D["mainMenuBackground"].drawOnScreen();

            switch (state)
            {
                //case states.mainMenu:
                #region
                case states.mainMenu:
                    {
                        textures2D["mainMenuTitle"].drawOnScreen();
                        textures2D["mainMenuBox"].drawOnScreen();
                        textures2D["newGame"].drawOnScreen();
                        textures2D["loadGame"].drawOnScreen();
                        textures2D["options"].drawOnScreen();
                        textures2D["help"].drawOnScreen();
                        textures2D["credits"].drawOnScreen();
                        textures2D["quit"].drawOnScreen();

                        if (Game1.input.mouse0)
                        {
                            if (textures2D["newGame"].intersectsWithMouseClick())
                            {
                                textures2D["newGamePressed"].drawOnScreen();
                                return;
                            }

                            if (textures2D["loadGame"].intersectsWithMouseClick())
                            {
                                textures2D["loadGamePressed"].drawOnScreen();
                                return;
                            }

                            if (textures2D["options"].intersectsWithMouseClick())
                            {
                                textures2D["optionsPressed"].drawOnScreen();
                                return;
                            }

                            if (textures2D["help"].intersectsWithMouseClick())
                            {
                                textures2D["helpPressed"].drawOnScreen();
                                return;
                            }

                            if (textures2D["credits"].intersectsWithMouseClick())
                            {
                                textures2D["creditsPressed"].drawOnScreen();
                                return;
                            }

                            if (textures2D["quit"].intersectsWithMouseClick())
                            {
                                textures2D["quitPressed"].drawOnScreen();
                                return;
                            }
                        }
                    }
                    break;
                #endregion
                //case states.newGame:
                #region
                case states.newGame:
                    {
                        textures2D["newGameBackground"].drawOnScreen();
                        textures2D["mainMenuTitle"].drawOnScreen();
                        textures2D["player" + shipIndex].drawOnScreen2();

                        //Draw HealthBar
                        {
                            Game1.spriteBatch.DrawString(Game1.Verdana12, "" + health, new Vector2((int)(shipPosition.X) - textures2D["player" + shipIndex].biggerSide / 2, (int)(shipPosition.Y) - textures2D["player" + shipIndex].biggerSide / 2 - 35), Color.Lime);
                            Game1.spriteBatch.DrawString(Game1.Verdana12, "" + energyShield, new Vector2((int)(shipPosition.X) + textures2D["player" + shipIndex].biggerSide / 4, (int)(shipPosition.Y) - textures2D["player" + shipIndex].biggerSide / 2 - 35), Color.CornflowerBlue);

                            Game1.spriteBatch.Draw(Game1.voidTexture, new Rectangle((int)(shipPosition.X) - textures2D["player" + shipIndex].biggerSide / 2 - 1, (int)(shipPosition.Y) - textures2D["player" + shipIndex].biggerSide / 2 - 16, textures2D["player" + shipIndex].biggerSide + 2, 4), Color.White);
                            Game1.spriteBatch.Draw(Game1.voidTexture, new Rectangle((int)(shipPosition.X) - textures2D["player" + shipIndex].biggerSide / 2, (int)(shipPosition.Y) - textures2D["player" + shipIndex].biggerSide / 2 - 15, textures2D["player" + shipIndex].biggerSide, 2), Color.CornflowerBlue);
                            Game1.spriteBatch.Draw(Game1.voidTexture, new Rectangle((int)(shipPosition.X) - textures2D["player" + shipIndex].biggerSide / 2 - 1, (int)(shipPosition.Y) - textures2D["player" + shipIndex].biggerSide / 2 - 11, textures2D["player" + shipIndex].biggerSide + 2, 4), Color.White);
                            Game1.spriteBatch.Draw(Game1.voidTexture, new Rectangle((int)(shipPosition.X) - textures2D["player" + shipIndex].biggerSide / 2, (int)(shipPosition.Y) - textures2D["player" + shipIndex].biggerSide / 2 - 10, textures2D["player" + shipIndex].biggerSide, 2), Color.Lime);
                        }

                        textures2D["newGame"].drawOnScreen();
                        textures2D["cancel"].drawOnScreen();
                        textures2D["left"].drawOnScreen();
                        textures2D["right"].drawOnScreen();
                        textures2D["backButton"].drawOnScreen();

                        Game1.spriteBatch.DrawString(Game1.Verdana12, "" + speed, Game1.config.vectors2["speed"], Color.White);
                        Game1.spriteBatch.DrawString(Game1.Verdana12, "" + acceleration, Game1.config.vectors2["acceleration"], Color.White);
                        Game1.spriteBatch.DrawString(Game1.Verdana12, "" + agility, Game1.config.vectors2["agility"], Color.White);
                        Game1.spriteBatch.DrawString(Game1.Verdana12, "" + armor, Game1.config.vectors2["armor"], Color.White);
                        Game1.spriteBatch.DrawString(Game1.Verdana12, "" + shieldPower, Game1.config.vectors2["shieldPower"], Color.White);
                        Game1.spriteBatch.DrawString(Game1.Verdana12, "" + shieldRecharge, Game1.config.vectors2["shieldRecharge"], Color.White);

                        Game1.spriteBatch.DrawString(Game1.Verdana12, "Maximum Weight: " + maximumWeight, Game1.config.vectors2["maximumWeight"], Color.White);
                        Game1.spriteBatch.DrawString(Game1.Verdana12, "Current Weight: " + currentWeight, Game1.config.vectors2["currentWeight"], Color.White);
                        Game1.spriteBatch.DrawString(Game1.Verdana12, "Available Weight: " + availableWeight, Game1.config.vectors2["availableWeight"], Color.White);

                        Game1.spriteBatch.DrawString(Game1.Verdana12, "$10000", Game1.config.vectors2["score"], Color.White);
                        Game1.spriteBatch.DrawString(Game1.Verdana12, "Level: 1", Game1.config.vectors2["level"], Color.White);

                        if (Game1.input.mouse0)
                        {
                            if (textures2D["newGame"].intersectsWithMouseClick())
                            {
                                textures2D["newGamePressed"].drawOnScreen();
                                return;
                            }
                            if (textures2D["cancel"].intersectsWithMouseClick())
                            {
                                textures2D["cancelPressed"].drawOnScreen();
                                return;
                            }
                            if (textures2D["left"].intersectsWithMouseClick())
                            {
                                textures2D["leftPressed"].drawOnScreen();
                                return;
                            }
                            if (textures2D["right"].intersectsWithMouseClick())
                            {
                                textures2D["rightPressed"].drawOnScreen();
                                return;
                            }
                            if (textures2D["backButton"].intersectsWithMouseClick())
                            {
                                textures2D["backButtonPressed"].drawOnScreen();
                                return;
                            }
                        }
                    }
                    break;
                #endregion
                //case states.loadGame:
                #region
                case states.loadGame:
                    {
                    }
                    break;
                #endregion
                //case states.options:
                #region
                case states.options:
                    {
                    }
                    break;
                #endregion
                //case states.help:
                #region
                case states.help:
                    {
                    }
                    break;
                #endregion
                //case states.credits:
                #region
                case states.credits:
                    {
                        textures2D["creditsBackground"].drawOnScreen();
                    }
                    break;
                #endregion
            }
        }

        //New Game
        #region
        private void setNewShipAtributes(short shipIndex)
        {
            speed = 10 + Game1.players.playerTypes[shipIndex].speedBonus;
            acceleration = 10 + Game1.players.playerTypes[shipIndex].accelerationBonus;
            agility = 10 + Game1.players.playerTypes[shipIndex].agilityBonus;
            armor = 10 + Game1.players.playerTypes[shipIndex].armorBonus;
            shieldPower = 10 + Game1.players.playerTypes[shipIndex].shieldPowerBonus;
            shieldRecharge = 10 + Game1.players.playerTypes[shipIndex].shieldRechargeBonus;
            health = Game1.config.hpPerLevel + ((armor - 10) * Game1.config.hpPerArmor);
            energyShield = Game1.config.spPerLevel + ((shieldPower - 9) * Game1.config.spPerPower);

            maximumWeight = Game1.config.baseWeitght;
            currentWeight = (((textures2D["player" + shipIndex].width - 1) * (textures2D["player" + shipIndex].height - 1)) / 10) + Game1.config.shopWeapons.First().Value.weight;
            availableWeight = maximumWeight - currentWeight;
        }
        #endregion
    }
}
