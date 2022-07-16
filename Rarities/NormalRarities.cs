using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting
{
    public class BlueRarity : ModRarity //12
    {
        public override Color RarityColor => new Color(0, 0, 255);

        public override int GetPrefixedRarity(int offset, float valueMult)
        {
            if (offset == 1)
            {
                return ModContent.RarityType<MagentaRarity>(); 
            }

            if (offset > 1)
            {
                return ModContent.RarityType<TealRarity>(); 
            }

            if (offset == -1)
            {
                return ItemRarityID.Purple;
            }

            if (offset < -1)
            {
                return ItemRarityID.Red;
            }

            return Type; // no 'lower' tier to go to, so return the type of this rarity.
        }
    }

    public class MagentaRarity : ModRarity //13
    {
        public override Color RarityColor => new Color(255, 0, 255);

        public override int GetPrefixedRarity(int offset, float valueMult)
        {
            if (offset == 1)
            {
                return ModContent.RarityType<TealRarity>();
            }

            if (offset > 1)
            {
                return ModContent.RarityType<TealRarity>();
            }

            if (offset == -1)
            {
                return ModContent.RarityType<BlueRarity>();
            }

            if (offset < -1)
            {
                return ItemRarityID.Purple;
            }

            return Type; 
        }
    }
    public class TealRarity : ModRarity //14
    {
        public override Color RarityColor => new Color(0, 255, 140);

        public override int GetPrefixedRarity(int offset, float valueMult)
        {
            if (offset == 1)
            {
                return ModContent.RarityType<FireOrangeRarity>();
            }

            if (offset > 1)
            {
                return ModContent.RarityType<YellowGreenRarity>();
            }

            if (offset == -1)
            {
                return ModContent.RarityType<MagentaRarity>();
            }

            if (offset < -1)
            {
                return ModContent.RarityType<BlueRarity>();
            }

            return Type;
        }
    }

    public class FireOrangeRarity : ModRarity //16
    {
        public override Color RarityColor => new Color(255, 80, 0);

        public override int GetPrefixedRarity(int offset, float valueMult)
        {
            if (offset == 1)
            {
                return ModContent.RarityType<YellowGreenRarity>();
            }

            if (offset > 1)
            {
                return ModContent.RarityType<DarkRedRarity>();
            }

            if (offset == -1)
            {
                return ModContent.RarityType<TealRarity>();
            }

            if (offset < -1)
            {
                return ModContent.RarityType<MagentaRarity>();
            }

            return Type;
        }
    }
    public class YellowGreenRarity : ModRarity //17
    {
        public override Color RarityColor => new Color(210, 255, 0);

        public override int GetPrefixedRarity(int offset, float valueMult)
        {
            if (offset == 1)
            {
                return ModContent.RarityType<DarkRedRarity>();
            }

            if (offset > 1)
            {
                return ModContent.RarityType<DarkGreenRarity>();
            }

            if (offset == -1)
            {
                return ModContent.RarityType<FireOrangeRarity>();
            }

            if (offset < -1)
            {
                return ModContent.RarityType<TealRarity>();
            }

            return Type;
        }
    }
    public class DarkRedRarity : ModRarity //18
    {
        public override Color RarityColor => new Color(128, 0, 0);

        public override int GetPrefixedRarity(int offset, float valueMult)
        {
            if (offset == 1)
            {
                return ModContent.RarityType<DarkGreenRarity>();
            }

            if (offset > 1)
            {
                return ModContent.RarityType<BrownRarity>();
            }

            if (offset == -1)
            {
                return ModContent.RarityType<YellowGreenRarity>();
            }

            if (offset < -1)
            {
                return ModContent.RarityType<FireOrangeRarity>();
            }

            return Type;
        }
    }
    public class DarkGreenRarity : ModRarity //19
    {
        public override Color RarityColor => new Color(0, 128, 0);

        public override int GetPrefixedRarity(int offset, float valueMult)
        {
            if (offset == 1)
            {
                return ModContent.RarityType<BrownRarity>();
            }

            if (offset > 1)
            {
                return ModContent.RarityType<BrownRarity>();
            }

            if (offset == -1)
            {
                return ModContent.RarityType<DarkRedRarity>();
            }

            if (offset < -1)
            {
                return ModContent.RarityType<YellowGreenRarity>();
            }

            return Type;
        }
    }
    public class BrownRarity : ModRarity //20
    {
        public override Color RarityColor => new Color(128, 128, 0);

        public override int GetPrefixedRarity(int offset, float valueMult)
        {
            if (offset == 1)
            {
                return ModContent.RarityType<BrownRarity>();
            }

            if (offset > 1)
            {
                return ModContent.RarityType<BrownRarity>();
            }

            if (offset == -1)
            {
                return ModContent.RarityType<DarkGreenRarity>();
            }

            if (offset < -1)
            {
                return ModContent.RarityType<DarkRedRarity>();
            }

            return Type; //remember to update 20 and 19 when adding more
        }
    }
}
