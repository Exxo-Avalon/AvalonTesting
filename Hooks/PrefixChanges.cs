using On.Terraria;

namespace Avalon.Hooks;

public static class PrefixChanges
{
    public static bool OnIsAPrefixableAccessory(Item.orig_IsAPrefixableAccessory orig, Terraria.Item self)
    {
        return self.IsArmor() || orig(self);
    }
}
