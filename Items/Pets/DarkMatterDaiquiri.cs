using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Pets;

public class DarkMatterDaiquiri : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Dark Matter Daiquiri");
        Tooltip.SetDefault("Summons a miniature Armageddon Slime");
    }

    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.KingSlimePetItem);
        Item.shoot = ModContent.ProjectileType<Projectiles.Pets.MiniArma>();
        Item.buffType = ModContent.BuffType<Buffs.MiniArma>();
    }

    public override void UseStyle(Player player, Rectangle heldItemFrame)
    {
        if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
        {
            player.AddBuff(Item.buffType, 3600, true);
        }
    }
}
