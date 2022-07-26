using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using Terraria.IO;
using Avalon.Items.Placeable.Bar;
using AltLibrary.Common.Systems;
using Avalon.Items.Weapons.Magic;
using Avalon.Items.Weapons.Ranged;

namespace Avalon.World.Passes;

public class ReplaceChestItems : GenPass
{
    public ReplaceChestItems() : base("ReplaceChestItems", 10)
    {

    }
    protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
    {
        foreach (Chest c in Main.chest)
        {
            if (c != null)
            {
                foreach (Item i in c.item)
                {
                    if (i != null && i.type == ItemID.EnchantedBoomerang)
                    {
                        i.SetDefaults(ModContent.ItemType<EnchantedBar>());
                        i.stack = WorldGen.genRand.Next(35, 49);
                    }
                    if (WorldBiomeManager.WorldJungle == "Avalon/TropicsAlternateBiome")
                    {
                        if (i != null && i.type == ItemID.Boomstick)
                            i.SetDefaults(ModContent.ItemType<Thompson>());
                    }
                    if (i != null && i.type == ItemID.StaffofRegrowth && WorldGen.genRand.Next(2) == 0)
                        i.SetDefaults(ModContent.ItemType<FlowerofTheJungle>());
                }
            }
        }
    }
}
