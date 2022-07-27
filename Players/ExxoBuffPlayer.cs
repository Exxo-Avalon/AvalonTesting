using Avalon.Buffs;
using Avalon.Buffs.AdvancedBuffs;
using Avalon.Network;
using Avalon.Network.Handlers;
using Avalon.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Players;
// TODO: Add duratanium shield DoT debuff logic
public class ExxoBuffPlayer : ModPlayer
{
    public const string LavaMermanName = "LavaMerman";
    public bool AccLavaMerman;
    public bool AdvancedBattle;
    public bool AdvancedCalming;
    public bool AstralProject;

    public bool Unloaded;
    public bool BrokenWeaponry;
    public bool Electrified;
    public bool BadgeOfBacteria;
    public bool BloodyWhetstone;
    public int DeleriumCount;
    public int FracturingArmorLastRecord;
    public int FracturingArmorLevel;
    public int InfectDamage;
    public bool Lucky;
    public bool Malaria;
    public bool Melting;
    public bool CaesiumPoison;
    public bool DarkInferno;
    public bool NoSticky;
    public int OldFallStart;
    public bool ShadowCharm;
    public bool FrostGauntlet;
    public bool EarthInsignia;
    public bool TerraClaws;
    public int TimeSlowCounter;

    public bool SkyBlessing;
    public int SkyStacks = 1;

    public int StingerProbeTimer;
    private bool lavaMerman;
    public float DaggerStaffRotation { get; set; }
    public float StingerProbeRotation { get; set; }
    public int FrameCount { get; private set; }
    public int ShadowCooldown { get; private set; }
    public int AstralCooldown { get; private set; }

    public override void Load()
    {
        if (Main.netMode == NetmodeID.Server)
        {
            return;
        }

        EquipLoader.AddEquipTexture(
            Mod, $"{nameof(Avalon)}/{Avalon.TextureAssetsPath}/Costumes/LavaMerman_Head", EquipType.Head,
            null, LavaMermanName);
        EquipLoader.AddEquipTexture(
            Mod, $"{nameof(Avalon)}/{Avalon.TextureAssetsPath}/Costumes/LavaMerman_Body", EquipType.Body,
            null, LavaMermanName);
        EquipLoader.AddEquipTexture(
            Mod, $"{nameof(Avalon)}/{Avalon.TextureAssetsPath}/Costumes/LavaMerman_Legs", EquipType.Legs,
            null, LavaMermanName);
    }

    public override void SetStaticDefaults()
    {
        if (Main.netMode == NetmodeID.Server)
        {
            return;
        }

        int lavaMermanHead = EquipLoader.GetEquipSlot(Mod, LavaMermanName, EquipType.Head);
        int lavaMermanBody = EquipLoader.GetEquipSlot(Mod, LavaMermanName, EquipType.Body);
        int lavaMermanLegs = EquipLoader.GetEquipSlot(Mod, LavaMermanName, EquipType.Legs);

        ArmorIDs.Head.Sets.DrawHead[lavaMermanHead] = false;
        ArmorIDs.Body.Sets.HidesTopSkin[lavaMermanBody] = true;
        ArmorIDs.Body.Sets.HidesArms[lavaMermanBody] = true;
        ArmorIDs.Legs.Sets.HidesBottomSkin[lavaMermanLegs] = true;
    }

    public override void ResetEffects()
    {
        AdvancedBattle = false;
        AdvancedCalming = false;
        AstralProject = false;
        EarthInsignia = false;
        Lucky = false;
        Malaria = false;
        Melting = false;
        BadgeOfBacteria = false;
        NoSticky = false;
        AccLavaMerman = false;
        lavaMerman = false;
        BloodyWhetstone = false;
        SkyBlessing = false;
        Unloaded = false;
        BrokenWeaponry = false;
        DarkInferno = false;
        CaesiumPoison = false;
        Electrified = false;
        ShadowCharm = false;
        FrostGauntlet = false;
    }

