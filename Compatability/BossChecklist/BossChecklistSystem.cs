using System.Collections.Generic;
using AltLibrary.Common.Systems;
using Avalon.Items.Consumables;
using Avalon.Items.MusicBoxes;
using Avalon.Items.Placeable.Trophy;
using Avalon.Items.Vanity;
using Avalon.Systems;
using Terraria.ModLoader;

namespace Avalon.Compatability.BossChecklist;

public class BossChecklistSystem : ModSystem
{
    /// <inheritdoc />
    public override void PostSetupContent()
    {
        if (!ModLoader.TryGetMod("BossChecklist", out Mod bossChecklist))
        {
            return;
        }

        // bacterium prime
        bossChecklist.Call(
                "AddBoss",
                Avalon.Mod,
                "Bacterium Prime",
                ModContent.NPCType<NPCs.BacteriumPrime>(),
                3f,
                () => ModContent.GetInstance<DownedBossSystem>().DownedBacteriumPrime,
                () => WorldBiomeManager.WorldEvil == "Avalon/ContagionAlternateBiome",
                new List<int> { ModContent.ItemType<BacteriumPrimeTrophy>(), ModContent.ItemType<BacteriumPrimeMask>(), ModContent.ItemType<MusicBoxBacteriumPrime>() },
                ModContent.ItemType<InfestedCarcass>(),
                "Use an [i: " + ModContent.ItemType<InfestedCarcass>() + "] or smash three Snot Orbs in a Contagion ring",
                "Bacterium Prime melts back into the ick",
                "Avalon/Assets/Textures/BossChecklist/BacteriumPrimeBossChecklist",
                "Avalon/NPCs/BacteriumPrime_Head_Boss");

        // desert beak
        bossChecklist.Call(
                "AddBoss",
                Avalon.Mod,
                "Desert Beak",
                ModContent.NPCType<NPCs.Bosses.DesertBeak>(),
                5.5f,
                () => ModContent.GetInstance<DownedBossSystem>().DownedDesertBeak,
                () => true,
                new List<int> { ModContent.ItemType<DesertBeakTrophy>(), ModContent.ItemType<DesertBeakMask>(), ModContent.ItemType<MusicBoxDesertBeak>() },
                ModContent.ItemType<TheBeak>(),
                "Use [i: " + ModContent.ItemType<TheBeak>() + "] in the Desert",
                "Desert Beak has retreated into the sky",
                "Avalon/Assets/Textures/BossChecklist/DesertBeakBossChecklist",
                "Avalon/NPCs/Bosses/DesertBeak_Head_Boss");

        // phantasm
        bossChecklist.Call(
                "AddBoss",
                Avalon.Mod,
                "Phantasm",
                ModContent.NPCType<NPCs.Bosses.Phantasm>(),
                19f,
                () => ModContent.GetInstance<DownedBossSystem>().DownedPhantasm,
                () => true,
                new List<int> { ModContent.ItemType<PhantasmTrophy>(), /*ModContent.ItemType<PhantasmMask>(),*/ ModContent.ItemType<MusicBoxPhantasm>() },
                ModContent.ItemType<EctoplasmicBeacon>(),
                "Use an [i: " + ModContent.ItemType<EctoplasmicBeacon>() + "] in the Hellcastle",
                "Phantasm fades away",
                "Avalon/Assets/Textures/BossChecklist/PhantasmBossChecklist",
                "Avalon/NPCs/Bosses/Phantasm_Head_Boss");

        // wall of steel
        bossChecklist.Call(
                "AddBoss",
                Avalon.Mod,
                "Wall of Steel",
                ModContent.NPCType<NPCs.Bosses.WallofSteel>(),
                20f,
                () => ModContent.GetInstance<AvalonWorld>().SuperHardmode,
                () => true,
                new List<int> { ModContent.ItemType<WallofSteelTrophy>(), /*ModContent.ItemType<WallofSteelMask>(), ModContent.ItemType<MusicBoxWallofSteel>()*/ },
                ModContent.ItemType<HellboundRemote>(),
                "Toss a [i: " + ModContent.ItemType<HellboundRemote>() + "] into lava in the Underworld",
                "The Wall of Steel hisses steam and sinks into the lava",
                "Avalon/Assets/Textures/BossChecklist/WallofSteelBossChecklist",
                "Avalon/NPCs/Bosses/WallofSteel_Head_Boss");

        // armageddon slime
        bossChecklist.Call(
                "AddBoss",
                Avalon.Mod,
                "Armageddon Slime",
                ModContent.NPCType<NPCs.Bosses.ArmageddonSlime>(),
                21f,
                () => ModContent.GetInstance<DownedBossSystem>().DownedArmageddon,
                () => true,
                new List<int> { ModContent.ItemType<ArmageddonSlimeTrophy>(), ModContent.ItemType<ArmageddonSlimeMask>(), ModContent.ItemType<MusicBoxArmageddonSlime>() },
                ModContent.ItemType<DarkMatterChunk>(),
                "Use a [i: " + ModContent.ItemType<DarkMatterChunk>() + "]",
                "Armageddon Slime disintegrates",
                "Avalon/Assets/Textures/BossChecklist/ArmageddonSlimeBossChecklist",
                "Avalon/NPCs/Bosses/ArmageddonSlime_Head_Boss");

        // dragon lord
        bossChecklist.Call(
                "AddBoss",
                Avalon.Mod,
                "Dragon Lord",
                ModContent.NPCType<NPCs.DragonLordHead>(),
                22f,
                () => ModContent.GetInstance<DownedBossSystem>().DownedDragonLord,
                () => true,
                new List<int> { ModContent.ItemType<DragonLordTrophy>(), /*ModContent.ItemType<DragonLordMask>(), ModContent.ItemType<MusicBoxDragonLord>()*/ },
                ModContent.ItemType<DragonSpine>(),
                "Use a [i: " + ModContent.ItemType<DragonSpine>() + "] at the Dragon Altar in the Sky Fortress",
                "Dragon Lord flies away",
                "Avalon/Assets/Textures/BossChecklist/DragonLordBossChecklist",
                "Avalon/NPCs/DragonLordHead_Head_Boss");

        // mechasting
        bossChecklist.Call(
                "AddBoss",
                Avalon.Mod,
                "Mechasting",
                ModContent.NPCType<NPCs.Bosses.Mechasting>(),
                23f,
                () => ModContent.GetInstance<DownedBossSystem>().DownedMechasting,
                () => true,
                new List<int> { ModContent.ItemType<MechastingTrophy>(), /*ModContent.ItemType<MechastingMask>(), ModContent.ItemType<MusicBoxMechasting>()*/ },
                ModContent.ItemType<MechanicalWasp>(),
                "Use a [i: " + ModContent.ItemType<MechanicalWasp>() + "] at night",
                "Mechasting retreats to its mechanical hive",
                "Avalon/Assets/Textures/BossChecklist/MechastingBossChecklist");
    }
}
