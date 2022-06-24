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

        public virtual bool IsShot()
        {
            return Keyboard.IsKeyPressed(Keyboard.Key.Space);
        }
    }
}
