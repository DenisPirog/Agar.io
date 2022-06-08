using SFML.System;
using SFML.Graphics;
using Agar.io.Utils;

namespace Agar.io.Objects
{
    public class Player : GameObject
    {
        public int speed = 1;

        public Player(int radius, Color color, Vector2f position, int outlineThickness)
        {
            Radius = radius;
            FillColor = color;
            Position = position;
            OutlineColor = Color.Black;
            OutlineThickness = outlineThickness;
        }

        public void Update(Vector2f input, Player[] players, Food[] food)
        {
            TryMove(input);
            TryEat(players);
            TryEat(food);
        }

        private void TryMove(Vector2f input)
        {
            Vector2f newPosition = Position + input;

            if (isInBorder(newPosition))
            {
                Position = newPosition;
            }
        }
        
        private bool isInBorder(Vector2f newPosition)
        {
            Vector2u windowSize = Game.GetWindowSize();

            bool inXBorder = newPosition.X <= windowSize.X - Radius * 2 && newPosition.X >= 0;
            bool inYBorder = newPosition.Y <= windowSize.Y - Radius * 2 && newPosition.Y >= 0;

            return inXBorder && inYBorder;
        }

        private void TryEat(GameObject[] gameObjects)
        {          
            foreach (GameObject gameObject in gameObjects)
            {
                if (isCollideWithGameObject(gameObject))
                {
                    if (CanEat(gameObject))
                    {
                        Radius += gameObject.Eat();
                    }
                }
            }   
        }

        private bool CanEat(GameObject objectToEat)
            => Radius - objectToEat.Radius > 0.001;

        private bool isCollideWithGameObject(GameObject gameObject)
            => this.isColliding(gameObject) && gameObject != this && gameObject.isAlive;
    }
}
