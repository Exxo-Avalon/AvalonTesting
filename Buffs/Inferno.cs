using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Buffs;

public class Inferno : ModBuff
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Inferno");
        Description.SetDefault("Losing life");
        Main.debuff[Type] = true;
        BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
    }

    public override void Update(NPC npc, ref int buffIndex)
    {
        npc.GetGlobalNPC<AvalonGlobalNPCInstance>().Inferno = true;
        for (int i = 0; i < Main.npc.Length; i++)
        {
            NPC N2 = Main.npc[i];
            if (N2.townNPC || N2.friendly || N2.whoAmI == npc.whoAmI)
            {
                continue;
            }

            if (N2.getRect().Intersects(npc.getRect()))
            {
                N2.AddBuff(ModContent.BuffType<Inferno>(), 540);
            }
        }

        if (Main.rand.NextBool(2))
        {
            int num10 = Dust.NewDust(npc.position, npc.width, npc.height, DustID.InfernoFork, 0f, -2f, 0, default, 2f);
            Main.dust[num10].noGravity = true;
            if (Main.rand.NextBool(10))
            {
                int num161 = Gore.NewGore(npc.GetSource_FromThis(), npc.position, new Vector2(Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-2f, 0f)),
                    Main.rand.Next(61, 64));
                Gore gore40 = Main.gore[num161];
                gore40.velocity *= 0.3f;
                gore40.scale = Main.rand.NextFloat(0.5f, 1f);
                gore40.alpha = 100;
                Main.gore[num161].velocity.X += Main.rand.Next(-1, 2);
                Main.gore[num161].velocity.Y += Main.rand.Next(-1, 2);
            }
        }
    }
}
