using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using FarseerPhysics;

namespace Space_Tanker.src
{
    internal class GameBody : Entity
    {
        protected Vector2 linearVelocity;
        internal Body body;
        private Color healthColor;
        internal int lastShieldRecharge;
        internal int shieldRechargeInterval;
        

        internal GameBody(int width, int height, int x, int y, BodyType bodyType) : base(width, height, x, y)
        {
            body = BodyFactory.CreateRectangle(Game1.world, ConvertUnits.ToSimUnits((float)width), ConvertUnits.ToSimUnits((float)height), 1f);
            body.BodyType = bodyType;
            body.FixedRotation = false;
            body.SleepingAllowed = true;
            body.LinearDamping = 1f;
            body.AngularDamping = 2f;
            setPosition(x, y);
        }

        internal new void setPosition(float x, float y)
        {
            Game1.needToDraw = true;
            position.X = x;
            position.Y = y;
            body.SetTransform(new Vector2(ConvertUnits.ToSimUnits(x), ConvertUnits.ToSimUnits(y)), 0);
        }

        internal void setPosition(float x, float y, float rotation)
        {
            Game1.needToDraw = true;
            position.X = x;
            position.Y = y;
            body.SetTransform(new Vector2(ConvertUnits.ToSimUnits(x), ConvertUnits.ToSimUnits(y)), rotation);
        }

        internal void getPosition()
        {
            position.X = ConvertUnits.ToDisplayUnits(body.Position.X);
            position.Y = ConvertUnits.ToDisplayUnits(body.Position.Y);
            rotation = body.Rotation;
            linearVelocity = body.LinearVelocity;
        }

        internal void drawHealthBar()
        {
            healthColor = new Color(500 - 500 * body.health / body.maxHealth, 500 * body.health / body.maxHealth, 0);

            Game1.spriteBatch.Draw(Game1.voidTexture, new Rectangle((int)(position.X + Game1.matrix.X) - biggerSide / 2 - 1, (int)(position.Y - Game1.matrix.Y) - height / 2 - 16, biggerSide + 2, 4), Color.White);
            Game1.spriteBatch.Draw(Game1.voidTexture, new Rectangle((int)(position.X + Game1.matrix.X) - biggerSide / 2, (int)(position.Y - Game1.matrix.Y) - height / 2 - 15, biggerSide, 2), Color.Black);
            Game1.spriteBatch.Draw(Game1.voidTexture, new Rectangle((int)(position.X + Game1.matrix.X) - biggerSide / 2, (int)(position.Y - Game1.matrix.Y) - height / 2 - 15, biggerSide * body.energyShield / body.maxEnergyShield, 2), Color.CornflowerBlue);

            Game1.spriteBatch.DrawString(Game1.Verdana12, "" + body.health, new Vector2((int)(position.X + Game1.matrix.X) - biggerSide / 2, (int)(position.Y - Game1.matrix.Y) - height / 2 - 35), healthColor);
            Game1.spriteBatch.DrawString(Game1.Verdana12, "" + body.energyShield, new Vector2((int)(position.X + Game1.matrix.X) + biggerSide / 4, (int)(position.Y - Game1.matrix.Y) - height / 2 - 35), Color.CornflowerBlue);
            Game1.spriteBatch.Draw(Game1.voidTexture, new Rectangle((int)(position.X + Game1.matrix.X) - biggerSide / 2 - 1, (int)(position.Y - Game1.matrix.Y) - height / 2 - 11, biggerSide + 2, 4), Color.White);
            Game1.spriteBatch.Draw(Game1.voidTexture, new Rectangle((int)(position.X + Game1.matrix.X) - biggerSide / 2, (int)(position.Y - Game1.matrix.Y) - height / 2 - 10, biggerSide, 2), Color.Black);
            Game1.spriteBatch.Draw(Game1.voidTexture, new Rectangle((int)(position.X + Game1.matrix.X) - biggerSide / 2, (int)(position.Y - Game1.matrix.Y) - height / 2 - 10, biggerSide * body.health / body.maxHealth, 2), healthColor);            
        }

        internal void unload()
        {
            body.Dispose();
        }
    }
}
