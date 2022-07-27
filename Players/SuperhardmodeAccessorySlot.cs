using Terraria;
using Terraria.ModLoader;

namespace Avalon.Players;
public class SuperhardmodeAccessorySlot : ModAccessorySlot
{
    public override bool IsEnabled() => Player.GetModPlayer<ExxoPlayer>().shmAcc;
    public override bool IsHidden() => !Player.GetModPlayer<ExxoPlayer>().shmAcc;
    public override bool CanAcceptItem(Item checkItem, AccessorySlotType context) => !checkItem.GetGlobalItem<AvalonGlobalItemInstance>().Tome;
}
