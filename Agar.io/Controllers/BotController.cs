using SFML.System;
using Agar.io.Utils;
using Agar.io.Objects;

namespace Agar.io.Controllers
{
    public class BotController : Controller
    {
        public BotController(Vector2f targetPosition)
        {
            this.targetPosition = targetPosition;
        }

        public override Vector2f GetDirection(Player player)
        {
            if (player.Position.IsEqual(targetPosition))
            {
                targetPosition = Generator.GetPositionOnGameField(player.Radius);
            }

            Vector2f input = new Vector2f();

            if (targetPosition.Y < player.Position.Y)
                input.Y = -player.speed;
            if (targetPosition.Y > player.Position.Y)
                input.Y = player.speed;
            if (targetPosition.X < player.Position.X)
                input.X = -player.speed;
            if (targetPosition.X > player.Position.X)
                input.X = player.speed;

            return input;
        }

        public override bool IsShot()
        {
            return false;
        }
    }
}