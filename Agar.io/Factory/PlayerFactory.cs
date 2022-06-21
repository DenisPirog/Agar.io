using Agar.io.Objects;
using Agar.io.Utils;
using Agar.io.Controllers;
using SFML.Graphics;
using SFML.System;

namespace Agar.io.Factory
{
    enum PlayerType
    {
        Player,
        AI,
    }

    class PlayerFactory
    {
        public static Player CreatePlayer(PlayerType playerType)
        {
            int radius = 0;

            switch (AgarioRandom.NextInt(1, 4))
            {
                case 1:
                    radius = 20;
                    break;
                case 2:
                    radius = 30;
                    break;
                case 3:
                    radius = 40;
                    break;
            }

            Vector2f position = Generator.GetPositionOnGameField(radius);

            Color color = Generator.GetRandomColor();

            Controller controller = ControllerFactory.CreateBot();
            if (playerType == PlayerType.Player) controller = ControllerFactory.CreatePlayer();
           
            Player player = new Player(radius, color, position, controller);

            Game.Add(player);
            return player;
        }
    }
}
