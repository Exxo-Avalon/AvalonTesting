using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;
using System.IO;
using Terraria.Audio;
using Terraria.ID;

namespace Avalon.Projectiles.Ranged;
internal class SpikeRailgunCharge : ModProjectile
{
    private int CHARGE;

    public override void SetStaticDefaults()
    {

    }
    public override void SetDefaults()
    {
        Projectile.DamageType = DamageClass.Ranged;
        Projectile.damage = 0;
        Projectile.width = Projectile.height = 10;
        AIType = ProjectileID.Bullet;
        Projectile.friendly = true;
        Projectile.penetrate = -1;
        Projectile.alpha = 255;
        Projectile.MaxUpdates = 1;
        Projectile.scale = 1f;
        Projectile.timeLeft = 1200;
    }
    public override void SendExtraAI(BinaryWriter writer)
    {
        writer.Write(CHARGE);
    }
    public override void ReceiveExtraAI(BinaryReader reader)
    {
        CHARGE = reader.ReadInt32();
    }
    public override void AI()
    {
        if (Projectile.ai[0] == 0f)
        {
            CHARGE = 0;
            Projectile.ai[0] = 1f;
        }
        CHARGE++;
        Projectile P = Projectile;
        P.damage = 0;
        Player O = Main.player[P.owner];
        if (Main.rand.NextBool(3))
        {
            Dust d = Dust.NewDustDirect(P.position, P.width, P.height, DustID.Torch);
            d.noGravity = true;
            d.velocity *= 0.95f;
        }
        //int Pindex = -1;
        if (CHARGE == 89)
        {

            //int pindex = Projectile.NewProjectile(O.GetSource_ItemUse_WithPotentialAmmo(ModContent.GetInstance<Items.Weapons.Ranged.SpikeRailgun>().Item,
            //                    ModContent.GetInstance<Items.Weapons.Ranged.SpikeRailgun>().Item.ammo), P.Center, P.velocity, ModContent.ProjectileType<SpikeRailgun>(), (int)O.GetDamage(DamageClass.Ranged).ApplyTo(120), 3f, P.owner);
            SoundEngine.PlaySound(new SoundStyle($"{nameof(Avalon)}/Sounds/Item/Railgun"), O.position);
            //Main.PlaySound(2, (int)P.position.X, (int)P.position.Y, 62);
        }
        if (CHARGE > 101)
            CHARGE = 101;
    }
}
