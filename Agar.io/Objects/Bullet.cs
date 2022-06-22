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
            TryDie();
        }

        private void Move()
        {
            Position = Position.Lerp(target, 0.03f);        
        }

        private void TryHit(List<GameObject> gameObjects)
        {
            foreach(GameObject objectToHit in gameObjects)
            {
                if (this.IsCollideWithAlive(objectToHit) && objectToHit != owner && objectToHit is Player)
                {
                    objectToHit.TakeDamage(damage);
                    Game.Delete(this);
                }
            }
        }

        private void TryDie()
        {
            lifeTime++;

            if (!Position.IsInBorder(Radius) || lifeTime >= maxLifeTime)
            {
                Game.Delete(this);
            }
        }

        public void Draw(RenderWindow window)
        {
            window.Draw(this);
        }
    }
}
