using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SpiritMod.Prim;
using SpiritMod.Utilities;
using SpiritMod;
using SpiritMod.Projectiles;

namespace SpiritMod.Items.Weapon.Summon.JinxprobeWand
{
    public class JinxprobeEnergy : ModProjectile, ITrailProjectile, IBasicPrimDraw
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mini Star");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 16;
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.aiStyle = 0;
            projectile.tileCollide = true;
            projectile.timeLeft = 360;
            projectile.extraUpdates = 3;
            projectile.ranged = true;
            projectile.friendly = true;
            projectile.penetrate = 3;
            projectile.ignoreWater = true;
            projectile.scale = Main.rand.NextFloat(0.6f, 0.9f);
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 30;
        }

        public override void AI()
        {
            projectile.rotation += 0.1f;

            projectile.velocity.Y += 0.1f;

            if (Main.rand.Next(50) == 0)
                Gore.NewGore(projectile.Center, projectile.velocity / 4, mod.GetGoreSlot("Gores/StarjinxGore"), 0.75f);
        }

        private float Timer => Main.GlobalTime * 2 + projectile.ai[0];

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.penetrate--;
            projectile.Bounce(oldVelocity, 0.5f);
            return false;
        }

        public void DoTrailCreation(TrailManager tM)
        {
            tM.CreateTrail(projectile, new StarjinxTrail(Timer, 2, 0.15f), new RoundCap(), new ArrowGlowPosition(), 48f * projectile.scale, 200f * projectile.scale, new DefaultShader());
            tM.CreateTrail(projectile, new StarjinxTrail(Timer, 2, 0.8f), new RoundCap(), new DefaultTrailPosition(), 20f * projectile.scale, 80f * projectile.scale, new DefaultShader());
            tM.CreateTrail(projectile, new StarjinxTrail(Timer, 2, 0.8f), new RoundCap(), new DefaultTrailPosition(), 20f * projectile.scale, 80f * projectile.scale, new DefaultShader());
            tM.CreateTrail(projectile, new StarjinxTrail(Timer, 2, 0.3f), new RoundCap(), new ArrowGlowPosition(), 48f * projectile.scale, 40f * projectile.scale, new DefaultShader());
        }

        public void DrawPrimShape(BasicEffect effect) => StarDraw.DrawStarBasic(effect, projectile.Center, projectile.rotation, projectile.scale * 15, Color.White);

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor) => false;

        public override void Kill(int timeLeft)
        {
            if (Main.netMode != NetmodeID.Server)
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/starHit").WithVolume(0.25f).WithPitchVariance(0.3f), projectile.Center);
        }
    }
}