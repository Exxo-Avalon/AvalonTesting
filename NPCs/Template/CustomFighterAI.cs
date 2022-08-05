using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.NPCs.Template;

public abstract class CustomFighterAI : ModNPC
{
    public virtual float MaxMoveSpeed { get; set; } = 2.5f;
    public virtual float MaxAirSpeed{ get; set; } = 3.5f;
    public virtual float Acceleration { get; set; } = 0.1f;
    public virtual float AirAcceleration { get; set; } = 0.1f;
    public virtual float MaxJumpHeight{ get; set; } = 8f;
    public virtual float JumpRadius { get; set; } = 150;
    public override bool? CanFallThroughPlatforms()
    {
        Player player = Main.player[NPC.FindClosestPlayer()];
        float upOrDown = NPC.Center.Y - player.Center.Y;

        return NPC.collideY && upOrDown < -15;
    }
    public virtual void CustomBehavior()
    {
    }
    private bool isHit;
    private bool isInJump;
    private float jumpdelay = 3;
    public override void AI()
    {
        Player player = Main.player[NPC.FindClosestPlayer()];
        float distanceBetweenPlayer = Vector2.Distance(player.Center, NPC.Center);
        float dir = NPC.Center.X - player.Center.X;
        float upOrDown = NPC.Center.Y - player.Center.Y;

        dir = Math.Sign(dir);

        float moveSpeedMulti = NPC.velocity.X + (Acceleration * -dir);
        float airSpeedMulti = NPC.velocity.X + (AirAcceleration * -dir);
        moveSpeedMulti = Math.Clamp(moveSpeedMulti, -MaxMoveSpeed, MaxMoveSpeed);
        if (!isHit)
        {
            airSpeedMulti = Math.Clamp(airSpeedMulti, -MaxAirSpeed, MaxAirSpeed);
        }
        NPC.spriteDirection = -(int)dir;

        if (NPC.velocity.Y == 0)
        {
            NPC.velocity.X = moveSpeedMulti;
        }
        else
        {
            NPC.velocity.X = airSpeedMulti;
        }
        if(distanceBetweenPlayer < JumpRadius && NPC.collideY && upOrDown > 1 && Collision.CanHitLine(NPC.position, NPC.width, NPC.height, player.position, player.width, player.height))
        {
            Jump(MaxJumpHeight);
        }

        Point a = NPC.Bottom.ToTileCoordinates();
        float height = 0;
        if ((NPC.collideY || Main.tileSolid[Main.tile[a.X, a.Y].TileType] && Main.tile[a.X, a.Y].HasTile) && NPC.collideX)
        {
            for (int i = 0; i < 10; i++)
            {
                if(Main.tile[a.X + 1 * -(int)dir, a.Y - i].HasTile && Main.tileSolid[Main.tile[a.X + 1 * -(int)dir, a.Y - i].TileType])
                {
                    height = i + 1;
                }
            }
            Jump(height);
        }
        if (NPC.collideY || Main.tileSolid[Main.tile[a.X, a.Y].TileType] && Main.tile[a.X, a.Y].HasTile)
        {
            Collision.StepUp(ref NPC.position, ref NPC.velocity, NPC.width, NPC.height, ref NPC.stepSpeed, ref NPC.gfxOffY);
            isInJump = false;
            isHit = false;
            if(jumpdelay != 0)
            {
                jumpdelay--;
            }
            if((!Main.tileSolid[Main.tile[a.X + 1 * -(int)dir, a.Y].TileType] || !Main.tile[a.X + 1 * -(int)dir, a.Y].HasTile) && (!Main.tileSolid[Main.tile[a.X + 2 * -(int)dir, a.Y].TileType] || !Main.tile[a.X + 2 * -(int)dir, a.Y].HasTile) && upOrDown > -20)
            {
                Jump(MaxJumpHeight);
            }
        }
        else
        {
            if (NPC.velocity.Y < 0)
            {
                isInJump = true;
            }
            if (NPC.velocity.Y > 0)
            {
                Collision.StepUp(ref NPC.position, ref NPC.velocity, NPC.width, NPC.height, ref NPC.stepSpeed, ref NPC.gfxOffY);
            }
        }
        CustomBehavior();
    }
    public void Jump(float height)
    {
        if(jumpdelay == 0)
        {
            height = Math.Clamp(height + 2.5f, 0f, MaxJumpHeight);
            NPC.velocity.Y = -(height);
            jumpdelay = 3;
        }
    }
    public override void OnHitByItem(Player player, Item item, int damage, float knockback, bool crit)
    {
        isHit = true;
    }
    public override void OnHitByProjectile(Projectile projectile, int damage, float knockback, bool crit)
    {
        isHit = true;
    }
}
