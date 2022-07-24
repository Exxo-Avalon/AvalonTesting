using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class DarkMatterCactus : ModCactus
{
    public override void SetStaticDefaults() => GrowsOnTileId = new[] { ModContent.TileType<DarkMatterSand>() };

    public override Asset<Texture2D> GetTexture() => Avalon.Mod.Assets.Request<Texture2D>("Tiles/DarkMatterCactus");

    public override Asset<Texture2D> GetFruitTexture() => Asset<Texture2D>.Empty;
}
