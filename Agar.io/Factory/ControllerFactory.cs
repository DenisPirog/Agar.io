using Agar.io.Objects;
using Agar.io.Utils;
using SFML.System;

namespace Agar.io.Factory
{
    class ControllerFactory
    {
        public static PlayerController CreateController(float radius)
        {
            Vector2f target = Generator.GetPositionOnGameField(radius);

            return new PlayerController(target);
        }
    }
}
