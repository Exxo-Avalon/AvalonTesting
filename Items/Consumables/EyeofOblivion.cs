using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace AvalonTesting.Items.Consumables;

class EyeofOblivion : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Eye of Oblivion");
        Tooltip.SetDefault("Summons Oblivion\nUse with care");
        Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 6;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.consumable = true;
        Item.rare = ItemRarityID.Yellow;
        Item.width = dims.Width;
        Item.useTime = 45;
        Item.useStyle = ItemUseStyleID.HoldUp;
        Item.value = 0;
        Item.maxStack = 20;
        Item.useAnimation = 45;
        Item.height = dims.Height;
    }

    public override bool CanUseItem(Player player)
    {
        return !NPC.AnyNPCs(ModContent.NPCType<NPCs.Bosses.Oblivion.OblivionPhase1>()) && !Main.dayTime;
    }

    public override bool? UseItem(Player player)
    {
        NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<NPCs.Bosses.Oblivion.OblivionPhase1>());
        SoundEngine.PlaySound(new SoundStyle($"{nameof(AvalonTesting)}/Sounds/Item/WoS"), player.position);
        return true;
    }
}
