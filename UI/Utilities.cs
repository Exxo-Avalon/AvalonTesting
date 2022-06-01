using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;

namespace AvalonTesting.UI;

public static class Utilities
{
    public static void DrawBoxedCursorTooltip(SpriteBatch spriteBatch, string text)
    {
        string[] array = Utils.WordwrapString(text, FontAssets.MouseText.Value, 460,
            10, out int lineAmount);
        lineAmount++;
        float num7 = 0f;
        for (int l = 0; l < lineAmount; l++)
        {
            float x = FontAssets.MouseText.Value.MeasureString(array[l]).X;
            if (num7 < x)
            {
                num7 = x;
            }
        }

        if (num7 > 460f)
        {
            num7 = 460f;
        }

        Vector2 vector = new Vector2(Main.mouseX, Main.mouseY) + new Vector2(16f);
        vector += new Vector2(8f, 2f);
        if (vector.Y > Main.screenHeight - (30 * lineAmount))
        {
            vector.Y = Main.screenHeight - (30 * lineAmount);
        }

        if (vector.X > Main.screenWidth - num7)
        {
            vector.X = Main.screenWidth - num7;
        }

        var color = new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor,
            Main.mouseTextColor);
        color = Color.Lerp(color, Color.White, 1f);
        const int num8 = 10;
        const int num9 = 5;
        Utils.DrawInvBG(
            spriteBatch,
            new Rectangle((int)vector.X - num8, (int)vector.Y - num9, (int)num7 + (num8 * 2),
                (30 * lineAmount) + num9 + (num9 / 2)), new Color(23, 25, 81, 255) * 0.925f * 0.85f);
        for (int m = 0; m < lineAmount; m++)
        {
            Utils.DrawBorderStringFourWay(Main.spriteBatch, FontAssets.MouseText.Value, array[m], vector.X,
                vector.Y + (m * 30), color, Color.Black, Vector2.Zero);
        }
    }
}
