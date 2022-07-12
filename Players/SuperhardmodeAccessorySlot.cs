using Terraria.ModLoader;

namespace AvalonTesting.Players;
public class SuperhardmodeAccessorySlot : ModAccessorySlot
{
    public override bool IsHidden() => !Player.GetModPlayer<ExxoPlayer>().shmAcc;
}
