using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Avalon.Common;
using Terraria.DataStructures;

namespace Avalon.Hooks;

[Autoload(Side = ModSide.Client)]
public class ShadowRing : ModHook
{
    protected override void Apply()
    {
        On.Terraria.DataStructures.PlayerDrawSet.BoringSetup += OnBoringSetup;
    }
    private static void OnBoringSetup(On.Terraria.DataStructures.PlayerDrawSet.orig_BoringSetup orig, ref PlayerDrawSet self, Player player,
        List<DrawData> drawData, List<int> dust, List<int> gore, Vector2 drawPosition, float shadowOpacity, float rotation, Vector2 rotationOrigin)
    {
        float s = shadowOpacity;
        if (player.GetModPlayer<Players.ExxoEquipEffectPlayer>().ShadowRing)// || player.GetModPlayer<Players.ExxoEquipEffectPlayer>().BlahArmor)
        {
            s -= 10f;
        }
        orig(ref self, player, drawData, dust, gore, drawPosition, s, rotation, rotationOrigin);
    }
}
