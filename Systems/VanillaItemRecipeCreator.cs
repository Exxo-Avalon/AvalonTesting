using Avalon.Items.Accessories;
using Avalon.Items.Material;
using Avalon.Items.Ore;
using Avalon.Items.Placeable.Bar;
using Avalon.Items.Placeable.Tile;
using Avalon.Items.Weapons.Melee;
using Avalon.Items.Weapons.Throw;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Systems;

public class VanillaItemRecipeCreator : ModSystem
{
    public override void AddRecipes()
    {
        Recipe.Create(ItemID.SlimeCrown).AddIngredient(ModContent.ItemType<Items.Vanity.BismuthCrown>()).AddIngredient(ItemID.Gel, 20).AddTile(TileID.DemonAltar).Register();
        foreach (int i in AvalonGlobalNPC.Emblems)
            Recipe.Create(i).AddIngredient(ModContent.ItemType<NullEmblem>()).AddTile(TileID.TinkerersWorkbench).Register();
        Recipe.Create(ItemID.Aglet).AddRecipeGroup("Avalon:CopperBar").AddRecipeGroup("Wood", 6).AddTile(TileID.Anvils).Register();
        Recipe.Create(ItemID.IronskinPotion).AddIngredient(ItemID.BottledWater).AddIngredient(ItemID.Daybloom).AddIngredient(ModContent.ItemType<NickelOre>()).AddTile(TileID.Bottles).Register();
        Recipe.Create(ItemID.SpelunkerPotion).AddIngredient(ItemID.BottledWater).AddIngredient(ItemID.Blinkroot).AddIngredient(ItemID.Moonglow).AddIngredient(ModContent.ItemType<BismuthOre>()).AddTile(TileID.Bottles).Register();
        Recipe.Create(ItemID.PeaceCandle).AddIngredient(ModContent.ItemType<Items.Placeable.Bar.BismuthBar>(), 2).AddIngredient(ItemID.PinkTorch).AddTile(TileID.WorkBenches).Register();
        Recipe.Create(ItemID.NightsEdge).AddIngredient(ModContent.ItemType<Snotsabre>()).AddIngredient(ItemID.Muramasa).AddIngredient(ItemID.BladeofGrass).AddIngredient(ItemID.FieryGreatsword).AddTile(TileID.DemonAltar).Register();
        Recipe.Create(ItemID.MagicMirror).AddIngredient(ItemID.RecallPotion, 3).AddRecipeGroup("IronBar", 5).AddIngredient(ItemID.Glass, 20).AddTile(TileID.Furnaces).Register();
        Recipe.Create(ItemID.GuideVoodooDoll).AddIngredient(ItemID.Silk, 5).AddIngredient(ModContent.ItemType<FleshyTendril>(), 5).AddIngredient(ItemID.SoulofNight, 5).AddTile(TileID.TinkerersWorkbench).Register();
        Recipe.Create(ItemID.MagicPowerPotion).AddIngredient(ItemID.BottledWater).AddIngredient(ItemID.Moonglow).AddIngredient(ModContent.ItemType<Bloodberry>()).AddIngredient(ItemID.FallenStar).AddTile(TileID.Bottles).Register();
        Recipe.Create(ItemID.MagicPowerPotion).AddIngredient(ItemID.BottledWater).AddIngredient(ItemID.Moonglow).AddIngredient(ModContent.ItemType<Barfbush>()).AddIngredient(ItemID.FallenStar).AddTile(TileID.Bottles).Register();
        Recipe.Create(ItemID.BattlePotion).AddIngredient(ItemID.BottledWater).AddIngredient(ModContent.ItemType<Bloodberry>()).AddIngredient(ItemID.Vertebrae).AddTile(TileID.Bottles).Register();
        Recipe.Create(ItemID.BattlePotion).AddIngredient(ItemID.BottledWater).AddIngredient(ModContent.ItemType<Barfbush>()).AddIngredient(ModContent.ItemType<YuckyBit>()).AddTile(TileID.Bottles).Register();
        Recipe.Create(ItemID.ThornsPotion).AddIngredient(ItemID.BottledWater).AddIngredient(ItemID.Deathweed).AddIngredient(ItemID.Cactus).AddIngredient(ItemID.WormTooth).AddIngredient(ModContent.ItemType<MosquitoProboscis>()).AddTile(TileID.Bottles).Register();
        Recipe.Create(ItemID.ThornsPotion).AddIngredient(ItemID.BottledWater).AddIngredient(ModContent.ItemType<Bloodberry>()).AddIngredient(ItemID.Cactus).AddIngredient(ItemID.WormTooth).AddIngredient(ItemID.Stinger).AddTile(TileID.Bottles).Register();
        Recipe.Create(ItemID.ThornsPotion).AddIngredient(ItemID.BottledWater).AddIngredient(ModContent.ItemType<Barfbush>()).AddIngredient(ItemID.Cactus).AddIngredient(ItemID.WormTooth).AddIngredient(ItemID.Stinger).AddTile(TileID.Bottles).Register();
        Recipe.Create(ItemID.ThornsPotion).AddIngredient(ItemID.BottledWater).AddIngredient(ModContent.ItemType<Bloodberry>()).AddIngredient(ItemID.Cactus).AddIngredient(ItemID.WormTooth).AddIngredient(ModContent.ItemType<MosquitoProboscis>()).AddTile(TileID.Bottles).Register();
        Recipe.Create(ItemID.ThornsPotion).AddIngredient(ItemID.BottledWater).AddIngredient(ModContent.ItemType<Barfbush>()).AddIngredient(ItemID.Cactus).AddIngredient(ItemID.WormTooth).AddIngredient(ModContent.ItemType<MosquitoProboscis>()).AddTile(TileID.Bottles).Register();
        Recipe.Create(ItemID.GravitationPotion).AddIngredient(ItemID.BottledWater).AddIngredient(ItemID.Fireblossom).AddIngredient(ModContent.ItemType<Bloodberry>()).AddIngredient(ItemID.Blinkroot).AddIngredient(ItemID.Feather).AddTile(TileID.Bottles).Register();
        Recipe.Create(ItemID.GravitationPotion).AddIngredient(ItemID.BottledWater).AddIngredient(ItemID.Fireblossom).AddIngredient(ModContent.ItemType<Barfbush>()).AddIngredient(ItemID.Blinkroot).AddIngredient(ItemID.Feather).AddTile(TileID.Bottles).Register();
        Recipe.Create(ItemID.CratePotion).AddIngredient(ItemID.BottledWater).AddIngredient(ItemID.Amber).AddIngredient(ItemID.Moonglow).AddIngredient(ItemID.Blinkroot).AddIngredient(ModContent.ItemType<Bloodberry>()).AddTile(TileID.Bottles).Register();
        Recipe.Create(ItemID.CratePotion).AddIngredient(ItemID.BottledWater).AddIngredient(ItemID.Amber).AddIngredient(ItemID.Moonglow).AddIngredient(ItemID.Blinkroot).AddIngredient(ModContent.ItemType<Barfbush>()).AddTile(TileID.Bottles).Register();
        Recipe.Create(ItemID.TitanPotion).AddIngredient(ItemID.BottledWater).AddIngredient(ItemID.Bone).AddIngredient(ModContent.ItemType<Bloodberry>()).AddIngredient(ItemID.Shiverthorn).AddTile(TileID.Bottles).Register();
        Recipe.Create(ItemID.TitanPotion).AddIngredient(ItemID.BottledWater).AddIngredient(ItemID.Bone).AddIngredient(ModContent.ItemType<Barfbush>()).AddIngredient(ItemID.Shiverthorn).AddTile(TileID.Bottles).Register();
        Recipe.Create(ItemID.RagePotion).AddIngredient(ItemID.BottledWater).AddIngredient(ItemID.Hemopiranha).AddIngredient(ModContent.ItemType<Bloodberry>()).AddTile(TileID.Bottles).Register();
        Recipe.Create(ItemID.RagePotion).AddIngredient(ItemID.BottledWater).AddIngredient(ItemID.Hemopiranha).AddIngredient(ModContent.ItemType<Barfbush>()).AddTile(TileID.Bottles).Register();
        Recipe.Create(ItemID.WrathPotion).AddIngredient(ItemID.BottledWater).AddIngredient(ItemID.Ebonkoi).AddIngredient(ModContent.ItemType<Bloodberry>()).AddTile(TileID.Bottles).Register();
        Recipe.Create(ItemID.WrathPotion).AddIngredient(ItemID.BottledWater).AddIngredient(ItemID.Ebonkoi).AddIngredient(ModContent.ItemType<Barfbush>()).AddTile(TileID.Bottles).Register();
        Recipe.Create(ItemID.RecallPotion).AddIngredient(ItemID.BottledWater).AddIngredient(ItemID.SpecularFish).AddIngredient(ModContent.ItemType<Bloodberry>()).AddTile(TileID.Bottles).Register();
        Recipe.Create(ItemID.RecallPotion).AddIngredient(ItemID.BottledWater).AddIngredient(ItemID.SpecularFish).AddIngredient(ModContent.ItemType<Barfbush>()).AddTile(TileID.Bottles).Register();
        Recipe.Create(ModContent.ItemType<Items.Potions.ForceFieldPotion>()).AddIngredient(ModContent.ItemType<BottledLava>()).AddIngredient(ItemID.SoulofNight, 3).AddIngredient(ModContent.ItemType<Sweetstem>(), 2).AddIngredient(ItemID.Hellstone).AddTile(TileID.Bottles).Register();
        Recipe.Create(ItemID.StinkPotion).AddIngredient(ItemID.BottledWater).AddIngredient(ItemID.Stinkfish).AddIngredient(ModContent.ItemType<Bloodberry>()).AddTile(TileID.Bottles).Register();
        Recipe.Create(ItemID.StinkPotion).AddIngredient(ItemID.BottledWater).AddIngredient(ItemID.Stinkfish).AddIngredient(ModContent.ItemType<Barfbush>()).AddTile(TileID.Bottles).Register();
        Recipe.Create(ItemID.IceSkates).AddIngredient(ItemID.Leather, 6).AddRecipeGroup("IronBar", 4).AddIngredient(ModContent.ItemType<FrostShard>(), 2).AddTile(TileID.TinkerersWorkbench).Register();
        Recipe.Create(ItemID.WaterWalkingBoots).AddIngredient(ItemID.Leather, 7).AddIngredient(ItemID.WaterWalkingPotion, 10).AddIngredient(ModContent.ItemType<WaterShard>(), 2).AddTile(TileID.TinkerersWorkbench).Register();
        Recipe.Create(ItemID.LavaCharm).AddIngredient(ItemID.ObsidianSkull).AddIngredient(ItemID.ObsidianSkinPotion, 10).AddIngredient(ModContent.ItemType<FireShard>(), 5).AddTile(TileID.TinkerersWorkbench).Register();
        Recipe.Create(ItemID.Starfury).AddIngredient(ItemID.GoldBroadsword).AddIngredient(ItemID.MeteoriteBar, 10).AddIngredient(ItemID.FallenStar, 20).AddTile(TileID.Anvils).Register();
        Recipe.Create(ItemID.Starfury).AddIngredient(ItemID.PlatinumBroadsword).AddIngredient(ItemID.MeteoriteBar, 10).AddIngredient(ItemID.FallenStar, 20).AddTile(TileID.Anvils).Register();
        Recipe.Create(ItemID.Starfury).AddIngredient(ModContent.ItemType<BismuthBroadsword>()).AddIngredient(ItemID.MeteoriteBar, 10).AddIngredient(ItemID.FallenStar, 20).AddTile(TileID.Anvils).Register();
        Recipe.Create(ItemID.AnkletoftheWind).AddIngredient(ItemID.Cloud, 25).AddIngredient(ModContent.ItemType<BreezeShard>(), 3).AddIngredient(ItemID.JungleSpores, 20).AddTile(TileID.TinkerersWorkbench).Register();
        Recipe.Create(ItemID.AnkletoftheWind).AddIngredient(ItemID.Cloud, 25).AddIngredient(ModContent.ItemType<BreezeShard>(), 3).AddIngredient(ModContent.ItemType<TropicalShroomCap>(), 20).AddTile(TileID.TinkerersWorkbench).Register();
        Recipe.Create(ItemID.CloudinaBottle).AddIngredient(ItemID.Bottle).AddIngredient(ItemID.Cloud, 30).AddIngredient(ItemID.Feather, 2).AddIngredient(ModContent.ItemType<BreezeShard>()).AddTile(TileID.TinkerersWorkbench).Register();
        Recipe.Create(ModContent.ItemType<LightninginaBottle>()).AddIngredient(ItemID.Bottle).AddIngredient(ModContent.ItemType<BlastShard>(), 3).AddIngredient(ModContent.ItemType<SacredShard>(), 2).AddIngredient(ItemID.SoulofFright, 15).AddTile(TileID.TinkerersWorkbench).Register();
        Recipe.Create(ItemID.SandstorminaBottle).AddIngredient(ItemID.Bottle).AddIngredient(ItemID.SandBlock, 50).AddIngredient(ModContent.ItemType<EarthShard>(), 5).AddIngredient(ModContent.ItemType<BreezeShard>(), 5).AddTile(TileID.TinkerersWorkbench).Register();
        Recipe.Create(ItemID.BlizzardinaBottle).AddIngredient(ItemID.Bottle).AddIngredient(ItemID.IceBlock, 50).AddIngredient(ModContent.ItemType<FrostShard>(), 5).AddIngredient(ModContent.ItemType<BreezeShard>(), 5).AddTile(TileID.TinkerersWorkbench).Register();
        Recipe.Create(ItemID.FlyingCarpet).AddIngredient(ItemID.Bottle).AddIngredient(ItemID.Silk, 20).AddIngredient(ItemID.Cloud, 25).AddIngredient(ItemID.SoulofFlight, 2).AddTile(TileID.TinkerersWorkbench).Register();
        Recipe.Create(ItemID.BandofStarpower).AddIngredient(ItemID.ManaCrystal, 3).AddIngredient(ItemID.Shackle, 2).AddIngredient(ModContent.ItemType<CorruptShard>(), 2).AddTile(TileID.TinkerersWorkbench).Register();
        Recipe.Create(ItemID.BandofRegeneration).AddIngredient(ItemID.LifeCrystal, 3).AddIngredient(ItemID.Shackle, 2).AddIngredient(ItemID.HealingPotion, 2).AddTile(TileID.TinkerersWorkbench).Register();
        Recipe.Create(ItemID.IceBlade).AddIngredient(ItemID.GoldBroadsword).AddIngredient(ModContent.ItemType<Icicle>(), 50).AddIngredient(ItemID.FallenStar, 8).AddIngredient(ModContent.ItemType<FrostShard>(), 4).AddTile(TileID.IceMachine).Register();
        Recipe.Create(ItemID.IceBlade).AddIngredient(ItemID.PlatinumBroadsword).AddIngredient(ModContent.ItemType<Icicle>(), 50).AddIngredient(ItemID.FallenStar, 8).AddIngredient(ModContent.ItemType<FrostShard>(), 4).AddTile(TileID.IceMachine).Register();
        Recipe.Create(ItemID.IceBlade).AddIngredient(ModContent.ItemType<BismuthBroadsword>()).AddIngredient(ModContent.ItemType<Icicle>(), 50).AddIngredient(ItemID.FallenStar, 8).AddIngredient(ModContent.ItemType<FrostShard>(), 4).AddTile(TileID.IceMachine).Register();
        Recipe.Create(ItemID.Extractinator).AddRecipeGroup("IronBar", 30).AddIngredient(ItemID.Glass, 5).AddIngredient(ItemID.Wire, 20).AddIngredient(ItemID.Timer1Second).AddTile(TileID.TinkerersWorkbench).Register();
        Recipe.Create(ItemID.HermesBoots).AddIngredient(ItemID.OldShoe).AddIngredient(ItemID.SwiftnessPotion, 2).AddIngredient(ItemID.Cloud, 60).AddIngredient(ModContent.ItemType<BreezeShard>(), 2).AddTile(TileID.TinkerersWorkbench).Register();
        Recipe.Create(ItemID.RodofDiscord).AddIngredient(ModContent.ItemType<ChaosDust>(), 45).AddIngredient(ItemID.SoulofLight, 25).AddIngredient(ItemID.Diamond, 10).AddIngredient(ItemID.SoulofMight).AddIngredient(ItemID.SoulofFright).AddIngredient(ItemID.SoulofSight).AddTile(TileID.TinkerersWorkbench).Register();
        Recipe.Create(ItemID.LihzahrdPowerCell).AddIngredient(ModContent.ItemType<SolariumStar>(), 5).AddIngredient(ItemID.LihzahrdBrick, 10).AddTile(TileID.MythrilAnvil).Register();
        Recipe.Create(ItemID.Leather).AddIngredient(ModContent.ItemType<RottenFlesh>(), 4).AddTile(TileID.WorkBenches).Register();
        Recipe.Create(ItemID.Leather).AddIngredient(ModContent.ItemType<YuckyBit>(), 6).AddTile(TileID.WorkBenches).Register();
        Recipe.Create(ItemID.Picksaw).AddIngredient(ModContent.ItemType<SolariumStar>(), 50).AddIngredient(ModContent.ItemType<Items.Placeable.Bar.BeetleBar>(), 3).AddIngredient(ItemID.SoulofMight, 15).AddTile(TileID.MythrilAnvil).Register();
        Recipe.Create(ModContent.ItemType<Items.Potions.BeeRepellent>(), 2).AddIngredient(ItemID.BottledHoney, 2).AddIngredient(ItemID.SoulofFlight).AddTile(TileID.Bottles).Register();
        Recipe.Create(ItemID.SunplateBlock, 2).AddIngredient(ItemID.GoldOre).AddIngredient(ItemID.Cloud).AddTile(TileID.Furnaces).Register();
        Recipe.Create(ItemID.BlinkrootSeeds, 2).AddIngredient(ItemID.StoneBlock, 5).AddIngredient(ItemID.Torch, 2).AddIngredient(ItemID.Seed, 8).AddTile(ModContent.TileType<Tiles.SeedFabricator>()).Register();
        Recipe.Create(ItemID.DaybloomSeeds, 2).AddIngredient(ItemID.Mushroom, 4).AddIngredient(ItemID.DirtBlock, 6).AddIngredient(ItemID.Seed, 8).AddTile(ModContent.TileType<Tiles.SeedFabricator>()).Register();
        Recipe.Create(ItemID.DeathweedSeeds, 2).AddIngredient(ItemID.DemoniteOre, 5).AddIngredient(ItemID.EbonstoneBlock, 5).AddIngredient(ItemID.Seed, 8).AddTile(ModContent.TileType<Tiles.SeedFabricator>()).Register();
        Recipe.Create(ItemID.MoonglowSeeds, 2).AddIngredient(ItemID.MudBlock, 6).AddIngredient(ItemID.GlowingMushroom).AddIngredient(ItemID.Seed, 8).AddTile(ModContent.TileType<Tiles.SeedFabricator>()).Register();
        Recipe.Create(ItemID.WaterleafSeeds, 2).AddIngredient(ItemID.SandBlock, 6).AddIngredient(ItemID.SharkFin).AddIngredient(ItemID.Seed, 8).AddTile(ModContent.TileType<Tiles.SeedFabricator>()).Register();
        Recipe.Create(ItemID.GrassSeeds, 2).AddIngredient(ItemID.DirtBlock, 2).AddIngredient(ItemID.Mushroom).AddIngredient(ItemID.Seed, 8).AddTile(ModContent.TileType<Tiles.SeedFabricator>()).Register();
        Recipe.Create(ItemID.CorruptSeeds, 2).AddIngredient(ItemID.GrassSeeds, 2).AddIngredient(ItemID.EbonstoneBlock, 5).AddIngredient(ItemID.DemoniteOre, 3).AddIngredient(ItemID.Seed, 8).AddTile(ModContent.TileType<Tiles.SeedFabricator>()).Register();
        Recipe.Create(ItemID.CrimsonSeeds, 2).AddIngredient(ItemID.GrassSeeds, 2).AddIngredient(ItemID.CrimstoneBlock, 5).AddIngredient(ItemID.CrimtaneOre, 3).AddIngredient(ItemID.Seed, 8).AddTile(ModContent.TileType<Tiles.SeedFabricator>()).Register();
        Recipe.Create(ItemID.HallowedSeeds, 2).AddIngredient(ItemID.GrassSeeds, 2).AddIngredient(ItemID.PearlstoneBlock, 5).AddIngredient(ModContent.ItemType<HallowedOre>(), 3).AddIngredient(ItemID.Seed, 8).AddTile(ModContent.TileType<Tiles.SeedFabricator>()).Register();
        Recipe.Create(ItemID.MushroomGrassSeeds, 2).AddIngredient(ItemID.GrassSeeds, 2).AddIngredient(ItemID.MudBlock, 5).AddIngredient(ItemID.GlowingMushroom, 6).AddIngredient(ItemID.Seed, 8).AddTile(ModContent.TileType<Tiles.SeedFabricator>()).Register();
        Recipe.Create(ItemID.JungleGrassSeeds, 2).AddIngredient(ItemID.GrassSeeds, 2).AddIngredient(ItemID.MudBlock, 5).AddIngredient(ItemID.JungleSpores, 6).AddIngredient(ItemID.Seed, 8).AddTile(ModContent.TileType<Tiles.SeedFabricator>()).Register();
        Recipe.Create(ItemID.Spike, 20).AddRecipeGroup("IronBar", 3).AddIngredient(ItemID.Bone, 10).AddTile(TileID.Anvils).Register();
        Recipe.Create(ItemID.WoodenSpike, 40).AddRecipeGroup("Wood", 40).AddIngredient(ItemID.BeetleHusk).AddTile(TileID.Anvils).Register();
        Recipe.Create(ItemID.ClayBlock, 50).AddIngredient(ItemID.DirtBlock, 50).AddIngredient(ModContent.ItemType<RottenFlesh>()).AddIngredient(ItemID.IronOre).AddCondition(Recipe.Condition.NearWater).Register();
        Recipe.Create(ItemID.ClayBlock, 50).AddIngredient(ItemID.DirtBlock, 50).AddIngredient(ModContent.ItemType<RottenFlesh>()).AddIngredient(ItemID.LeadOre).AddCondition(Recipe.Condition.NearWater).Register();
        Recipe.Create(ItemID.ClayBlock, 50).AddIngredient(ItemID.DirtBlock, 50).AddIngredient(ModContent.ItemType<RottenFlesh>()).AddIngredient(ModContent.ItemType<NickelOre>()).AddCondition(Recipe.Condition.NearWater).Register();
        Recipe.Create(ItemID.FireblossomSeeds, 3).AddIngredient(ItemID.AshBlock, 8).AddIngredient(ItemID.Hellstone, 2).AddIngredient(ItemID.Seed, 8).AddTile(ModContent.TileType<Tiles.SeedFabricator>()).Register();
        // end seed fabricator stuff
        Recipe.Create(ItemID.AncientBattleArmorHat).AddIngredient(ModContent.ItemType<Items.Placeable.Bar.TroxiniumBar>(), 10).AddIngredient(ItemID.AncientBattleArmorMaterial).AddTile(TileID.MythrilAnvil).Register();
        Recipe.Create(ItemID.AncientBattleArmorShirt).AddIngredient(ModContent.ItemType<Items.Placeable.Bar.TroxiniumBar>(), 20).AddIngredient(ItemID.AncientBattleArmorMaterial).AddTile(TileID.MythrilAnvil).Register();
        Recipe.Create(ItemID.AncientBattleArmorPants).AddIngredient(ModContent.ItemType<Items.Placeable.Bar.TroxiniumBar>(), 16).AddIngredient(ItemID.AncientBattleArmorMaterial).AddTile(TileID.MythrilAnvil).Register();
		Recipe.Create(ItemID.GravediggerShovel).AddIngredient(ModContent.ItemType<Items.Placeable.Bar.NickelBar>(), 12).AddRecipeGroup("Wood", 3).AddTile(TileID.Anvils).AddCondition(Recipe.Condition.InGraveyardBiome).Register();
		Recipe.Create(ItemID.Magiluminescence).AddIngredient(ItemID.Topaz, 5).AddIngredient(ModContent.ItemType<Items.Placeable.Bar.PandemiteBar>(), 12).AddTile(TileID.Anvils).Register();
        Recipe.Create(ItemID.FlinxStaff).AddIngredient(ItemID.FlinxFur, 6).AddIngredient(ModContent.ItemType<Items.Placeable.Bar.BismuthBar>(), 10).AddTile(TileID.WorkBenches).Register();
        Recipe.Create(ItemID.FlinxFurCoat).AddIngredient(ItemID.Silk, 10).AddIngredient(ItemID.FlinxFur, 8).AddIngredient(ModContent.ItemType<Items.Placeable.Bar.BismuthBar>(), 8).AddTile(TileID.Loom).Register();
        Recipe.Create(ItemID.DeerThing).AddIngredient(ItemID.FlinxFur, 3).AddIngredient(ModContent.ItemType<PandemiteOre>(), 5).AddIngredient(ItemID.Lens).AddTile(TileID.DemonAltar).Register();
        Recipe.Create(ItemID.MonsterLasagna).AddIngredient(ModContent.ItemType<YuckyBit>(), 8).AddTile(TileID.CookingPots).Register();
        Recipe.Create(ItemID.CoffinMinecart).AddRecipeGroup("IronBar", 5).AddRecipeGroup("Wood", 10).AddIngredient(ModContent.ItemType<YuckyBit>(), 10).AddTile(TileID.Anvils).AddCondition(Recipe.Condition.InGraveyardBiome).Register();
        Recipe.Create(ItemID.VoidLens).AddIngredient(ItemID.Bone, 30).AddIngredient(ItemID.JungleSpores, 15).AddIngredient(ModContent.ItemType<Booger>(), 30).AddTile(TileID.DemonAltar).Register();
        Recipe.Create(ItemID.VoidLens).AddIngredient(ItemID.Bone, 30).AddIngredient(ModContent.ItemType<TropicalShroomCap>(), 15).AddIngredient(ItemID.ShadowScale, 30).AddTile(TileID.DemonAltar).Register();
        Recipe.Create(ItemID.VoidLens).AddIngredient(ItemID.Bone, 30).AddIngredient(ModContent.ItemType<TropicalShroomCap>(), 15).AddIngredient(ItemID.Vertebrae, 30).AddTile(TileID.DemonAltar).Register();
        Recipe.Create(ItemID.VoidLens).AddIngredient(ItemID.Bone, 30).AddIngredient(ModContent.ItemType<TropicalShroomCap>(), 15).AddIngredient(ModContent.ItemType<Booger>(), 30).AddTile(TileID.DemonAltar).Register();
        Recipe.Create(ItemID.VoidVault).AddIngredient(ItemID.Bone, 15).AddIngredient(ItemID.JungleSpores, 8).AddIngredient(ModContent.ItemType<Booger>(), 15).AddTile(TileID.DemonAltar).Register();
        Recipe.Create(ItemID.VoidVault).AddIngredient(ItemID.Bone, 15).AddIngredient(ModContent.ItemType<TropicalShroomCap>(), 8).AddIngredient(ItemID.ShadowScale, 15).AddTile(TileID.DemonAltar).Register();
        Recipe.Create(ItemID.VoidVault).AddIngredient(ItemID.Bone, 15).AddIngredient(ModContent.ItemType<TropicalShroomCap>(), 8).AddIngredient(ItemID.Vertebrae, 15).AddTile(TileID.DemonAltar).Register();
        Recipe.Create(ItemID.VoidVault).AddIngredient(ItemID.Bone, 15).AddIngredient(ModContent.ItemType<TropicalShroomCap>(), 8).AddIngredient(ModContent.ItemType<Booger>(), 15).AddTile(TileID.DemonAltar).Register();

        Recipe.Create(ItemID.BrokenHeroSword)
            .AddIngredient(ModContent.ItemType<BrokenVigilanteTome>())
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.NimbusRod)
            .AddIngredient(ModContent.ItemType<LivingLightningBlock>(), 80)
            .AddIngredient(ItemID.Cloud, 50)
            .AddIngredient(ItemID.RainCloud, 80)
            .AddTile(TileID.MythrilAnvil).Register();

