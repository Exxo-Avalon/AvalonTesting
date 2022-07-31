using Avalon.Items.Accessories;
using Avalon.Items.Ammo;
using Avalon.Items.Armor;
using Avalon.Items.Consumables;
using Avalon.Items.Material;
using Avalon.Items.Placeable.Bar;
using Avalon.Items.Placeable.Beam;
using Avalon.Items.Placeable.Crafting;
using Avalon.Items.Placeable.Furniture;
using Avalon.Items.Placeable.Light;
using Avalon.Items.Placeable.Seed;
using Avalon.Items.Placeable.Statue;
using Avalon.Items.Placeable.Storage;
using Avalon.Items.Placeable.Tile;
using Avalon.Items.Placeable.Wall;
using Avalon.Items.Tools;
using Avalon.Items.Weapons.Magic;
using Avalon.Items.Weapons.Melee;
using Avalon.Items.Weapons.Ranged;
using Avalon.Items.Ore;
using Avalon.Items.Weapons.Throw;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Systems;

public class RecipeCreator : ModSystem
{
    public override void AddRecipes()
    {
        Recipe.Create(ModContent.ItemType<DarkSlimeBathtub>())
            .AddIngredient(ModContent.ItemType<DarkSlimeBlock>(), 14)
            .AddTile(TileID.WorkBenches).Register();

        Recipe.Create(ModContent.ItemType<DarkSlimeBed>())
            .AddIngredient(ModContent.ItemType<DarkSlimeBlock>(), 15)
            .AddIngredient(ItemID.Silk, 5)
            .AddTile(TileID.WorkBenches).Register();

        Recipe.Create(ModContent.ItemType<DarkSlimeBookcase>())
            .AddIngredient(ModContent.ItemType<DarkSlimeBlock>(), 20)
            .AddIngredient(ItemID.Book, 10)
            .AddTile(TileID.WorkBenches).Register();

        Recipe.Create(ModContent.ItemType<DarkSlimeCandelabra>())
            .AddIngredient(ModContent.ItemType<DarkSlimeBlock>(), 5)
            .AddIngredient(ItemID.Torch, 3)
            .AddTile(TileID.WorkBenches).Register();

        Recipe.Create(ModContent.ItemType<DarkSlimeCandle>())
            .AddIngredient(ModContent.ItemType<DarkSlimeBlock>(), 4)
            .AddIngredient(ItemID.Torch)
            .AddTile(TileID.WorkBenches).Register();

        Recipe.Create(ModContent.ItemType<DarkSlimeChair>())
            .AddIngredient(ModContent.ItemType<DarkSlimeBlock>(), 4)
            .AddTile(TileID.WorkBenches).Register();

        Recipe.Create(ModContent.ItemType<DarkSlimeChandelier>())
            .AddIngredient(ModContent.ItemType<DarkSlimeBlock>(), 4)
            .AddIngredient(ItemID.Torch, 4)
            .AddIngredient(ItemID.Chain)
            .AddTile(TileID.WorkBenches).Register();

        Recipe.Create(ModContent.ItemType<DarkSlimeChest>())
            .AddIngredient(ModContent.ItemType<DarkSlimeBlock>(), 8)
            .AddRecipeGroup("IronBar", 2)
            .AddTile(TileID.WorkBenches).Register();

        Recipe.Create(ModContent.ItemType<DarkSlimeClock>())
            .AddRecipeGroup("IronBar", 3)
            .AddIngredient(ItemID.Glass, 6)
            .AddIngredient(ModContent.ItemType<DarkSlimeBlock>(), 10)
            .AddTile(TileID.WorkBenches).Register();

        Recipe.Create(ModContent.ItemType<DarkSlimeDoor>())
            .AddIngredient(ModContent.ItemType<DarkSlimeBlock>(), 6)
            .AddTile(TileID.WorkBenches).Register();

        Recipe.Create(ModContent.ItemType<DarkSlimeDresser>())
            .AddIngredient(ModContent.ItemType<DarkSlimeBlock>(), 16)
            .AddTile(TileID.WorkBenches).Register();

        Recipe.Create(ModContent.ItemType<DarkSlimeLamp>())
            .AddIngredient(ItemID.Torch)
            .AddIngredient(ModContent.ItemType<DarkSlimeBlock>(), 3)
            .AddTile(TileID.WorkBenches).Register();

        Recipe.Create(ModContent.ItemType<DarkSlimeLantern>())
            .AddIngredient(ModContent.ItemType<DarkSlimeBlock>(), 6)
            .AddIngredient(ItemID.Torch)
            .AddTile(TileID.WorkBenches).Register();

        Recipe.Create(ModContent.ItemType<DarkSlimePiano>())
            .AddIngredient(ItemID.Bone, 4)
            .AddIngredient(ModContent.ItemType<DarkSlimeBlock>(), 15)
            .AddIngredient(ItemID.Book)
            .AddTile(TileID.WorkBenches).Register();

        Recipe.Create(ModContent.ItemType<DarkSlimeSofa>())
            .AddIngredient(ModContent.ItemType<DarkSlimeBlock>(), 5)
            .AddIngredient(ItemID.Silk, 2)
            .AddTile(TileID.WorkBenches).Register();

        Recipe.Create(ModContent.ItemType<DarkSlimeWorkBench>())
            .AddIngredient(ModContent.ItemType<DarkSlimeBlock>(), 10).Register();

        Recipe.Create(ModContent.ItemType<DarkSlimeSink>())
            .AddIngredient(ModContent.ItemType<DarkSlimeBlock>(), 6)
            .AddIngredient(ItemID.WaterBucket)
            .AddTile(TileID.WorkBenches).Register();

        Recipe.Create(ModContent.ItemType<HeartstoneBathtub>())
            .AddIngredient(ModContent.ItemType<Heartstone>(), 14)
            .AddTile(TileID.WorkBenches).Register();

        Recipe.Create(ModContent.ItemType<HeartstoneBed>())
            .AddIngredient(ModContent.ItemType<Heartstone>(), 15)
            .AddIngredient(ItemID.Silk, 5)
            .AddTile(TileID.WorkBenches).Register();

        Recipe.Create(ModContent.ItemType<HeartstoneBookcase>())
            .AddIngredient(ModContent.ItemType<Heartstone>(), 20)
            .AddIngredient(ItemID.Book, 10)
            .AddTile(TileID.WorkBenches).Register();

        Recipe.Create(ModContent.ItemType<HeartstoneCandelabra>())
            .AddIngredient(ModContent.ItemType<Heartstone>(), 5)
            .AddIngredient(ItemID.Torch, 3)
            .AddTile(TileID.WorkBenches).Register();

        Recipe.Create(ModContent.ItemType<HeartstoneCandle>())
            .AddIngredient(ModContent.ItemType<Heartstone>(), 4)
            .AddIngredient(ItemID.Torch)
            .AddTile(TileID.WorkBenches).Register();

        Recipe.Create(ModContent.ItemType<HeartstoneChair>())
            .AddIngredient(ModContent.ItemType<Heartstone>(), 4)
            .AddTile(TileID.WorkBenches).Register();

        Recipe.Create(ModContent.ItemType<HeartstoneChandelier>())
            .AddIngredient(ModContent.ItemType<Heartstone>(), 4)
            .AddIngredient(ItemID.Torch, 4)
            .AddIngredient(ItemID.Chain)
            .AddTile(TileID.WorkBenches).Register();

        Recipe.Create(ModContent.ItemType<HeartstoneChest>())
            .AddIngredient(ModContent.ItemType<Heartstone>(), 8)
            .AddRecipeGroup("IronBar", 2)
            .AddTile(TileID.WorkBenches).Register();

        Recipe.Create(ModContent.ItemType<HeartstoneClock>())
            .AddIngredient(ItemID.Glass, 6)
            .AddIngredient(ModContent.ItemType<Heartstone>(), 10)
            .AddRecipeGroup("IronBar", 3)
            .AddTile(TileID.WorkBenches).Register();

        Recipe.Create(ModContent.ItemType<HeartstoneDoor>())
            .AddIngredient(ModContent.ItemType<Heartstone>(), 6)
            .AddTile(TileID.WorkBenches).Register();

        Recipe.Create(ModContent.ItemType<HeartstoneDresser>())
            .AddIngredient(ModContent.ItemType<Heartstone>(), 16)
            .AddTile(TileID.WorkBenches).Register();

        Recipe.Create(ModContent.ItemType<HeartstoneLamp>())
            .AddIngredient(ItemID.Torch)
            .AddIngredient(ModContent.ItemType<Heartstone>(), 3)
            .AddTile(TileID.WorkBenches).Register();

        Recipe.Create(ModContent.ItemType<HeartstoneLantern>())
            .AddIngredient(ModContent.ItemType<Heartstone>(), 6)
            .AddIngredient(ItemID.Torch)
            .AddTile(TileID.WorkBenches).Register();

        Recipe.Create(ModContent.ItemType<HeartstonePiano>())
            .AddIngredient(ItemID.Bone, 4)
            .AddIngredient(ModContent.ItemType<Heartstone>(), 15)
            .AddIngredient(ItemID.Book)
            .AddTile(TileID.WorkBenches).Register();

        Recipe.Create(ModContent.ItemType<HeartstoneSofa>())
            .AddIngredient(ModContent.ItemType<Heartstone>(), 5)
            .AddIngredient(ItemID.Silk, 2)
            .AddTile(TileID.WorkBenches).Register();

        Recipe.Create(ModContent.ItemType<HeartstoneTable>())
            .AddIngredient(ModContent.ItemType<Heartstone>(), 8)
            .AddTile(TileID.WorkBenches).Register();

        Recipe.Create(ModContent.ItemType<HeartstoneWorkBench>())
            .AddIngredient(ModContent.ItemType<Heartstone>(), 10).Register();

        Recipe.Create(ModContent.ItemType<HeartstoneSink>())
            .AddIngredient(ModContent.ItemType<Heartstone>(), 6)
            .AddIngredient(ItemID.WaterBucket)
            .AddTile(TileID.WorkBenches).Register();

        Recipe.Create(ModContent.ItemType<Echoplasm>(), 10)
            .AddIngredient(ItemID.Ectoplasm)
            .AddTile(TileID.WorkBenches).Register();

        Recipe.Create(ModContent.ItemType<EctoplasmBathtub>())
            .AddIngredient(ModContent.ItemType<Echoplasm>(), 14)
            .AddTile(TileID.WorkBenches).Register();

        Recipe.Create(ModContent.ItemType<EctoplasmBed>())
            .AddIngredient(ModContent.ItemType<Echoplasm>(), 15)
            .AddIngredient(ItemID.Silk, 5)
            .AddTile(TileID.WorkBenches).Register();

        Recipe.Create(ModContent.ItemType<EctoplasmBookcase>())
            .AddIngredient(ModContent.ItemType<Echoplasm>(), 20)
            .AddIngredient(ItemID.Book, 10)
            .AddTile(TileID.WorkBenches).Register();

        Recipe.Create(ModContent.ItemType<EctoplasmCandelabra>())
            .AddIngredient(ModContent.ItemType<Echoplasm>(), 5)
            .AddIngredient(ItemID.Torch, 3)
            .AddTile(TileID.WorkBenches).Register();

        Recipe.Create(ModContent.ItemType<EctoplasmCandle>())
            .AddIngredient(ModContent.ItemType<Echoplasm>(), 4)
            .AddIngredient(ItemID.Torch)
            .AddTile(TileID.WorkBenches).Register();

        Recipe.Create(ModContent.ItemType<EctoplasmChair>())
            .AddIngredient(ModContent.ItemType<Echoplasm>(), 4)
            .AddTile(TileID.WorkBenches).Register();

        Recipe.Create(ModContent.ItemType<EctoplasmChandelier>())
            .AddIngredient(ModContent.ItemType<Echoplasm>(), 4)
            .AddIngredient(ItemID.Torch, 4)
            .AddIngredient(ItemID.Chain)
            .AddTile(TileID.WorkBenches).Register();

        Recipe.Create(ModContent.ItemType<EctoplasmClock>())
            .AddIngredient(ItemID.Glass, 6)
            .AddIngredient(ModContent.ItemType<Echoplasm>(), 10)
            .AddRecipeGroup("IronBar", 3)
            .AddTile(TileID.WorkBenches).Register();

        Recipe.Create(ModContent.ItemType<EctoplasmDresser>())
            .AddIngredient(ModContent.ItemType<Echoplasm>(), 16)
            .AddTile(TileID.WorkBenches).Register();

        Recipe.Create(ModContent.ItemType<EctoplasmLamp>())
            .AddIngredient(ItemID.Torch)
            .AddIngredient(ModContent.ItemType<Echoplasm>(), 3)
            .AddTile(TileID.WorkBenches).Register();

        Recipe.Create(ModContent.ItemType<EctoplasmLantern>())
            .AddIngredient(ModContent.ItemType<Echoplasm>(), 6)
            .AddIngredient(ItemID.Torch)
            .AddTile(TileID.WorkBenches).Register();

        Recipe.Create(ModContent.ItemType<EctoplasmPiano>())
            .AddIngredient(ItemID.Bone, 4)
            .AddIngredient(ModContent.ItemType<Echoplasm>(), 15)
            .AddIngredient(ItemID.Book)
            .AddTile(TileID.WorkBenches).Register();

        Recipe.Create(ModContent.ItemType<EctoplasmSofa>())
            .AddIngredient(ModContent.ItemType<Echoplasm>(), 5)
            .AddIngredient(ItemID.Silk, 2)
            .AddTile(TileID.WorkBenches).Register();

        Recipe.Create(ModContent.ItemType<EctoplasmTable>())
            .AddIngredient(ModContent.ItemType<Echoplasm>(), 8)
            .AddTile(TileID.WorkBenches).Register();

        Recipe.Create(ModContent.ItemType<EctoplasmWorkBench>())
            .AddIngredient(ModContent.ItemType<Echoplasm>(), 10).Register();

        Recipe.Create(ModContent.ItemType<EctoplasmSink>())
            .AddIngredient(ModContent.ItemType<Echoplasm>(), 6)
            .AddIngredient(ItemID.WaterBucket)
            .AddTile(TileID.WorkBenches).Register();

        Recipe.Create(ModContent.ItemType<VertebraeBookcase>())
            .AddIngredient(ItemID.Vertebrae, 20)
            .AddIngredient(ItemID.Book, 10)
            .AddTile(TileID.WorkBenches).Register();

        Recipe.Create(ModContent.ItemType<VertebraeCandle>())
            .AddIngredient(ItemID.Vertebrae, 4)
            .AddIngredient(ItemID.Torch)
            .AddTile(TileID.WorkBenches).Register();

        Recipe.Create(ModContent.ItemType<VertebraeChair>())
            .AddIngredient(ItemID.Vertebrae, 4)
            .AddTile(TileID.WorkBenches).Register();

        Recipe.Create(ModContent.ItemType<VertebraeChest>())
            .AddIngredient(ItemID.Vertebrae, 8)
            .AddRecipeGroup("IronBar", 2)
            .AddTile(TileID.WorkBenches).Register();

        Recipe.Create(ModContent.ItemType<VertebraeDoor>())
            .AddIngredient(ItemID.Vertebrae, 6)
            .AddTile(TileID.WorkBenches).Register();

        Recipe.Create(ModContent.ItemType<VertebraeLantern>())
            .AddIngredient(ItemID.Vertebrae, 6)
            .AddIngredient(ItemID.Torch)
            .AddTile(TileID.WorkBenches).Register();

        Recipe.Create(ModContent.ItemType<VertebraeWorkBench>())
            .AddIngredient(ItemID.Vertebrae, 10).Register();

        Recipe.Create(ModContent.ItemType<ResistantWoodWorkBench>())
            .AddIngredient(ModContent.ItemType<ResistantWood>(), 10).Register();

        Recipe.Create(ModContent.ItemType<ResistantWoodBathtub>())
            .AddIngredient(ModContent.ItemType<ResistantWood>(), 14)
            .AddTile(TileID.Anvils).Register();

        Recipe.Create(ModContent.ItemType<ResistantWoodChandelier>())
            .AddIngredient(ModContent.ItemType<ResistantWood>(), 4)
            .AddIngredient(ItemID.Torch, 4)
            .AddIngredient(ItemID.Chain)
            .AddTile(TileID.Anvils).Register();

        Recipe.Create(ModContent.ItemType<ResistantWoodBed>())
            .AddIngredient(ModContent.ItemType<ResistantWood>(), 15)
            .AddIngredient(ItemID.Silk, 5)
            .AddTile(TileID.Sawmill).Register();

        Recipe.Create(ModContent.ItemType<ResistantWoodBookcase>())
            .AddIngredient(ModContent.ItemType<ResistantWood>(), 20)
            .AddIngredient(ItemID.Book, 10)
            .AddTile(TileID.Sawmill).Register();

        Recipe.Create(ModContent.ItemType<ResistantWoodClock>())
            .AddIngredient(ItemID.Glass, 6)
            .AddIngredient(ModContent.ItemType<ResistantWood>(), 10)
            .AddRecipeGroup("IronBar", 3)
            .AddTile(TileID.Sawmill).Register();

        Recipe.Create(ModContent.ItemType<ResistantWoodDresser>())
            .AddIngredient(ModContent.ItemType<ResistantWood>(), 16)
            .AddTile(TileID.Sawmill).Register();

        Recipe.Create(ModContent.ItemType<ResistantWoodPiano>())
            .AddIngredient(ItemID.Bone, 4)
            .AddIngredient(ModContent.ItemType<ResistantWood>(), 15)
            .AddIngredient(ItemID.Book)
            .AddTile(TileID.Sawmill).Register();

        Recipe.Create(ModContent.ItemType<ResistantWoodSofa>())
            .AddIngredient(ModContent.ItemType<ResistantWood>(), 5)
            .AddIngredient(ItemID.Silk, 2)
            .AddTile(TileID.Sawmill).Register();

        Recipe.Create(ModContent.ItemType<ResistantWoodCandelabra>())
            .AddIngredient(ModContent.ItemType<ResistantWood>(), 5)
            .AddIngredient(ItemID.Torch, 3)
            .AddTile(TileID.WorkBenches).Register();

        Recipe.Create(ModContent.ItemType<ResistantWoodCandle>())
            .AddIngredient(ModContent.ItemType<ResistantWood>(), 4)
            .AddIngredient(ItemID.Torch)
            .AddTile(TileID.WorkBenches).Register();

        Recipe.Create(ModContent.ItemType<ResistantWoodChair>())
            .AddIngredient(ModContent.ItemType<ResistantWood>(), 4)
            .AddTile(TileID.WorkBenches).Register();

        Recipe.Create(ModContent.ItemType<ResistantWoodChest>())
            .AddIngredient(ModContent.ItemType<ResistantWood>(), 8)
            .AddRecipeGroup("IronBar", 2)
            .AddTile(TileID.WorkBenches).Register();

        Recipe.Create(ModContent.ItemType<ResistantWoodLamp>())
            .AddIngredient(ItemID.Torch)
            .AddIngredient(ModContent.ItemType<ResistantWood>(), 3)
            .AddTile(TileID.WorkBenches).Register();

        Recipe.Create(ModContent.ItemType<ResistantWoodLantern>())
            .AddIngredient(ModContent.ItemType<ResistantWood>(), 6)
            .AddIngredient(ItemID.Torch)
            .AddTile(TileID.WorkBenches).Register();

        Recipe.Create(ModContent.ItemType<ResistantWoodTable>())
            .AddIngredient(ModContent.ItemType<ResistantWood>(), 8)
            .AddTile(TileID.WorkBenches).Register();

        Recipe.Create(ModContent.ItemType<ResistantWoodSink>())
            .AddIngredient(ModContent.ItemType<ResistantWood>(), 6)
            .AddIngredient(ItemID.WaterBucket)
            .AddTile(TileID.WorkBenches).Register();

        Recipe.Create(ModContent.ItemType<OrangeDungeonSink>())
            .AddIngredient(ModContent.ItemType<OrangeBrick>(), 6)
            .AddIngredient(ItemID.WaterBucket)
            .AddTile(TileID.WorkBenches).Register();

        Recipe.Create(ModContent.ItemType<BronzeChandelier>())
            .AddIngredient(ModContent.ItemType<BronzeBar>(), 4)
            .AddIngredient(ItemID.Torch, 4)
            .AddIngredient(ItemID.Chain)
            .AddTile(TileID.Anvils).Register();

        Recipe.Create(ModContent.ItemType<ZincChandelier>())
            .AddIngredient(ModContent.ItemType<ZincBar>(), 4)
            .AddIngredient(ItemID.Torch, 4)
            .AddIngredient(ItemID.Chain)
            .AddTile(TileID.Anvils).Register();

        Recipe.Create(ModContent.ItemType<BismuthChandelier>())
            .AddIngredient(ModContent.ItemType<BismuthBar>(), 4)
            .AddIngredient(ItemID.Torch, 4)
            .AddIngredient(ItemID.Chain)
            .AddTile(TileID.Anvils).Register();

        Recipe.Create(ModContent.ItemType<BismuthCandle>())
            .AddIngredient(ModContent.ItemType<BismuthBar>())
            .AddIngredient(ItemID.Torch)
            .AddTile(TileID.WorkBenches).Register();

        Recipe.Create(ModContent.ItemType<BismuthCandelabra>())
            .AddIngredient(ModContent.ItemType<BismuthBar>(), 5)
            .AddIngredient(ItemID.Torch, 3)
            .AddTile(TileID.WorkBenches).Register();

        


        Recipe.Create(ModContent.ItemType<CoughwoodHelmet>())
            .AddIngredient(ModContent.ItemType<Coughwood>(), 25)
            .AddTile(TileID.WorkBenches).Register();

        Recipe.Create(ModContent.ItemType<CoughwoodBreastplate>())
            .AddIngredient(ModContent.ItemType<Coughwood>(), 30)
            .AddTile(TileID.WorkBenches).Register();

        Recipe.Create(ModContent.ItemType<CoughwoodGreaves>())
            .AddIngredient(ModContent.ItemType<Coughwood>(), 20)
            .AddTile(TileID.WorkBenches).Register();

        Recipe.Create(ModContent.ItemType<CoughwoodSword>())
            .AddIngredient(ModContent.ItemType<Coughwood>(), 9)
            .AddTile(TileID.WorkBenches).Register();

        Recipe.Create(ModContent.ItemType<CoughwoodHammer>())
            .AddIngredient(ModContent.ItemType<Coughwood>(), 8)
            .AddTile(TileID.WorkBenches).Register();

        Recipe.Create(ModContent.ItemType<CoughwoodBow>())
            .AddIngredient(ModContent.ItemType<Coughwood>(), 8)
            .AddTile(TileID.WorkBenches).Register();

        Recipe.Create(ModContent.ItemType<DarkSlimeBlock>())
            .AddIngredient(ModContent.ItemType<DarkMatterGel>())
            .AddTile(TileID.Solidifier).Register();

        Recipe.Create(ModContent.ItemType<DarkSlimeBlockWall>(), 4)
            .AddIngredient(ModContent.ItemType<DarkSlimeBlock>())
            .AddTile(TileID.WorkBenches).Register();

        

        Recipe.Create(ModContent.ItemType<BlueLihzahrdStatue>())
            .AddIngredient(ModContent.ItemType<BlueLihzahrdBrick>(), 25)
            .AddTile(TileID.WorkBenches).Register();

        

        Recipe.Create(ModContent.ItemType<HellstoneSeed>(), 25)
            .AddIngredient(ItemID.Seed, 25)
            .AddIngredient(ItemID.HellstoneBar)
            .AddTile(TileID.Hellforge).Register();

        

        Recipe.Create(ModContent.ItemType<PurityBomb>())
            .AddIngredient(ItemID.GreenSolution, 15)
            .AddIngredient(ItemID.StoneBlock, 50)
            .AddIngredient(ModContent.ItemType<SoulofBlight>(), 2)
            .AddIngredient(ItemID.Explosives)
            .AddTile(TileID.TinkerersWorkbench).Register();

        Recipe.Create(ModContent.ItemType<CorruptionBomb>())
            .AddIngredient(ItemID.PurpleSolution, 15)
            .AddIngredient(ItemID.EbonstoneBlock, 50)
            .AddIngredient(ModContent.ItemType<SoulofBlight>(), 2)
            .AddIngredient(ItemID.Explosives)
            .AddTile(TileID.TinkerersWorkbench).Register();

        Recipe.Create(ModContent.ItemType<JungleBomb>())
            .AddIngredient(ModContent.ItemType<LimeGreenSolution>(), 15)
            .AddIngredient(ItemID.MudBlock, 50)
            .AddIngredient(ModContent.ItemType<SoulofBlight>(), 2)
            .AddIngredient(ItemID.Explosives)
            .AddTile(TileID.TinkerersWorkbench).Register();

        Recipe.Create(ModContent.ItemType<CrimsonBomb>())
            .AddIngredient(ItemID.RedSolution, 15)
            .AddIngredient(ItemID.CrimstoneBlock, 50)
            .AddIngredient(ModContent.ItemType<SoulofBlight>(), 2)
            .AddIngredient(ItemID.Explosives)
            .AddTile(TileID.TinkerersWorkbench).Register();

        Recipe.Create(ModContent.ItemType<ContagionBomb>())
            .AddIngredient(ModContent.ItemType<YellowSolution>(), 15)
            .AddIngredient(ModContent.ItemType<ChunkstoneBlock>(), 50)
            .AddIngredient(ModContent.ItemType<SoulofBlight>(), 2)
            .AddIngredient(ItemID.Explosives)
            .AddTile(TileID.TinkerersWorkbench).Register();

        Recipe.Create(ModContent.ItemType<MushroomBomb>())
            .AddIngredient(ItemID.DarkBlueSolution, 15)
            .AddIngredient(ItemID.MudBlock, 50)
            .AddIngredient(ModContent.ItemType<SoulofBlight>(), 2)
            .AddIngredient(ItemID.Explosives)
            .AddTile(TileID.TinkerersWorkbench).Register();

        Recipe.Create(ModContent.ItemType<HallowBomb>())
            .AddIngredient(ItemID.BlueSolution, 15)
            .AddIngredient(ItemID.PearlstoneBlock, 50)
            .AddIngredient(ModContent.ItemType<SoulofBlight>(), 2)
            .AddIngredient(ItemID.Explosives)
            .AddTile(TileID.TinkerersWorkbench).Register();

        
        Recipe.Create(ModContent.ItemType<SandCastle>())
            .AddIngredient(ItemID.SandBlock, 50)
            .AddIngredient(ItemID.SandstoneBrick, 5)
            .AddTile(TileID.Anvils).Register();

        Recipe.Create(ModContent.ItemType<CursedFlamelash>())
            .AddIngredient(ItemID.Flamelash)
            .AddIngredient(ItemID.CursedFlame, 99)
            .AddTile(TileID.MythrilAnvil).Register();

        Recipe.Create(ModContent.ItemType<PathogenMist>())
            .AddIngredient(ItemID.SpellTome)
            .AddIngredient(ModContent.ItemType<Pathogen>(), 30)
            .AddIngredient(ItemID.SoulofNight, 15)
            .AddTile(TileID.Bookcases).Register();

        Recipe.Create(ModContent.ItemType<LensApparatus>())
            .AddIngredient(ItemID.Lens, 20)
            .AddIngredient(ModContent.ItemType<BloodshotLens>(), 10)
            .AddIngredient(ItemID.BlackLens)
            .AddTile(TileID.Bookcases).Register();

        Recipe.Create(ModContent.ItemType<DevilsScythe>())
            .AddIngredient(ItemID.DemonScythe)
            .AddIngredient(ItemID.Fireblossom, 20)
            .AddIngredient(ItemID.LivingFireBlock, 100)
            .AddIngredient(ItemID.SoulofFright, 7)
            .AddTile(TileID.Bookcases).Register();

        Recipe.Create(ModContent.ItemType<FocusBeam>())
            .AddIngredient(ItemID.SpellTome)
            .AddIngredient(ItemID.SoulofMight, 20)
            .AddIngredient(ModContent.ItemType<Tourmaline>(), 15)
            .AddIngredient(ModContent.ItemType<LensApparatus>())
            .AddTile(TileID.Bookcases).Register();

        Recipe.Create(ModContent.ItemType<GigaHorn>())
            .AddIngredient(ModContent.ItemType<BrokenVigilanteTome>())
            .AddIngredient(ItemID.SoulofMight, 15)
            .AddIngredient(ModContent.ItemType<Onyx>(), 20)
            .AddTile(TileID.Bookcases).Register();

        //replaceme11111
        //.AddIngredient(ItemID.SpellTome)
        //.AddIngredient(ItemID.Emerald, 15)
        //.AddIngredient(ItemID.SoulofSight, 7)
        //.AddIngredient(ItemID.Lens, 3)
        //.AddTile(TileID.Bookcases)
        //.SetResult(ModContent.ItemType<Items.Venoshock>())
        //.AddRecipe()

        Recipe.Create(ModContent.ItemType<MusicBoxEssence>())
            .AddIngredient(ItemID.MusicBoxTitle)
            .AddIngredient(ItemID.MusicBoxSnow)
            .AddIngredient(ItemID.MusicBoxDesert)
            .AddIngredient(ItemID.MusicBoxSpace)
            .AddIngredient(ItemID.MusicBoxCrimson)
            .AddIngredient(ItemID.MusicBoxDungeon)
            .AddIngredient(ItemID.MusicBoxPlantera)
            .AddIngredient(ItemID.MusicBoxTemple)
            .AddIngredient(ItemID.MusicBoxEclipse)
            .AddIngredient(ItemID.MusicBoxPumpkinMoon)
            .AddIngredient(ItemID.MusicBoxFrostMoon)
            .AddTile(TileID.Bookcases).Register();

        Recipe.Create(ModContent.ItemType<Jukebox>())
            .AddIngredient(ItemID.MusicBox, 2)
            .AddIngredient(ItemID.PixieDust, 20)
            .AddIngredient(ItemID.SoulofFlight, 10)
            .AddIngredient(ModContent.ItemType<MusicBoxEssence>())
            .AddTile(TileID.Bookcases).Register();

        Recipe.Create(ModContent.ItemType<Heartstone>(), 45)
            .AddIngredient(ItemID.LifeCrystal)
            .AddTile(TileID.Furnaces).Register();

        Recipe.Create(ItemID.LifeCrystal)
            .AddIngredient(ModContent.ItemType<Heartstone>(), 45)
            .AddTile(TileID.Furnaces).Register();

        Recipe.Create(ModContent.ItemType<Shurikerang>())
            .AddIngredient(ItemID.EnchantedBoomerang)
            .AddIngredient(ItemID.Shuriken, 50)
            .AddTile(TileID.Anvils).Register();

        Recipe.Create(ItemID.EnchantedBoomerang)
            .AddIngredient(ModContent.ItemType<EnchantedBar>(), 12)
            .AddTile(TileID.Anvils).Register();

        Recipe.Create(ModContent.ItemType<EnchantedShuriken>(), 25)
            .AddIngredient(ModContent.ItemType<EnchantedBar>())
            .AddTile(TileID.Anvils).Register();

        Recipe.Create(ItemID.EnchantedSword)
            .AddIngredient(ModContent.ItemType<EnchantedBar>(), 20)
            .AddIngredient(ModContent.ItemType<BrokenHiltPiece>(), 3)
            .AddTile(TileID.Anvils).Register();

        Recipe.Create(ModContent.ItemType<InfestedCarcass>())
            .AddIngredient(ModContent.ItemType<YuckyBit>(), 15)
            .AddIngredient(ModContent.ItemType<VirulentPowder>(), 30)
            .AddTile(TileID.DemonAltar).Register();

        Recipe.Create(ModContent.ItemType<TheBeak>())
            .AddIngredient(ModContent.ItemType<Beak>(), 6)
            .AddIngredient(ItemID.SandBlock, 30)
            .AddTile(TileID.DemonAltar).Register();

        Recipe.Create(ModContent.ItemType<HornetFood>())
            .AddIngredient(ModContent.ItemType<ToxinShard>(), 30)
            .AddIngredient(ItemID.Stinger, 15)
            .AddTile(TileID.DemonAltar).Register();

        

        Recipe.Create(ModContent.ItemType<OddFertilizer>())
            .AddIngredient(ModContent.ItemType<LifeDew>(), 5)
            .AddIngredient(ItemID.Stinger, 15)
            .AddIngredient(ItemID.JungleSpores, 15)
            .AddIngredient(ItemID.SoulofMight, 5)
            .AddIngredient(ItemID.SoulofFright, 5)
            .AddTile(TileID.MythrilAnvil).Register();

        

        Recipe.Create(ModContent.ItemType<TimechangerMkII>())
            .AddIngredient(ModContent.ItemType<SoulofTime>(), 40)
            .AddIngredient(ModContent.ItemType<SoulofBlight>())
            .AddIngredient(ModContent.ItemType<Timechanger>())
            .AddTile(TileID.MythrilAnvil).Register();

        

        Recipe.Create(ModContent.ItemType<OblivionBrick>())
            .AddIngredient(ItemID.StoneBlock)
            .AddIngredient(ModContent.ItemType<Items.Ore.OblivionOre>())
            .AddTile(ModContent.TileType<Tiles.CaesiumForge>()).Register();

        

        Recipe.Create(ModContent.ItemType<PyroscoricBrick>(), 3)
            .AddIngredient(ModContent.ItemType<PyroscoricOre>())
            .AddIngredient(ItemID.StoneBlock, 3)
            .AddTile(ModContent.TileType<Tiles.CaesiumForge>()).Register();

        Recipe.Create(ModContent.ItemType<PyroscoricBar>())
            .AddIngredient(ModContent.ItemType<PyroscoricOre>(), 5)
            .AddTile(ModContent.TileType<Tiles.CaesiumForge>()).Register();

        Recipe.Create(ModContent.ItemType<PyroscoricFlamesword>())
            .AddIngredient(ModContent.ItemType<PyroscoricBar>(), 20)
            .AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();

        Recipe.Create(ModContent.ItemType<PyroscoricPickaxe>())
            .AddIngredient(ModContent.ItemType<PyroscoricBar>(), 25)
            .AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();

        Recipe.Create(ModContent.ItemType<PyroscoricBullet>(), 70)
            .AddIngredient(ModContent.ItemType<PyroscoricBar>())
            .AddIngredient(ItemID.MusketBall, 70)
            .AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();

        Recipe.Create(ModContent.ItemType<TritanoriumBar>())
            .AddIngredient(ModContent.ItemType<TritanoriumOre>(), 5)
            .AddTile(ModContent.TileType<Tiles.CaesiumForge>()).Register();

        Recipe.Create(ModContent.ItemType<TritanoriumBroadsword>())
            .AddIngredient(ModContent.ItemType<TritanoriumBar>(), 35)
            .AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();

        Recipe.Create(ModContent.ItemType<TritanoriumPickaxe>())
            .AddIngredient(ModContent.ItemType<TritanoriumBar>(), 25)
            .AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();

        Recipe.Create(ModContent.ItemType<TritonBullet>(), 70)
            .AddIngredient(ModContent.ItemType<TritanoriumBar>())
            .AddIngredient(ItemID.MusketBall, 70)
            .AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();

        Recipe.Create(ModContent.ItemType<UnvolanditeBar>())
            .AddIngredient(ModContent.ItemType<UnvolanditeOre>(), 6)
            .AddTile(ModContent.TileType<Tiles.CaesiumForge>()).Register();

        Recipe.Create(ModContent.ItemType<UnvolanditeGreatsword>())
            .AddIngredient(ModContent.ItemType<UnvolanditeBar>(), 24)
            .AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();

        Recipe.Create(ModContent.ItemType<UnvolanditeKunziteWaveStaff>())
            .AddIngredient(ModContent.ItemType<UnvolanditeBar>(), 15)
            .AddIngredient(ModContent.ItemType<Kunzite>(), 9)
            .AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();

        Recipe.Create(ModContent.ItemType<UnvolanditeFusebow>())
            .AddIngredient(ItemID.PulseBow)
            .AddIngredient(ModContent.ItemType<UnvolanditeBar>(), 13)
            .AddIngredient(ModContent.ItemType<Kunzite>(), 5)
            .AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();

        Recipe.Create(ModContent.ItemType<UnvolanditeHeadpiece>())
            .AddIngredient(ModContent.ItemType<UnvolanditeBar>(), 40)
            .AddIngredient(ModContent.ItemType<Kunzite>(), 17)
            .AddIngredient(ModContent.ItemType<SpikedBlastShell>(), 2)
            .AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();

        Recipe.Create(ModContent.ItemType<UnvolanditeBodyplate>())
            .AddIngredient(ModContent.ItemType<UnvolanditeBar>(), 46)
            .AddIngredient(ModContent.ItemType<Kunzite>(), 10)
            .AddIngredient(ModContent.ItemType<SpikedBlastShell>(), 3)
            .AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();

        Recipe.Create(ModContent.ItemType<UnvolanditeLeggings>())
            .AddIngredient(ModContent.ItemType<UnvolanditeBar>(), 32)
            .AddIngredient(ModContent.ItemType<Kunzite>(), 8)
            .AddIngredient(ModContent.ItemType<SpikedBlastShell>(), 2)
            .AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();

        Recipe.Create(ModContent.ItemType<VorazylcumBar>())
            .AddIngredient(ModContent.ItemType<VorazylcumOre>(), 5)
            .AddTile(ModContent.TileType<Tiles.CaesiumForge>()).Register();

        Recipe.Create(ModContent.ItemType<VoraylzumKatana>())
            .AddIngredient(ModContent.ItemType<VorazylcumBar>(), 22)
            .AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();

        Recipe.Create(ModContent.ItemType<VorazylcumKunziteBoltStaff>())
            .AddIngredient(ModContent.ItemType<VorazylcumBar>(), 15)
            .AddIngredient(ModContent.ItemType<Kunzite>(), 9)
            .AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();

        Recipe.Create(ModContent.ItemType<VorazylcumFusebow>())
            .AddIngredient(ItemID.PulseBow)
            .AddIngredient(ModContent.ItemType<VorazylcumBar>(), 13)
            .AddIngredient(ModContent.ItemType<Kunzite>(), 5)
            .AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();

        Recipe.Create(ModContent.ItemType<VorazylcumHeadpiece>())
            .AddIngredient(ModContent.ItemType<VorazylcumBar>(), 40)
            .AddIngredient(ModContent.ItemType<Kunzite>(), 17)
            .AddIngredient(ModContent.ItemType<SpikedBlastShell>(), 3)
            .AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();

        Recipe.Create(ModContent.ItemType<VorazylcumBodyplate>())
            .AddIngredient(ModContent.ItemType<VorazylcumBar>(), 46)
            .AddIngredient(ModContent.ItemType<Kunzite>(), 10)
            .AddIngredient(ModContent.ItemType<SpikedBlastShell>(), 4)
            .AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();

        Recipe.Create(ModContent.ItemType<VorazylcumLeggings>())
            .AddIngredient(ModContent.ItemType<VorazylcumBar>(), 32)
            .AddIngredient(ModContent.ItemType<Kunzite>(), 8)
            .AddIngredient(ModContent.ItemType<SpikedBlastShell>())
            .AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();

        Recipe.Create(ModContent.ItemType<Elektriwave>())
            .AddIngredient(ModContent.ItemType<Phantoplasm>(), 30)
            .AddIngredient(ModContent.ItemType<HydrolythBar>(), 30)
            .AddIngredient(ModContent.ItemType<SoulofTorture>(), 20)
            .AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();


        Recipe.Create(ModContent.ItemType<Electrobullet>(), 200)
            .AddIngredient(ItemID.MusketBall, 200)
            .AddIngredient(ModContent.ItemType<Phantoplasm>())
            .AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();

        Recipe.Create(ModContent.ItemType<BlahsHelmet>())
            .AddIngredient(ModContent.ItemType<Phantoplasm>(), 30)
            .AddIngredient(ModContent.ItemType<SuperhardmodeBar>(), 30)
            .AddIngredient(ModContent.ItemType<SoulofTorture>(), 15)
            .AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();

        Recipe.Create(ModContent.ItemType<BlahsBodyarmor>())
            .AddIngredient(ModContent.ItemType<Phantoplasm>(), 35)
            .AddIngredient(ModContent.ItemType<SuperhardmodeBar>(), 35)
            .AddIngredient(ModContent.ItemType<SoulofTorture>(), 20)
            .AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();

        Recipe.Create(ModContent.ItemType<BlahsGreaves>())
            .AddIngredient(ModContent.ItemType<Phantoplasm>(), 30)
            .AddIngredient(ModContent.ItemType<SuperhardmodeBar>(), 25)
            .AddIngredient(ModContent.ItemType<SoulofTorture>(), 17)
            .AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();

        Recipe.Create(ModContent.ItemType<BlahsPicksaw>())
            .AddIngredient(ModContent.ItemType<Phantoplasm>(), 50)
            .AddIngredient(ModContent.ItemType<SuperhardmodeBar>(), 25)
            .AddIngredient(ModContent.ItemType<SoulofTorture>(), 25)
            .AddIngredient(ModContent.ItemType<InstantaniumPicksaw>())
            .AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();

        Recipe.Create(ModContent.ItemType<BlahsWarhammer>())
            .AddIngredient(ModContent.ItemType<Phantoplasm>(), 25)
            .AddIngredient(ModContent.ItemType<SuperhardmodeBar>(), 25)
            .AddIngredient(ModContent.ItemType<SoulofTorture>(), 25)
            .AddIngredient(ModContent.ItemType<TheBanhammer>())
            .AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();


        Recipe.Create(ModContent.ItemType<SoulEdge>())
            .AddIngredient(ItemID.TerraBlade)
            .AddIngredient(ItemID.SpectreStaff)
            .AddIngredient(ModContent.ItemType<SoulofBlight>(), 40)
            .AddIngredient(ModContent.ItemType<SuperhardmodeBar>(), 5)
            .AddIngredient(ItemID.Ectoplasm, 60)
            .AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();

        Recipe.Create(ModContent.ItemType<OblivionGlaive>())
            .AddIngredient(ModContent.ItemType<OblivionBar>(), 30)
            .AddIngredient(ModContent.ItemType<SoulofBlight>(), 10)
            .AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();

        Recipe.Create(ModContent.ItemType<Oblivirod>())
            .AddIngredient(ModContent.ItemType<OblivionBar>(), 10)
            .AddIngredient(ModContent.ItemType<SoulofBlight>(), 2)
            .AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();

        Recipe.Create(ModContent.ItemType<OpalStaff>())
            .AddIngredient(ModContent.ItemType<Opal>(), 40)
            .AddIngredient(ModContent.ItemType<OblivionBar>(), 10)
            .AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();

        Recipe.Create(ModContent.ItemType<OnyxStaff>())
            .AddIngredient(ModContent.ItemType<Onyx>(), 25)
            .AddIngredient(ModContent.ItemType<HydrolythBar>(), 10)
            .AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();

        Recipe.Create(ModContent.ItemType<ElementalRod>())
            .AddIngredient(ModContent.ItemType<CaesiumBar>(), 30)
            .AddIngredient(ModContent.ItemType<ElementShard>(), 20)
            .AddIngredient(ItemID.Flamelash)
            .AddIngredient(ItemID.MagicMissile)
            .AddIngredient(ItemID.DirtRod)
            .AddIngredient(ItemID.RainbowRod)
            .AddTile(ModContent.TileType<Tiles.CaesiumForge>()).Register();

        


        Recipe.Create(ModContent.ItemType<AwakenedRoseCrown>())
            .AddIngredient(ModContent.ItemType<SoulofHumidity>(), 20)
            .AddIngredient(ModContent.ItemType<DarkMatterGel>(), 60)
            .AddIngredient(ModContent.ItemType<AncientHeadpiece>())
            .AddIngredient(ModContent.ItemType<LifeDew>(), 15)
            .AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();

        Recipe.Create(ModContent.ItemType<AwakenedRosePlateMail>())
            .AddIngredient(ModContent.ItemType<SoulofHumidity>(), 23)
            .AddIngredient(ModContent.ItemType<DarkMatterGel>(), 65)
            .AddIngredient(ModContent.ItemType<AncientBodyplate>())
            .AddIngredient(ModContent.ItemType<LifeDew>(), 15)
            .AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();

        Recipe.Create(ModContent.ItemType<AwakenedRoseSubligar>())
            .AddIngredient(ModContent.ItemType<SoulofHumidity>(), 17)
            .AddIngredient(ModContent.ItemType<DarkMatterGel>(), 70)
            .AddIngredient(ModContent.ItemType<AncientLeggings>())
            .AddIngredient(ModContent.ItemType<LifeDew>(), 15)
            .AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();


        

        Recipe.Create(ModContent.ItemType<MiloticCrown>())
            .AddIngredient(ModContent.ItemType<HydrolythBar>(), 20)
            .AddIngredient(ModContent.ItemType<SoulofBlight>(), 3)
            .AddIngredient(ModContent.ItemType<DarkMatterGel>(), 60)
            .AddIngredient(ModContent.ItemType<AncientHeadpiece>())
            .AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();

        Recipe.Create(ModContent.ItemType<MiloticSkinplate>())
            .AddIngredient(ModContent.ItemType<HydrolythBar>(), 23)
            .AddIngredient(ModContent.ItemType<SoulofBlight>(), 4)
            .AddIngredient(ModContent.ItemType<DarkMatterGel>(), 75)
            .AddIngredient(ModContent.ItemType<AncientBodyplate>())
            .AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();

        Recipe.Create(ModContent.ItemType<MiloticJodpurs>())
            .AddIngredient(ModContent.ItemType<HydrolythBar>(), 17)
            .AddIngredient(ModContent.ItemType<SoulofBlight>(), 3)
            .AddIngredient(ModContent.ItemType<DarkMatterGel>(), 70)
            .AddIngredient(ModContent.ItemType<AncientLeggings>())
            .AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();

        #region Avalon Armor

        Recipe.Create(ModContent.ItemType<AvalonHelmet>())
            .AddIngredient(ModContent.ItemType<SoulofTorture>(), 50)
            .AddIngredient(ModContent.ItemType<VictoryPiece>())
            .AddIngredient(ModContent.ItemType<BerserkerHeadpiece>())
            .AddIngredient(ModContent.ItemType<SuperhardmodeBar>(), 2)
            .AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();

        Recipe.Create(ModContent.ItemType<AvalonBodyarmor>())
            .AddIngredient(ModContent.ItemType<SoulofTorture>(), 50)
            .AddIngredient(ModContent.ItemType<VictoryPiece>())
            .AddIngredient(ModContent.ItemType<BerserkerBodyarmor>())
            .AddIngredient(ModContent.ItemType<SuperhardmodeBar>(), 2)
            .AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();

        Recipe.Create(ModContent.ItemType<AvalonCuisses>())
            .AddIngredient(ModContent.ItemType<SoulofTorture>(), 50)
            .AddIngredient(ModContent.ItemType<VictoryPiece>())
            .AddIngredient(ModContent.ItemType<BerserkerCuisses>())
            .AddIngredient(ModContent.ItemType<SuperhardmodeBar>(), 2)
            .AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();

        Recipe.Create(ModContent.ItemType<AvalonHelmet>())
            .AddIngredient(ModContent.ItemType<SoulofTorture>(), 50)
            .AddIngredient(ModContent.ItemType<VictoryPiece>())
            .AddIngredient(ModContent.ItemType<AwakenedRoseCrown>())
            .AddIngredient(ModContent.ItemType<SuperhardmodeBar>(), 2)
            .AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();

        Recipe.Create(ModContent.ItemType<AvalonBodyarmor>())
            .AddIngredient(ModContent.ItemType<SoulofTorture>(), 50)
            .AddIngredient(ModContent.ItemType<VictoryPiece>())
            .AddIngredient(ModContent.ItemType<AwakenedRosePlateMail>())
            .AddIngredient(ModContent.ItemType<SuperhardmodeBar>(), 2)
            .AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();

        Recipe.Create(ModContent.ItemType<AvalonCuisses>())
            .AddIngredient(ModContent.ItemType<SoulofTorture>(), 50)
            .AddIngredient(ModContent.ItemType<VictoryPiece>())
            .AddIngredient(ModContent.ItemType<AwakenedRoseSubligar>())
            .AddIngredient(ModContent.ItemType<SuperhardmodeBar>(), 2)
            .AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();

        Recipe.Create(ModContent.ItemType<AvalonHelmet>())
            .AddIngredient(ModContent.ItemType<SoulofTorture>(), 50)
            .AddIngredient(ModContent.ItemType<VictoryPiece>())
            .AddIngredient(ModContent.ItemType<SpectrumHelmet>())
            .AddIngredient(ModContent.ItemType<SuperhardmodeBar>(), 2)
            .AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();

        Recipe.Create(ModContent.ItemType<AvalonBodyarmor>())
            .AddIngredient(ModContent.ItemType<SoulofTorture>(), 50)
            .AddIngredient(ModContent.ItemType<VictoryPiece>())
            .AddIngredient(ModContent.ItemType<SpectrumBreastplate>())
            .AddIngredient(ModContent.ItemType<SuperhardmodeBar>(), 2)
            .AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();

        Recipe.Create(ModContent.ItemType<AvalonCuisses>())
            .AddIngredient(ModContent.ItemType<SoulofTorture>(), 50)
            .AddIngredient(ModContent.ItemType<VictoryPiece>())
            .AddIngredient(ModContent.ItemType<SpectrumGreaves>())
            .AddIngredient(ModContent.ItemType<SuperhardmodeBar>(), 2)
            .AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();

        Recipe.Create(ModContent.ItemType<AvalonHelmet>())
            .AddIngredient(ModContent.ItemType<SoulofTorture>(), 50)
            .AddIngredient(ModContent.ItemType<VictoryPiece>())
            .AddIngredient(ModContent.ItemType<MiloticCrown>())
            .AddIngredient(ModContent.ItemType<SuperhardmodeBar>(), 2)
            .AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();

        Recipe.Create(ModContent.ItemType<AvalonBodyarmor>())
            .AddIngredient(ModContent.ItemType<SoulofTorture>(), 50)
            .AddIngredient(ModContent.ItemType<VictoryPiece>())
            .AddIngredient(ModContent.ItemType<MiloticSkinplate>())
            .AddIngredient(ModContent.ItemType<SuperhardmodeBar>(), 2)
            .AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();

        Recipe.Create(ModContent.ItemType<AvalonCuisses>())
            .AddIngredient(ModContent.ItemType<SoulofTorture>(), 50)
            .AddIngredient(ModContent.ItemType<VictoryPiece>())
            .AddIngredient(ModContent.ItemType<MiloticJodpurs>())
            .AddIngredient(ModContent.ItemType<SuperhardmodeBar>(), 2)
            .AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();

        #endregion Avalon Armor

        Recipe.Create(ModContent.ItemType<RedPhasecleaver>())
            .AddIngredient(ItemID.RedPhasesaber)
            .AddIngredient(ModContent.ItemType<HydrolythBar>(), 25)
            .AddTile(ModContent.TileType<Tiles.CaesiumForge>()).Register();

        Recipe.Create(ModContent.ItemType<YellowPhasecleaver>())
            .AddIngredient(ItemID.YellowPhasesaber)
            .AddIngredient(ModContent.ItemType<HydrolythBar>(), 25)
            .AddTile(ModContent.TileType<Tiles.CaesiumForge>()).Register();

        Recipe.Create(ModContent.ItemType<GreenPhasecleaver>())
            .AddIngredient(ItemID.GreenPhasesaber)
            .AddIngredient(ModContent.ItemType<HydrolythBar>(), 25)
            .AddTile(ModContent.TileType<Tiles.CaesiumForge>()).Register();

        Recipe.Create(ModContent.ItemType<BluePhasecleaver>())
            .AddIngredient(ItemID.BluePhasesaber)
            .AddIngredient(ModContent.ItemType<HydrolythBar>(), 25)
            .AddTile(ModContent.TileType<Tiles.CaesiumForge>()).Register();

        Recipe.Create(ModContent.ItemType<PurplePhasecleaver>())
            .AddIngredient(ItemID.PurplePhasesaber)
            .AddIngredient(ModContent.ItemType<HydrolythBar>(), 25)
            .AddTile(ModContent.TileType<Tiles.CaesiumForge>()).Register();

        Recipe.Create(ModContent.ItemType<WhitePhasecleaver>())
            .AddIngredient(ItemID.WhitePhasesaber)
            .AddIngredient(ModContent.ItemType<HydrolythBar>(), 25)
            .AddTile(ModContent.TileType<Tiles.CaesiumForge>()).Register();

        Recipe.Create(ModContent.ItemType<FieryBladeofGrass>())
            .AddIngredient(ItemID.BladeofGrass)
            .AddIngredient(ItemID.FieryGreatsword)
            .AddTile(TileID.Anvils).Register();

        Recipe.Create(ModContent.ItemType<HallowedAltar>())
            .AddIngredient(ItemID.HallowedBar, 10)
            .AddIngredient(ItemID.LightShard, 2)
            .AddIngredient(ModContent.ItemType<SacredShard>(), 2)
            .AddIngredient(ItemID.PixieDust, 20)
            .AddIngredient(ItemID.PearlstoneBlock, 20)
            .AddIngredient(ModContent.ItemType<SoulofBlight>())
            .AddTile(TileID.MythrilAnvil).Register();

        Recipe.Create(ModContent.ItemType<DemonAltar>())
            .AddIngredient(ItemID.EbonstoneBlock, 50)
            .AddIngredient(ItemID.RottenChunk, 10)
            .AddIngredient(ItemID.Deathweed, 5)
            .AddIngredient(ItemID.SoulofNight, 20)
            .AddIngredient(ItemID.ShadowScale, 20)
            .AddIngredient(ItemID.DemoniteBar, 25)
            .AddTile(TileID.MythrilAnvil).Register();

        Recipe.Create(ModContent.ItemType<CrimsonAltar>())
            .AddIngredient(ItemID.CrimstoneBlock, 50)
            .AddIngredient(ItemID.Vertebrae, 10)
            .AddIngredient(ModContent.ItemType<Bloodberry>(), 5)
            .AddIngredient(ItemID.SoulofNight, 20)
            .AddIngredient(ItemID.TissueSample, 20)
            .AddIngredient(ItemID.CrimtaneBar, 25)
            .AddTile(TileID.MythrilAnvil).Register();

        Recipe.Create(ModContent.ItemType<IckyAltar>())
            .AddIngredient(ModContent.ItemType<ChunkstoneBlock>(), 50)
            .AddIngredient(ModContent.ItemType<YuckyBit>(), 10)
            .AddIngredient(ModContent.ItemType<Barfbush>(), 5)
            .AddIngredient(ItemID.SoulofNight, 20)
            .AddIngredient(ModContent.ItemType<Booger>(), 20)
            .AddIngredient(ModContent.ItemType<PandemiteBar>(), 25)
            .AddTile(TileID.MythrilAnvil).Register();

        

        

        Recipe.Create(ModContent.ItemType<SunsShadow>())
            .AddIngredient(ItemID.BeetleHusk, 8)
            .AddIngredient(ItemID.TurtleShell)
            .AddIngredient(ItemID.ChlorophyteBar, 3)
            .AddTile(TileID.MythrilAnvil).Register();

        #region catalyzer

        //start stone types
        Recipe.Create(ItemID.EbonstoneBlock, 50)
            .AddIngredient(ItemID.PearlstoneBlock, 50)
            .AddIngredient(ModContent.ItemType<Sulphur>())
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.CrimstoneBlock, 50)
            .AddIngredient(ItemID.EbonstoneBlock, 50)
            .AddIngredient(ModContent.ItemType<Sulphur>())
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ModContent.ItemType<ChunkstoneBlock>(), 50)
            .AddIngredient(ItemID.CrimstoneBlock, 50)
            .AddIngredient(ModContent.ItemType<Sulphur>())
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.PearlstoneBlock, 50)
            .AddIngredient(ModContent.ItemType<ChunkstoneBlock>(), 50)
            .AddIngredient(ModContent.ItemType<Sulphur>())
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();
        //end stone types
        //start wood
        Recipe.Create(ItemID.Wood, 50)
            .AddIngredient(ModContent.ItemType<ApocalyptusWood>(), 50)
            .AddIngredient(ModContent.ItemType<Sulphur>())
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.Ebonwood, 50)
            .AddIngredient(ItemID.Wood, 50)
            .AddIngredient(ModContent.ItemType<Sulphur>())
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.Shadewood, 50)
            .AddIngredient(ItemID.Ebonwood, 50)
            .AddIngredient(ModContent.ItemType<Sulphur>())
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ModContent.ItemType<Coughwood>(), 50)
            .AddIngredient(ItemID.Shadewood, 50)
            .AddIngredient(ModContent.ItemType<Sulphur>())
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.Pearlwood, 50)
            .AddIngredient(ModContent.ItemType<Coughwood>(), 50)
            .AddIngredient(ModContent.ItemType<Sulphur>())
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.BorealWood, 50)
            .AddIngredient(ItemID.Pearlwood, 50)
            .AddIngredient(ModContent.ItemType<Sulphur>())
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.PalmWood, 50)
            .AddIngredient(ItemID.BorealWood, 50)
            .AddIngredient(ModContent.ItemType<Sulphur>())
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.RichMahogany, 50)
            .AddIngredient(ItemID.PalmWood, 50)
            .AddIngredient(ModContent.ItemType<Sulphur>())
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ModContent.ItemType<BleachedEbony>(), 50)
            .AddIngredient(ItemID.RichMahogany, 50)
            .AddIngredient(ModContent.ItemType<Sulphur>())
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ModContent.ItemType<ApocalyptusWood>(), 50)
            .AddIngredient(ModContent.ItemType<BleachedEbony>(), 50)
            .AddIngredient(ModContent.ItemType<Sulphur>())
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();
        //end wood
        //evil ores
        Recipe.Create(ItemID.DemoniteOre, 40)
            .AddIngredient(ModContent.ItemType<PandemiteOre>(), 40)
            .AddIngredient(ModContent.ItemType<Sulphur>())
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.CrimtaneOre, 40)
            .AddIngredient(ItemID.DemoniteOre, 40)
            .AddIngredient(ModContent.ItemType<Sulphur>())
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ModContent.ItemType<PandemiteOre>(), 40)
            .AddIngredient(ItemID.CrimtaneOre, 40)
            .AddIngredient(ModContent.ItemType<Sulphur>())
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();
        //end evil ores
        //hardmode ores
        Recipe.Create(ItemID.CobaltOre, 20)
            .AddIngredient(ModContent.ItemType<DurataniumOre>(), 20)
            .AddIngredient(ModContent.ItemType<Sulphur>())
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.PalladiumOre, 20)
            .AddIngredient(ItemID.CobaltOre, 20)
            .AddIngredient(ModContent.ItemType<Sulphur>())
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ModContent.ItemType<DurataniumOre>(), 20)
            .AddIngredient(ItemID.PalladiumOre, 20)
            .AddIngredient(ModContent.ItemType<Sulphur>())
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.MythrilOre, 20)
            .AddIngredient(ModContent.ItemType<NaquadahOre>(), 20)
            .AddIngredient(ModContent.ItemType<Sulphur>(), 2)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.OrichalcumOre, 20)
            .AddIngredient(ItemID.MythrilOre, 20)
            .AddIngredient(ModContent.ItemType<Sulphur>(), 2)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ModContent.ItemType<NaquadahOre>(), 20)
            .AddIngredient(ItemID.OrichalcumOre, 20)
            .AddIngredient(ModContent.ItemType<Sulphur>(), 2)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.AdamantiteOre, 10)
            .AddIngredient(ModContent.ItemType<TroxiniumOre>(), 10)
            .AddIngredient(ModContent.ItemType<Sulphur>(), 2)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.TitaniumOre, 10)
            .AddIngredient(ItemID.AdamantiteOre, 10)
            .AddIngredient(ModContent.ItemType<Sulphur>(), 2)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ModContent.ItemType<TroxiniumOre>(), 10)
            .AddIngredient(ItemID.TitaniumOre, 10)
            .AddIngredient(ModContent.ItemType<Sulphur>(), 2)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();
        //end hardmode ores
        //evil bars
        Recipe.Create(ItemID.DemoniteBar, 10)
            .AddIngredient(ModContent.ItemType<PandemiteBar>(), 10)
            .AddIngredient(ModContent.ItemType<Sulphur>())
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.CrimtaneBar, 10)
            .AddIngredient(ItemID.DemoniteBar, 10)
            .AddIngredient(ModContent.ItemType<Sulphur>())
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ModContent.ItemType<PandemiteBar>(), 10)
            .AddIngredient(ItemID.CrimtaneBar, 10)
            .AddIngredient(ModContent.ItemType<Sulphur>())
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();
        //end evil bars

        //phm ores
        Recipe.Create(ItemID.CopperOre, 30)
            .AddIngredient(ModContent.ItemType<BronzeOre>(), 30)
            .AddIngredient(ModContent.ItemType<Sulphur>())
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.TinOre, 30)
            .AddIngredient(ItemID.CopperOre, 30)
            .AddIngredient(ModContent.ItemType<Sulphur>())
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ModContent.ItemType<BronzeOre>(), 30)
            .AddIngredient(ItemID.TinOre, 30)
            .AddIngredient(ModContent.ItemType<Sulphur>())
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.IronOre, 30)
            .AddIngredient(ModContent.ItemType<NickelOre>(), 30)
            .AddIngredient(ModContent.ItemType<Sulphur>())
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.LeadOre, 30)
            .AddIngredient(ItemID.IronOre, 30)
            .AddIngredient(ModContent.ItemType<Sulphur>())
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ModContent.ItemType<NickelOre>(), 30)
            .AddIngredient(ItemID.LeadOre, 30)
            .AddIngredient(ModContent.ItemType<Sulphur>())
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.SilverOre, 40)
            .AddIngredient(ModContent.ItemType<ZincOre>(), 40)
            .AddIngredient(ModContent.ItemType<Sulphur>())
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.TungstenOre, 40)
            .AddIngredient(ItemID.SilverOre, 40)
            .AddIngredient(ModContent.ItemType<Sulphur>())
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ModContent.ItemType<ZincOre>(), 40)
            .AddIngredient(ItemID.TungstenOre, 40)
            .AddIngredient(ModContent.ItemType<Sulphur>())
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.GoldOre, 40)
            .AddIngredient(ModContent.ItemType<BismuthOre>(), 40)
            .AddIngredient(ModContent.ItemType<Sulphur>())
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.PlatinumOre, 40)
            .AddIngredient(ItemID.GoldOre, 40)
            .AddIngredient(ModContent.ItemType<Sulphur>())
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ModContent.ItemType<BismuthOre>(), 40)
            .AddIngredient(ItemID.PlatinumOre, 40)
            .AddIngredient(ModContent.ItemType<Sulphur>())
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();
        //end phm ores

        //phm bars
        Recipe.Create(ItemID.CopperBar, 10)
            .AddIngredient(ModContent.ItemType<BronzeBar>(), 10)
            .AddIngredient(ModContent.ItemType<Sulphur>())
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.TinBar, 10)
            .AddIngredient(ItemID.CopperBar, 10)
            .AddIngredient(ModContent.ItemType<Sulphur>())
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ModContent.ItemType<BronzeBar>(), 10)
            .AddIngredient(ItemID.TinBar, 10)
            .AddIngredient(ModContent.ItemType<Sulphur>())
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.IronBar, 10)
            .AddIngredient(ModContent.ItemType<NickelBar>(), 10)
            .AddIngredient(ModContent.ItemType<Sulphur>())
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.LeadBar, 10)
            .AddRecipeGroup("IronBar", 10)
            .AddIngredient(ModContent.ItemType<Sulphur>())
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ModContent.ItemType<NickelBar>(), 10)
            .AddIngredient(ItemID.LeadBar, 10)
            .AddIngredient(ModContent.ItemType<Sulphur>())
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.SilverBar, 10)
            .AddIngredient(ModContent.ItemType<ZincBar>(), 10)
            .AddIngredient(ModContent.ItemType<Sulphur>())
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.TungstenBar, 10)
            .AddIngredient(ItemID.SilverBar, 10)
            .AddIngredient(ModContent.ItemType<Sulphur>())
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ModContent.ItemType<ZincBar>(), 10)
            .AddIngredient(ItemID.TungstenBar, 10)
            .AddIngredient(ModContent.ItemType<Sulphur>())
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.GoldBar, 10)
            .AddIngredient(ModContent.ItemType<BismuthBar>(), 10)
            .AddIngredient(ModContent.ItemType<Sulphur>())
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.PlatinumBar, 10)
            .AddIngredient(ItemID.GoldBar, 10)
            .AddIngredient(ModContent.ItemType<Sulphur>())
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ModContent.ItemType<BismuthBar>(), 10)
            .AddIngredient(ItemID.PlatinumBar, 10)
            .AddIngredient(ModContent.ItemType<Sulphur>())
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();
        //end phm bars

        //hardmode ore bars
        Recipe.Create(ItemID.CobaltBar, 10)
            .AddIngredient(ModContent.ItemType<DurataniumBar>(), 10)
            .AddIngredient(ModContent.ItemType<Sulphur>())
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.PalladiumBar, 10)
            .AddIngredient(ItemID.CobaltBar, 10)
            .AddIngredient(ModContent.ItemType<Sulphur>())
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ModContent.ItemType<DurataniumBar>(), 10)
            .AddIngredient(ItemID.PalladiumBar, 10)
            .AddIngredient(ModContent.ItemType<Sulphur>())
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.MythrilBar, 10)
            .AddIngredient(ModContent.ItemType<NaquadahBar>(), 10)
            .AddIngredient(ModContent.ItemType<Sulphur>(), 2)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.OrichalcumBar, 10)
            .AddIngredient(ItemID.MythrilBar, 10)
            .AddIngredient(ModContent.ItemType<Sulphur>(), 2)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ModContent.ItemType<NaquadahBar>(), 10)
            .AddIngredient(ItemID.OrichalcumBar, 10)
            .AddIngredient(ModContent.ItemType<Sulphur>(), 2)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.AdamantiteBar, 5)
            .AddIngredient(ModContent.ItemType<TroxiniumBar>(), 5)
            .AddIngredient(ModContent.ItemType<Sulphur>(), 2)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.TitaniumBar, 5)
            .AddIngredient(ItemID.AdamantiteBar, 5)
            .AddIngredient(ModContent.ItemType<Sulphur>(), 2)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ModContent.ItemType<TroxiniumBar>(), 5)
            .AddIngredient(ItemID.TitaniumBar, 5)
            .AddIngredient(ModContent.ItemType<Sulphur>(), 2)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();
        //end hardmode ore bars
        //evil boss materials
        Recipe.Create(ItemID.ShadowScale, 3)
            .AddIngredient(ModContent.ItemType<Booger>(), 3)
            .AddIngredient(ModContent.ItemType<Sulphur>())
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.TissueSample, 3)
            .AddIngredient(ItemID.ShadowScale, 3)
            .AddIngredient(ModContent.ItemType<Sulphur>())
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ModContent.ItemType<Booger>(), 3)
            .AddIngredient(ItemID.TissueSample, 3)
            .AddIngredient(ModContent.ItemType<Sulphur>())
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();
        //end evil boss materials

        Recipe.Create(ItemID.CursedFlame, 33)
            .AddIngredient(ModContent.ItemType<Pathogen>(), 33)
            .AddIngredient(ModContent.ItemType<Sulphur>())
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.Ichor, 33)
            .AddIngredient(ItemID.CursedFlame, 33)
            .AddIngredient(ModContent.ItemType<Sulphur>())
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ModContent.ItemType<Pathogen>(), 33)
            .AddIngredient(ItemID.Ichor, 33)
            .AddIngredient(ModContent.ItemType<Sulphur>())
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.RottenChunk, 50)
            .AddIngredient(ModContent.ItemType<YuckyBit>(), 50)
            .AddIngredient(ModContent.ItemType<Sulphur>())
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.Vertebrae, 50)
            .AddIngredient(ItemID.RottenChunk, 50)
            .AddIngredient(ModContent.ItemType<Sulphur>())
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ModContent.ItemType<YuckyBit>(), 50)
            .AddIngredient(ItemID.Vertebrae, 50)
            .AddIngredient(ModContent.ItemType<Sulphur>())
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.GreenSolution, 100)
            .AddIngredient(ModContent.ItemType<LimeGreenSolution>(), 100)
            .AddIngredient(ModContent.ItemType<Sulphur>(), 10)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.PurpleSolution, 100)
            .AddIngredient(ItemID.GreenSolution, 100)
            .AddIngredient(ModContent.ItemType<Sulphur>(), 10)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.RedSolution, 100)
            .AddIngredient(ItemID.PurpleSolution, 100)
            .AddIngredient(ModContent.ItemType<Sulphur>(), 10)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ModContent.ItemType<YellowSolution>(), 100)
            .AddIngredient(ItemID.RedSolution, 100)
            .AddIngredient(ModContent.ItemType<Sulphur>(), 10)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.BlueSolution, 100)
            .AddIngredient(ModContent.ItemType<YellowSolution>(), 100)
            .AddIngredient(ModContent.ItemType<Sulphur>(), 10)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.DarkBlueSolution, 100)
            .AddIngredient(ItemID.BlueSolution, 100)
            .AddIngredient(ModContent.ItemType<Sulphur>(), 10)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ModContent.ItemType<LimeGreenSolution>(), 100)
            .AddIngredient(ItemID.DarkBlueSolution, 100)
            .AddIngredient(ModContent.ItemType<Sulphur>(), 10)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        #endregion catalyzer
    }
}
