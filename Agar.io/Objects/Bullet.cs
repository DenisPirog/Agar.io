using SFML.System;
using SFML.Graphics;
using Agar.io.Interfaces;
using Agar.io.Utils;
using System.Collections.Generic;

namespace Agar.io.Objects
{
    public class Bullet : GameObject, IDrawable, IUpdatable
    {
        private Player owner;
        private Vector2f target;

        private int lifeTime = 0;
        private int maxLifeTime = 100;
        private int damage;

        public Bullet(int radius, Vector2f origin, Player owner, Vector2i target)
        {
            Radius = radius;
            FillColor = owner.FillColor;
            Position = owner.Position;
            Origin = origin;
            OutlineColor = Color.Black;
            OutlineThickness = Radius / 30;

            damage = radius / 2;
            this.owner = owner;
            this.target = (Vector2f)target;
            owner.TakeDamage(radius / 5);
        }

        public void Update(List<GameObject> gameObjects)
        {      
            Move();
            TryHit(gameObjects);
            UpdateLifetime();
        }

        private void Move()
        {
            Position = Position.Lerp(target, 0.03f);  
            
            if (!Position.IsInBorder(Radius))
            {
                Die();
            }
        }

        private void TryHit(List<GameObject> gameObjects)
        {
            foreach(GameObject objectToHit in gameObjects)
            {
                if (this.IsCollideWithAlive(objectToHit) && objectToHit != owner && objectToHit is Player)
                {
                    objectToHit.TakeDamage(damage);
                    Die();
                }
            }
        }

        private void UpdateLifetime()
        {
            lifeTime++;

            if (lifeTime >= maxLifeTime)
            {
                Die();
            }
        }

        public void Draw(RenderWindow window)
        {
            window.Draw(this);
        }
    }
}
