

using Avalon.Common;
using Avalon.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Hooks;

[Autoload(Side = ModSide.Both)]
internal class AdjTiles : ModHook
{
    protected override void Apply()
    {
        On.Terraria.Player.AdjTiles += OnAdjTiles;
    }
    private static void OnAdjTiles(On.Terraria.Player.orig_AdjTiles orig, Player self)
    {
        if ((self.GetModPlayer<ExxoEquipEffectPlayer>().GoblinToolbelt ||
            self.GetModPlayer<ExxoEquipEffectPlayer>().GoblinAK ||
            self.GetModPlayer<ExxoEquipEffectPlayer>().BuilderBelt) && Main.myPlayer == self.whoAmI)
        {
            if (!self.adjTile[TileID.TinkerersWorkbench])
            {
                self.adjTile[TileID.TinkerersWorkbench] = true;
            }
            if (!self.adjTile[TileID.WorkBenches])
            {
                self.adjTile[TileID.WorkBenches] = true;
            }
            if (!self.adjTile[TileID.HeavyWorkBench])
            {
                self.adjTile[TileID.HeavyWorkBench] = true;
            }
            if (self.GetModPlayer<ExxoEquipEffectPlayer>().GoblinAK ||
                self.GetModPlayer<ExxoEquipEffectPlayer>().BuilderBelt)
            {
                if (!self.adjTile[TileID.Anvils])
                {
                    self.adjTile[TileID.Anvils] = true;
                }
                if (!self.adjTile[TileID.MythrilAnvil])
                {
                    self.adjTile[TileID.MythrilAnvil] = true;
                }
            }
            Recipe.FindRecipes();
        }
        else orig(self);
    }
}
