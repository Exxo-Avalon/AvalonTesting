using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Avalon.Buffs;

public class MiniArma : ModBuff
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Mini Arma");
        Description.SetDefault("The cutest harbinger of destruction");
        Main.buffNoTimeDisplay[Type] = true;
        Main.vanityPet[Type] = true;
    }

    public override void Update(Player player, ref int buffIndex)
    {
        player.buffTime[buffIndex] = 18000;

        int projType = ModContent.ProjectileType<Projectiles.Pets.MiniArma>();

        if (player.whoAmI == Main.myPlayer && player.ownedProjectileCounts[projType] <= 0)
        {
            var entitySource = player.GetSource_Buff(buffIndex);

            Projectile.NewProjectile(entitySource, player.Center, Vector2.Zero, projType, 0, 0f, player.whoAmI);
        }
    }
}
