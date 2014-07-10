using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics.Dynamics;
using FarseerPhysics;
using Microsoft.Xna.Framework;
using FarseerPhysics.Dynamics.Contacts;

namespace Space_Tanker.src
{
    class PlayerShip : GameBody
    {
        //Trigonometria
        private float cateto_adjacente;
        private float cateto_oposto;
        private float hipotenusa;
        private float dy;
        private float dx;
        private Vector3 destination;
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
        internal bool shoot;

        internal PlayerShip(int width, int height, int x, int y)
            : base(width, height, x, y, BodyType.Dynamic)
        {
            //Atualizar calculos em hangar, level up, e EnemyType

            //Física
            body.health = (Game1.memoryCard.level * Game1.config.hpPerLevel) + ((Game1.memoryCard.armor - 10) * Game1.config.hpPerArmor);
            body.maxHealth = body.health;
            body.energyShield = (Game1.memoryCard.level * Game1.config.spPerLevel) + ((Game1.memoryCard.shieldPower - 9) * Game1.config.spPerPower);
            body.maxEnergyShield = body.energyShield;
            shieldRechargeInterval = 100 - Game1.memoryCard.shieldRecharge;
            destination = Vector3.Zero;
            //body.Restitution = 0;
            body.Friction = 0f;

            //Atributos
            angularImpulse = (Game1.memoryCard.agility / 100f) * 0.03f;
            maxAngularVelocity = Game1.memoryCard.agility / 10f;
            maxLinearVelocity = (Game1.memoryCard.speed / 100f) * 3f;
            force = (Game1.memoryCard.acceleration / 100f) * 3f;

            //Weapons
            foreach (KeyValuePair<string, HardPoint> hardPoint in Game1.memoryCard.hardPoints)
            {
                if (!hardPoint.Value.isEmpty)
                {
                    hardPoint.Value.maxAmmoLoaded = hardPoint.Value.amountEquiped * Game1.config.shopWeapons[hardPoint.Value.weaponName].shootsPerRound;
                }
            }

            Game1.memoryCard.energy = 100 * Game1.memoryCard.level;

            for (int i = 0; i < Game1.memoryCard.ammo.Count; i++)
            {
                Game1.memoryCard.ammo.Values.ElementAt(i).count = 5 * Game1.memoryCard.level;
            }

            body.OnCollision += playerBody_OnCollision;
        }

        bool playerBody_OnCollision(Fixture me, Fixture him, Contact contact)
        {
            if (him.Body.IsBullet)
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
                me.Body.health--;
            }

            if (me.Body.health <= 0)
            {
                me.Body.Dispose();
            }

            return true;
        }

