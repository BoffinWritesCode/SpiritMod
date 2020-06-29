using SpiritMod.Items.Material;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace SpiritMod.Items.Armor.ReaperArmor
{
	[AutoloadEquip(EquipType.Head)]
	public class BlightHelm : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Reaper's Crown");
			Tooltip.SetDefault("Increases max life by 50 and increases life regen");
		}

		public override void SetDefaults()
		{
			item.width = 28;
			item.height = 24;
			item.value = Item.sellPrice(0, 3, 0, 0);
			item.rare = ItemRarityID.Yellow;
			item.defense = 17;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs) 
			=> body.type == ModContent.ItemType<BlightArmor>() && legs.type == ModContent.ItemType<BlightLegs>();

		public override void UpdateArmorSet(Player player)
		{
			string tapDir = Language.GetTextValue(Main.ReversedUpDownArmorSetBonuses ? "Key.UP" : "Key.DOWN");
			player.setBonus = $"Double tap {tapDir} to relese a barrage of bolts from the cursor 5 times\n45 second cooldown\nAttacks inflict 'Fel Brand'";
			player.GetSpiritPlayer().reaperSet = true;
		}

		public override void UpdateEquip(Player player)
		{
			player.statLifeMax2 += 50;
			player.lifeRegen += 3;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<CursedFire>(), 14);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}