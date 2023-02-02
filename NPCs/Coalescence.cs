using Avalon.Items.Banners;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace Avalon.NPCs;

public class Coalescence : ModNPC
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Coalescence");
    }
    public override void SetDefaults()
    {
        NPC.damage = 90;
        NPC.lifeMax = 50;
        NPC.defense = 50;
        NPC.Size = new Vector2(38, 36);
        NPC.aiStyle = -1;
        NPC.scale = 1.2f;
        NPC.value = 1000f;
        NPC.knockBackResist = 0f;
        NPC.HitSound = SoundID.NPCHit1;
        NPC.DeathSound = SoundID.NPCDeath1;
        NPC.noGravity = true;
        //Banner = NPC.type;
        //BannerItem = ModContent.ItemType<SilverSlimeBanner>();
    }
    public float playerDir;
    public float upOrDown;
    public override void AI()
    {
        Player player = Main.player[NPC.FindClosestPlayer()];
        playerDir = player.Center.X - NPC.Center.X;
        upOrDown = NPC.Center.Y - player.Center.Y;

        playerDir = Math.Sign(playerDir);

        NPC.spriteDirection = NPC.direction;
        NPC.TargetClosest(false);

        NPC.rotation = NPC.velocity.X / 20;

        NPC.velocity += (Vector2.Normalize(player.Center - NPC.Center) * 0.5f);

        NPC.ai[0]++;

        if (NPC.ai[0] > 30)
        {
            NPC.velocity = NPC.velocity.RotatedByRandom(MathHelper.PiOver2);
            NPC.ai[0] = 0;
        }

        if (NPC.velocity.Length() > 5f)
        {
            NPC.velocity = Vector2.Normalize(NPC.velocity) * 5f;
        }
    }
    public override float SpawnChance(NPCSpawnInfo spawnInfo)
    {
        if (spawnInfo.Player.ZoneHallow && spawnInfo.Player.ZoneOverworldHeight && ModContent.GetInstance<AvalonWorld>().SuperHardmode && Main.hardMode)
        {
            return 0.5f;
        }
        return 0f;
    }
    public override bool? CanFallThroughPlatforms()
    {
        return true;
    }
    public override void HitEffect(int hitDirection, double damage)
    {
        if(NPC.life <= 0)
        {
            for (int i = 0; i < 60; i++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.t_Slime, NPC.velocity.X * Main.rand.NextFloat(-0.1f,1f), NPC.velocity.Y * Main.rand.NextFloat(0.8f,1.2f) - 3, 150, Color.Magenta, 1f);
            }
        }
    }
    //public float squishX = 1f;
    //public float squishY = 1f;
    //public int squishTimer;
    //public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    //{
    //    Texture2D texture = ModContent.Request<Texture2D>("Avalon/NPCs/SlinkySlime").Value;
    //    int frameHeight = texture.Height / Main.npcFrameCount[NPC.type];
    //    Rectangle sourceRectangle = new Rectangle(0, NPC.frame.Y, texture.Width, frameHeight);
    //    Vector2 frameOrigin = sourceRectangle.Size() / 2f;
    //    Vector2 offset = new Vector2(NPC.width / 2 - frameOrigin.X, NPC.height - sourceRectangle.Height);

    //    Vector2 drawPos = NPC.position - Main.screenPosition + frameOrigin + offset;

    //    if(jumpPlayerDir == 1)
    //    {
    //        if (NPC.oldVelocity == NPC.velocity)
    //        {
    //            squishX = MathHelper.Lerp(squishX, 0.9f, 0.1f);
    //            squishY = MathHelper.Lerp(squishY, 1.1f, 0.1f);
    //            Main.EntitySpriteDraw(texture, drawPos, sourceRectangle, Color.Lerp(drawColor, new Color(255, 255, 255, 70), 0.5f), NPC.rotation, frameOrigin, new Vector2(NPC.scale * squishX, NPC.scale * squishY), SpriteEffects.FlipHorizontally, 0);
    //        }
    //        else
    //        {
    //            squishX = MathHelper.Lerp(squishX, 1.5f, 0.5f);
    //            squishY = MathHelper.Lerp(squishY, 0.75f, 0.1f);
    //            Main.EntitySpriteDraw(texture, drawPos, sourceRectangle, Color.Lerp(drawColor, new Color(255, 255, 255, 70), 0.5f), NPC.rotation, frameOrigin - new Vector2(0, 3f), new Vector2(NPC.scale * squishX, NPC.scale * squishY), SpriteEffects.FlipHorizontally, 0);
    //        }
    //    }
    //    else
    //    {
    //        if (NPC.oldVelocity == NPC.velocity)
    //        {
    //            squishX = MathHelper.Lerp(squishX, 0.9f, 0.1f);
    //            squishY = MathHelper.Lerp(squishY, 1.1f, 0.1f);
    //            Main.EntitySpriteDraw(texture, drawPos, sourceRectangle, Color.Lerp(drawColor, new Color(255, 255, 255, 70), 0.5f), NPC.rotation, frameOrigin, new Vector2(NPC.scale * squishX, NPC.scale * squishY), SpriteEffects.None, 0);
    //        }
    //        else
    //        {
    //            squishX = MathHelper.Lerp(squishX, 1.5f, 0.5f);
    //            squishY = MathHelper.Lerp(squishY, 0.75f, 0.1f);
    //            Main.EntitySpriteDraw(texture, drawPos, sourceRectangle, Color.Lerp(drawColor, new Color(255, 255, 255, 70), 0.5f), NPC.rotation, frameOrigin - new Vector2(0, 3f), new Vector2(NPC.scale * squishX, NPC.scale * squishY), SpriteEffects.None, 0);
    //        }
    //    }
    //    return false;
    //}
}
