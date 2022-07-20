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
        int xCenter = Main.ScreenSize.X / 2; //Main.screenWidth / 2;
        int yCenter = Main.ScreenSize.Y / 2;
        int modifier = (Main.screenWidth - Main.screenHeight) / 2;
        if (modifier < 0) modifier = (Main.screenHeight - Main.screenWidth) / 2;
        var percentage = new Vector2(1920 / Main.ScreenSize.X, 1080 / Main.ScreenSize.Y); // FIX FOR 4k LATER
        int xModifier = (int)(modifier * percentage.X);
        int xPos2 = (xCenter - (AvalonTesting.DarkMatterBlackHole.Value.Width / 100) + xModifier) / 2;
        int yPos2 = (yCenter - (AvalonTesting.DarkMatterBlackHole.Value.Height / 100)) / 2;

        spriteBatch.End();
        spriteBatch.Begin();
        spriteBatch.Draw(AvalonTesting.DarkMatterBlackHole2.Value, new Vector2(xPos2, yPos2), null, new Color(255, 255, 255, 255), 0f, new Vector2(AvalonTesting.DarkMatterBlackHole2.Width() >> 1, AvalonTesting.DarkMatterBlackHole2.Height() >> 1), 0.25f, SpriteEffects.None, 1f);

        spriteBatch.End();
        spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.PointClamp, null, null);
        spriteBatch.Draw(AvalonTesting.DarkMatterBlackHole.Value, new Vector2(xPos2, yPos2), null, Color.White, 0f, new Vector2(AvalonTesting.DarkMatterBlackHole2.Width() >> 1, AvalonTesting.DarkMatterBlackHole2.Height() >> 1), 0.25f, SpriteEffects.None, 1f);
        spriteBatch.Draw(AvalonTesting.DarkMatterBackgrounds[surfaceFrame].Value, new Vector2(xPos2, yPos2), null, Color.White, 0f, new Vector2(AvalonTesting.DarkMatterBackgrounds[surfaceFrame].Width() >> 1, AvalonTesting.DarkMatterBackgrounds[surfaceFrame].Height() >> 1), 6f, SpriteEffects.None, 1f);
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
