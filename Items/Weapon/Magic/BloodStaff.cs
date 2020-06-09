using SpiritMod.Projectiles.Magic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritMod.Items.Weapon.Magic
{
    public class BloodStaff : ModItem
    {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Vessel Drainer");
            Tooltip.SetDefault("Summons a blood tentacle that splits into life-stealing blood clots");
        }


        public override void SetDefaults() {
            item.damage = 36;
            item.magic = true;
            item.mana = 11;
            item.width = 52;
            item.height = 52;
            item.useTime = 44;
            item.useAnimation = 44;
            item.useStyle = ItemUseStyleID.HoldingOut;
            Item.staff[item.type] = true;
            item.noMelee = true;
            item.knockBack = 3;
            item.value = Terraria.Item.sellPrice(0, 0, 50, 0);
            item.rare = 4;
            item.crit += 10;
            item.UseSound = SoundID.Item20;
            item.autoReuse = true;
            item.shoot = ModContent.ProjectileType<BloodVessel>();
            item.shootSpeed = 19f;
        }

        public override void AddRecipes() {
            ModRecipe modRecipe = new ModRecipe(mod);
            modRecipe.AddIngredient(ModContent.ItemType<CrimsonStaff>(), 1);
            modRecipe.AddIngredient(ModContent.ItemType<JungleStaff>(), 1);
            modRecipe.AddIngredient(ModContent.ItemType<DungeonStaff>(), 1);
            modRecipe.AddIngredient(ModContent.ItemType<HellStaff>(), 1);
            modRecipe.AddTile(26);
            modRecipe.SetResult(this, 1);
            modRecipe.AddRecipe();
        }

    }
}
