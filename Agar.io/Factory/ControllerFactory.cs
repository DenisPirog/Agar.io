using Agar.io.Controllers;
using Agar.io.Utils;
using SFML.System;

namespace Agar.io.Factory
{
    class ControllerFactory
    {
        public static PlayerController CreatePlayer()
        {
            int defaultRadius = 30;
            Vector2f target = Generator.GetPositionOnGameField(defaultRadius);

            return new PlayerController(target);
        }

        public static BotController CreateBot()
        {
            int defaultRadius = 30;
            Vector2f target = Generator.GetPositionOnGameField(defaultRadius);

            return new BotController(target);
        }
    }
}
