using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritMod.Items.Armor.BeekeeperSet
{
	[AutoloadEquip(EquipType.Body)]
	public class BeekeeperBody : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Beekeeper's Suit");
		}
		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 30;
			item.value = Item.sellPrice(0, 0, 20, 0);
			item.rare = ItemRarityID.Green;

			item.vanity = true;
		}
    }
}
