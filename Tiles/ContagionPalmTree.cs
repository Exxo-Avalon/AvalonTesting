using AvalonTesting.Dusts;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace AvalonTesting.Tiles;

public class ContagionPalmTree : ModPalmTree
{
    public override TreePaintingSettings TreeShaderSettings => new();

    public override void SetStaticDefaults() => GrowsOnTileId = new[] { ModContent.TileType<Snotsand>() };

    public override Asset<Texture2D> GetOasisTopTextures() => Asset<Texture2D>.Empty;

    public override Asset<Texture2D> GetOasisBranchTextures() => Asset<Texture2D>.Empty;

    public override Asset<Texture2D> GetBranchTextures() => Asset<Texture2D>.Empty;

    public override Asset<Texture2D> GetTexture() =>
        AvalonTesting.Mod.Assets.Request<Texture2D>("Tiles/ContagionPalmTree");

    public override Asset<Texture2D> GetTopTextures() =>
        AvalonTesting.Mod.Assets.Request<Texture2D>("Tiles/ContagionPalmTreeTop");

    public override int DropWood() => ModContent.ItemType<Items.Placeable.Tile.Coughwood>();

    public override int CreateDust() => ModContent.DustType<CoughwoodDust>();

    public override int SaplingGrowthType(ref int style)
    {
        style = 0;
        return ModContent.TileType<ContagionPalmSapling>();
    }
}
