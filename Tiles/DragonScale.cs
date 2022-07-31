using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class DragonScale : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(Color.MediumSpringGreen);
        Main.tileSolid[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileFrameImportant[Type] = true;
        ItemDrop = ModContent.ItemType<Items.Material.DragonScale>();
        DustType = DustID.MagicMirror;
    }
}
