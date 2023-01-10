using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Avalon.Projectiles;

namespace Avalon.Buffs;
public class KunziteSparks : ModBuff
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Kunzite Blades");
        Description.SetDefault("Mortal humans like you shouldn't be afflicted with this curse.");
        Main.debuff[Type] = true;
        BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
    }
    public override void Update(NPC npc, ref int buffIndex)
    {
        npc.GetGlobalNPC<KunziteEnergyGlobalNPC>().KunziteBlades = true;
    }
}
public class KunziteEnergyGlobalNPC : GlobalNPC
{
    public override bool InstancePerEntity => true;
    public bool KunziteBlades;
    public int KunziteBladeCooldown;
    public override void ResetEffects(NPC npc)
    {
        if (KunziteBladeCooldown > 0)
        {
            KunziteBladeCooldown--;
        }
        KunziteBlades = false;
    }

    public override void DrawEffects(NPC npc, ref Color drawColor)
    {
        if (KunziteBlades)
        {
            drawColor = Color.Lerp(drawColor, Color.HotPink, 0.5f);
            int d = Dust.NewDust(npc.position, npc.width, npc.height, Main.rand.Next(254, 256));
            Main.dust[d].color.A = 0;
            Main.dust[d].noGravity = true;
        }
    }
    public override void PostAI(NPC npc)
    {
        if (KunziteBlades)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient && KunziteBladeCooldown == 0)
            {
                KunziteBladeCooldown = 15;
                SoundEngine.PlaySound(SoundID.Item1, npc.Center);
                int p = Projectile.NewProjectile(npc.GetSource_FromThis(), npc.Center + Main.rand.NextVector2CircularEdge(npc.width * 2.5f, npc.height * 2.5f) + Main.rand.NextVector2Circular(1, 1), Vector2.Zero, ModContent.ProjectileType<KunziteBlade>(), 100, 6, Main.myPlayer);
                Main.projectile[p].velocity = new Vector2(Main.rand.NextFloat(npc.width * 0.2f, npc.height * 0.2f), 0).RotatedBy(Main.projectile[p].Center.DirectionTo(npc.Center).ToRotation());
            }
        }
    }
}
