using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace AvalonTesting.Tiles;

public class TropicalTree : ModTree
{
    public override TreePaintingSettings TreeShaderSettings => new();
    public override void SetStaticDefaults() => GrowsOnTileId = new[] { ModContent.TileType<TropicalGrass>() };

    public override void SetTreeFoliageSettings(Tile tile, ref int xoffset, ref int treeFrame, ref int floorY,
                                                ref int topTextureFrameWidth,
                                                ref int topTextureFrameHeight)
    {
        xoffset = 2;
        topTextureFrameWidth = 116;
        topTextureFrameHeight = 96;
    }

    public override Asset<Texture2D> GetTexture() => AvalonTesting.Mod.Assets.Request<Texture2D>("Tiles/TropicalTree");

    public override Asset<Texture2D> GetBranchTextures() =>
        AvalonTesting.Mod.Assets.Request<Texture2D>("Tiles/TropicalTreeBranches");

    public override Asset<Texture2D> GetTopTextures() =>
        AvalonTesting.Mod.Assets.Request<Texture2D>("Tiles/TropicalTreeTop");

    public override int DropWood() => ModContent.ItemType<Items.Placeable.Tile.TropicalWood>();

    public override int CreateDust() => 51;

    public override int SaplingGrowthType(ref int style)
    {
        style = 0;
        return ModContent.TileType<TropicalSapling>();
    }
}