        Recipe.Create(ItemID.LihzahrdPowerCell)
            .AddIngredient(ModContent.ItemType<SolariumStar>(), 5)
            .AddIngredient(ItemID.LihzahrdBrick, 10)
            .AddTile(TileID.MythrilAnvil).Register();

        Recipe.Create(ItemID.ShroomiteBar)
            .AddIngredient(ModContent.ItemType<ShroomiteOre>(), 5)
            .AddTile(TileID.AdamantiteForge).Register();

        Recipe.Create(ItemID.EnchantedSword)
            .AddIngredient(ModContent.ItemType<EnchantedBar>(), 20)
            .AddIngredient(ModContent.ItemType<BrokenHiltPiece>(), 3)
            .AddTile(TileID.Anvils).Register();

        Recipe.Create(ItemID.TurtleHelmet)
            .AddIngredient(ItemID.ChlorophyteMask)
            .AddIngredient(ItemID.TurtleShell)
            .AddTile(TileID.MythrilAnvil).Register();

        Recipe.Create(ItemID.TurtleScaleMail)
            .AddIngredient(ItemID.ChlorophytePlateMail)
            .AddIngredient(ItemID.TurtleShell)
            .AddTile(TileID.MythrilAnvil).Register();

        Recipe.Create(ItemID.TurtleLeggings)
            .AddIngredient(ItemID.ChlorophyteGreaves)
            .AddIngredient(ItemID.TurtleShell)
            .AddTile(TileID.MythrilAnvil).Register();

