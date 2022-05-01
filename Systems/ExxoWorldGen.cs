using System.Collections.Generic;
using System.Reflection;
using AvalonTesting.Items.Placeable.Tile;
using AvalonTesting.Tiles;
using AvalonTesting.World.Passes;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.WorldBuilding;

namespace AvalonTesting.Systems;

public class ExxoWorldGen : ModSystem
{
    public enum AdamantiteVariant
    {
        Adamantite,
        Titanium,
        Troxinium,
        Random
    }

    public enum CobaltVariant
    {
        Cobalt,
        Palladium,
        Duratanium,
        Random
    }

    public enum CopperVariant
    {
        Copper,
        Tin,
        Bronze,
        Random
    }

    public enum EvilBiome
    {
        Corruption,
        Crimson,
        Contagion
    }

    public enum GoldVariant
    {
        Gold,
        Platinum,
        Bismuth,
        Random
    }

    public enum IronVariant
    {
        Iron,
        Lead,
        Nickel,
        Random
    }

    public enum JungleVariant
    {
        Jungle,
        Tropics,
        Random
    }

    public enum MythrilVariant
    {
        Mythril,
        Orichalcum,
        Naquadah,
        Random
    }

    public enum RhodiumVariant
    {
        Rhodium,
        Osmium,
        Iridium,
        Random
    }

    public enum SHMTier1Variant
    {
        Tritanorium,
        Pyroscoric,
        Random
    }

    public enum SHMTier2Variant
    {
        Unvolandite,
        Vorazylcum,
        Random
    }

    public enum SilverVariant
    {
        Silver,
        Tungsten,
        Zinc,
        Random
    }

    public AdamantiteVariant AdamantiteOre = AdamantiteVariant.Random;
    public CobaltVariant CobaltOre = CobaltVariant.Random;

    public CopperVariant CopperOre = CopperVariant.Random;

    public int DungeonLocation;
    public int DungeonSide;
    public GoldVariant GoldOre = GoldVariant.Random;
    public int HallowedAltarCount;
    public IronVariant IronOre = IronVariant.Random;
    public JungleVariant JungleMenuSelection = JungleVariant.Random;
    public int JungleX;
    public MythrilVariant MythrilOre = MythrilVariant.Random;
    public RhodiumVariant RhodiumOre = RhodiumVariant.Random;
    public SHMTier1Variant SHMTier1Ore = SHMTier1Variant.Random;
    public SHMTier2Variant SHMTier2Ore = SHMTier2Variant.Random;
    public SilverVariant SilverOre = SilverVariant.Random;
    public EvilBiome WorldEvil;

    public override void PreWorldGen()
    {
        if (WorldGen.WorldGenParam_Evil == -1)
        {
            WorldEvil = (EvilBiome)WorldGen.genRand.Next(3);
        }
        else
        {
            WorldEvil = (EvilBiome)WorldGen.WorldGenParam_Evil;
        }
    }

    public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
    {
        GenPass currentPass;

        int reset = tasks.FindIndex(genpass => genpass.Name == "Reset");
        if (reset != -1)
        {
            currentPass = new AvalonSetup();
            tasks.Insert(reset + 1, currentPass);
            totalWeight += currentPass.Weight;
        }

        int underworld = tasks.FindIndex(genPass => genPass.Name == "Underworld");
        if (underworld != -1)
        {
            currentPass = new Underworld();
            tasks.Insert(underworld + 1, currentPass);
            totalWeight += currentPass.Weight;
        }

        int shinies = tasks.FindIndex(genpass => genpass.Name == "Shinies");
        if (shinies != -1)
        {
            currentPass = new OreGenPreHardMode();
            tasks.Insert(shinies + 1, currentPass);
            totalWeight += currentPass.Weight;
        }

        int smoothWorld = tasks.FindIndex(genPass => genPass.Name == "Smooth World");
        if (smoothWorld != -1)
        {
            currentPass = new SmoothWorld();
            tasks.Insert(smoothWorld + 1, currentPass);
            totalWeight += currentPass.Weight;
        }

        int iceWalls = tasks.FindIndex(genpass => genpass.Name == "Cave Walls");
        if (iceWalls != -1)
        {
            currentPass = new Shrines();
            tasks.Insert(iceWalls + 1, currentPass);
            totalWeight += currentPass.Weight;
        }

        int vines = tasks.FindIndex(genPass => genPass.Name == "Vines");
        if (vines != -1)
        {
            currentPass = new World.Passes.Impvines();
            tasks.Insert(vines + 1, currentPass);
            totalWeight += currentPass.Weight;
        }

        if (WorldEvil == EvilBiome.Contagion)
        {
            int corruption = tasks.FindIndex(genPass => genPass.Name == "Corruption");
            if (corruption != -1)
            {
                currentPass = new Contagion();
                tasks[corruption] = currentPass;
                // tasks.Insert(vines + 1, currentPass);
                // totalWeight += currentPass.Weight;
            }
        }
    }

    public override void PostWorldGen()
    {
        JungleX =
            (int)typeof(WorldGen).GetField("JungleX", BindingFlags.Static | BindingFlags.NonPublic)!.GetValue(null)!;
    }

