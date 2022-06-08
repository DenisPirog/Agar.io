﻿using Agar.io.Objects;
using Agar.io.Utils;
using SFML.Graphics;
using SFML.System;

namespace Agar.io.Factory
{
    class PlayerFactory
    {
        public static Player CreatePlayer()
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

            int outlineThickness = radius / 30;

            return new Player(radius, color, position, outlineThickness);
        }
    }
}