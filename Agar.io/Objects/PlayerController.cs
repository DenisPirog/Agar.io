using SFML.System;
using SFML.Window;
using Agar.io.Utils;

namespace Agar.io.Objects
{
    public class PlayerController
    {
        public Vector2f targetPosition;
        public bool isBot = true;

        public PlayerController(Vector2f targetPosition)
        {
            this.targetPosition = targetPosition;
        }

        public Vector2f GetDirection(Player player)
        {
            Vector2f input = new Vector2f();

            if (!isBot)
            {
                if (Keyboard.IsKeyPressed(Keyboard.Key.W)) input.Y = -player.speed;
                if (Keyboard.IsKeyPressed(Keyboard.Key.S)) input.Y = player.speed;
                if (Keyboard.IsKeyPressed(Keyboard.Key.A)) input.X = -player.speed;
                if (Keyboard.IsKeyPressed(Keyboard.Key.D)) input.X = player.speed;
            }
            else
            {
                if (player.Position.IsEqual(targetPosition))
                {
                    targetPosition = Generator.GetPositionOnGameField(player.Radius);
                }

                if (targetPosition.Y < player.Position.Y) input.Y = -player.speed;
                if (targetPosition.Y > player.Position.Y) input.Y = player.speed;
                if (targetPosition.X < player.Position.X) input.X = -player.speed;
                if (targetPosition.X > player.Position.X) input.X = player.speed;              
            }

            return input;
        }
    }
}
