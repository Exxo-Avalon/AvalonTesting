using Terraria.ID;
using AvalonTesting.Tiles;
using Terraria.ModLoader;

namespace AvalonTesting.Items;

public class Pot : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("[c/CD8647:C][c/817D5D:i][c/87583D:r][c/93847A:i][c/55727B:l][c/6E848C:n][c/D1D8D9:i][c/C0E0C5:t][c/D8CBD7:z][c/CBB349:o][c/B5C2D9:p][c/D7AFD9:b][c/7863C5:e][c/ED494E:r][c/8FBB5D:a][c/AF8386:d][c/2BBAD9:s][c/A5D194:i][c/FBDB09:s][c/EE6646:h][c/3DA4C4:c][c/F05B33:a][c/9E707A:d][c/9DD290:m][c/EF71F8:o][c/4A4EBA:n][c/AD2335:a][c/90ADAE:t][c/BDE026:t][c/20CAEE:e][c/D9C150:h][c/75D813:c][c/BED115:a][c/008CF4:s][c/FFF327:o][c/5EE5A3:l][c/FF8A2B:p][c/A2AD5D:r][c/E1C6B0:u][c/A094F6:v][c/951B4C:o][c/1AFFFB:h] Bar");
        ItemID.Sets.SortingPriorityMaterials[Item.type] = 90;
    }

    public override void SetDefaults()
    {
        Item.width = 20;
        Item.height = 20;
        Item.maxStack = 99;
        Item.value = 20000;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useTurn = true;
        Item.useAnimation = 15;
        Item.useTime = 10;
        Item.autoReuse = true;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<ContagionPot>();
        Item.placeStyle = 0;
        Item.rare = ItemRarityID.Purple;
    }
}
