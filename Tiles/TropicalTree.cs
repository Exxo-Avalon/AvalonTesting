using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace AvalonTesting.Tiles;

public class TropicalTree : ModTree
{
    public override TreePaintingSettings TreeShaderSettings => new();
    public override void SetStaticDefaults() => GrowsOnTileId = new[] { ModContent.TileType<TropicalGrass>() };

    public override void SetTreeFoliageSettings(Tile tile, ref int xoffset, ref int treeFrame, ref int floorY, ref int topTextureFrameWidth, ref int topTextureFrameHeight)
    {
        xoffset = 2;
        topTextureFrameWidth = 118;
        topTextureFrameHeight = 96;
    }

    public override Asset<Texture2D> GetTexture() => AvalonTesting.Mod.Assets.Request<Texture2D>("Tiles/TropicalTree");

    public override Asset<Texture2D> GetBranchTextures() =>
        AvalonTesting.Mod.Assets.Request<Texture2D>("Tiles/TropicalTreeBranches");

    public override Asset<Texture2D> GetTopTextures() =>
        AvalonTesting.Mod.Assets.Request<Texture2D>("Tiles/TropicalTreeTop");

    public override int DropWood() => ModContent.ItemType<Items.Placeable.Tile.BleachedEbony>();

    public override int CreateDust() => 51;
    public override int TreeLeaf() => ModContent.Find<ModGore>("AvalonTesting/TropicsTreeLeaf").Type;
    public override bool Shake(int x, int y, ref bool createLeaves)
    {
        if (Main.rand.NextBool(10))
        {
            if (Main.rand.NextBool(2))
            {
                Item.NewItem(WorldGen.GetItemSource_FromTreeShake(x, y), new Vector2(x, y) * 16, ModContent.ItemType<Items.Food.Mangosteen>());
            }
            else Item.NewItem(WorldGen.GetItemSource_FromTreeShake(x, y), new Vector2(x, y) * 16, ModContent.ItemType<Items.Food.Raspberry>());
            return false;
        }
        return true;
    }
    public override int SaplingGrowthType(ref int style)
    {
        style = 0;
        return ModContent.TileType<TropicalSapling>();
    }
}
