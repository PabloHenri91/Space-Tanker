using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using FarseerPhysics.Dynamics.Contacts;

namespace Space_Tanker.src
{
    internal class Shot : GameBody
    {
        float dx, dy;
        internal bool isOnScreen;

        internal Shot(int width, int height, int x, int y, float dx, float dy, float rotation, int demage)
            : base(width, height, x, y, BodyType.Dynamic)
        {
            this.dx = dx;
            this.dy = dy;
            body.Rotation = rotation;
            this.rotation = rotation;
            body.OnCollision += body_OnCollision;
            body.demage = demage;
            body.IsBullet = true;
            //body.Restitution = 0;
            body.LinearDamping = 0;
        }

        bool body_OnCollision(Fixture me, Fixture him, Contact contact)
        {
            if (him.Body.IsBullet)
            {
                if (him.Body.isMyBullet)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                this.body.Dispose();
                return true;
            }
        }

        internal void update()
        {
            isOnScreen = onScreen();
            if (Math.Abs(body.LinearVelocity.X) + Math.Abs(body.LinearVelocity.Y) < Game1.config.shotsMaxSpeed)
            {
                body.ApplyForce(new Vector2((float)(dx), (float)(dy)), body.Position);
            }
            getPosition();
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

        internal void updateMissile()
        {
            if (Math.Abs(body.LinearVelocity.X) + Math.Abs(body.LinearVelocity.Y) < Game1.config.shotsMaxSpeed)
            {
                dx = (float)-Math.Sin(body.Rotation);
                dy = (float)Math.Cos(body.Rotation);
                body.ApplyForce(new Vector2((float)(dx), (float)(dy)), body.Position);
            }
            getPosition();
        }

        internal void updateLeser()
        {
            if (Math.Abs(body.LinearVelocity.X) + Math.Abs(body.LinearVelocity.Y) < Game1.config.shotsMaxSpeed)
            {
                body.ApplyForce(new Vector2((float)(dx), (float)(dy)), body.Position);
            }
            getPosition();
        }

        internal void updateAutocannon()
        {
            if (Math.Abs(body.LinearVelocity.X) + Math.Abs(body.LinearVelocity.Y) < Game1.config.shotsMaxSpeed)
            {
                body.ApplyForce(new Vector2((float)(dx), (float)(dy)), body.Position);
            }
            getPosition();
        }
    }
}