        Recipe.Create(ItemID.BlueBrickWall, 4)
            .AddIngredient(ItemID.BlueBrick)
            .AddTile(TileID.BoneWelder).Register();

        Recipe.Create(ItemID.GreenBrickWall, 4)
            .AddIngredient(ItemID.GreenBrick)
            .AddTile(TileID.BoneWelder).Register();

        Recipe.Create(ItemID.PinkBrickWall, 4)
            .AddIngredient(ItemID.PinkBrick)
            .AddTile(TileID.BoneWelder).Register();

        Recipe.Create(ItemID.Cannonball)
            .AddIngredient(ItemID.Bomb, 5)
            .AddTile(TileID.TinkerersWorkbench).Register();

        Recipe.Create(ItemID.BundleofBalloons)
            .AddIngredient(ModContent.ItemType<SandyStormcloudinaBottle>())
            .AddIngredient(ItemID.ShinyRedBalloon, 3)
            .AddTile(TileID.TinkerersWorkbench).Register();

        Recipe.Create(ItemID.MechanicalWorm)
            .AddIngredient(ModContent.ItemType<YuckyBit>(), 6)
            .AddRecipeGroup("IronBar", 5)
            .AddIngredient(ItemID.SoulofNight, 6)
            .AddTile(TileID.MythrilAnvil).Register();

