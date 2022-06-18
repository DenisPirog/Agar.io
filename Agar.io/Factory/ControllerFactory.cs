using Agar.io.Controllers;
using Agar.io.Utils;
using SFML.System;

namespace Agar.io.Factory
{
    enum ControllerType
    {
        Human,
        AI,
    }

    class ControllerFactory
    {
        public static PlayerController CreatePlayer()
            => (PlayerController) CreateController(ControllerType.Human);

        public static BotController CreateBot()
            => (BotController)CreateController(ControllerType.AI);

        private static Controller CreateController(ControllerType controllerType)
        {
            int defaultRadius = 30;
            Vector2f target = Generator.GetPositionOnGameField(defaultRadius);

            switch (controllerType)
            {
                case ControllerType.Human : return new PlayerController(target);
                case ControllerType.AI: return new BotController(target);
            }

            throw null;
        }
    }
}
