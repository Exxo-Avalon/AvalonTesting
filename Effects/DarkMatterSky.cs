using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Graphics.Effects;

namespace AvalonTesting.Effects;
public class DarkMatterSky : CustomSky
{
    private bool skyActive;
    private float opacity;
    private static int surfaceFrameCounter;
    private static int surfaceFrame;
    
    public override void Activate(Vector2 position, params object[] args)
    {
        skyActive = true;
    }
    public override void Deactivate(params object[] args)
    {
        skyActive = false;
    }
    public override void Reset()
    {
        skyActive = false;
    }
    public override bool IsActive()
    {
        if (!skyActive)
        {
            return opacity > 0f;
        }
        return true;
    }
    public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
    {
        if (!(maxDepth >= float.MaxValue) || !(minDepth < float.MaxValue))
        {
            return;
        }

        spriteBatch.End();
        spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.PointClamp, null, null);
        spriteBatch.Draw(AvalonTesting.DarkMatterSky, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Main.ColorOfTheSkies * opacity);
        if (Main.netMode == NetmodeID.Server) return;
        if (++surfaceFrameCounter > 3)
        {
            surfaceFrame = (surfaceFrame + 1) % 25;
            surfaceFrameCounter = 0;
        }
        int xCenter = Main.screenWidth / 2;
        int yCenter = Main.screenHeight / 2;
        int modifier = (Main.screenWidth - Main.screenHeight) / 2;
        int xPos = (xCenter - (AvalonTesting.DarkMatterBlackHole.Height / 1000) + modifier) / 2;
        int yPos = (yCenter - (AvalonTesting.DarkMatterBlackHole.Height / 1000)) / 2;

        spriteBatch.Draw(AvalonTesting.DarkMatterBlackHole, new Rectangle(xPos, yPos, Main.screenWidth - xPos * 2, Main.screenHeight - yPos * 2), new Color(255, 255, 255, opacity * 10));
        spriteBatch.Draw(AvalonTesting.DarkMatterBackgrounds[surfaceFrame], new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), new Color(255, 255, 255, opacity * 10));
    }
    public override void Update(GameTime gameTime)
    {
        if (!Main.LocalPlayer.GetModPlayer<Players.ExxoBiomePlayer>().ZoneDarkMatter)
        {
            skyActive = false;
        }
        if (skyActive && opacity < 1f)
        {
            opacity += 0.02f;
        }
        else if (!skyActive && opacity > 0f)
        {
            opacity -= 0.02f;
        }
    }
    public override float GetCloudAlpha()
    {
        return (1f - opacity) * 0.97f + 0.03f;
    }
    public override Color OnTileColor(Color inColor)
    {
        return new Color(126, 71, 107);
    }
}
