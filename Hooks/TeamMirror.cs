using Terraria;
using Terraria.ModLoader;
using AvalonTesting.Common;

namespace AvalonTesting.Hooks;

[Autoload(Side = ModSide.Both)]
class TeamMirror : ModHook
{
    protected override void Apply()
    {
        On.Terraria.Player.HasUnityPotion += OnHasUnityItem;
        On.Terraria.Player.TakeUnityPotion += OnTakeUnityItem;
    }
    private static void OnTakeUnityItem(On.Terraria.Player.orig_TakeUnityPotion orig, Player self)
    {
        if (self.HasItem(ModContent.ItemType<Items.Tools.TeamMirror>())) return;

        orig(self);
    }

    private static bool OnHasUnityItem(On.Terraria.Player.orig_HasUnityPotion orig, Player self)
    {
        if (self.HasItem(ModContent.ItemType<Items.Tools.TeamMirror>())) return true;
        return orig(self);
    }
}
