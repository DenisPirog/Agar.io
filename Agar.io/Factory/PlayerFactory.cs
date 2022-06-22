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
            int radius = AgarioRandom.NextInt(20, 40);

            Vector2f position = Generator.GetPositionOnGameField(radius);

            Color color = Generator.GetRandomColor();

            Controller controller = new Controller();

            switch (playerType)
            {
                case PlayerType.Player:
                    color = Color.Black;
                    controller = ControllerFactory.CreatePlayer();
                    break;
                case PlayerType.AI:
                    controller = ControllerFactory.CreateBot();
                    break;
            }  
            
            Player player = new Player(radius, color, position, controller);

            Game.Add(player);
            return player;
        }
    }
}