        internal void updatePlayer()
        {
            if (body.health > 0)
            {
                if (Game1.memoryCard.energy > 0)
                {
                    rechargeShield();

                    if (Game1.input.click0 || Game1.input.click1)
                    {
                        destination = new Vector3(Game1.input.mouseX, -Game1.input.mouseY, (float)Game1.frameCount);
                        needToMove = true;
                    }

                    if (needToMove)
                    {
                        cateto_adjacente = destination.X - position.X;
                        cateto_oposto = destination.Y - position.Y;
                        hipotenusa = (float)Math.Sqrt((cateto_adjacente * cateto_adjacente) + (cateto_oposto * cateto_oposto));
                    }

                    if (Game1.input.mouse0)
                    {
                        rotation = MathHelper.Pi + (float)-Math.Atan2(position.X - Game1.input.mouseX, position.Y - -Game1.input.mouseY);
                    }
                    else
                    {
                        rotation = MathHelper.Pi + (float)-Math.Atan2(position.X - destination.X, position.Y - destination.Y);
                    }

                    if (Math.Abs(body.AngularVelocity) < maxAngularVelocity && (needToMove || Game1.input.mouse0))
                    {
                        totalRotation = rotation - body.Rotation;
                        while (totalRotation < -MathHelper.Pi) totalRotation += MathHelper.TwoPi;
                        while (totalRotation > MathHelper.Pi) totalRotation -= MathHelper.TwoPi;
                        body.ApplyAngularImpulse(totalRotation * angularImpulse);
                    }

                    if (needToMove)
                    {
                        if (hipotenusa < height || Game1.frameCount - destination.Z > 30)
                        {
                            needToMove = false;
                        }
                        else
                        {
                            if (needToMove && Math.Abs(totalRotation) < MathHelper.PiOver2 || Game1.input.mouse0)
                            {
                                if (Math.Abs(body.LinearVelocity.X) + Math.Abs(body.LinearVelocity.Y) < maxLinearVelocity)
                                {
                                    if (Game1.input.mouse0)
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
                                    Game1.memoryCard.energy--;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                die();
            }
            getPosition();
        }

        internal void updateWeapons()
        {
            foreach (KeyValuePair<string, HardPoint> hardPoint in Game1.memoryCard.hardPoints)
            {
                //Reload
                if (hardPoint.Value.maxAmmoLoaded > 0)
                {
                    if (!hardPoint.Value.needToLoad)
                    {
                        if (hardPoint.Value.ammoLoaded == 0)
                        {
                            if (Game1.config.shopWeapons[hardPoint.Value.weaponName].isEnergyWeapon)
                            {
                                hardPoint.Value.needToLoad = true;
                                hardPoint.Value.lastReloaded = Game1.frameCount;
                            }
                            else if (Game1.memoryCard.ammo[Game1.config.shopWeapons[hardPoint.Value.weaponName].ammoKey].count - hardPoint.Value.maxAmmoLoaded > 0)
                            {
                                hardPoint.Value.needToLoad = true;
                                hardPoint.Value.lastReloaded = Game1.frameCount;
                            }
                        }
                    }
                    if (hardPoint.Value.needToLoad)
                    {
                        if (Game1.frameCount - hardPoint.Value.lastReloaded > Game1.config.shopWeapons[hardPoint.Value.weaponName].reloadTime)
                        {
                            hardPoint.Value.ammoLoaded = hardPoint.Value.maxAmmoLoaded;
                            hardPoint.Value.needToLoad = false;
                            if (!Game1.config.shopWeapons[hardPoint.Value.weaponName].isEnergyWeapon)
                            {
                                Game1.memoryCard.ammo[Game1.config.shopWeapons[hardPoint.Value.weaponName].ammoKey].count -= hardPoint.Value.maxAmmoLoaded;
                            }
                        }
                    }
                }

                //Fire
                if (shoot)
                {
                    if (!hardPoint.Value.isEmpty)
                    {
                        if (hardPoint.Value.ammoLoaded > 0)
                        {
                            if (Game1.memoryCard.energy - Game1.config.shopWeapons[hardPoint.Value.weaponName].energy > 0)
                            {
                                if (hardPoint.Key.Substring(1, 1) == "1")
                                {
                                    if (Game1.frameCount % Game1.config.shootingInterval == 0)
                                    {
                                        fire(hardPoint);
                                    }
                                }
                                else
                                {
                                    if (Game1.frameCount % Game1.config.shootingInterval == Game1.config.shootingInterval / 2)
                                    {
                                        fire(hardPoint);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void fire(KeyValuePair<string, HardPoint> hardPoint)
        {
            dx = (float)-Math.Sin(body.Rotation);
            dy = (float)Math.Cos(body.Rotation);

            double auxX = 0, auxY = 0;
            switch (hardPoint.Value.shipPosition)
            {
                case "l":
                    auxX = -Math.Sin(body.Rotation - MathHelper.PiOver2) * width / 2;
                    auxY = Math.Cos(body.Rotation - MathHelper.PiOver2) * width / 2;
                    break;
                case "c":
                    auxX = 0;
                    auxY = 0;
                    break;
                case "r":
                    auxX = -Math.Sin(body.Rotation + MathHelper.PiOver2) * width / 2;
                    auxY = Math.Cos(body.Rotation + MathHelper.PiOver2) * width / 2;
                    break;
            }

            Shot shot = new Shot(Game1.mission.textures2D[hardPoint.Value.weaponName + "Shot"].width, Game1.mission.textures2D[hardPoint.Value.weaponName + "Shot"].height, (int)(position.X + auxX), (int)(position.Y + auxY), (float)dx, (float)dy, body.Rotation, Game1.config.shopWeapons[hardPoint.Value.weaponName].demage);
            shot.body.LinearVelocity = new Vector2(Game1.config.shotsMaxSpeed * dx, Game1.config.shotsMaxSpeed * dy);
            shot.body.isMyBullet = true;
            shot.body.IgnoreCollisionWith(Game1.mission.playerShip.body);
            Game1.mission.shotList[hardPoint.Value.weaponName].Add(shot);
            Game1.memoryCard.energy -= Game1.config.shopWeapons[hardPoint.Value.weaponName].energy;
            hardPoint.Value.ammoLoaded--;
        }

        internal void rechargeShield()
        {
            if (Game1.frameCount - lastShieldRecharge > shieldRechargeInterval)
            {
                if (body.energyShield + 1 < body.maxEnergyShield)
                {
                    body.energyShield++;
                    Game1.memoryCard.energy--;
                }
                lastShieldRecharge = Game1.frameCount;
            }
        }

        private void die()
        {
            Game1.nextState = Game1.states.mainMenu;
        }

        internal void jump()
        {
            if (Game1.memoryCard.energy >= 10)
            {
                body.ApplyForce(new Vector2((float)-Math.Sin(body.Rotation) * force * 2f, (float)Math.Cos(body.Rotation) * force * 2f), body.Position);
                Game1.memoryCard.energy -= 10;

                if (Math.Abs(body.LinearVelocity.X) + Math.Abs(body.LinearVelocity.Y) > maxLinearVelocity)
                {
                    if (Game1.input.backButtonPressedCount > 90)
                    {
                        Game1.nextState = Game1.states.hangar;
                    }
                }
            }
            else
            {
                body.ApplyForce(new Vector2((float)-Math.Sin(body.Rotation) * force * 2f, (float)Math.Cos(body.Rotation) * force * 2f), body.Position);
                body.health --;

                if (Math.Abs(body.LinearVelocity.X) + Math.Abs(body.LinearVelocity.Y) > maxLinearVelocity)
                {
                    if (Game1.input.backButtonPressedCount > 90)
                    {
                        Game1.nextState = Game1.states.hangar;
                    }
                }
            }
        }
    }
}

