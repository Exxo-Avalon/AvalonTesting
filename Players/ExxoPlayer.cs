using AvalonTesting.Buffs;
using AvalonTesting.Items.Accessories;
using AvalonTesting.Prefixes;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace AvalonTesting.Players;

public class ExxoPlayer : ModPlayer
{
    public static Asset<Texture2D>[] spectrumArmorTextures;
    public bool caesiumPoison;
    public float CritDamageMult;
    public bool curseOfIcarus;
    public bool darkInferno;
    public bool hadesCross;
    public int screenShakeTimer;
    public bool spikeImmune;
    public bool trapImmune;

    public override void ResetEffects()
    {
        darkInferno = false;
        curseOfIcarus = false;
        trapImmune = false;
        spikeImmune = false;
        caesiumPoison = false;
        hadesCross = false;
        CritDamageMult = 1f;
    }

    public override void FrameEffects()
    {
        // TODO: Need new hook, FrameEffects doesn't run while paused.
        if (hadesCross)
        {
            HadesCross exampleCostume = ModContent.GetInstance<HadesCross>();
            Player.head = Mod.GetEquipSlot(exampleCostume.Name, EquipType.Head);
            Player.body = Mod.GetEquipSlot(exampleCostume.Name, EquipType.Body);
            Player.legs = Mod.GetEquipSlot(exampleCostume.Name, EquipType.Legs);
        }
    }

    public override void PostUpdate()
    {
        if (screenShakeTimer == 1)
        {
            SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot(Mod, "Sounds/Item/Stomp"), (int)Player.position.X,
                (int)Player.position.Y).Volume *= 0.8f;
        }

        if (screenShakeTimer > 0)
        {
            screenShakeTimer--;
        }
    }

    public override void PostUpdateEquips()
    {
        for (int i = 0; i < 3; i++)
        {
            ArmorPrefix prefix;
            if ((prefix = PrefixLoader.GetPrefix(Player.armor[i].prefix) as ArmorPrefix) != null)
            {
                prefix.UpdateEquip(Player);
            }
        }

        if (curseOfIcarus)
        {
            Player.rocketTimeMax = 42;
            Player.wingsLogic = 0;
            if (Player.mount.CanFly() ||
                Player.mount.CanHover()) // Setting player.mount._flyTime does not work for all mounts. Bye-bye mounts!
            {
                Player.mount.Dismount(Player);
            }
        }
    }

    public override void ModifyScreenPosition()
    {
        if (screenShakeTimer > 0)
        {
            Main.screenPosition += Main.rand.NextVector2Circular(20, 20);
        }
    }

    public override void UpdateEquips()
    {
        if (hadesCross)
        {
            Player.AddBuff(ModContent.BuffType<Varefolk>(), 60);
        }

        for (int k = 13; k < 18 + Player.extraAccessorySlots; k++)
        {
            Item item = Player.armor[k];
            if (item.Exists() &&
                item.GetGlobalItem<AvalonTestingGlobalItemInstance>().UpdateInvisibleVanity)
            {
                item.ModItem.UpdateVanity(Player);
            }
        }
    }

    public override void UpdateBadLifeRegen()
    {
        if (darkInferno)
        {
            if (Player.lifeRegen > 0)
            {
                Player.lifeRegen = 0;
            }

            Player.lifeRegenTime = 0;
            Player.lifeRegen -= 16;
        }

        if (caesiumPoison)
        {
            if (Player.lifeRegen > 0)
            {
                Player.lifeRegen = 0;
            }

            Player.lifeRegenTime = 0;
            Player.lifeRegen -= 20;
        }
    }

    public override void ModifyHitPvp(Item item, Player target, ref int damage, ref bool crit)
    {
        if (crit)
        {
            damage += MultiplyCritDamage(damage);
        }
    }

    public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
    {
        if (crit)
        {
            damage += MultiplyCritDamage(damage);
        }
    }

    public override void ModifyHitPvpWithProj(Projectile proj, Player target, ref int damage, ref bool crit)
    {
        if (crit)
        {
            damage += MultiplyCritDamage(damage);
        }
    }

    public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback,
                                              ref bool crit,
                                              ref int hitDirection)
    {
        if (crit)
        {
            damage += MultiplyCritDamage(damage);
        }
    }

    private int MultiplyCritDamage(int baseDamage)
    {
        int bonusDmg = -baseDamage;
        bonusDmg += (int)(baseDamage * (CritDamageMult + 1f) / 2);
        return bonusDmg;
    }
}
