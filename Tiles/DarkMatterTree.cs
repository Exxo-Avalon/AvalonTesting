using AvalonTesting.Dusts;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace AvalonTesting.Tiles;

public class DarkMatterTree : ModTree
{
    public override TreePaintingSettings TreeShaderSettings => new();

    public override void SetStaticDefaults() => GrowsOnTileId = new[] { ModContent.TileType<DarkMatterGrass>() };

    public override void SetTreeFoliageSettings(Tile tile, ref int xoffset, ref int treeFrame, ref int floorY, ref int topTextureFrameWidth, ref int topTextureFrameHeight)
    {

    }

    public override Asset<Texture2D> GetTexture() => AvalonTesting.Mod.Assets.Request<Texture2D>("Tiles/DarkMatterTree");

    public override Asset<Texture2D> GetTopTextures() =>
        AvalonTesting.Mod.Assets.Request<Texture2D>("Tiles/DarkMatterTreeTop");

    public override Asset<Texture2D> GetBranchTextures() =>
        AvalonTesting.Mod.Assets.Request<Texture2D>("Tiles/DarkMatterTreeBranches");

    public override int DropWood() => ModContent.ItemType<Items.Placeable.Tile.DarkMatterWood>();

    public override int CreateDust() => ModContent.DustType<DarkMatterWoodDust>();

    public override int SaplingGrowthType(ref int style)
    {
        style = 0;
        return ModContent.TileType<DarkMatterSapling>();
    }
}
