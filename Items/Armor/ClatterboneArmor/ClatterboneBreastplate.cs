
using SpiritMod.Items.Material;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritMod.Items.Armor.ClatterboneArmor
{
    [AutoloadEquip(EquipType.Body)]
    public class ClatterboneBreastplate : ModItem
    {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Clatterbone Breastplate");
            Tooltip.SetDefault("4% increased melee damage");
        }
        public override void SetDefaults() {
            item.width = 34;
            item.height = 30;
            item.value = Item.buyPrice(silver: 60);
            item.rare = 2;

            item.defense = 6;
        }

        public override void UpdateEquip(Player player) {
            player.meleeDamage += 0.04F;
        }

        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<Carapace>(), 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
