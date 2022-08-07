using System.Collections.Generic;
using System.Reflection;
using Avalon.Items.Ore;
using Avalon.Items.Placeable.Tile;
using Avalon.Tiles;
using Avalon.World.Passes;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.WorldBuilding;

namespace Avalon.Systems;

public class ExxoWorldGen : ModSystem
{
    public enum AdamantiteVariant
    {
        Adamantite = 0,
        Titanium = 1,
        Troxinium = 2,
    }

    public enum CobaltVariant
    {
        Cobalt = 0,
        Palladium = 1,
        Duratanium = 2,
    }
    public enum CopperVariant
    {
        Copper = 0,
        Tin = 1,
        Bronze = 2,
    }

    public enum EvilBiome
    {
        Corruption = 0,
        Crimson = 1,
        Contagion = 2,
    }

    public enum GoldVariant
    {
        Gold = 0,
        Platinum = 1,
        Bismuth = 2,
    }


    public enum IronVariant
    {
        Iron = 0,
        Lead = 1,
        Nickel = 2,
    }

    public enum JungleVariant
    {
        Jungle = 0,
        Tropics = 1,
    }

    public enum MythrilVariant
    {
        Mythril = 0,
        Orichalcum = 1,
        Naquadah = 2,
    }

    public enum RhodiumVariant
    {
        Rhodium = 0,
        Osmium = 1,
        Iridium = 2,
    }

    public enum SHMTier1Variant
    {
        Tritanorium = 0,
        Pyroscoric = 1,
    }

    public enum SHMTier2Variant
    {
        Unvolandite = 0,
        Vorazylcum = 1,
    }

    public enum SilverVariant
    {
        Silver = 0,
        Tungsten = 1,
        Zinc = 2,
    }

    public AdamantiteVariant? AdamantiteOre { get; set; }
    public CobaltVariant? CobaltOre { get; set; }

    public CopperVariant? CopperOre { get; set; }

    public int DungeonLocation { get; set; }
    public int DungeonSide { get; set; }
    public GoldVariant? GoldOre { get; set; }
    public int HallowedAltarCount { get; set; }
    public IronVariant? IronOre { get; set; }
    public JungleVariant? JungleMenuSelection { get; set; }
    public int JungleX { get; set; }
    public MythrilVariant? MythrilOre { get; set; }
    public RhodiumVariant? RhodiumOre { get; set; }
    public SHMTier1Variant? SHMTier1Ore { get; set; }
    public SHMTier2Variant? SHMTier2Ore { get; set; }
    public SilverVariant? SilverOre { get; set; }
    public EvilBiome WorldEvil { get; set; }

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
    public override void ModifyHardmodeTasks(List<GenPass> list)
    {
        int index = list.FindIndex(genpass => genpass.Name.Equals("Hardmode Good"));
        list.Insert(index + 2, new PassLegacy("Hardmode snow ore generation", new WorldGenLegacyMethod(SnowHardMode.Method)));
    }
    public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
    {
        GenPass currentPass;

        int reset = tasks.FindIndex(genPass => genPass.Name == "Reset");
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

        int shinies = tasks.FindIndex(genPass => genPass.Name == "Shinies");
        if (shinies != -1)
        {
            currentPass = new OreGenPreHardMode();
            tasks.Insert(shinies + 1, currentPass);
            totalWeight += currentPass.Weight;
        }

        //int smoothWorld = tasks.FindIndex(genPass => genPass.Name == "Smooth World");
        //if (smoothWorld != -1)
        //{
        //    currentPass = new Underworld();
        //    tasks.Insert(smoothWorld + 1, currentPass);
        //    totalWeight += currentPass.Weight;
        //}

        int iceWalls = tasks.FindIndex(genPass => genPass.Name == "Cave Walls");
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
            currentPass = new TropicsVines();
            tasks.Insert(vines + 2, currentPass);
            totalWeight += currentPass.Weight;

            currentPass = new Hooks.DungeonRemoveCrackedBricks();
            tasks.Insert(vines + 3, currentPass);
            totalWeight += currentPass.Weight;

            currentPass = new CrystalMinesPass();
            tasks.Insert(vines + 4, currentPass);
            totalWeight += currentPass.Weight;
        }
        int weeds = tasks.FindIndex(genPass => genPass.Name == "Weeds");
        if (weeds != -1)
        {
            currentPass = new ReplaceChestItems();
            tasks.Insert(weeds + 1, currentPass);
            totalWeight += currentPass.Weight;
        }

