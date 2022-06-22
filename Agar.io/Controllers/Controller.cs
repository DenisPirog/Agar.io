using SFML.System;
using SFML.Window;
using Agar.io.Objects;

namespace Agar.io.Controllers
{
    public class Controller
    {
        public Vector2f targetPosition;

        public virtual Vector2f GetDirection(Player player)
        {
            return new Vector2f(0, 0);
        }

        public virtual Vector2i GetMousePosition()
        {
            return Mouse.GetPosition(Game.game.window);
        }

        public virtual bool isShot()
        {
            return Keyboard.IsKeyPressed(Keyboard.Key.Space);
        }
    }
}
