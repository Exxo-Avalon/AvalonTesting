using System.Collections.Generic;
using Avalon.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories;

public class AstrallineArtifact : ModItem
{
    private int AstralCooldown;

    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Astralline Artifact");
        Tooltip.SetDefault("Allows you to astral project\n" +
            "Run into enemies to mark them while astral projecting\n" +
            "Enemies marked will take triple damage for 45 seconds");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = 1;
        Item.width = dims.Width;
        Item.accessory = true;
        Item.value = Item.sellPrice(0, 15);
        Item.height = dims.Height;
        Item.expert = true;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var AstralCooldownInfo = new TooltipLine(Mod, "Controls:AstralCooldown", "Time before you can astral project: " + AstralCooldown / 60 + " seconds");
        tooltips.Add(AstralCooldownInfo);
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<ExxoEquipEffectPlayer>().AstralProject = true;
        AstralCooldown = player.GetModPlayer<ExxoEquipEffectPlayer>().AstralCooldown;
    }
}
