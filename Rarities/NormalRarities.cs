using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting
{
    public class Rarity12 : ModRarity
    {
        public override Color RarityColor => new Color(0, 0, 255);

        public override int GetPrefixedRarity(int offset, float valueMult)
        {
            if (offset > 0)
            {
                return ModContent.RarityType<Rarity13>(); // Make the rarity of items that have this rarity with a positive modifier the higher tier one.
            }

            if (offset < 0)
            {
                return ItemRarityID.Purple;
            }

            return Type; // no 'lower' tier to go to, so return the type of this rarity.
        }
    }

    public class Rarity13 : ModRarity
    {
        public override Color RarityColor => new Color(255, 0, 255);

        public override int GetPrefixedRarity(int offset, float valueMult)
        {
            if (offset > 0)
            { 
                return ModContent.RarityType<Rarity14>(); 
            }

            if (offset < 0)
            {
                return ModContent.RarityType<Rarity12>();
            }

            return Type; 
        }
    }
    public class Rarity14 : ModRarity
    {
        public override Color RarityColor => new Color(27, 132, 47);

        public override int GetPrefixedRarity(int offset, float valueMult)
        {
            if (offset > 0)
            { 
                return ModContent.RarityType<Rarity14>(); // change when more are added!!!
            }

            if (offset < 0)
            {
                return ModContent.RarityType<Rarity13>();
            }

            return Type;
        }
    }
}
