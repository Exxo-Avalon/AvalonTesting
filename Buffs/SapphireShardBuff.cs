using Terraria;
using Terraria.ModLoader;

namespace AvalonTesting.Buffs;

public class SapphireShardBuff : ModBuff
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Magic Damage Up");
        Description.SetDefault("Magic damage is increased by 8%");
    }
    public override void Update(Player player, ref int buffIndex)
    {
        player.GetDamage(DamageClass.Magic) += 0.08f;
    }
}