    public override void PreUpdateBuffs()
    {
        StingerProbeRotation = (StingerProbeRotation % MathHelper.TwoPi) + 0.01f;
        DaggerStaffRotation = (DaggerStaffRotation % MathHelper.TwoPi) + 0.01f;
        if (Player.active)
        {
            FrameCount++;
            ShadowCooldown++;
            AstralCooldown++;
        }
    }
    public override void PostUpdateEquips()
    {
        if (!AstralProject && Player.HasBuff<AstralProjecting>())
        {
            Player.ClearBuff(ModContent.BuffType<AstralProjecting>());
        }

        if (AccLavaMerman && !Player.GetModPlayer<ExxoEquipEffectPlayer>().HideVarefolk && Collision.LavaCollision(Player.position, Player.width, Player.height))
        {
            lavaMerman = true;
            Player.merman = true;
        }
    }
    public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
    {
        if (FrostGauntlet && proj.DamageType == DamageClass.Melee)
        {
            target.AddBuff(BuffID.Frostburn, 60 * 4);
        }
        if (TerraClaws && proj.DamageType == DamageClass.Melee)
        {
            switch (Main.rand.Next(5))
            {
                case 0:
                    target.AddBuff(BuffID.OnFire, 7 * 60);
                    break;
                case 1:
                    target.AddBuff(BuffID.Poisoned, 7 * 60);
                    break;
                case 2:
                    target.AddBuff(BuffID.Venom, 7 * 60);
                    break;
                case 3:
                    target.AddBuff(BuffID.Frostburn2, 7 * 60);
                    break;
                case 4:
                    target.AddBuff(BuffID.Ichor, 7 * 60);
                    break;
            }
        }
    }
    public override void PostUpdateBuffs()
    {
        OldFallStart = Player.fallStart;
    }
    public override void SyncPlayer(int toWho, int fromWho, bool newPlayer) =>
        ModContent.GetInstance<ExxoBuffPlayerSyncHandler>()
            .Send(new BasicPlayerNetworkArgs(Player), toWho, fromWho);

    public override bool CanConsumeAmmo(Item weapon, Item ammo)
    {
        if (Player.HasBuff<AdvAmmoReservation>() && Main.rand.NextFloat() < AdvAmmoReservation.Chance)
        {
            return false;
        }

        return base.CanConsumeAmmo(weapon, ammo);
    }

    public override void ProcessTriggers(TriggersSet triggersSet)
    {
        if (Player.whoAmI != Main.myPlayer)
        {
            return;
        }
        if (AstralProject && KeybindSystem.AstralHotkey.JustPressed)
        {
            if (Player.HasBuff<AstralProjecting>())
            {
                Player.ClearBuff(ModContent.BuffType<AstralProjecting>());
                AstralCooldown = 0;
            }
            else if (AstralCooldown >= 3600)
            {
                Player.AddBuff(ModContent.BuffType<AstralProjecting>(), 15 * 60);
            }
        }
    }

    public override void UpdateBadLifeRegen()
    {
        if (DarkInferno)
        {
            if (Player.lifeRegen > 0)
            {
                Player.lifeRegen = 0;
            }
            Player.lifeRegenTime = 0;
            if (Player.GetModPlayer<ExxoEquipEffectPlayer>().DuraShield && Main.rand.NextBool(6))
            {
                Player.lifeRegen += Player.HasItemInArmor(ModContent.ItemType<Items.Accessories.DurataniumOmegaShield>()) ? 6 : 4;
            }
            else
            {
                Player.lifeRegen -= 16;
            }
        }
        if (CaesiumPoison)
        {
            if (Player.lifeRegen > 0)
            {
                Player.lifeRegen = 0;
            }
            Player.lifeRegenTime = 0;
            if (Player.GetModPlayer<ExxoEquipEffectPlayer>().DuraShield && Main.rand.NextBool(6))
            {
                Player.lifeRegen += Player.HasItemInArmor(ModContent.ItemType<Items.Accessories.DurataniumOmegaShield>()) ? 3 : 2;
            }
            else
            {
                Player.lifeRegen -= 20;
            }
        }
        if (Melting)
        {
            if (Player.lifeRegen > 0)
            {
                Player.lifeRegen = 0;
            }

            Player.lifeRegenTime = 0;
            Player.lifeRegen -= 32;
        }
        if (Malaria)
        {
            if (Player.lifeRegen > 0)
            {
                Player.lifeRegen = 0;
            }

            Player.lifeRegenTime = 0;
            Player.lifeRegen -= 30;
        }
        if (Electrified)
        {
            if (Player.lifeRegen > 0)
            {
                Player.lifeRegen = 0;
            }
            Player.lifeRegenTime = 0;
            int minus = Player.GetModPlayer<ExxoEquipEffectPlayer>().DuraShield && Player.HasItemInArmor(ModContent.ItemType<Items.Accessories.DurataniumShield>()) ? 4 :
                Player.GetModPlayer<ExxoEquipEffectPlayer>().DuraShield ? 6 : 8;
            int minus2 = Player.GetModPlayer<ExxoEquipEffectPlayer>().DuraShield && Player.HasItemInArmor(ModContent.ItemType<Items.Accessories.DurataniumOmegaShield>())
                ? 16 : Player.GetModPlayer<ExxoEquipEffectPlayer>().DuraShield ? 24 : 32;
            Player.lifeRegen -= minus;
            if (Player.velocity.X != 0)
            {
                Player.lifeRegen -= minus2;
            }
        }
    }

