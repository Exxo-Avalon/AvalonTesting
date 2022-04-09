using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Tiles;

public class SporeBlock : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(133, 235, 38));
        Main.tileSolid[Type] = true;
        ItemDrop = Mod.Find<ModItem>("SporeBlock").Type;
        DustType = DustID.GreenFairy;
    }
}