        if (WorldEvil == EvilBiome.Contagion)
        {
            int corruption = tasks.FindIndex(genPass => genPass.Name == "Corruption");
            if (corruption != -1)
            {
                currentPass = new Contagion();
                tasks[corruption] = currentPass;
            }
        }
    }

    public override void PostWorldGen() =>
        JungleX = WorldGen.JungleX;

    public override void SaveWorldData(TagCompound tag)
    {
        tag["DungeonSide"] = DungeonSide;
        tag["DungeonLocation"] = DungeonLocation;
        tag["HallowedAltarCount"] = HallowedAltarCount;
        tag["WorldEvil"] = (byte)WorldEvil;
        tag["CopperOre"] = (byte?)CopperOre;
        tag["IronOre"] = (byte?)IronOre;
        tag["SilverOre"] = (byte?)SilverOre;
        tag["GoldOre"] = (byte?)GoldOre;
        tag["RhodiumOre"] = (byte?)RhodiumOre;
        tag["CobaltOre"] = (byte?)CobaltOre;
        tag["MythrilOre"] = (byte?)MythrilOre;
        tag["AdamantiteOre"] = (byte?)AdamantiteOre;
        tag["SHMTier1Ore"] = (byte?)SHMTier1Ore;
        tag["SHMTier2Ore"] = (byte?)SHMTier2Ore;
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
            WorldEvil = WorldGen.crimson ? EvilBiome.Crimson : EvilBiome.Corruption;
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
                    if (!Main.tile[x, y].HasTile || (Main.tile[x, y].TileType != TileID.JungleGrass &&
                                                     Main.tile[x, y].TileType != ModContent.TileType<TropicalGrass>()))
                    {
                        continue;
                    }

                    JungleX = x;
                    found = true;
                    break;
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
    public static int GetRhodiumVariantItemOre(this ExxoWorldGen.RhodiumVariant? rhodiumVariant) =>
        rhodiumVariant switch
        {
            ExxoWorldGen.RhodiumVariant.Osmium => ModContent.ItemType<OsmiumOre>(),
            ExxoWorldGen.RhodiumVariant.Rhodium => ModContent.ItemType<RhodiumOre>(),
            ExxoWorldGen.RhodiumVariant.Iridium => ModContent.ItemType<IridiumOre>(),
            _ => -1,
        };

    public static int GetSHMTier1VariantTileOre(this ExxoWorldGen.SHMTier1Variant? shmTier1Variant) =>
        shmTier1Variant switch
        {
            ExxoWorldGen.SHMTier1Variant.Tritanorium => ModContent.TileType<Tiles.Ores.TritanoriumOre>(),
            ExxoWorldGen.SHMTier1Variant.Pyroscoric => ModContent.TileType<Tiles.Ores.PyroscoricOre>(),
            _ => -1,
        };

    public static int GetSHMTier2VariantTileOre(this ExxoWorldGen.SHMTier2Variant? shmTier2Variant) =>
        shmTier2Variant switch
        {
            ExxoWorldGen.SHMTier2Variant.Unvolandite => ModContent.TileType<Tiles.Ores.UnvolanditeOre>(),
            ExxoWorldGen.SHMTier2Variant.Vorazylcum => ModContent.TileType<Tiles.Ores.VorazylcumOre>(),
            _ => -1,
        };
}
