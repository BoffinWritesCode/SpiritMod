using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritMod.Items.Weapon.Summon.WyvernStaff
{
	public class WyvernStaffBody : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Wyvern");
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 3;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
			Main.projFrames[projectile.type] = 3;
		}
		public override void SetDefaults()
		{
			projectile.penetrate = 600;
			projectile.tileCollide = false;
			projectile.hostile = false;
			projectile.friendly = true;
			projectile.damage = 13;
			//projectile.extraUpdates = 1;
			projectile.width = projectile.height = 20;
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 9;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
			projectile.minion = true;
			projectile.minionSlots = 0;

		}
		public override void AI()
		{
			if(Main.netMode != NetmodeID.MultiplayerClient) {
				if(!Main.projectile[(int)projectile.ai[0]].active) {
					projectile.timeLeft = 0;
					projectile.active = false;
					// NetMessage.SendData(28, -1, -1, "", projectile.whoAmI, -1f, 0.0f, 0.0f, 0, 0, 0);
				}
			}

			if(projectile.ai[0] < (double)Main.projectile.Length) {
				// We're getting the center of this projectile.
				Vector2 projectileCenter = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
				// Then using that center, we calculate the direction towards the 'parent projectile' of this projectile.
				float dirX = Main.projectile[(int)projectile.ai[0]].position.X + (float)(Main.projectile[(int)projectile.ai[0]].width / 2) - projectileCenter.X;
				float dirY = Main.projectile[(int)projectile.ai[0]].position.Y + (float)(Main.projectile[(int)projectile.ai[0]].height / 2) - projectileCenter.Y;
				// We then use Atan2 to get a correct rotation towards that parent projectile.
				projectile.rotation = (float)Math.Atan2(dirY, dirX) + 1.57f;
				// We also get the length of the direction vector.
				float length = (float)Math.Sqrt(dirX * dirX + dirY * dirY);
				// We calculate a new, correct distance.
				float dist = (length - (float)projectile.width) / length;
				float posX = dirX * dist;
				float posY = dirY * dist;

				// Reset the velocity of this projectile, because we don't want it to move on its own
				projectile.velocity = Vector2.Zero;
				// And set this projectiles position accordingly to that of this projectiles parent projectile.
				projectile.position.X = projectile.position.X + posX;
				projectile.position.Y = projectile.position.Y + posY;
				projectile.spriteDirection = Main.projectile[(int)projectile.ai[0]].spriteDirection;
				if (Main.projectile[(int)projectile.ai[0]].type != ModContent.ProjectileType<WyvernStaffBody>() && Main.projectile[(int)projectile.ai[0]].type != ModContent.ProjectileType<WyvernStaffHead>())
				{
					Main.NewText("Uh oh. Something went fucksy wucksy with the Wyvern Staff. Report to Spirit Mod devs immediately: Issue #1");
					projectile.active = false;
				}
			}
			else
			{
				Main.NewText("Uh oh. Something went fucksy wucksy with the Wyvern Staff. Report to Spirit Mod devs immediately: Issue #2");
				projectile.active = false;
			}
		}
	}
}