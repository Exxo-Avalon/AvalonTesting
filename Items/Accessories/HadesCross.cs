using AvalonTesting.Players;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Accessories;

public class HadesCross : ModItem
{
    public override void Load()
    {
        if (Main.netMode == NetmodeID.Server)
        {
            return;
        }

        Mod.AddEquipTexture(this, EquipType.Head,
            $"{nameof(AvalonTesting)}/{AvalonTesting.TextureAssetsPath}/Costumes/LavaMerman_Head");
        Mod.AddEquipTexture(this, EquipType.Body,
            $"{nameof(AvalonTesting)}/{AvalonTesting.TextureAssetsPath}/Costumes/LavaMerman_Body");
        Mod.AddEquipTexture(this, EquipType.Legs,
            $"{nameof(AvalonTesting)}/{AvalonTesting.TextureAssetsPath}/Costumes/LavaMerman_Legs");
    }

    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Hades' Cross");
        Tooltip.SetDefault("Turns the holder into varefolk upon entering lava");
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        if (Main.netMode == NetmodeID.Server)
        {
            return;
        }

        int equipSlotHead = Mod.GetEquipSlot(Name, EquipType.Head);
        int equipSlotBody = Mod.GetEquipSlot(Name, EquipType.Body);
        int equipSlotLegs = Mod.GetEquipSlot(Name, EquipType.Legs);

        ArmorIDs.Head.Sets.DrawHead[equipSlotHead] = false;
        ArmorIDs.Body.Sets.HidesTopSkin[equipSlotBody] = true;
        ArmorIDs.Body.Sets.HidesArms[equipSlotBody] = true;
        ArmorIDs.Legs.Sets.HidesBottomSkin[equipSlotLegs] = true;
    }

    public override void SetDefaults()
    {
        Item.width = 24;
        Item.height = 28;
        Item.accessory = true;
        Item.defense = 3;
        Item.value = Item.buyPrice(0, 9, 72);
        Item.rare = ItemRarityID.LightPurple;
        Item.canBePlacedInVanityRegardlessOfConditions = true;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<ExxoBuffPlayer>().AccLavaMerman = true;
        player.lavaImmune = true;
        player.fireWalk = true;
        player.ignoreWater = true;
    }

    public override bool IsVanitySet(int head, int body, int legs) => true;
}
