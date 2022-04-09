using Terraria.ModLoader;

namespace AvalonTesting.Buffs.AdvancedBuffs;

public class AdvAmmoReservation : ModBuff
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Advanced Ammo Reservation");
        Description.SetDefault("30% chance to not consume ammo");
    }
}
