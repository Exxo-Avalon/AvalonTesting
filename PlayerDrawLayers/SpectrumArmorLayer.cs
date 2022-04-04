using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvalonTesting.Players;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace AvalonTesting.PlayerDrawLayers;

public class SpectrumArmorLayer : PlayerDrawLayer
{
    public override bool IsHeadLayer => true;
    public override Position GetDefaultPosition()
    {
        return new AfterParent(Terraria.DataStructures.PlayerDrawLayers.Head);
    }
    public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
    {
        return true;
    }

    protected override void Draw(ref PlayerDrawSet drawInfo)
    {
        if (drawInfo.shadow != 0f)
        {
            return;
        }
        Player p = drawInfo.drawPlayer;
        Color rb = new Color(Items.Armor.SpectrumHelmet.R, Items.Armor.SpectrumHelmet.G, Items.Armor.SpectrumHelmet.B, drawInfo.colorArmorBody.A);
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
                spriteEffects = (SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically);
            }
            if (!p.dead)
            {
                p.legPosition.Y = 6f;
                p.headPosition.Y = 6f;
                p.bodyPosition.Y = 6f;
            }
        }
        Vector2 vector2 = new Vector2(p.legFrame.Width * 0.5f, p.legFrame.Height * 0.75f);
        Vector2 origin = new Vector2(p.legFrame.Width * 0.5f, p.legFrame.Height * 0.5f);
        Vector2 vector3 = new Vector2(p.legFrame.Width * 0.5f, p.legFrame.Height * 0.4f);
        if (p.head == AvalonTesting.Mod.GetEquipSlot("SpectrumHelmet", EquipType.Head))
        {

            var value = default(DrawData);
            value = new DrawData(ExxoPlayer.spectrumArmorTextures[0].Value, new Vector2((int)(drawInfo.Position.X - Main.screenPosition.X - p.bodyFrame.Width / 2 + p.width / 2), (int)(drawInfo.Position.Y - Main.screenPosition.Y + p.height - p.bodyFrame.Height + 4f)) + p.headPosition + vector3, new Rectangle?(p.bodyFrame), rb, p.headRotation, vector3, 1f, spriteEffects, 0);
            drawInfo.DrawDataCache.Add(value);
        }
        if (p.body == AvalonTesting.Mod.GetEquipSlot("SpectrumBreastplate", EquipType.Body))
        {
            //Vector2 vector = new Vector2((float)(int)(drawInfo.Position.X - Main.screenPosition.X - (float)(drawInfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawInfo.drawPlayer.width / 2)), (float)(int)(drawInfo.Position.Y - Main.screenPosition.Y + (float)drawInfo.drawPlayer.height - (float)drawInfo.drawPlayer.bodyFrame.Height + 4f)) + drawInfo.drawPlayer.bodyPosition + new Vector2((float)(drawInfo.drawPlayer.bodyFrame.Width / 2), (float)(drawInfo.drawPlayer.bodyFrame.Height / 2));
            //Vector2 value = Main.OffsetsPlayerHeadgear[drawInfo.drawPlayer.bodyFrame.Y / drawInfo.drawPlayer.bodyFrame.Height];
            //value.Y -= 2f;
            //vector += value * (float)(-((Enum)drawInfo.playerEffect).HasFlag((Enum)(object)(SpriteEffects)2).ToDirectionInt());
            //float bodyRotation = drawInfo.drawPlayer.bodyRotation;
            //Vector2 val = vector;
            //Vector2 bodyVect = drawInfo.bodyVect;
            //Vector2 compositeOffset_BackArm = GetCompositeOffset_BackArm(ref drawInfo);
            //_ = val + compositeOffset_BackArm;
            //bodyVect += compositeOffset_BackArm;
            //if (!drawInfo.drawPlayer.invis)
            //{
            //    Texture2D value2 = ExxoPlayer.spectrumArmorTextures[1].Value;
            //    DrawData drawData2 = new DrawData(value2, vector, drawInfo.compTorsoFrame, drawInfo.colorArmorBody, bodyRotation, drawInfo.bodyVect, 1f, drawInfo.playerEffect, 0);
            //    drawData2.shader = drawInfo.cBody;
            //    DrawData drawData = drawData2;
            //    Terraria.DataStructures.PlayerDrawLayers.DrawCompositeArmorPiece(ref drawInfo, CompositePlayerDrawContext.Torso, drawData);
            //    drawInfo.DrawDataCache.Add(drawData);
            //}
            Rectangle bodyFrame2 = p.bodyFrame;
            int num55 = drawInfo.armorAdjust;
            bodyFrame2.X += num55;
            bodyFrame2.Width -= num55;
            var dd = new DrawData(ExxoPlayer.spectrumArmorTextures[1].Value, new Vector2((int)(drawInfo.Position.X - Main.screenPosition.X - p.bodyFrame.Width / 2 + p.width / 2) + num55, (int)(drawInfo.Position.Y - Main.screenPosition.Y + p.height - p.bodyFrame.Height + 4f)) + p.bodyPosition + new Vector2(p.bodyFrame.Width / 2, p.bodyFrame.Height / 2), new Rectangle?(bodyFrame2), rb, p.bodyRotation, origin, 1f, spriteEffects, 0);
            drawInfo.DrawDataCache.Add(dd);
        }
        if (p.legs == AvalonTesting.Mod.GetEquipSlot("SpectrumGreaves", EquipType.Legs))
        {
            var value = new DrawData(ExxoPlayer.spectrumArmorTextures[2].Value, new Vector2((int)(drawInfo.Position.X - Main.screenPosition.X - p.legFrame.Width / 2 + p.width / 2), (int)(drawInfo.Position.Y - Main.screenPosition.Y + p.height - p.legFrame.Height + 4f)) + p.legPosition + vector2, new Rectangle?(p.legFrame), rb, p.legRotation, vector2, 1f, spriteEffects, 0);
            drawInfo.DrawDataCache.Add(value);
        }
    }
    private static Vector2 GetCompositeOffset_BackArm(ref PlayerDrawSet drawinfo)
    {
        return new Vector2((float)(6 * ((!((Enum)drawinfo.playerEffect).HasFlag((Enum)(object)(SpriteEffects)1)) ? 1 : (-1))), (float)(2 * ((!((Enum)drawinfo.playerEffect).HasFlag((Enum)(object)(SpriteEffects)2)) ? 1 : (-1))));
    }

    private static Vector2 GetCompositeOffset_FrontArm(ref PlayerDrawSet drawinfo)
    {
        return new Vector2((float)(-5 * ((!((Enum)drawinfo.playerEffect).HasFlag((Enum)(object)(SpriteEffects)1)) ? 1 : (-1))), 0f);
    }
}