        Recipe.Create(ItemID.PirateMap)
            .AddIngredient(ItemID.SoulofFright, 5)
            .AddIngredient(ItemID.Coral, 15)
            .AddIngredient(ItemID.SharkFin)
            .AddTile(TileID.MythrilAnvil).Register();

        Recipe.Create(ItemID.BattlePotion)
            .AddIngredient(ItemID.BottledWater)
            .AddIngredient(ModContent.ItemType<Bloodberry>())
            .AddIngredient(ItemID.RottenChunk)
            .AddTile(TileID.Bottles).Register();

        Recipe.Create(ItemID.BattlePotion)
            .AddIngredient(ItemID.BottledWater)
            .AddIngredient(ModContent.ItemType<Barfbush>())
            .AddIngredient(ModContent.ItemType<YuckyBit>())
            .AddTile(TileID.Bottles).Register();

        Recipe.Create(ItemID.NightsEdge)
            .AddIngredient(ItemID.LightsBane)
            .AddIngredient(ItemID.Muramasa)
            .AddIngredient(ModContent.ItemType<InsectoidBlade>())
            .AddIngredient(ItemID.FieryGreatsword)
            .AddTile(TileID.DemonAltar)
            .Register();

        Recipe.Create(ItemID.NightsEdge)
            .AddIngredient(ItemID.BloodButcherer)
            .AddIngredient(ItemID.Muramasa)
            .AddIngredient(ModContent.ItemType<InsectoidBlade>())
            .AddIngredient(ItemID.FieryGreatsword)
            .AddTile(TileID.DemonAltar)
            .Register();

        Recipe.Create(ItemID.NightsEdge)
            .AddIngredient(ModContent.ItemType<Snotsabre>())
            .AddIngredient(ItemID.Muramasa)
            .AddIngredient(ModContent.ItemType<InsectoidBlade>())
            .AddIngredient(ItemID.FieryGreatsword)
            .AddTile(TileID.DemonAltar)
            .Register();

        Recipe.Create(ItemID.PoisonedKnife, 50)
            .AddIngredient(ItemID.ThrowingKnife, 50)
            .AddIngredient(ModContent.ItemType<VirulentPowder>())
            .Register();
    }
}
