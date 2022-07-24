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
            return new Color(Main.DiscoR + 200, Main.DiscoG + 200, Main.DiscoB + 200);
        }
    }

    public override int GetPrefixedRarity(int offset, float valueMult)
    {
        return Type; // no 'lower' tier to go to, so return the type of this rarity.
    }
}
