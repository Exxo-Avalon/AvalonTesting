using Avalon.Dusts;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace Avalon.Tiles;

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
    public override int TreeLeaf() => ModContent.Find<ModGore>("AvalonTesting/DarkMatterTreeLeaf").Type;
    public override bool Shake(int x, int y, ref bool createLeaves)
    {
        if (Main.rand.NextBool(10))
        {
            Item.NewItem(WorldGen.GetItemSource_FromTreeShake(x, y), new Vector2(x, y) * 16, ModContent.ItemType<Items.Food.Blackberry>());
            return false;
        }
        return true;
    }
    public override int CreateDust() => ModContent.DustType<DarkMatterWoodDust>();

    public override int SaplingGrowthType(ref int style)
    {
        style = 0;
        return ModContent.TileType<DarkMatterPalmSapling>();
    }
}
