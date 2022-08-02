using Terraria;
using Avalon.Players;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Summon;
public class PrimeArmsCounter : ModProjectile
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Prime Arms Counter");
    }
    public override void SetDefaults()
    {
        Projectile.netImportant = true;
        Projectile.width = 10;
        Projectile.height = 10;
        Projectile.penetrate = -1;
        Projectile.ignoreWater = true;
        Projectile.tileCollide = false;
        Projectile.friendly = true;
        Projectile.minion = true;
        Projectile.minionSlots = 1f;
        Projectile.timeLeft = 60;
        Projectile.aiStyle = -1;
        Projectile.hide = true;
    }
    public override void AI()
    {
        Player owner = Main.player[Projectile.owner];
        owner.AddBuff(ModContent.BuffType<Buffs.PrimeArms>(), 3600);
        Projectile.position = owner.position;
        Projectile.damage = 0;
        if (owner.dead)
        {
            owner.GetModPlayer<ExxoSummonPlayer>().PrimeMinion = false;
        }
        if (owner.GetModPlayer<ExxoSummonPlayer>().PrimeMinion)
        {
            Projectile.timeLeft = 2;
        }
    }
}
