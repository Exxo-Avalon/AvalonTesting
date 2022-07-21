using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Graphics.Effects;
using ReLogic.Content;
using Terraria.GameContent;

namespace AvalonTesting.Effects;
public class DarkMatterSky : CustomSky
{
    private bool skyActive;
    private float opacity;
    private static int surfaceFrameCounter;
    private static int surfaceFrame;
    private static int blackHoleCounter;
    private static int blackHoleFrame;
    
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
        // End the spritebatch and begin again to draw with transparency and non-blurry scaling
        spriteBatch.End();
        spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.PointClamp, null, null);

        // Draw the sky texture
        spriteBatch.Draw(AvalonTesting.DarkMatterSky, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), new Color(51, 41, 48) * opacity); // Main.ColorOfTheSkies

        if (Main.netMode == NetmodeID.Server) return;

        // Surface frame counter
        if (++surfaceFrameCounter > 2)
        {
            surfaceFrame = (surfaceFrame + 1) % 50;
            surfaceFrameCounter = 0;
        }

        // Black hole pulse timer
        if (++blackHoleCounter > 2)
        {
            blackHoleFrame = (blackHoleFrame + 1) % 10;
            blackHoleCounter = 0;
        }

        // Math to allow the pulsing to work
        float scaleMod = (float)((blackHoleFrame % 5) - 2) * 0.0003f;
        if ((blackHoleFrame >= 5 && blackHoleFrame <= 9)) scaleMod *= -1;
        
        // Find the center of the screen
        int xCenter = Main.ScreenSize.X / 2;
        int yCenter = Main.ScreenSize.Y / 2;

        // Modifier to allow the black hole to draw in the same place no matter your resolution
        int modifier = (Main.screenWidth - Main.screenHeight) / 2;

        // If the your game screen's height is greater than your game screen's width, swap the modifier
        if (modifier < 0) modifier = (Main.screenHeight - Main.screenWidth) / 2;

        // Create a variable for the percentage to scale the textures by
        var percentage = new Vector2(1920 / Main.ScreenSize.X, 1080 / Main.ScreenSize.Y); // FIX FOR 4k LATER 3840x2160

        // Create the modifier for the X coordinate of the black hole
        int xModifier = (int)(modifier * percentage.X);

        // Assign positions for the black hole and spiral clouds
        int xPos = (xCenter - (AvalonTesting.DarkMatterBlackHole.Value.Width / 100) + xModifier) / 2;
        int yPos = (yCenter - (AvalonTesting.DarkMatterBlackHole.Value.Height / 100)) / 2;

        // Redraw stars that are in the background
        Vector2 origin = default;
        Vector2 position = default;
        for (int i = 0; i < Main.star.Length; i++)
        {
            Star star = Main.star[i];
            if (star != null)
            {
                Texture2D starTex = TextureAssets.Star[star.type].Value;
                origin = new Vector2(starTex.Width * 0.5f, starTex.Height * 0.5f);
                int bgTop = (int)((double)(0f - Main.screenPosition.Y) / (Main.worldSurface * 16.0 - 600.0) * 200.0);
                float posX = star.position.X * ((float)Main.screenWidth / 800f);
                float posY = star.position.Y * ((float)Main.screenHeight / 600f);
                position = new Vector2(posX + origin.X, posY + origin.Y + bgTop);
                spriteBatch.Draw(starTex, position, new Rectangle(0, 0, starTex.Width, starTex.Height), Color.White * star.twinkle * 0.952f * opacity, star.rotation, origin, star.scale * star.twinkle, SpriteEffects.None, 0f);
            }
        }

        // End the spritebatch and begin again to allow for drawing the black hole center without transparency
        spriteBatch.End();
        spriteBatch.Begin();

        // Draw the black hole's center
        spriteBatch.Draw(AvalonTesting.DarkMatterBlackHole2.Value, new Vector2(xPos, yPos), null, new Color(255, 255, 255, 255), 0f, new Vector2(AvalonTesting.DarkMatterBlackHole2.Width() >> 1, AvalonTesting.DarkMatterBlackHole2.Height() >> 1), 0.25f + scaleMod, SpriteEffects.None, 1f);

        // Draw the floating rocks
        spriteBatch.Draw(AvalonTesting.DarkMatterFloatingRocks.Value, new Vector2(xPos, yPos + 200), null, new Color(255, 255, 255, 255), 0f, new Vector2(AvalonTesting.DarkMatterFloatingRocks.Width() >> 1, AvalonTesting.DarkMatterFloatingRocks.Height() >> 1), 1f, SpriteEffects.None, 1f);
        spriteBatch.Draw(AvalonTesting.DarkMatterFloatingRocks.Value, new Vector2(xPos + AvalonTesting.DarkMatterFloatingRocks.Value.Width, yPos + 200), null, new Color(255, 255, 255, 255), 0f, new Vector2(AvalonTesting.DarkMatterFloatingRocks.Width() >> 1, AvalonTesting.DarkMatterFloatingRocks.Height() >> 1), 1f, SpriteEffects.None, 1f);

        // End and begin again, allowing transparency and non-blurry scaling
        spriteBatch.End();
        spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.PointClamp, null, null);

        // Draw the black hole
        spriteBatch.Draw(AvalonTesting.DarkMatterBlackHole.Value, new Vector2(xPos, yPos), null, Color.White, 0f, new Vector2(AvalonTesting.DarkMatterBlackHole2.Width() >> 1, AvalonTesting.DarkMatterBlackHole2.Height() >> 1), 0.25f + scaleMod, SpriteEffects.None, 1f);

        // Draw the spiral clouds
        spriteBatch.Draw(AvalonTesting.DarkMatterBackgrounds[surfaceFrame].Value, new Vector2(xPos, yPos), null, new Color(255, 255, 255, 255), 0f, new Vector2(AvalonTesting.DarkMatterBackgrounds[surfaceFrame].Width() >> 1, AvalonTesting.DarkMatterBackgrounds[surfaceFrame].Height() >> 1), 3f, SpriteEffects.None, 1f);
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
