﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritMod.Projectiles
{
	public class Starshock : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Starshock");
		}

		public override void SetDefaults()
		{
			projectile.width = 12;
			projectile.height = 12;
			projectile.hostile = true;
			projectile.alpha = 255;
			projectile.penetrate = 1;
			projectile.extraUpdates = 1;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return Color.White;
		}

		public override void AI()
		{
			projectile.ai[0] += 1f;
			if (projectile.ai[0] > 5f)
			{
				projectile.velocity.Y = projectile.velocity.Y + 0.01f;
				projectile.velocity.X = projectile.velocity.X * 1.01f;
				projectile.alpha -= 23;
				projectile.scale = 0.8f * (255f - (float)projectile.alpha) / 255f;
				if (projectile.alpha < 0)
					projectile.alpha = 0;
			}
			if (projectile.alpha >= 255 && projectile.ai[0] > 5f)
			{
				projectile.Kill();
				return;
			}

			if (Main.rand.Next(4) == 0)
			{
				int num193 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 180, 0f, 0f, 100, default(Color), 1f);
				Main.dust[num193].position = projectile.Center;
				Main.dust[num193].scale += (float)Main.rand.Next(50) * 0.01f;
				Main.dust[num193].noGravity = true;
				Dust expr_835F_cp_0 = Main.dust[num193];
				expr_835F_cp_0.velocity.Y = expr_835F_cp_0.velocity.Y - 2f;
			}
			if (Main.rand.Next(6) == 0)
			{
				int num194 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 176, 0f, 0f, 100, default(Color), 1f);
				Main.dust[num194].position = projectile.Center;
				Main.dust[num194].scale += 0.3f + (float)Main.rand.Next(50) * 0.01f;
				Main.dust[num194].noGravity = true;
				Main.dust[num194].velocity *= 0.1f;
			}

			if (projectile.localAI[1] == 0f)
			{
				projectile.localAI[1] = 1f;
			}
		}

		public override void Kill(int timeLeft)
		{
			for (int k = 0; k < 3; k++)
			{
				Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 176, projectile.oldVelocity.X * 0.1f, projectile.oldVelocity.Y * 0.1f);
				Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 180, projectile.oldVelocity.X * 0.1f, projectile.oldVelocity.Y * 0.1f);
			}
		}
	}
}