    public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
    {
        if (Player.HasBuff<SpectrumBlur>())
        {
            Player.eocDash = 1;
        }
    }

    public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit,
                                 ref bool customDamage,
                                 ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
    {
        if (Player.HasBuff<SpectrumBlur>() && Player.whoAmI == Main.myPlayer && Main.rand.Next(10) == 0)
        {
            SpectrumDodge();
            return false;
        }

        return base.PreHurt(pvp, quiet, ref damage, ref hitDirection, ref crit,
            ref customDamage,
            ref playSound, ref genGore, ref damageSource);
    }

    public override void PostUpdateRunSpeeds() => FloorVisualsAvalon();

    public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback,
                                              ref bool crit,
                                              ref int hitDirection)
    {
        if (target.HasBuff(ModContent.BuffType<AstralCurse>()))
        {
            damage *= 3;
        }

        if (Player.HasBuff(ModContent.BuffType<BacteriaEndurance>()))
        {
            damage += 8;
        }
    }

    public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
    {
        if (target.HasBuff(ModContent.BuffType<AstralCurse>()))
        {
            damage *= 3;
        }

        if (Player.HasBuff(ModContent.BuffType<BacteriaEndurance>()))
        {
            damage += 8;
        }
    }

    public override void OnHitByNPC(NPC npc, int damage, bool crit)
    {
        if (Player.whoAmI == Main.myPlayer && BadgeOfBacteria)
        {
            Player.AddBuff(ModContent.BuffType<BacteriaEndurance>(), 6 * 60);
            npc.AddBuff(ModContent.BuffType<BacteriaInfection>(), 6 * 60);
        }
    }
    public override void OnHitPvp(Item item, Player target, int damage, bool crit)
    {
        if (FrostGauntlet && item.DamageType == DamageClass.Melee)
        {
            target.AddBuff(BuffID.Frostburn, 60 * 4);
        }
        if (TerraClaws && item.DamageType == DamageClass.Melee)
        {
            switch (Main.rand.Next(5))
            {
                case 0:
                    target.AddBuff(BuffID.OnFire, 7 * 60);
                    break;
                case 1:
                    target.AddBuff(BuffID.Poisoned, 7 * 60);
                    break;
                case 2:
                    target.AddBuff(BuffID.Venom, 7 * 60);
                    break;
                case 3:
                    target.AddBuff(BuffID.Frostburn2, 7 * 60);
                    break;
                case 4:
                    target.AddBuff(BuffID.Ichor, 7 * 60);
                    break;
            }
        }
    }
    public override void OnHitPvpWithProj(Projectile proj, Player target, int damage, bool crit)
    {
        if (FrostGauntlet && proj.DamageType == DamageClass.Melee)
        {
            target.AddBuff(BuffID.Frostburn, 60 * 4);
        }
        if (TerraClaws && proj.DamageType == DamageClass.Melee)
        {
            switch (Main.rand.Next(5))
            {
                case 0:
                    target.AddBuff(BuffID.OnFire, 7 * 60);
                    break;
                case 1:
                    target.AddBuff(BuffID.Poisoned, 7 * 60);
                    break;
                case 2:
                    target.AddBuff(BuffID.Venom, 7 * 60);
                    break;
                case 3:
                    target.AddBuff(BuffID.Frostburn2, 7 * 60);
                    break;
                case 4:
                    target.AddBuff(BuffID.Ichor, 7 * 60);
                    break;
            }
        }
    }
    public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
    {
        if (Player.whoAmI != Main.myPlayer)
        {
            return;
        }
        if (FrostGauntlet && item.DamageType == DamageClass.Melee)
        {
            target.AddBuff(BuffID.Frostburn, 60 * 4);
        }
        if (item.DamageType == DamageClass.Melee && BloodyWhetstone)
        {
            if (!target.HasBuff<Bleeding>())
            {
                target.GetGlobalNPC<AvalonGlobalNPCInstance>().BleedStacks = 1;
            }

            target.AddBuff(ModContent.BuffType<Bleeding>(), 120);
        }
        if (TerraClaws && item.DamageType == DamageClass.Melee)
        {
            switch (Main.rand.Next(5))
            {
                case 0:
                    target.AddBuff(BuffID.OnFire, 7 * 60);
                    break;
                case 1:
                    target.AddBuff(BuffID.Poisoned, 7 * 60);
                    break;
                case 2:
                    target.AddBuff(BuffID.Venom, 7 * 60);
                    break;
                case 3:
                    target.AddBuff(BuffID.Frostburn2, 7 * 60);
                    break;
                case 4:
                    target.AddBuff(BuffID.Ichor, 7 * 60);
                    break;
            }
        }
    }

    public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a,
                                     ref bool fullBright)
    {
        if (lavaMerman)
        {
            Player.head = EquipLoader.GetEquipSlot(Mod, LavaMermanName, EquipType.Head);
            Player.body = EquipLoader.GetEquipSlot(Mod, LavaMermanName, EquipType.Body);
            Player.legs = EquipLoader.GetEquipSlot(Mod, LavaMermanName, EquipType.Legs);
        }
        if (ShadowCharm)
        {

        }
    }

    public void ResetShadowCooldown() => ShadowCooldown = 0;

    public void FloorVisualsAvalon()
    {
        int num = (int)((Player.position.X + (Player.width / 2)) / 16f);
        int num2 = (int)((Player.position.Y + Player.height) / 16f);
        int num3 = -1;
        if (Main.tile[num, num2].HasUnactuatedTile && Main.tileSolid[Main.tile[num, num2].TileType])
        {
            num3 = Main.tile[num, num2].TileType;
        }
        else if (Main.tile[num - 1, num2].HasUnactuatedTile && Main.tileSolid[Main.tile[num - 1, num2].TileType])
        {
            num3 = Main.tile[num - 1, num2].TileType;
        }
        else if (Main.tile[num + 1, num2].HasUnactuatedTile && Main.tileSolid[Main.tile[num + 1, num2].TileType])
        {
            num3 = Main.tile[num + 1, num2].TileType;
        }

        if (num3 > -1)
        {
            if (num3 == 229 && !NoSticky)
            {
                Player.sticky = true;
            }
            else
            {
                Player.sticky = false;
            }
        }
    }

    private void SpectrumDodge()
    {
        Player.immune = true;
        if (Player.longInvince)
        {
            Player.immuneTime = 60;
        }
        else
        {
            Player.immuneTime = 30;
        }

        SoundEngine.PlaySound(new SoundStyle($"{nameof(Avalon)}/Sounds/Item/SpectrumDodge"), Player.position);
        for (int i = 0; i < Player.hurtCooldowns.Length; i++)
        {
            Player.hurtCooldowns[i] = Player.immuneTime;
        }

        if (Player.whoAmI == Main.myPlayer)
        {
            NetMessage.SendData(Terraria.ID.MessageID.Dodge, -1, -1, null, Player.whoAmI, 1f);
        }
    }
}
