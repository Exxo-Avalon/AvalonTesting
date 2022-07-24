using Avalon.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace Avalon.Dusts;

public class IceBeamDust : ModDust
{
    public override bool Update(Dust dust)
    {
        foreach (NPC n in Main.npc)
        {
            if (n.getRect().Intersects(new Rectangle((int)dust.position.X, (int)dust.position.Y, 8, 8)))
            {
                if (n.type != NPCID.TargetDummy) n.AddBuff(ModContent.BuffType<IcySlowdown>(), 60 * 10);
            }
        }

        float lightFade = dust.scale > 1 ? 1 : dust.scale;
        Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), 0.1f * lightFade,
            0.1f * lightFade, 0.1f * lightFade);
        return true;
    }

    public override Color? GetAlpha(Dust dust, Color lightColor)
    {
        return new Color(255, 255, 255, 100);
    }
}
