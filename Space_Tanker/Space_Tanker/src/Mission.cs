using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Common.PhysicsLogic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;

namespace Space_Tanker.src
{
    internal class Mission : State
    {
        internal PlayerShip playerShip;
        private Vector2 hudPosition;

        internal Dictionary<string, List<Shot>> shotList;
        internal List<EnemyShip> enemyShipList;

        StringBuilder hudSb;
        private int lastSpawn;
        private int enemyShipCount;

        internal float radians10 = MathHelper.ToRadians(10);
        private int spawnedEnemies;
        private int shootsOnScreen;
        internal bool noEnemiesOnScreen;

        SoundEffect music;
        SoundEffectInstance musicInstance;

        //Explosions
        public ExplosionParticleSystem explosionParticleSystem;
        private bool needToJump;

        internal Mission()
            : base()
        {
            //Enemies
            for (int i = 1; i <= Game1.memoryCard.mission + 1; i++)
            {
                textures2Dlocations.Add("enemy" + (i));
            }

            Game1.enemies.loadMission();

            //Player
            textures2Dlocations.Add("player" + Game1.memoryCard.shipIndex);

            //Weapon Shots
            foreach (KeyValuePair<string, ShopWeapon> shopWeapon in Game1.config.shopWeapons)
            {
                textures2Dlocations.Add(shopWeapon.Key + "Shot");
            }

            textures2Dlocations.Add("smallStars");
            textures2Dlocations.Add("bigStars");

            textures2DlocationsCount = textures2Dlocations.Count;
#if MUSIC
            music = Game1.myContentManager.Load<SoundEffect>("raptor"+ (int)Game1.random.Next(1, 7));
            musicInstance = music.CreateInstance();
            musicInstance.IsLooped = true;
            musicInstance.Play();
#endif
        }

        internal bool load()
        {
            //load textures
            if (textures2DlocationsCount > 0)
            {
                string reference = textures2Dlocations[textures2DlocationsCount - 1];
                textures2D.Add(reference, new Sprite(reference));
                updatePercentLoaded();
                return false;
            }

            Game1.world.Clear();

            //load player
            playerShip = new PlayerShip(textures2D["player" + Game1.memoryCard.shipIndex].width, textures2D["player" + Game1.memoryCard.shipIndex].height, 0, 0);

            //my shot lists
            shotList = new Dictionary<string, List<Shot>>();
            foreach (KeyValuePair<string, ShopWeapon> shopWeapon in Game1.config.shopWeapons)
            {
                shotList.Add(shopWeapon.Key, new List<Shot>());
            }

            //enemies
            enemyShipList = new List<EnemyShip>();

            //heads-up display
            hudSb = new StringBuilder();
            hudPosition = new Vector2(10, 10);

            //spaw
            enemyShipCount = Game1.memoryCard.mission + 1;
            spawnedEnemies = 0;

            explosionParticleSystem = new ExplosionParticleSystem(5);
            explosionParticleSystem.Initialize();
            explosionParticleSystem.LoadContent();

            return true;
        }

        internal void doLogic()
        {
            if (Game1.input.backButtonClick)
            {
                needToJump = !needToJump;
            }
            if (needToJump)
            {
                playerShip.jump();
                Game1.input.backButtonPressedCount++;
            }
            else
            {
                Game1.input.backButtonPressedCount = 0;
            }

            Game1.world.Step(3f / Game1.fps);
            Game1.needToDraw = true;

            playerShip.updatePlayer();
            translateMatrix(playerShip.position.X, playerShip.position.Y);
            playerShip.shoot = false;
            updateEnemies();

            playerShip.updateWeapons();
            updateShots();

            if (playerShip.body.IsDisposed)
            {
                Game1.nextState = Game1.states.mainMenu;
            }

            explosionParticleSystem.Update();
        }

        private void translateMatrix(float x, float y)
        {
            Game1.matrix.X = -x + Game1.display.displayWidthOver2;
            Game1.matrix.Y = y - Game1.display.displayHeightOver2;
        }

