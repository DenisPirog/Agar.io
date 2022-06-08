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

        public void TryMove(Vector2f input)
        {
            Vector2f newPosition = Position + input;

            if (MathExtensions.IsInBorder(newPosition, Radius))
            {
                Position = newPosition;
            }
        }

        public void TryEat(GameObject[] gameObjects)
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
