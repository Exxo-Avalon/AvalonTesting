using System;
using Avalon.Items.Armor;
using Avalon.Players;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Avalon.PlayerDrawLayers;

public class SpectrumArmorHeadLayer : PlayerDrawLayer
{
    public override bool IsHeadLayer => true;

    public override Position GetDefaultPosition() => new AfterParent(Terraria.DataStructures.PlayerDrawLayers.Head);

    public override bool GetDefaultVisibility(PlayerDrawSet drawInfo) => true;

    protected override void Draw(ref PlayerDrawSet drawInfo)
    {
        if (drawInfo.shadow != 0f)
        {
            return;
        }

        Player p = drawInfo.drawPlayer;
        var rb = new Color(SpectrumHelmet.R, SpectrumHelmet.G, SpectrumHelmet.B, drawInfo.colorArmorBody.A * (1 - drawInfo.drawPlayer.immuneAlpha));
        SpriteEffects spriteEffects = SpriteEffects.None;
        if (p.gravDir == 1f)
        {
            if (p.direction == 1)
            {
                spriteEffects = SpriteEffects.None;
            }
            else
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }

            if (!p.dead)
            {
                p.legPosition.Y = 0f;
                p.headPosition.Y = 0f;
                p.bodyPosition.Y = 0f;
            }
        }
        else
        {
            if (p.direction == 1)
            {
                spriteEffects = SpriteEffects.FlipVertically;
            }
            else
            {
                spriteEffects = SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically;
            }

            if (!p.dead)
            {
                p.legPosition.Y = 6f;
                p.headPosition.Y = 6f;
                p.bodyPosition.Y = 6f;
            }
        }
        var vector3 = new Vector2(p.legFrame.Width * 0.5f, p.legFrame.Height * 0.4f);
        if (p.head == EquipLoader.GetEquipSlot(Avalon.Mod, "SpectrumHelmet", EquipType.Head))
        {
            var value = new DrawData(Mod.Assets.Request<Texture2D>("Items/Armor/SpectrumHelmet_Glow_Head").Value,
                new Vector2(
                    (int)(drawInfo.Position.X - Main.screenPosition.X - (p.bodyFrame.Width / 2) + (p.width / 2)),
                    (int)(drawInfo.Position.Y - Main.screenPosition.Y + p.height - p.bodyFrame.Height + 4f)) +
                p.headPosition + vector3, p.bodyFrame, rb, p.headRotation, vector3, 1f, spriteEffects, 0);
            value.shader = drawInfo.cHead;
            drawInfo.DrawDataCache.Add(value);
        }
    }
}
