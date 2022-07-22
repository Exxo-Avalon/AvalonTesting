using AvalonTesting.Dusts;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace AvalonTesting.Tiles;

public class ContagionTree : ModTree
{
    public override TreePaintingSettings TreeShaderSettings => new();

    public override void SetStaticDefaults() => GrowsOnTileId = new[] { ModContent.TileType<Ickgrass>() };
    public override int TreeLeaf() => ModContent.Find<ModGore>("AvalonTesting/ContagionTreeLeaf").Type;
    public override void SetTreeFoliageSettings(Tile tile, ref int xoffset, ref int treeFrame, ref int floorY, ref int topTextureFrameWidth, ref int topTextureFrameHeight)
    {

    }

    public override Asset<Texture2D> GetTexture() => AvalonTesting.Mod.Assets.Request<Texture2D>("Tiles/ContagionTree");

    public override Asset<Texture2D> GetTopTextures() =>
        AvalonTesting.Mod.Assets.Request<Texture2D>("Tiles/ContagionTreeTop");

    public override Asset<Texture2D> GetBranchTextures() =>
        AvalonTesting.Mod.Assets.Request<Texture2D>("Tiles/ContagionTreeBranches");

    public override bool Shake(int x, int y, ref bool createLeaves)
    {
        if (Main.rand.NextBool(10))
        {
            Item.NewItem(WorldGen.GetItemSource_FromTreeShake(x, y), new Vector2(x, y) * 16, ModContent.ItemType<Items.Food.Durian>());
            return false;
        }
        return true;
    }
    public override int DropWood() => ModContent.ItemType<Items.Placeable.Tile.Coughwood>();

    public override int CreateDust() => ModContent.DustType<CoughwoodDust>();

    public override int SaplingGrowthType(ref int style)
    {
        style = 0;
        return ModContent.TileType<ContagionSapling>();
    }
}
