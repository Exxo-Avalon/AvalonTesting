using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace AvalonTesting.NPCs.Utils;

public static class Debug
{
    public static void DrawIndicator(SpriteBatch spriteBatch, Vector2 worldPosition)
    {
        spriteBatch.Draw(AvalonTesting.Mod.Assets.Request<Texture2D>("Sprites/DebugIndicator").Value, worldPosition - Main.screenPosition, Color.White);
    }
}
