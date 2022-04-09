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
}
