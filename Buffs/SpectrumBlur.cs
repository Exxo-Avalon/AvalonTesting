using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Buffs;

public class SpectrumBlur : ModBuff
{
    private Asset<Texture2D> animatedTexture;
    public const int FrameCount = 19;
    public const int AnimationSpeed = 60;
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Spectrum Blur");
        Description.SetDefault("10% chance to dodge attacks");
        Main.buffNoTimeDisplay[Type] = true;
        if (Main.netMode != NetmodeID.Server)
        {
            animatedTexture = ModContent.Request<Texture2D>("Avalon/Buffs/SpectrumBlur_Animation");
        }
    }
    public override void Unload()
    {
        animatedTexture = null;
    }
    public override bool PreDraw(SpriteBatch spriteBatch, int buffIndex, ref BuffDrawParams drawParams)
    {
        Rectangle ourSourceRectangle = animatedTexture.Value.Frame(verticalFrames: FrameCount, frameY: (int)Main.GameUpdateCount / 20 % FrameCount);
        drawParams.Texture = animatedTexture.Value;
        drawParams.SourceRectangle = ourSourceRectangle;
        return true;

        //spriteBatch.Draw(drawParams.Texture, drawParams.Position, drawParams.SourceRectangle, drawParams.DrawColor, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        //spriteBatch.Draw(animatedTexture.Value, drawParams.Position, drawParams.SourceRectangle, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB, drawParams.DrawColor.A), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        //return false;
    }
}
