﻿using System;
using System.Collections.Generic;
using System.Text;
using IGME106GroupGame.MovementAndAI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IGME106GroupGame.GameObjects
{
    class Projectile : GameObject, IEntity
    {
        //Fields
        //private bool canRicochet;
        //private int framesActive;
        private int damage;
        private int health;

        //Properties
        public int Damage { get => damage; set => damage = value; }
        //health is the bullet's pierce
        public int Health { get => health; set => health = value; }
        //public int Speed
        //public bool CanRicochet { get => canRicochet; }
        //public int FramesActive { get => framesActive; }

        public Rectangle CollisionBox => new Rectangle((int)position.X, (int)position.Y, sprite.Width, sprite.Height);

        //Constructor
        public Projectile (Texture2D sprite, Vector2 startPos, Vector2 mousePosition) :
            base(sprite, startPos)
        {
            movement = new ProjectileMovement(25, startPos, mousePosition);
            health = 1;
            damage = 1;
        }
    }
}