    public override void SaveWorldData(TagCompound tag)
    {
        tag["DungeonSide"] = DungeonSide;
        tag["DungeonLocation"] = DungeonLocation;
        tag["HallowedAltarCount"] = HallowedAltarCount;
        tag["WorldEvil"] = (byte)WorldEvil;
        tag["CopperOre"] = (byte)CopperOre;
        tag["IronOre"] = (byte)IronOre;
        tag["SilverOre"] = (byte)SilverOre;
        tag["GoldOre"] = (byte)GoldOre;
        tag["RhodiumOre"] = (byte)RhodiumOre;
        tag["CobaltOre"] = (byte)CobaltOre;
        tag["MythrilOre"] = (byte)MythrilOre;
        tag["AdamantiteOre"] = (byte)AdamantiteOre;
        tag["SHMTier1Ore"] = (byte)SHMTier1Ore;
        tag["SHMTier2Ore"] = (byte)SHMTier2Ore;
        tag["JungleX"] = JungleX;
    }

    public override void LoadWorldData(TagCompound tag)
    {
        if (tag.ContainsKey("WorldEvil"))
        {
            WorldEvil = (EvilBiome)tag.Get<byte>("WorldEvil");
        }
        else
        {
            if (WorldGen.crimson)
            {
                WorldEvil = EvilBiome.Crimson;
            }
            else
            {
                WorldEvil = EvilBiome.Corruption;
            }
        }

        if (tag.ContainsKey("DungeonSide"))
        {
            DungeonSide = tag.Get<int>("DungeonSide");
        }

        if (tag.ContainsKey("DungeonLocation"))
        {
            DungeonLocation = tag.Get<int>("DungeonLocation");
        }

        if (tag.ContainsKey("HallowedAltarCount"))
        {
            HallowedAltarCount = tag.Get<int>("HallowedAltarCount");
        }

        if (tag.ContainsKey("CopperOre"))
        {
            CopperOre = (CopperVariant)tag.Get<byte>("CopperOre");
        }

        if (tag.ContainsKey("IronOre"))
        {
            IronOre = (IronVariant)tag.Get<byte>("IronOre");
        }

        if (tag.ContainsKey("SilverOre"))
        {
            SilverOre = (SilverVariant)tag.Get<byte>("SilverOre");
        }

        if (tag.ContainsKey("GoldOre"))
        {
            GoldOre = (GoldVariant)tag.Get<byte>("GoldOre");
        }

        if (tag.ContainsKey("RhodiumOre"))
        {
            RhodiumOre = (RhodiumVariant)tag.Get<byte>("RhodiumOre");
        }

        if (tag.ContainsKey("CobaltOre"))
        {
            CobaltOre = (CobaltVariant)tag.Get<byte>("CobaltOre");
        }

        if (tag.ContainsKey("MythrilOre"))
        {
            MythrilOre = (MythrilVariant)tag.Get<byte>("MythrilOre");
        }

        if (tag.ContainsKey("AdamantiteOre"))
        {
            AdamantiteOre = (AdamantiteVariant)tag.Get<byte>("AdamantiteOre");
        }

        if (tag.ContainsKey("SHMTier1Ore"))
        {
            SHMTier1Ore = (SHMTier1Variant)tag.Get<byte>("SHMTier1Ore");
        }

        if (tag.ContainsKey("SHMTier2Ore"))
        {
            SHMTier2Ore = (SHMTier2Variant)tag.Get<byte>("SHMTier2Ore");
        }

        if (tag.ContainsKey("JungleX"))
        {
            JungleX = tag.Get<int>("JungleX");
        }
        
        if (JungleX == 0)
        {
            bool found = false;
            for (int y = (int)Main.worldSurface - 150; y < Main.maxTilesY; y++)
            {
                for (int x = 0; x < Main.maxTilesX; x++)
                {
                    if (Main.tile[x, y].HasTile && (Main.tile[x, y].TileType == TileID.JungleGrass ||
                                                    Main.tile[x, y].TileType == ModContent.TileType<TropicalGrass>()))
                    {
                        JungleX = x;
                        found = true;
                        break;
                    }
                }

                if (found)
                {
                    break;
                }
            }
        }
    }
}

public static class ExxoWorldGenExtensions
{
    public static int GetRhodiumVariantItemOre(this ExxoWorldGen.RhodiumVariant osmiumVariant)
    {
        switch (osmiumVariant)
        {
            case ExxoWorldGen.RhodiumVariant.Osmium:
                return ModContent.ItemType<OsmiumOre>();
            case ExxoWorldGen.RhodiumVariant.Rhodium:
                return ModContent.ItemType<RhodiumOre>();
            case ExxoWorldGen.RhodiumVariant.Iridium:
                return ModContent.ItemType<IridiumOre>();
            default:
                return -1;
        }
    }

    public static int GetSHMTier1VariantTileOre(this ExxoWorldGen.SHMTier1Variant shmTier1Variant)
    {
        switch (shmTier1Variant)
        {
            case ExxoWorldGen.SHMTier1Variant.Tritanorium:
                return ModContent.TileType<Tiles.Ores.TritanoriumOre>();
            case ExxoWorldGen.SHMTier1Variant.Pyroscoric:
                return ModContent.TileType<Tiles.Ores.PyroscoricOre>();
            default:
                return -1;
        }
    }

    public static int GetSHMTier2VariantTileOre(this ExxoWorldGen.SHMTier2Variant shmTier2Variant)
    {
        switch (shmTier2Variant)
        {
            case ExxoWorldGen.SHMTier2Variant.Unvolandite:
                return ModContent.TileType<Tiles.Ores.UnvolanditeOre>();
            case ExxoWorldGen.SHMTier2Variant.Vorazylcum:
                return ModContent.TileType<Tiles.Ores.VorazylcumOre>();
            default:
                return -1;
        }
    }
}
