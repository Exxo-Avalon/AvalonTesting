using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AvalonTesting.Tiles;

public class DarkMatterWood : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(80, 63, 88));
        Main.tileSolid[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileBrick[Type] = true;
        ItemDrop = Mod.Find<ModItem>("DarkMatterWood").Type;
        DustType = ModContent.DustType<Dusts.DarkMatterWoodDust>();
    }
}
