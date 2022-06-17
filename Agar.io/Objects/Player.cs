using SFML.System;
using SFML.Graphics;
using Agar.io.Controllers;
using Agar.io.Utils;

namespace Agar.io.Objects
{
    public class Player : GameObject
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
        }

        public void UpdatePlayer(Player[] players, Food[] food)
        {
            TryMove(controller.GetDirection(this));
            TryEat(players);
            TryEat(food);
        }

        private void TryMove(Vector2f input)
        {
            Vector2f newPosition = Position + input;

            if (newPosition.IsInBorder(Radius))
            {
                Position = newPosition;
            }
        }

        private void TryEat(GameObject[] gameObjects)
        {          
            foreach (GameObject gameObject in gameObjects)
            {
                if (IsCollideWithGameObject(gameObject) && CanEat(gameObject))
                {
                    Radius += gameObject.Eat();
                }
            }   
        }

        private bool CanEat(GameObject objectToEat)
            => Radius - objectToEat.Radius > 0.001;

        private bool IsCollideWithGameObject(GameObject gameObject)
            => this.IsColliding(gameObject) && gameObject != this && gameObject.isAlive;
    }
}
