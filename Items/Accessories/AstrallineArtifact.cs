using System.Collections.Generic;
using Avalon.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories;

public class AstrallineArtifact : ModItem
{
    public int AstralCooldown;

    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Astralline Artifact");
        Tooltip.SetDefault(
            "Allows you to astral project\nRun into enemies to mark them while astral projecting\nEnemies marked will take triple damage for 45 seconds");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = -12;
        Item.width = dims.Width;
        Item.accessory = true;
        Item.value = Item.sellPrice(0, 15);
        Item.height = dims.Height;
        Item.expert = true;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var AstralCooldownInfo = new TooltipLine(Mod, "Controls:AstralCooldown", "AstralCooldown: " + AstralCooldown);
        tooltips.Add(AstralCooldownInfo);
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<ExxoBuffPlayer>().AstralProject = true;
        AstralCooldown = player.GetModPlayer<Players.ExxoBuffPlayer>().AstralCooldown;
    }
}
