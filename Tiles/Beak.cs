using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class Beak : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry((Color.DarkOrange), LanguageManager.Instance.GetText("Beak"));
        Main.tileSolid[Type] = true;
        ItemDrop = ModContent.ItemType<Items.Placeable.Tile.Beak>();
        HitSound = SoundID.NPCHit2;
    }
}
