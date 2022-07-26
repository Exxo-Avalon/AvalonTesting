using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Rarities;

public class RainbowRarity : ModRarity
{
    public override Color RarityColor
    {
        get
        {
            List<Color> colors = new List<Color>
            {
                new Color(207, 1, 151),
                new Color(252, 131, 151),
                new Color(253, 106, 151),
                new Color(195, 255, 180),
                new Color(66, 255, 228),
                new Color(0, 235, 255),
                new Color(11, 204, 255),
                new Color(64, 144, 250),
                new Color(122, 84, 212),
                new Color(176, 30, 173)
            };
            int numColors = colors.Count;
            float fade = Main.GameUpdateCount % 60 / 60f;
            int index = (int)(Main.GameUpdateCount / 60 % numColors);
            int nextIndex = (index + 1) % numColors;
            return Color.Lerp(colors[index], colors[nextIndex], fade);
        }
    }

    public override int GetPrefixedRarity(int offset, float valueMult)
    {
        return Type; // no 'lower' tier to go to, so return the type of this rarity.
    }
}
