using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using FarseerPhysics.Dynamics.Contacts;

namespace Space_Tanker.src
{
    internal class EnemyShip : GameBody
    {
        internal int type;

        //Trigonometria
        private float cateto_adjacente;
        private float cateto_oposto;
        private float hipotenusa;
        private float dy;
        private float dx;
        private Vector2 destination;
        private float totalRotation;

        //Auxiliares
        private bool needToMove;
        private float toRadians10 = MathHelper.ToRadians(10f);

        //Física
        private float angularImpulse;
        private float maxAngularVelocity;
        private float maxLinearVelocity;
        private float force;

        //Projéteis
        internal Dictionary<string, int> lastFire;
        private Dictionary<string, EnemyHardPoint> hardPoints;

        //Random Move
        private int lastMove;
        private int movingInterval;
        private int movingTipe;
        private bool isOnScreen;
        private bool mouse0;

        internal EnemyShip(int width, int height, int type)
            : base(width, height, 0, 0, BodyType.Dynamic)
        {
            //Física
            this.type = type;
            body.health = Game1.enemies.enemyTypes[type].health;
            body.maxHealth = body.health;
            body.energyShield = Game1.enemies.enemyTypes[type].energyShield;
            body.maxEnergyShield = body.energyShield;
            shieldRechargeInterval = Game1.enemies.enemyTypes[type].shieldRechargeInterval;
            destination = Vector2.Zero;
            //body.Restitution = 0;
            body.Friction = 0f;

            //Atributos
            angularImpulse = Game1.enemies.enemyTypes[type].angularImpulse;
            maxAngularVelocity = Game1.enemies.enemyTypes[type].maxAngularVelocity;
            maxLinearVelocity = Game1.enemies.enemyTypes[type].maxLinearVelocity;
            force = Game1.enemies.enemyTypes[type].force;

            //projéteis
            lastFire = new Dictionary<string, int>();
            lastFire.Add("l", Game1.frameCount);
            lastFire.Add("c", Game1.frameCount);
            lastFire.Add("r", Game1.frameCount);

            hardPoints = new Dictionary<string, EnemyHardPoint>(Game1.enemies.enemyTypes[type].hardPoints);

            isOnScreen = true;
            while (isOnScreen)
            {
                position.X = Game1.mission.playerShip.position.X + (float)(-Game1.display.displayWidthOver2 * Game1.config.spawningZone + (Game1.random.NextDouble() * (float)Game1.display.displayWidth * Game1.config.spawningZone));
                position.Y = Game1.mission.playerShip.position.Y + (float)(-Game1.display.displayWidthOver2 * Game1.config.spawningZone + (Game1.random.NextDouble() * (float)Game1.display.displayWidth * Game1.config.spawningZone));
                isOnScreen = onScreen();
            }
            
            setPosition(position.X, position.Y);

            body.OnCollision += body_OnCollision;
        }

        private bool onScreen()
        {
            if (position.X > Game1.mission.playerShip.position.X + Game1.display.displayWidthOver2)
            {
                return false;
            }
            if (position.X < Game1.mission.playerShip.position.X - Game1.display.displayWidthOver2)
            {
                return false;
            }

            if (position.Y > Game1.mission.playerShip.position.Y + Game1.display.displayHeightOver2)
            {
                return false;
            }
            if (position.Y < Game1.mission.playerShip.position.Y - Game1.display.displayHeightOver2)
            {
                return false;
            }
            return true;
        }

        bool body_OnCollision(Fixture me, Fixture him, Contact contact)
        {
            if (him.Body.IsBullet)
            {
                if (him.Body.isEnergyBullet)
                {
                    if (him.Body.demage <= me.Body.energyShield)
                    {
                        me.Body.energyShield -= him.Body.demage;
                        return false;
                    }
                    else
                    {
                        me.Body.health -= him.Body.demage;
                    }
                }
                else
                {
                    me.Body.health -= him.Body.demage;
                }

                if (him.Body.isMyBullet)
                {
                    Game1.memoryCard.score += him.Body.demage;
                    if (me.Body.health <= 0)
                    {
                        Game1.memoryCard.score += 5000;
                    }
                }
            }
            else
            {
                me.Body.health--;
            }

            if (me.Body.health <= 0)
            {
                me.Body.Dispose();
            }

            return true;
        }

