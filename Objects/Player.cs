using SFML.System;
using SFML.Graphics;
using System;

namespace Agar.io
{
    public class Player : CircleShape
    {
        public bool isAlive = true;
        public Vector2f targetPosition;

        public Player(int radius, Color color, Vector2f position, Vector2f target, int outlineThickness)
        {
            Radius = radius;
            FillColor = color;
            Position = position;
            OutlineColor = Color.Black;
            OutlineThickness = outlineThickness;
            targetPosition = target;
        }

        public void TryMove(Vector2f newPosition)
        {
            if (isInBorder(newPosition)) Position = newPosition;
        }
        
        private bool isInBorder(Vector2f newPosition)
        {
            bool inXBorder = newPosition.X <= Game.width - Radius * 2 && newPosition.X >= 0;
            bool inYBorder = newPosition.Y <= Game.height - Radius * 2 && newPosition.Y >= 0;

            return inXBorder && inYBorder;
        }

        public void TryEat(Player[] players, Food[] food)
        {
            foreach (Player player in players)
            {
                if (isCollideWithAlivePlayer(player))
                {
                    if (CanEatPlayer(player))
                    {
                        Radius += player.Eat();
                        RepelPlayer(player.Radius);
                    }
                }
            }

            foreach (Food meal in food)
            {
                if (isCollideWithFood(meal))
                {
                    Radius += meal.Eat();
                    RepelPlayer(meal.Radius / 2);
                }
            }    
        }

        private bool CanEatPlayer(Player playerToEat)
            => Radius - playerToEat.Radius > 0.001;

        private bool isCollideWithAlivePlayer(Player player)
            => VectorExtensions.isColliding(this, player) && player != this && player.isAlive;

        private bool isCollideWithFood(Food food)
            => food.isAlive && VectorExtensions.isColliding(this, food);

        private void RepelPlayer(float force)
        {
            if (Position.X >= Game.width / 2 && Position.Y >= Game.height) Position += new Vector2f(-force, -force);
            if (Position.X >= Game.width / 2 && Position.Y <= Game.height) Position += new Vector2f(-force, 0);
            if (Position.X <= Game.width / 2 && Position.Y >= Game.height) Position += new Vector2f(0, -force);
        }

        public Vector2f CalculatePath()
        {
            if(VectorExtensions.isPositionsAreEqual(Position, targetPosition))
            {
                targetPosition = Generator.GeneratePosition(Radius);
            }

            if (isInBorder(targetPosition))
            {
                if (targetPosition.X > Position.X) Position += new Vector2f(1, 0);
                if (targetPosition.X < Position.X) Position += new Vector2f(-1, 0);
                if (targetPosition.Y > Position.Y) Position += new Vector2f(0, 1);
                if (targetPosition.Y < Position.Y) Position += new Vector2f(0, -1);
            }

            return Position;
        }

        public float Eat()
        {
            isAlive = false;
            return Radius / 2;
        }
    }
}
