using System.Collections.Generic;
using SFML.System;
using SFML.Graphics;
using Agar.io.Factory;
using Agar.io.Interfaces;
using Agar.io.Controllers;
using Agar.io.Utils;

namespace Agar.io.Objects
{
    public class Player : GameObject, IUpdatable, IDrawable
    {
        public Controller controller;
        public int speed = 1;

        public Player(int radius, Color color, Vector2f position, Controller controller)
        {
            Radius = radius;
            FillColor = color;
            Position = position;
            OutlineColor = Color.Black;
            OutlineThickness = radius / 30;
            this.controller = controller;
            eatPoints = Radius / 2;
        }

        public void Update(List<GameObject> gameObjects)
        {
            TryMove(controller.GetDirection(this));
            TryShot(controller.GetMousePosition());
            TryEat(gameObjects);
        }

        public void Draw(RenderWindow window)
        {
            window.Draw(this);
        }

        private void TryMove(Vector2f input)
        {
            Vector2f newPosition = Position + input;
            Origin = new Vector2f(Radius, Radius);

            if (newPosition.IsInBorder(Radius))
            {
                Position = newPosition;
            }
        }

        private void TryEat(List<GameObject> gameObjects)
        {          
            foreach (GameObject gameObject in gameObjects)
            {
                if (this.IsCollideWithAlive(gameObject) && this.CanEat(gameObject))
                {
                    Radius += gameObject.Eat();
                }
            }   
        }

        private void TryShot(Vector2i target)
        {
            if (controller.IsShot() && Radius > 20)
            {
                Game.Add(BulletFactory.CreateBullet(this, target));
            }         
        }
    }
}
