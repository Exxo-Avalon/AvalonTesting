using Terraria.ModLoader;

namespace AvalonTesting.Buffs.AdvancedBuffs;

public class AdvAmmoReservation : ModBuff
{
    public const float Chance = 0.30f;

    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Advanced Ammo Reservation");
        Description.SetDefault($"{Chance * 100}% chance to not consume ammo");
    }
}