        internal void update()
        {
            isOnScreen = onScreen();
            tryToFire();
            rechargeShield();
            randomMove();
            getPosition();

            if (!Game1.mission.playerShip.shoot && isOnScreen)
            {
                float rotationAux = MathHelper.Pi + (float)-Math.Atan2(Game1.mission.playerShip.position.X - position.X, Game1.mission.playerShip.position.Y - position.Y);
                totalRotation = rotationAux - Game1.mission.playerShip.body.Rotation;
                while (totalRotation < -MathHelper.Pi) totalRotation += MathHelper.TwoPi;
                while (totalRotation > MathHelper.Pi) totalRotation -= MathHelper.TwoPi;
                if (totalRotation < 0f) totalRotation *= -1f;

                if (totalRotation < Game1.mission.radians10)
                {
                    Game1.mission.playerShip.shoot = true;
                }
            }
        }

        internal void rechargeShield()
        {
            if (Game1.frameCount - lastShieldRecharge > shieldRechargeInterval)
            {
                if (body.energyShield + 1 < body.maxEnergyShield)
                {
                    body.energyShield++;
                }
                lastShieldRecharge = Game1.frameCount;
            }
        }

        internal void tryToFire()
        {
            if (isOnScreen)
            {
                float rotationAux = MathHelper.Pi + (float)-Math.Atan2(position.X - Game1.mission.playerShip.position.X, position.Y - Game1.mission.playerShip.position.Y);
                totalRotation = rotationAux - body.Rotation;
                while (totalRotation < -MathHelper.Pi) totalRotation += MathHelper.TwoPi;
                while (totalRotation > MathHelper.Pi) totalRotation -= MathHelper.TwoPi;
                if (totalRotation < 0f) totalRotation *= -1f;
            }

            foreach (KeyValuePair<string, EnemyHardPoint> hardPoint in hardPoints)
            {
                //Reload
                if (hardPoint.Value.maxAmmoLoaded > 0)
                {
                    if (!hardPoint.Value.needToLoad)
                    {
                        if (hardPoint.Value.ammoLoaded == 0)
                        {
                            hardPoint.Value.needToLoad = true;
                            hardPoint.Value.lastReloaded = Game1.frameCount;
                        }
                    }
                    if (hardPoint.Value.needToLoad)
                    {
                        if (Game1.frameCount - hardPoint.Value.lastReloaded > Game1.config.shopWeapons[hardPoint.Value.weaponName].reloadTime)
                        {
                            hardPoint.Value.ammoLoaded = hardPoint.Value.maxAmmoLoaded;
                            hardPoint.Value.needToLoad = false;
                        }
                    }
                }

                //Fire
                if (isOnScreen)
                {
                    if (!hardPoint.Value.isEmpty)
                    {
                        if (hardPoint.Value.ammoLoaded > 0)
                        {
                            if (totalRotation < Game1.mission.radians10)
                            {
                                if (Game1.frameCount - lastFire[hardPoint.Key] > Game1.config.shootingInterval)
                                {
                                    fire(hardPoint);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void fire(KeyValuePair<string, EnemyHardPoint> hardPoint)
        {
            dx = (float)-Math.Sin(body.Rotation);
            dy = (float)Math.Cos(body.Rotation);

            float auxX = 0, auxY = 0;
            switch (hardPoint.Value.slotName)
            {
                case "l":
                    auxX = (float)-Math.Sin(body.Rotation - MathHelper.PiOver2) * width / 2f;
                    auxY = (float)Math.Cos(body.Rotation - MathHelper.PiOver2) * width / 2f;
                    break;
                case "c":
                    auxX = 0;
                    auxY = 0;
                    break;
                case "r":
                    auxX = (float)-Math.Sin(body.Rotation + MathHelper.PiOver2) * width / 2f;
                    auxY = (float)Math.Cos(body.Rotation + MathHelper.PiOver2) * width / 2f;
                    break;
            }

            Shot shot = new Shot(Game1.mission.textures2D[hardPoint.Value.weaponName + "Shot"].width, Game1.mission.textures2D[hardPoint.Value.weaponName + "Shot"].height, (int)(position.X + auxX), (int)(position.Y + auxY), (float)dx, (float)dy, body.Rotation, Game1.config.shopWeapons[hardPoint.Value.weaponName].demage);
            shot.body.LinearVelocity = new Vector2(Game1.config.shotsMaxSpeed * dx, Game1.config.shotsMaxSpeed * dy);
            shot.body.isEnergyBullet = Game1.config.shopWeapons[hardPoint.Value.weaponName].isEnergyWeapon;
            shot.body.IgnoreCollisionWith(body);
            Game1.mission.shotList[hardPoint.Value.weaponName].Add(shot);
            hardPoint.Value.ammoLoaded--;
            lastFire[hardPoint.Key] = Game1.frameCount;
        }

        private void seekPlayer()
        {
            rotateToPlayer();

            if (Math.Abs(totalRotation) < MathHelper.PiOver2)
            {
                if (Math.Abs(body.LinearVelocity.X) + Math.Abs(body.LinearVelocity.Y) < maxLinearVelocity)
                {
                    dx = (float)-Math.Sin(body.Rotation);
                    dy = (float)Math.Cos(body.Rotation);
                    body.ApplyForce(new Vector2(dx * force, dy * force), body.Position);
                }
            }
        }

        private void rotateToPlayer()
        {
            rotation = MathHelper.Pi + (float)-Math.Atan2(position.X - Game1.mission.playerShip.position.X, position.Y - Game1.mission.playerShip.position.Y);

            if (Math.Abs(body.AngularVelocity) < maxAngularVelocity)
            {
                totalRotation = rotation - body.Rotation;
                while (totalRotation < -MathHelper.Pi) totalRotation += MathHelper.TwoPi;
                while (totalRotation > MathHelper.Pi) totalRotation -= MathHelper.TwoPi;
                body.ApplyAngularImpulse(totalRotation * angularImpulse);
            }
        }

        void randomMove()
        {
            if (isOnScreen)
            {
                Game1.mission.noEnemiesOnScree = false;

                if (Game1.frameCount - lastMove > movingInterval)
                {
                    lastMove = Game1.frameCount;
                    movingInterval = 30 + (int)((float)Game1.random.NextDouble() * 150f);
                    movingTipe = 1 + (int)((float)Game1.random.NextDouble() * 6f);
                    mouse0 = !mouse0;
                }

                switch (movingTipe)
                {
                    case 1:
                    case 2:
                    case 3:
                        {
                            destination = new Vector2(Game1.mission.playerShip.position.X + (float)(-Game1.display.displayWidthOver2 * Game1.config.spawningZone + (Game1.random.NextDouble() * (float)Game1.display.displayWidth * Game1.config.spawningZone)), Game1.mission.playerShip.position.Y + (float)(-Game1.display.displayWidthOver2 * Game1.config.spawningZone + (Game1.random.NextDouble() * (float)Game1.display.displayWidth * Game1.config.spawningZone)));
                            needToMove = true;
                            movingTipe = 0;
                        }
                        break;
                    case 4:
                        {
                            destination = new Vector2(Game1.mission.playerShip.position.X, Game1.mission.playerShip.position.Y);
                            needToMove = true;
                            movingTipe = 0;
                        }
                        break;
                    case 5:
                    case 6:
                        {
                            needToMove = false;
                            movingTipe = 0;
                        }
                        break;
                    default:
                        break;
                }

                if (needToMove)
                {
                    cateto_adjacente = destination.X - position.X;
                    cateto_oposto = destination.Y - position.Y;
                    hipotenusa = (float)Math.Sqrt((cateto_adjacente * cateto_adjacente) + (cateto_oposto * cateto_oposto));
                }

                if (mouse0)
                {
                    rotation = MathHelper.Pi + (float)-Math.Atan2(position.X - Game1.mission.playerShip.position.X, position.Y - Game1.mission.playerShip.position.Y);
                }
                else
                {
                    rotation = MathHelper.Pi + (float)-Math.Atan2(position.X - destination.X, position.Y - destination.Y);
                }

                if (Math.Abs(body.AngularVelocity) < maxAngularVelocity && (needToMove || mouse0))
                {
                    totalRotation = rotation - body.Rotation;
                    while (totalRotation < -MathHelper.Pi) totalRotation += MathHelper.TwoPi;
                    while (totalRotation > MathHelper.Pi) totalRotation -= MathHelper.TwoPi;
                    body.ApplyAngularImpulse(totalRotation * angularImpulse);
                }

                if (needToMove)
                {
                    if (hipotenusa < biggerSide)
                    {
                        needToMove = false;
                    }
                    else
                    {
                        if (needToMove && (Math.Abs(totalRotation) < MathHelper.PiOver2 || mouse0))
                        {
                            if (Math.Abs(body.LinearVelocity.X) + Math.Abs(body.LinearVelocity.Y) < maxLinearVelocity)
                            {
                                if (mouse0)
                                {
                                    dy = cateto_oposto / hipotenusa; //seno
                                    dx = cateto_adjacente / hipotenusa; //cosseno
                                }
                                else
                                {
                                    dx = (float)-Math.Sin(body.Rotation);
                                    dy = (float)Math.Cos(body.Rotation);
                                }
                                body.ApplyForce(new Vector2(dx * force, dy * force), body.Position);
                            }
                        }
                    }
                }
            }
            else if (Game1.mission.noEnemiesOnScree)
            {
                seekPlayer();
            }
        }
    }
}
