using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.ResourceSets;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AvalonTesting.Hooks;

public static class ExtraHealth
{
    public static readonly Func<ClassicPlayerResourcesDisplaySet, int> GetUIScreenAnchorX =
        Utilities.CreatePropertyOrFieldReaderDelegate<ClassicPlayerResourcesDisplaySet, int>("UI_ScreenAnchorX");

    public static void OnDrawLife(
        On.Terraria.GameContent.UI.ResourceSets.ClassicPlayerResourcesDisplaySet.orig_DrawLife orig,
        ClassicPlayerResourcesDisplaySet self)
    {
        if (Main.player[Main.myPlayer].statLifeMax <= 500)
        {
            orig(self);
        }
        else
        {
            Player localPlayer = Main.LocalPlayer;
            SpriteBatch spriteBatch = Main.spriteBatch;
            var color = new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor);
            float lifePerHeart = 20f;
            if (localPlayer.ghost)
            {
                return;
            }

            int baseHearts = Main.player[Main.myPlayer].statLifeMax / 20;
            int lifeFruitHearts = (Main.player[Main.myPlayer].statLifeMax - 400) / 5;
            int crystalFruitHearts = (Main.player[Main.myPlayer].statLifeMax - 500) / 5;
            if (lifeFruitHearts < 0)
            {
                lifeFruitHearts = 0;
            }

            if (crystalFruitHearts < 0)
            {
                crystalFruitHearts = 0;
            }

            if (lifeFruitHearts > 0)
            {
                baseHearts = Main.player[Main.myPlayer].statLifeMax / (20 + (lifeFruitHearts / 4));
                lifePerHeart = Main.player[Main.myPlayer].statLifeMax / 20f;
            }

            if (crystalFruitHearts > 0)
            {
                baseHearts = Main.player[Main.myPlayer].statLifeMax / (20 + (lifeFruitHearts / 4));
                lifePerHeart = Main.player[Main.myPlayer].statLifeMax / 20f;
            }

            int num4 = Main.player[Main.myPlayer].statLifeMax2 - Main.player[Main.myPlayer].statLifeMax;
            lifePerHeart += num4 / baseHearts;
            int amountHearts = (int)(Main.player[Main.myPlayer].statLifeMax2 / lifePerHeart);
            if (amountHearts >= 10)
            {
                amountHearts = 10;
            }

            string text =
                $"{Language.GetText("LegacyInterface.0").Value} {localPlayer.statLifeMax2}/{localPlayer.statLifeMax2}";
            string text2 = $"{localPlayer.statLife}/{localPlayer.statLifeMax2}";
            Vector2 vector = FontAssets.MouseText.Value.MeasureString(text);
            Vector2 vector2 = FontAssets.MouseText.Value.MeasureString(text2);
            if (!localPlayer.ghost)
            {
                spriteBatch.DrawString(FontAssets.MouseText.Value, Language.GetText("LegacyInterface.0").Value,
                    new Vector2(500 + (13 * amountHearts) - (vector.X * 0.5f) + GetUIScreenAnchorX(self), 6f), color,
                    0f,
                    default, 1f, SpriteEffects.None, 0f);
                spriteBatch.DrawString(FontAssets.MouseText.Value, text2,
                    new Vector2(500 + (13 * amountHearts) + (vector.X * 0.5f) + GetUIScreenAnchorX(self), 6f), color,
                    0f,
                    new Vector2(vector2.X, 0f), 1f, SpriteEffects.None, 0f);
            }

            for (int i = 1; i < (int)(Main.player[Main.myPlayer].statLifeMax2 / lifePerHeart) + 1; i++)
            {
                float scale = 1f;
                bool filledHeart = false;
                int intensity;
                if (Main.player[Main.myPlayer].statLife >= i * lifePerHeart)
                {
                    intensity = 255;
                    if (Main.player[Main.myPlayer].statLife == i * lifePerHeart)
                    {
                        filledHeart = true;
                    }
                }
                else
                {
                    float num8 = (Main.player[Main.myPlayer].statLife - ((i - 1) * lifePerHeart)) / lifePerHeart;
                    intensity = (int)(30f + (225f * num8));
                    if (intensity < 30)
                    {
                        intensity = 30;
                    }

                    scale = (num8 / 4f) + 0.75f;
                    if (scale < 0.75)
                    {
                        scale = 0.75f;
                    }

                    if (num8 > 0f)
                    {
                        filledHeart = true;
                    }
                }

                if (filledHeart)
                {
                    scale += Main.cursorScale - 1f;
                }

                int num9 = 0;
                int num10 = 0;
                if (i > 10)
                {
                    num9 -= 260;
                    num10 += 26;
                }

                int alpha = (int)((float)intensity * 0.9);
                if (!Main.player[Main.myPlayer].ghost)
                {
                    if (crystalFruitHearts > 0)
                    {
                        spriteBatch.Draw(
                            ModContent.Request<Texture2D>($"{AvalonTesting.AssetPath}Textures/UI/Heart3").Value,
                            new Vector2(
                                500 + (26 * (i - 1)) + num9 + GetUIScreenAnchorX(self) +
                                (TextureAssets.Heart.Width() / 2),
                                32f + ((TextureAssets.Heart.Height() - (TextureAssets.Heart.Height() * scale)) / 2f) +
                                num10 + (TextureAssets.Heart.Height() / 2f)),
                            new Rectangle(0, 0, TextureAssets.Heart.Width(), TextureAssets.Heart.Height()),
                            new Color(intensity, intensity, intensity, alpha), 0f,
                            new Vector2(TextureAssets.Heart.Width() / 2f, TextureAssets.Heart.Height() / 2f), scale,
                            SpriteEffects.None, 0f);
                    }
                    else if (lifeFruitHearts > 0)
                    {
                        lifeFruitHearts--;
                        spriteBatch.Draw(TextureAssets.Heart2.Value,
                            new Vector2(
                                500 + (26 * (i - 1)) + num9 + GetUIScreenAnchorX(self) +
                                (TextureAssets.Heart.Width() / 2),
                                32f + ((TextureAssets.Heart.Height() - (TextureAssets.Heart.Height() * scale)) / 2f) +
                                num10 + (TextureAssets.Heart.Height() / 2f)),
                            new Rectangle(0, 0, TextureAssets.Heart.Width(), TextureAssets.Heart.Height()),
                            new Color(intensity, intensity, intensity, alpha), 0f,
                            new Vector2(TextureAssets.Heart.Width() / 2f, TextureAssets.Heart.Height() / 2f), scale,
                            SpriteEffects.None, 0f);
                    }
                    else
                    {
                        spriteBatch.Draw(TextureAssets.Heart.Value,
                            new Vector2(
                                500 + (26 * (i - 1)) + num9 + GetUIScreenAnchorX(self) +
                                (TextureAssets.Heart.Width() / 2),
                                32f + ((TextureAssets.Heart.Height() - (TextureAssets.Heart.Height() * scale)) / 2f) +
                                num10 + (TextureAssets.Heart.Height() / 2f)),
                            new Rectangle(0, 0, TextureAssets.Heart.Width(), TextureAssets.Heart.Height()),
                            new Color(intensity, intensity, intensity, alpha), 0f,
                            new Vector2(TextureAssets.Heart.Width() / 2f, TextureAssets.Heart.Height() / 2f), scale,
                            SpriteEffects.None, 0f);
                    }
                }
            }
        }
    }
}
