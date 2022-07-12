using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace AvalonTesting.Items.Other;

class SkyInsignia : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Sky Insignia");
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.width = dims.Width;
        Item.height = dims.Height;
    }

    public override bool CanPickup(Player player)
    {
        return true;
    }

    public override bool OnPickup(Player player)
    {
        player.AddBuff(ModContent.BuffType<Buffs.SkyBlessing>(), 60 * 7);
        SoundEngine.PlaySound(SoundID.Grab, player.position);
        return false;
    }
}
