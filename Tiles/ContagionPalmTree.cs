using Avalon.Dusts;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace Avalon.Tiles;

public class ContagionPalmTree : ModPalmTree
{
    public override TreePaintingSettings TreeShaderSettings => new();

    public override void SetStaticDefaults() => GrowsOnTileId = new[] { ModContent.TileType<Snotsand>() };

    public override Asset<Texture2D> GetOasisTopTextures() => Asset<Texture2D>.Empty;

    public override Asset<Texture2D> GetTexture() =>
        Avalon.Mod.Assets.Request<Texture2D>("Tiles/ContagionPalmTree");

    public override Asset<Texture2D> GetTopTextures() =>
        Avalon.Mod.Assets.Request<Texture2D>("Tiles/ContagionPalmTreeTop");

    public override int DropWood() => ModContent.ItemType<Items.Placeable.Tile.Coughwood>();

    public override int CreateDust() => ModContent.DustType<CoughwoodDust>();
    public override int TreeLeaf() => ModContent.Find<ModGore>("Avalon/ContagionTreeLeaf").Type;
    public override bool Shake(int x, int y, ref bool createLeaves)
    {
        if (Main.rand.NextBool(10))
        {
            Item.NewItem(WorldGen.GetItemSource_FromTreeShake(x, y), new Vector2(x, y) * 16, ModContent.ItemType<Items.Food.Durian>());
            return false;
        }
        return true;
    }
    public override int SaplingGrowthType(ref int style)
    {
        style = 0;
        return ModContent.TileType<ContagionPalmSapling>();
    }
}
