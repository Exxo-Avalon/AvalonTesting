using AvalonTesting.Buffs;
using AvalonTesting.Buffs.AdvancedBuffs;
using AvalonTesting.Items.Accessories;
using AvalonTesting.Network;
using AvalonTesting.Network.Handlers;
using AvalonTesting.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Players;

public class ExxoBuffPlayer : ModPlayer
{
    public bool AccLavaMerman;
    public bool AdvancedBattle;
    public bool AdvancedCalming;
    public bool AstralProject;

    public bool BadgeOfBacteria;
    public bool BloodyWhetstone;
    public int DeleriumCount;
    public bool EarthInsignia;
    public int FracturingArmorLastRecord;
    public int FracturingArmorLevel;
    public int InfectDamage;
    private bool lavaMerman;
    public bool Lucky;
    public bool Malaria;
    public bool Melting;
    public bool NoSticky;
    public int OldFallStart;

    public int StingerProbeTimer;
    public float DaggerStaffRotation { get; set; }
    public float StingerProbeRotation { get; set; }
    public int FrameCount { get; private set; }
    public int ShadowCooldown { get; private set; }
    public int AstralCooldown { get; private set; }

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

        if (AccLavaMerman && Collision.LavaCollision(Player.position, Player.width, Player.height))
        {
            lavaMerman = true;
            Player.merman = true;
        }
    }

    public override void PostUpdateBuffs()
    {
        OldFallStart = Player.fallStart;
    }

    public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
    {
        ModContent.GetInstance<ExxoBuffPlayerSyncHandler>()
            .Send(new BasicPlayerNetworkArgs(Player), toWho, fromWho);
    }

    public override bool CanConsumeAmmo(Item weapon, Item ammo)
    {
        if (Player.HasBuff<AdvAmmoReservation>() && Main.rand.NextFloat() < AdvAmmoReservation.Chance)
        {
            return false;
        }

        return base.CanConsumeAmmo(weapon, ammo);
    }

    public void ResetShadowCooldown()
    {
        ShadowCooldown = 0;
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

        return base.PreHurt(pvp, quiet, ref damage, ref hitDirection, ref crit, ref customDamage, ref playSound,
            ref genGore, ref damageSource);
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

        SoundEngine.PlaySound(SoundID.Item, Player.position,
            SoundLoader.GetSoundSlot(Mod, "Sounds/Item/SpectrumDodge"));
        for (int i = 0; i < Player.hurtCooldowns.Length; i++)
        {
            Player.hurtCooldowns[i] = Player.immuneTime;
        }

        if (Player.whoAmI == Main.myPlayer)
        {
            NetMessage.SendData(Terraria.ID.MessageID.Dodge, -1, -1, null, Player.whoAmI, 1f);
        }
    }

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

    public override void PostUpdateRunSpeeds()
    {
        FloorVisualsAvalon();
    }

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

    public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
    {
        if (Player.whoAmI != Main.myPlayer)
        {
            return;
        }

        if (item.DamageType == DamageClass.Melee && BloodyWhetstone)
        {
            if (!target.HasBuff<Bleeding>())
            {
                target.GetGlobalNPC<AvalonTestingGlobalNPCInstance>().BleedStacks = 1;
            }

            target.AddBuff(ModContent.BuffType<Bleeding>(), 120);
        }
    }

    public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a,
                                     ref bool fullBright)
    {
        if (lavaMerman)
        {
            HadesCross hadesCross = ModContent.GetInstance<HadesCross>();
            Player.head = Mod.GetEquipSlot(hadesCross.Name, EquipType.Head);
            Player.body = Mod.GetEquipSlot(hadesCross.Name, EquipType.Body);
            Player.legs = Mod.GetEquipSlot(hadesCross.Name, EquipType.Legs);
        }
    }
}
