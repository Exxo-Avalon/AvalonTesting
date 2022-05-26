using AvalonTesting.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Players;

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
            if (potionChecked.stack > 0 && potionChecked.type > 0 && potionChecked.GetGlobalItem<AvalonTestingGlobalItemInstance>().HealStamina > 0)
            {
                int num3 = potionChecked.GetGlobalItem<AvalonTestingGlobalItemInstance>().HealStamina - num;
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
        if (potionToBeUsed.GetGlobalItem<AvalonTestingGlobalItemInstance>().HealStamina < stamNeeded && stamNeeded != 0)
        {
            return;
        }
        SoundEngine.PlaySound(SoundID.Item3, Player.position);
        StatStam += potionToBeUsed.GetGlobalItem<AvalonTestingGlobalItemInstance>().HealStamina;
        if (StatStam > StatStamMax2)
        {
            StatStam = StatStamMax2;
        }
        if (potionToBeUsed.GetGlobalItem<AvalonTestingGlobalItemInstance>().HealStamina > 0 && Main.myPlayer == Player.whoAmI)
        {
            Player.AddBuff(ModContent.BuffType<StaminaDrain>(), 8 * 60);
            StaminaHealEffect(potionToBeUsed.GetGlobalItem<AvalonTestingGlobalItemInstance>().HealStamina, true);
        }
        potionToBeUsed.stack--;
        if (potionToBeUsed.stack <= 0)
        {
            potionToBeUsed.type = 0;
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
