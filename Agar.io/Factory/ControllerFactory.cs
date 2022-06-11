using Agar.io.Objects;
using Agar.io.Utils;
using SFML.System;

namespace Agar.io.Factory
{
    class ControllerFactory
    {
        public static PlayerController CreateController()
        {
            int defaultRadius = 30;
            Vector2f target = Generator.GetPositionOnGameField(defaultRadius);

            return new PlayerController(target);
        }
    }
}
