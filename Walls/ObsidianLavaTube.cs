using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Walls;

public class ObsidianLavaTube : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        ItemDrop = ModContent.ItemType<ObsidianLavaTubeWall>();
        AddMapEntry(new Color(51, 47, 96));
        DustType = DustID.Obsidian;
    }
}