        private void updateEnemies()
        {
            noEnemiesOnScreen = true;
            tryToSpawEnemyShip();

            foreach (EnemyShip enemyShip in enemyShipList)
            {
                enemyShip.update();
            }

            for (int i = enemyShipList.Count - 1; i >= 0; i--)
            {
                if (enemyShipList[i].body.IsDisposed)
                {
                    enemyShipList.RemoveAt(i);
                    spawnedEnemies--;
                }
            }

            if (enemyShipCount == 0)
            {
                if (spawnedEnemies == 0)
                {
                    Game1.nextState = Game1.states.hangar;
                    if (Game1.memoryCard.mission + 1 == Game1.memoryCard.missionUnlocked)
                    {
                        Game1.memoryCard.missionUnlocked++;
                    }
                }
            }
        }

        private void updateShots()
        {
            shootsOnScreen = 0;
            foreach (KeyValuePair<string, List<Shot>> list in shotList)
            {

                foreach (Shot shot in list.Value)
                {
                    shot.update();
                    shootsOnScreen++;
                    if (!shot.isOnScreen)
                    {
                        shot.body.Dispose();
                    }
                }
                for (int i = list.Value.Count - 1; i >= 0; i--)
                {
                    if (list.Value[i].body.IsDisposed)
                    {
                        list.Value.RemoveAt(i);
                        shootsOnScreen--;
                    }
                }
            }
        }

        private void tryToSpawEnemyShip()
        {
            if (enemyShipCount > 0)
            {
                if (playerShip.body.health > 0)
                {
                    if (Game1.frameCount - lastSpawn > Game1.config.spawningInterval)
                    {
                        spawnEnemyShip();
                        spawnedEnemies++;
                        enemyShipCount--;
                        lastSpawn = Game1.frameCount;
                    }
                }
            }
        }

        private void spawnEnemyShip()
        {
            int type = Game1.random.Next(1, Game1.memoryCard.mission + 1);
            EnemyShip enemyShip = new EnemyShip(textures2D["enemy" + type].width, textures2D["enemy" + type].height, type);
            enemyShipList.Add(enemyShip);
        }

        internal void draw()
        {
            Game1.spriteBatch.End();

            Game1.spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.LinearWrap, null, Game1.scissorTestRasterizerState, null, Game1.display.camera);
            textures2D["smallStars"].drawStars((int)playerShip.position.X / 4, (int)playerShip.position.Y / 4);
            textures2D["bigStars"].drawStars((int)playerShip.position.X, (int)playerShip.position.Y);
            Game1.spriteBatch.End();

            Game1.spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, Game1.scissorTestRasterizerState, null, Game1.display.camera);

            foreach (KeyValuePair<string, List<Shot>> list in shotList)
            {
                foreach (Shot shot in list.Value)
                {
                    textures2D[list.Key + "Shot"].draw((int)shot.position.X, (int)shot.position.Y, (float)(shot.rotation + MathHelper.Pi));
                }
            }

            

            foreach (EnemyShip enemyShip in enemyShipList)
            {
                textures2D["enemy" + enemyShip.type].draw((int)enemyShip.position.X, (int)enemyShip.position.Y, (float)enemyShip.rotation);
                enemyShip.drawHealthBar();
            }

            textures2D["player" + Game1.memoryCard.shipIndex].draw((int)playerShip.position.X, (int)playerShip.position.Y, (float)playerShip.rotation);
            playerShip.drawHealthBar();

            explosionParticleSystem.Draw();

            drawHUD();


        }

        private void drawHUD()
        {
            hudSb.Clear();

            hudSb.AppendLine("Score:" + Game1.memoryCard.score);
            hudSb.AppendLine("Health:" + playerShip.body.health);
            hudSb.AppendLine("Energy:" + Game1.memoryCard.energy);

            foreach (KeyValuePair<string, HardPoint> hardPoint in Game1.memoryCard.hardPoints)
            {
                if (!hardPoint.Value.isEmpty)
                {
                    if (Game1.config.shopWeapons[hardPoint.Value.weaponName].isEnergyWeapon)
                    {
                        hudSb.AppendLine(Game1.config.shopWeapons[hardPoint.Value.weaponName].textWeaponName + ": -|" + hardPoint.Value.ammoLoaded);
                    }
                    else
                    {
                        hudSb.AppendLine(Game1.config.shopWeapons[hardPoint.Value.weaponName].textWeaponName + ": " + Game1.memoryCard.ammo[Game1.config.shopWeapons[hardPoint.Value.weaponName].ammoKey].count + "|" + hardPoint.Value.ammoLoaded);
                    }
                }
            }

            Game1.spriteBatch.DrawString(Game1.Verdana12, hudSb, hudPosition, Color.Lime);
        }
    }
}
