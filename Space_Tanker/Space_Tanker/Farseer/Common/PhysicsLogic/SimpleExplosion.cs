﻿using System;
using System.Collections.Generic;
using FarseerPhysics.Collision;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;

namespace FarseerPhysics.Common.PhysicsLogic
{
    /// <summary>
    /// Creates a simple explosion that ignores other bodies hiding behind static bodies.
    /// </summary>
    public sealed class SimpleExplosion : PhysicsLogic
    {
        public SimpleExplosion(World world)
            : base(world, PhysicsLogicType.Explosion)
        {
            Power = 1; //linear
        }

        /// <summary>
        /// This is the power used in the power function. A value of 1 means the force
        /// applied to bodies in the explosion is linear. A value of 2 means it is exponential.
        /// </summary>
        public float Power { get; set; }

        /// <summary>
        /// Activate the explosion at the specified shipPosition.
        /// </summary>
        /// <param slotName="pos">The shipPosition (center) of the explosion.</param>
        /// <param slotName="radius">The radius of the explosion.</param>
        /// <param slotName="force">The force applied</param>
        /// <param slotName="maxForce">A maximum amountEquiped of force. When force gets over this value, it will be equal to maxForce</param>
        /// <returns>A list of bodies and the amountEquiped of force that was applied to them.</returns>
        public Dictionary<Body, Vector2> Activate(Vector2 pos, float radius, float force, float maxForce)
        {
            HashSet<Body> affectedBodies = new HashSet<Body>();

            AABB aabb;
            aabb.LowerBound = pos - new Vector2(radius);
            aabb.UpperBound = pos + new Vector2(radius);

            // Query the world for bodies within the radius.
            World.QueryAABB(fixture =>
            {
                if (Vector2.Distance(fixture.Body.Position, pos) <= radius)
                {
                    if (!affectedBodies.Contains(fixture.Body))
                        affectedBodies.Add(fixture.Body);
                }

                return true;
            }, ref aabb);

            return ApplyImpulse(pos, radius, force, maxForce, affectedBodies);
        }

        private Dictionary<Body, Vector2> ApplyImpulse(Vector2 pos, float radius, float force, float maxForce, HashSet<Body> overlappingBodies)
        {
            Dictionary<Body, Vector2> forces = new Dictionary<Body, Vector2>(overlappingBodies.Count);

            foreach (Body overlappingBody in overlappingBodies)
            {
                if (IsActiveOn(overlappingBody))
                {
                    float distance = Vector2.Distance(pos, overlappingBody.Position);
                    float forcePercent = GetPercent(distance, radius);

                    Vector2 forceVector = pos - overlappingBody.Position;
                    forceVector *= 1f / (float)Math.Sqrt(forceVector.X * forceVector.X + forceVector.Y * forceVector.Y);
                    forceVector *= MathHelper.Min(force * forcePercent, maxForce);
                    forceVector *= -1;

                    overlappingBody.ApplyLinearImpulse(forceVector);
                    forces.Add(overlappingBody, forceVector);
                }
            }

            return forces;
        }

        private float GetPercent(float distance, float radius)
        {
            //(1-(distance/radius))^power-1
            float percent = (float)Math.Pow(1 - ((distance - radius) / radius), Power) - 1;

            if (float.IsNaN(percent))
                return 0f;

            return MathHelper.Clamp(percent, 0f, 1f);
        }
    }
}