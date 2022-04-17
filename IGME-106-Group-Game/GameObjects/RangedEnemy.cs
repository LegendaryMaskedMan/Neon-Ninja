﻿using System;
using System.Collections.Generic;
using System.Text;
using IGME106GroupGame.MovementAndAI;
using IGME106GroupGame.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IGME106GroupGame.GameObjects
{
    public class RangedEnemy: GameObject, IEntity
    {
        //Fields
        Random random;
        private int health;
        private bool collidedWithOtherEnemy = false;
        private Vector2 collisionPosition;
        private int fireDelay;

        //Properties
        public int Health { get => health; set => health = value; }
        public int FireDelay => fireDelay;

        //Constructor
        public RangedEnemy (Texture2D sprite, Vector2 startPos, Player player) : 
            base(sprite, startPos)
        {
            movement = new RangedEnemyMovement(5, this, player);
            health = 1;
            random = new Random();
            fireDelay = random.Next(45, 315);
        }

        // Methods
        public override void Update(GameObjectHandler gameObjectHandler)
        {
            movement.Update();
            HandleCollisions(gameObjectHandler);

            if(collidedWithOtherEnemy)
            {
                Vector2 direction = position - collisionPosition;
                if (direction.Length() < 100)
                {
                    direction = position - collisionPosition;
                    direction.Normalize();
                    movement.Vector = direction * (5 / direction.Length());
                }
                else
                {
                    collidedWithOtherEnemy = false;
                    collisionPosition = Vector2.Zero;
                }
            }

            position += movement.Vector;

            fireDelay--;
            //-1 so there's a frame where it actually equals 0 for the handler to check
            if (fireDelay <= -1)
            {
                fireDelay = random.Next(45, 125);
            }
        }

        public override void HandleCollision(GameObject other)
        {
            if(other is Projectile && !((Projectile)other).IsEnemyProjectile)
            {
                health--;
            }

            if (other is IEntity && !(other is Projectile))
            {
                collidedWithOtherEnemy = true;
                collisionPosition = other.Position;
            }
        }
    }
}
