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

public class SlinkySlime : ModNPC
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Slinky Slime");
        Main.npcFrameCount[NPC.type] = 2;
    }

    public override void SetDefaults()
    {
        NPC.damage = 90;
        NPC.lifeMax = 5600;
        NPC.defense = 60;
        NPC.Size = new Vector2(48);
        NPC.aiStyle = -1;
        NPC.value = 1000f;
        NPC.knockBackResist = 0f;
        NPC.HitSound = SoundID.NPCHit1;
        NPC.DeathSound = SoundID.NPCDeath1;
        NPC.alpha = 60;
        //Banner = NPC.type;
        //BannerItem = ModContent.ItemType<SilverSlimeBanner>();
    }

    public int jumpTimer;
    public float jumpPlayerDir;
    public bool isInJump;
    public float playerDir;
    public float upOrDown;
    public override void AI()
    {
        Player player = Main.player[NPC.FindClosestPlayer()];
        playerDir = player.Center.X - NPC.Center.X;
        Point npcBottom = NPC.Bottom.ToTileCoordinates();
        upOrDown = NPC.Center.Y - player.Center.Y;

        playerDir = Math.Sign(playerDir);

        NPC.spriteDirection = NPC.direction;
        NPC.TargetClosest(false);

        if (NPC.collideY || Main.tileSolid[Main.tile[npcBottom.X, npcBottom.Y].TileType] && Main.tile[npcBottom.X, npcBottom.Y].HasTile || Main.tileSolid[Main.tile[npcBottom.X + 1, npcBottom.Y].TileType] && Main.tile[npcBottom.X + 1, npcBottom.Y].HasTile || Main.tileSolid[Main.tile[npcBottom.X - 1, npcBottom.Y].TileType] && Main.tile[npcBottom.X - 1, npcBottom.Y].HasTile)
        {
            jumpTimer++;
            NPC.velocity.X *= 0.85f;
            isInJump = false;
            NPC.TargetClosest(true);
            Collision.StepUp(ref NPC.position, ref NPC.velocity, NPC.width, NPC.height, ref NPC.stepSpeed, ref NPC.gfxOffY);
        }
        if (jumpTimer == 10)
        {
            float disX = player.Center.X - NPC.Center.X;
            float playerVelX = player.velocity.X;
            if(disX < 0)
            {
                disX = -disX;
            }
            if(playerVelX < 0)
            {
                playerVelX = -playerVelX;
            }
            NPC.velocity.Y -= 8f + -((player.Center.Y - NPC.Center.Y) / 50);
            NPC.velocity.X += (disX / 50 + playerVelX) * playerDir;
            jumpPlayerDir = playerDir;
            jumpTimer = 0;
            isInJump = true;
        }
        if (isInJump)
        {
            NPC.ai[1] = 0;
            NPC.height = 90;
            if (NPC.ai[0] == 0)
            {
                NPC.Center = new Vector2(NPC.Center.X, NPC.Center.Y - 42f);
                NPC.ai[0]++;
            }
            if(NPC.velocity.Y > 0 && NPC.velocity.X == 0)
            {
                NPC.velocity.X = NPC.velocity.Y * playerDir;
            }
        }
        if (!isInJump)
        {
            NPC.ai[0] = 0;
            NPC.height = 48;
            if (NPC.ai[1] == 0)
            {
                NPC.Center = new Vector2(NPC.Center.X, NPC.Center.Y + 42f);
                NPC.ai[1]++;
            }
        }

        NPC.rotation = NPC.velocity.Y * 0.02f * -jumpPlayerDir;

        float maxVelX = 14f;
        float maxVelY = 20f;

        if (NPC.velocity.X > maxVelX)
        {
            NPC.velocity.X = maxVelX;
        }
        if (NPC.velocity.X < -maxVelX)
        {
            NPC.velocity.X = -maxVelX;
        }
        if(NPC.velocity.Y < -maxVelY)
        {
            NPC.velocity.Y = -maxVelY;
        }
    }

    public override bool? CanFallThroughPlatforms()
    {
        return isInJump && upOrDown < -50;
    }

    public override void FindFrame(int frameHeight)
    {
        if (NPC.oldVelocity == NPC.velocity)
        {
            NPC.frame.Y = frameHeight;
        }
        else
        {
            NPC.frame.Y = 0;
        }
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
    /*public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
        Texture2D texture = ModContent.Request<Texture2D>("Avalon/NPCs/SlinkySlime").Value;
        int frameHeight = texture.Height / Main.projFrames[NPC.type];
        Rectangle sourceRectangle = new Rectangle(0, NPC.frame.Y, texture.Width, frameHeight);
        Vector2 frameOrigin = sourceRectangle.Size() / 2f;
        Vector2 offset = new Vector2(NPC.width / 2 - frameOrigin.X, NPC.height - sourceRectangle.Height);

        Vector2 drawPos = NPC.position - Main.screenPosition + frameOrigin + offset;

        if(playerDir == -1)
        {
            Main.EntitySpriteDraw(texture, drawPos, sourceRectangle, drawColor, NPC.rotation, frameOrigin - new Vector2(0, 11), new Vector2(NPC.scale, NPC.scale), SpriteEffects.None, 0);
        }
        else
        {
            Main.EntitySpriteDraw(texture, drawPos, sourceRectangle, drawColor, NPC.rotation, frameOrigin - new Vector2(0, 11), new Vector2(NPC.scale, NPC.scale), SpriteEffects.FlipHorizontally, 0);
        }
        return false;
    }*/
}
