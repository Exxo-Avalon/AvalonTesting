using AvalonTesting.Dusts;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace AvalonTesting.Tiles;

public class DarkMatterPalmTree : ModPalmTree
{
    public override TreePaintingSettings TreeShaderSettings => new();

    public override void SetStaticDefaults() => GrowsOnTileId = new[] { ModContent.TileType<DarkMatterSand>() };

    public override Asset<Texture2D> GetOasisTopTextures() => AvalonTesting.Mod.Assets.Request<Texture2D>("Tiles/DarkMatterPalmTreeTopOasis");

    public override Asset<Texture2D> GetTexture() =>
        AvalonTesting.Mod.Assets.Request<Texture2D>("Tiles/DarkMatterPalmTree");

    public override Asset<Texture2D> GetTopTextures() =>
        AvalonTesting.Mod.Assets.Request<Texture2D>("Tiles/DarkMatterPalmTreeTop");

    public override int DropWood() => ModContent.ItemType<Items.Placeable.Tile.ApocalyptusWood>();

    public override int CreateDust() => ModContent.DustType<DarkMatterWoodDust>();

    public override int SaplingGrowthType(ref int style)
    {
        style = 0;
        return ModContent.TileType<DarkMatterPalmSapling>();
    }
}
