using System;
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
        On.Terraria.Player.AdjTiles += OnAdjTilesPocketBench;
    }
    private static void OnAdjTilesPocketBench(On.Terraria.Player.orig_AdjTiles orig, Player self)
    {
        if (Array.Exists(self.bank.item, element => Data.Sets.Item.Stations.Contains(element.createTile)) && self.GetModPlayer<ExxoEquipEffectPlayer>().PocketBench)
        {
            for (int o = 0; o < self.bank.item.Length; o++)
            {
                if (self.bank.item[o].createTile > -1)
                {
                    if (!self.adjTile[self.bank.item[o].createTile])
                    {
                        self.adjTile[self.bank.item[o].createTile] = true;
                    }
                }
            }
        }
        else
            orig(self);

    }
    private static void OnAdjTiles(On.Terraria.Player.orig_AdjTiles orig, Player self)
    {
        if ((self.GetModPlayer<ExxoEquipEffectPlayer>().GoblinToolbelt ||
            self.GetModPlayer<ExxoEquipEffectPlayer>().GoblinAK ||
            self.GetModPlayer<ExxoEquipEffectPlayer>().BuilderBelt) && Main.myPlayer == self.whoAmI)
        {
            
            //else if (!self.GetModPlayer<ExxoEquipEffectPlayer>().PocketBench)
            //{
            //    for (int k = 0; k < self.inventory.Length; k++)
            //    {
            //        if (self.inventory[k].createTile > -1)
            //        {
            //            if (self.adjTile[self.inventory[k].createTile])
            //            {
            //                self.adjTile[self.inventory[k].createTile] = false;
            //            }
            //        }
            //    }
            //}
            {
                if (!self.adjTile[TileID.TinkerersWorkbench])
                {
                    self.adjTile[TileID.TinkerersWorkbench] = true;
                }
                else if (!self.GetModPlayer<ExxoEquipEffectPlayer>().GoblinToolbelt &&
                    !self.GetModPlayer<ExxoEquipEffectPlayer>().GoblinAK &&
                    !self.GetModPlayer<ExxoEquipEffectPlayer>().BuilderBelt)
                {
                    self.adjTile[TileID.TinkerersWorkbench] = false;
                }
                if (!self.adjTile[TileID.WorkBenches])
                {
                    self.adjTile[TileID.WorkBenches] = true;
                }
                else if (!self.GetModPlayer<ExxoEquipEffectPlayer>().GoblinToolbelt &&
                    !self.GetModPlayer<ExxoEquipEffectPlayer>().GoblinAK &&
                    !self.GetModPlayer<ExxoEquipEffectPlayer>().BuilderBelt)
                {
                    self.adjTile[TileID.WorkBenches] = false;
                }
                if (!self.adjTile[TileID.HeavyWorkBench])
                {
                    self.adjTile[TileID.HeavyWorkBench] = true;
                }
                else if (!self.GetModPlayer<ExxoEquipEffectPlayer>().GoblinToolbelt &&
                    !self.GetModPlayer<ExxoEquipEffectPlayer>().GoblinAK &&
                    !self.GetModPlayer<ExxoEquipEffectPlayer>().BuilderBelt)
                {
                    self.adjTile[TileID.HeavyWorkBench] = false;
                }
                if (self.GetModPlayer<ExxoEquipEffectPlayer>().GoblinAK ||
                    self.GetModPlayer<ExxoEquipEffectPlayer>().BuilderBelt)
                {
                    if (!self.adjTile[TileID.Anvils])
                    {
                        self.adjTile[TileID.Anvils] = true;
                    }
                    else if (!self.GetModPlayer<ExxoEquipEffectPlayer>().GoblinToolbelt &&
                        !self.GetModPlayer<ExxoEquipEffectPlayer>().GoblinAK &&
                        !self.GetModPlayer<ExxoEquipEffectPlayer>().BuilderBelt)
                    {
                        self.adjTile[TileID.Anvils] = false;
                    }
                    if (!self.adjTile[TileID.MythrilAnvil])
                    {
                        self.adjTile[TileID.MythrilAnvil] = true;
                    }
                    else if (!self.GetModPlayer<ExxoEquipEffectPlayer>().GoblinToolbelt &&
                        !self.GetModPlayer<ExxoEquipEffectPlayer>().GoblinAK &&
                        !self.GetModPlayer<ExxoEquipEffectPlayer>().BuilderBelt)
                    {
                        self.adjTile[TileID.MythrilAnvil] = false;
                    }
                }
            }
            Recipe.FindRecipes();
        }
        else
            orig(self);
    }
}
