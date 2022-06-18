using SFML.System;
using SFML.Graphics;
using SFML.Window;
using Agar.io.Objects;

namespace Agar.io.Controllers
{
    public class PlayerController : Controller
    {
        public PlayerController(Vector2f targetPosition)
        {
            this.targetPosition = targetPosition;
        }

        public override Vector2f GetDirection(Player player)
        {
            Vector2f input = new Vector2f();

            if (Keyboard.IsKeyPressed(Keyboard.Key.W))
                input.Y = -player.speed;
            if (Keyboard.IsKeyPressed(Keyboard.Key.S))
                input.Y = player.speed;
            if (Keyboard.IsKeyPressed(Keyboard.Key.A))
                input.X = -player.speed;
            if (Keyboard.IsKeyPressed(Keyboard.Key.D))
                input.X = player.speed;

            return input;
        }
    }
}
