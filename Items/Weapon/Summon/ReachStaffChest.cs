using Microsoft.Xna.Framework;
using SpiritMod.Buffs.Summon;
using SpiritMod.Projectiles.Summon;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace SpiritMod.Items.Weapon.Summon
{
	public class ReachStaffChest : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Thornwild Staff");
			Tooltip.SetDefault("Summons a mini Wildwood Watcher to fight for you");

		}

		public override void SetDefaults()
		{
			item.width = 26;
			item.height = 28;
			item.value = Item.sellPrice(0, 0, 35, 0);
			item.rare = ItemRarityID.Green;
			item.mana = 15;
			item.damage = 10;
			item.knockBack = 0;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.useTime = 30;
			item.useAnimation = 30;
			item.summon = true;
			item.noMelee = true;
			item.buffType = ModContent.BuffType<ReachSummonBuff>();
			item.shoot = ModContent.ProjectileType<ReachSummon>();
			item.UseSound = SoundID.Item44;
		}
		
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack) {
			player.AddBuff(item.buffType, 2);
			position = Main.MouseWorld;
			return true;
		}
	}
}