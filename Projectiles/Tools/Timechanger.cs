using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AvalonTesting.Projectiles.Tools;

public class Timechanger : ModProjectile
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Timechanger");
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.penetrate = -1;
        Projectile.width = dims.Width * 10 / 32;
        Projectile.height = dims.Height * 10 / 32 / Main.projFrames[Projectile.type];
        Projectile.aiStyle = -1;
        Projectile.friendly = true;
        Projectile.damage = 0;
        Projectile.tileCollide = false;
    }

    public override void AI()
    {
        if (Main.dayTime)
        {
            Main.time = 53999;
        }
        else
        {
            Main.time = 32399;
        }
        if (Main.dayTime)
        {
            if (Main.netMode == 0)
            {
                Main.NewText("It is now Night.", 50, 255, 130);
            }
            else if (Main.netMode == 2)
            {
                ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(Main.player[Projectile.owner].name + ": It is now Night."), new Color(50, 255, 130));
            }
        }
        else
        {
            if (Main.netMode == 0)
            {
                Main.NewText("It is now Day.", 50, 255, 130);
            }
            else if (Main.netMode == 2)
            {
                ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(Main.player[Projectile.owner].name + ": It is now Day."), new Color(50, 255, 130));
            }
        }
        Projectile.active = false;
    }
}
