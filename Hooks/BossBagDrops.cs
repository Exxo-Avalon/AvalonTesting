using System.Collections.Generic;
using AvalonTesting.Items.Accessories;
using AvalonTesting.Items.Material;
using AvalonTesting.Items.Weapons.Magic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Hooks;

class BossBagDrops
{
    public static void OnOpenBossBag(On.Terraria.Player.orig_OpenBossBag orig, Player self, int type)
    {
        if (type == ItemID.GolemBossBag)
        {
            var list = new List<int>
            {
                ItemID.Stynger,
                ItemID.StaffofEarth,
                ItemID.EyeoftheGolem,
                ItemID.PossessedHatchet,
                ItemID.GolemFist,
                ItemID.SunStone,
                ItemID.HeatRay,
                ModContent.ItemType<Sunstorm>(),
                ModContent.ItemType<EarthenInsignia>(),
                ModContent.ItemType<HeartoftheGolem>()
            };
            int item1 = list.RemoveAtIndex(Main.rand.Next(list.Count));
            int item2 = list.RemoveAtIndex(Main.rand.Next(list.Count));
            self.QuickSpawnItem(self.GetItemSource_OpenItem(ItemID.GolemBossBag), item1);
            self.QuickSpawnItem(self.GetItemSource_OpenItem(ItemID.GolemBossBag), item2);
            if (item1 == ItemID.Stynger || item2 == ItemID.Stynger)
            {
                self.QuickSpawnItem(self.GetItemSource_OpenItem(ItemID.GolemBossBag), ItemID.StyngerBolt, Main.rand.Next(60, 101));
            }
            self.QuickSpawnItem(self.GetItemSource_OpenItem(ItemID.GolemBossBag), ModContent.ItemType<EarthStone>(), Main.rand.Next(1, 4));
            self.QuickSpawnItem(self.GetItemSource_OpenItem(ItemID.GolemBossBag), ItemID.BeetleHusk, Main.rand.Next(18, 24));
            self.QuickSpawnItem(self.GetItemSource_OpenItem(ItemID.GolemBossBag), ItemID.ShinyStone);
            self.QuickSpawnItem(self.GetItemSource_OpenItem(ItemID.GolemBossBag), ItemID.GoldCoin, 12);
            if (Main.rand.Next(7) == 0)
            {
                self.QuickSpawnItem(self.GetItemSource_OpenItem(ItemID.GolemBossBag), ItemID.GolemMask);
            }
            if (Main.rand.Next(3) == 0)
            {
                self.QuickSpawnItem(self.GetItemSource_OpenItem(ItemID.GolemBossBag), ItemID.Picksaw);
            }
        }
        else orig(self, type);
    }
}
