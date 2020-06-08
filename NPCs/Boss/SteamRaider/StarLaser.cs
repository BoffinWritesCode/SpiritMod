﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace SpiritMod.NPCs.Boss.SteamRaider
{
    public class StarLaser : ModProjectile
    {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Star Laser");
        }

        public override void SetDefaults() {
            projectile.hostile = true;
            projectile.width = 2;
            projectile.height = 2;
            projectile.aiStyle = -1;
            projectile.friendly = false;
            projectile.penetrate = 8;
            projectile.alpha = 255;
            projectile.timeLeft = 90;
            projectile.tileCollide = false;
            projectile.extraUpdates = 2;
        }


        private void Trail(Vector2 from, Vector2 to) {
            float distance = Vector2.Distance(from, to);
            float step = 1 / distance;
            for(float w = 0; w < distance; w += 8) {
                Dust.NewDustPerfect(Vector2.Lerp(from, to, w * step), 226, Vector2.Zero).noGravity = true;
            }
        }
        public override bool PreAI() {
            Trail(projectile.position, projectile.position + projectile.velocity);
            return true;
        }

    }
}