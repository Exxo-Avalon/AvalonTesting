using AvalonTesting.Items.Material;
using AvalonTesting.Items.Placeables.Bars;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Systems;
public class RecipeChanger : ModSystem
{
    public override void PostAddRecipes()
    {
        for (int i = 0; i < Recipe.numRecipes; i++)
        {
            Item q;
            Recipe recipe = Main.recipe[i];
            if (recipe.HasResult(ItemID.FrostHelmet))
            {
                if (recipe.TryGetIngredient(ItemID.AdamantiteBar, out Item ing))
                {
                    recipe.RemoveIngredient(ing);
                    recipe.AddIngredient(ModContent.ItemType<FeroziumBar>(), 12);
                    recipe.AddIngredient(ModContent.ItemType<FrigidShard>());
                }
                else if (recipe.HasIngredient(ItemID.TitaniumBar))
                {
                    recipe.RemoveRecipe();
                    i--;
                }
            }
            if (recipe.HasResult(ItemID.FrostBreastplate))
            {
                if (recipe.TryGetIngredient(ItemID.AdamantiteBar, out Item ing))
                {
                    recipe.RemoveIngredient(ing);
                    recipe.AddIngredient(ModContent.ItemType<FeroziumBar>(), 24);
                    recipe.AddIngredient(ModContent.ItemType<FrigidShard>());
                }
                else if (recipe.HasIngredient(ItemID.TitaniumBar))
                {
                    recipe.RemoveRecipe();
                    i--;
                }
            }
            if (recipe.HasResult(ItemID.FrostLeggings))
            {
                if (recipe.TryGetIngredient(ItemID.AdamantiteBar, out Item ing))
                {
                    recipe.RemoveIngredient(ing);
                    recipe.AddIngredient(ModContent.ItemType<FeroziumBar>(), 18);
                    recipe.AddIngredient(ModContent.ItemType<FrigidShard>());
                }
                else if (recipe.HasIngredient(ItemID.TitaniumBar))
                {
                    recipe.RemoveRecipe();
                    i--;
                }
            }
            
            if (recipe.HasResult(ItemID.ClayPot))
            {
                if (recipe.TryGetIngredient(ItemID.ClayBlock, out Item ing))
                {
                    recipe.RemoveIngredient(ing);
                    recipe.AddIngredient(ItemID.ClayBlock, 4);
                }
            }
            if (recipe.HasResult(ItemID.BladeofGrass))
            {
                recipe.AddIngredient(ModContent.ItemType<ToxinShard>());
            }
            if (recipe.HasResult(ItemID.ThornChakram))
            {
                recipe.AddIngredient(ModContent.ItemType<ToxinShard>());
            }
            if (recipe.HasResult(ItemID.IvyWhip))
            {
                recipe.AddIngredient(ModContent.ItemType<ToxinShard>());
            }
            if (recipe.HasResult(ItemID.JungleHat))
            {
                recipe.AddIngredient(ModContent.ItemType<ToxinShard>());
            }
            if (recipe.HasResult(ItemID.JungleShirt))
            {
                recipe.AddIngredient(ModContent.ItemType<ToxinShard>());
            }
            if (recipe.HasResult(ItemID.JunglePants))
            {
                recipe.AddIngredient(ModContent.ItemType<ToxinShard>());
            }
            if (recipe.HasResult(ItemID.Flamarang))
            {
                recipe.AddIngredient(ModContent.ItemType<FireShard>());
            }
            if (recipe.HasResult(ItemID.MoltenFury))
            {
                recipe.AddIngredient(ModContent.ItemType<FireShard>());
            }
            if (recipe.HasResult(ItemID.FieryGreatsword))
            {
                recipe.AddIngredient(ModContent.ItemType<FireShard>());
            }
            if (recipe.HasResult(ItemID.MoltenPickaxe))
            {
                recipe.AddIngredient(ModContent.ItemType<FireShard>());
            }
            if (recipe.HasResult(ItemID.MoltenHamaxe))
            {
                recipe.AddIngredient(ModContent.ItemType<FireShard>());
            }
            if (recipe.HasResult(ItemID.PhoenixBlaster))
            {
                recipe.AddIngredient(ModContent.ItemType<FireShard>());
            }
            if (recipe.HasResult(ItemID.ImpStaff))
            {
                recipe.AddIngredient(ModContent.ItemType<FireShard>());
            }
        }
    }
}
