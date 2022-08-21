using Avalon.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Avalon.Players;

namespace Avalon.NPCs;

public class FishingRift : ModNPC
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault(" ");
        Main.npcFrameCount[NPC.type] = 1;
    }

    public override void SetDefaults()
    {
        NPC.width = 60;
        NPC.height = 42;
        NPC.noTileCollide = NPC.noGravity = true;
        NPC.npcSlots = 0f;
        NPC.damage = 0;
        NPC.lifeMax = 100;
        NPC.dontTakeDamage = true;
        NPC.defense = 0;
        NPC.aiStyle = -1;
        NPC.value = 0;
        NPC.knockBackResist = 0f;
        NPC.scale = 1f;
        NPC.timeLeft = 7200;
        NPC.HitSound = SoundID.NPCHit1;
        NPC.DeathSound = SoundID.NPCDeath39;
    }
    public override bool PreAI()
    {
        Lighting.AddLight(NPC.position, 100 / 255f, 67 / 255f, 100 / 255f);
        return true;
    }

    public override void AI() //Needs to despawn after x amount of time
    {
        for (int i = 0; i < Main.maxProjectiles; i++)
        {
            Projectile projectile = Main.projectile[i];
            if (projectile == null || !projectile.active || projectile.aiStyle != 61)
            {
                continue;
            }
            if (projectile.Hitbox.Intersects(NPC.Hitbox)) //If colliding with the hitbox, the bobber should be gradually pulled towards the centre of the rift
            {
                Player p = Main.player[Player.FindClosest(NPC.position, NPC.width, NPC.height)];
                //This whole section needs to somehow be done in a different way so that the biome isn't actually being changed, no clue how
                if (p.ZoneCorrupt)
                {
                    if (Main.rand.Next(2) == 0) // crimson loot
                    {
                        p.ZoneCorrupt = false;
                        p.ZoneCrimson = true;
                    }
                    else
                    {
                        p.ZoneCorrupt = false;
                        //needs to be ZoneContagion but idk how
                        p.ZoneCrimson = true;
                    }
                }
                //Doesn't work cause ZoneCrimson is set to true above, so it creates an infinite loop (should be easy to fix)

                //if (p.ZoneCrimson)
                //{
                //    if (Main.rand.Next(2) == 0) // crimson loot
                //    {
                //        p.ZoneCrimson = false;
                //        p.ZoneCorrupt = true;
                //    }
                //    else
                //    {
                //        p.ZoneCrimson = false;
                //        //needs to be ZoneContagion but idk how
                //        p.ZoneCorrupt = true;
                //    }
                //}

                //Doesn't work cause idk how to check for ZoneContagion

                //if (p.ZoneContagion)
                //{
                //    if (Main.rand.Next(2) == 0) // crimson loot
                //    {
                //        p.ZoneContagion = false;
                //        p.ZoneCorrupt = true;
                //    }
                //    else
                //    {
                //        p.ZoneContagion = false;
                //        p.ZoneCrimson = true;
                //    }
                //}
            }
        }
    }
}
