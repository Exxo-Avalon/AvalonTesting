using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Tiles;

public class DarkMatter : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(135, 15, 170));
        Main.tileSolid[Type] = true;
        Main.tileBlockLight[Type] = true;
        ItemDrop = Mod.Find<ModItem>("DarkMatterBlock").Type;
        SoundType = SoundID.Tink;
        SoundStyle = 1;
        DustType = ModContent.DustType<Dusts.DarkMatterDust>();
    }

    public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
    {
        if (!fail)
        {
            AvalonTestingWorld.WorldDarkMatterTiles--;
        }
        base.KillTile(i, j, ref fail, ref effectOnly, ref noItem);
    }
}
