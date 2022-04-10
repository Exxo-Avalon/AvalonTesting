using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AvalonTesting.Tiles;

public class IckyCactus : ModCactus
{
    public override Texture2D GetTexture()
    {
        return AvalonTesting.Mod.Assets.Request<Texture2D>("Tiles/IckyCactus").Value;
    }
}
