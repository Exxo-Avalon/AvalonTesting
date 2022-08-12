using Avalon.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Avalon.Players;

public class ExxoStaminaPlayer : ModPlayer
{
    public static int StaminaDrainTime = 10 * 60;

    public int FlightRestoreCooldown = 0;
    public bool FlightRestoreUnlocked = false;
    public bool ReleaseQuickStamina;
    public bool RocketJumpUnlocked = false;
    public bool SprintUnlocked = false;
    public bool StamFlower = false;
    public bool StaminaDrain = false;
    public float StaminaDrainMult = 1.2f;
    public int StaminaDrainStacks = 1;
    public int StaminaRegen;
    public int StaminaRegenCost = 1000;
    public int StaminaRegenCount;
    public int StamRegenDelay;
    public int StatStam = 100;
    public int StatStamMax = 30;
    public int StatStamMax2;
    public bool SwimmingUnlocked = false;
    public bool TeleportUnlocked = false;

    public override void ResetEffects()
    {
        StaminaDrain = false;
        StamFlower = false;
        StaminaRegen = 1000;
    }
    public void QuickStamina(int stamNeeded = 0) // todo: make stamina flower not allow you to consume stam pots that wouldn't allow you to continue using stamina
    {
        if (Player.noItems)
        {
            return;
        }
        if (StatStam == StatStamMax2)
        {
            return;
        }
        int num = StatStamMax2 - StatStam;
        Item potionToBeUsed = null;
        int num2 = -StatStamMax2;
        for (int i = 0; i < 58; i++)
        {
            Item potionChecked = Player.inventory[i];
            if (potionChecked.stack > 0 && potionChecked.type > 0 && potionChecked.GetGlobalItem<AvalonGlobalItemInstance>().HealStamina > 0)
            {
                int num3 = potionChecked.GetGlobalItem<AvalonGlobalItemInstance>().HealStamina - num;
                if (num2 < 0)
                {
                    if (num3 > num2)
                    {
                        potionToBeUsed = potionChecked;
                        num2 = num3;
                    }
                }
                else if (num3 < num2 && num3 >= 0)
                {
                    potionToBeUsed = potionChecked;
                    num2 = num3;
                }
            }
        }
        if (potionToBeUsed == null)
        {
            return;
        }
        if (potionToBeUsed.GetGlobalItem<AvalonGlobalItemInstance>().HealStamina < stamNeeded && stamNeeded != 0)
        {
            return;
        }
        SoundEngine.PlaySound(SoundID.Item3, Player.position);
        StatStam += potionToBeUsed.GetGlobalItem<AvalonGlobalItemInstance>().HealStamina;
        if (StatStam > StatStamMax2)
        {
            StatStam = StatStamMax2;
        }
        if (potionToBeUsed.GetGlobalItem<AvalonGlobalItemInstance>().HealStamina > 0 && Main.myPlayer == Player.whoAmI)
        {
            Player.AddBuff(ModContent.BuffType<StaminaDrain>(), 8 * 60);
            StaminaHealEffect(potionToBeUsed.GetGlobalItem<AvalonGlobalItemInstance>().HealStamina, true);
        }
        potionToBeUsed.stack--;
        if (potionToBeUsed.stack <= 0)
        {
            potionToBeUsed.type = 0;
        }
    }
    public override void SaveData(TagCompound tag)
    {
        tag["Avalon:Stamina"] = StatStamMax;
        tag["Avalon:RocketJumpUnlocked"] = RocketJumpUnlocked;
        tag["Avalon:TeleportUnlocked"] = TeleportUnlocked;
        tag["Avalon:SwimmingUnlocked"] = SwimmingUnlocked;
        tag["Avalon:SprintUnlocked"] = SprintUnlocked;
        tag["Avalon:FlightRestoreUnlocked"] = FlightRestoreUnlocked;
    }
    public override void LoadData(TagCompound tag)
    {
        if (tag.ContainsKey("Avalon:Stamina"))
        {
            StatStamMax = tag.GetAsInt("Avalon:Stamina");
        }
        if (tag.ContainsKey("Avalon:RocketJumpUnlocked"))
        {
            RocketJumpUnlocked = tag.Get<bool>("Avalon:RocketJumpUnlocked");
        }
        if (tag.ContainsKey("Avalon:TeleportUnlocked"))
        {
            TeleportUnlocked = tag.Get<bool>("Avalon:TeleportUnlocked");
        }
        if (tag.ContainsKey("Avalon:SwimmingUnlocked"))
        {
            SwimmingUnlocked = tag.Get<bool>("Avalon:SwimmingUnlocked");
        }
        if (tag.ContainsKey("Avalon:SprintUnlocked"))
        {
            SprintUnlocked = tag.Get<bool>("Avalon:SprintUnlocked");
        }
        if (tag.ContainsKey("Avalon:FlightRestoreUnlocked"))
        {
            FlightRestoreUnlocked = tag.Get<bool>("Avalon:FlightRestoreUnlocked");
        }
    }
    public void StaminaHealEffect(int healAmount, bool broadcast = true)
    {
        CombatText.NewText(Player.getRect(), new Color(5, 200, 255, 255), string.Concat(healAmount), false, false);
        if (broadcast && Main.netMode == 1 && Player.whoAmI == Main.myPlayer)
        {
            ModPacket packet = Network.MessageHandler.GetPacket(Network.MessageID.StaminaHeal);
            packet.Write(Player.whoAmI);
            packet.Write(healAmount);
            packet.Send();
        }
    }
}
