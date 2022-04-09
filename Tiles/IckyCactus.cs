using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AvalonTesting.Tiles;

public class IckyCactus : ModCactus
{
    public override Texture2D GetTexture()
    {
        return ModContent.Request<Texture2D>("AvalonTesting/Tiles/IckyCactus").Value;
    }
}
