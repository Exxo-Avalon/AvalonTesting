using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AvalonTesting.Tiles;

public class RedVelCandyCane : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(Color.GreenYellow);
        Main.tileSolid[Type] = true;
        ItemDrop = Mod.Find<ModItem>("RedVelvetCandyCaneBlock").Type;
    }
}
