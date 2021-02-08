using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritMod.Projectiles.Magic
{
	public class AdamantiteStaffProj : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Adamantite Blast");
		}

		public override void SetDefaults()
		{
			projectile.hostile = false;
			projectile.magic = true;
			projectile.width = 10;
			projectile.height = 10;
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.penetrate = 1;
			projectile.alpha = 255;
			projectile.timeLeft = 50;
		}

		int counter = -720;
		public override bool PreAI()
		{
			/*int num = 5;
			for (int k = 0; k < 10; k++) {
				int index2 = Dust.NewDust(projectile.position, 10, 10, 60, 0.0f, 0.0f, 0, new Color(), 1.3f);
				Main.dust[index2].position = projectile.Center - projectile.velocity / num * (float)k;
				Main.dust[index2].scale = .5f;
				Main.dust[index2].velocity *= 0f;
				Main.dust[index2].noGravity = true;
				Main.dust[index2].noLight = true;
			}*/
			projectile.rotation = projectile.velocity.ToRotation() + 1.57f;
			return true;
		}

		public override void Kill(int timeLeft)
		{
			if (Main.netMode != NetmodeID.MultiplayerClient) {
				Main.PlaySound(SoundID.Item110, projectile.Center);
				float maxprojs = 8;
				for (int i = 0; i < 8; i++) {
					if (i != 3 && i != 7) {
						Vector2 BaseSpeed = new Vector2(0, 7.5f);
						BaseSpeed = BaseSpeed.RotatedBy(i * MathHelper.TwoPi / maxprojs);
						Projectile.NewProjectile(projectile.Center, BaseSpeed, mod.ProjectileType("AdamantiteStaffProj2"), projectile.damage, 0f, projectile.owner, 0f, 0f);
					}
				}
			}
		}
	}
}
