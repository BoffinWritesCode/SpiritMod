﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using SpiritMod.ChargeMeter;
using SpiritMod.Prim;
using SpiritMod.Utilities;
using SpiritMod.Prim;

namespace SpiritMod.Items.Weapon.Swung.AnimeSword
{
    public class AnimeSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Anime Sword");
        }

        public override void SetDefaults()
        {
            item.channel = true;
            item.damage = 40;
            item.width = 60;
            item.height = 60;
            item.useTime = 60;
            item.useAnimation = 60;
            item.crit = 4;
            item.useStyle = ItemUseStyleID.HoldingOut;
            Item.staff[item.type] = true;
            item.melee = true;
            item.noMelee = true;
            item.knockBack = 1;
            item.useTurn = false;
            item.value = Terraria.Item.sellPrice(0, 0, 1, 0);
            item.rare = ItemRarityID.Pink;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("AnimeSwordProj");
            item.shootSpeed = 6f;
            item.noUseGraphic = true;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, 0);
        }
    }
    public class AnimeSwordProj : ModProjectile
    {
        public NPC[] hit = new NPC[12];
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Anime Sword Proj");
        }

        public override void SetDefaults()
		{
            projectile.width = projectile.height = 40;
			projectile.hostile = false;
			projectile.melee = true;
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.tileCollide = false;
            projectile.alpha = 255;
		}
        public readonly int MAXCHARGE = 69;
        public int charge = 0;
        int index = 0;
        NPC mostrecent;
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            player.heldProj = projectile.whoAmI;
            player.itemTime = 2;
            player.itemAnimation = 2;
            projectile.Center = player.Center;
            if (player.channel)
            {
                projectile.timeLeft = 120;
                charge++;
                if (charge < 60)
                    charge++;
                if (charge == 60)
                {
                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/slashdash"), projectile.Center);
                    SpiritMod.primitives.CreateTrail(new AnimePrimTrail(projectile));
                }
                if (charge > 60 && charge < MAXCHARGE)
                {
					player.GetModPlayer<MyPlayer>().AnimeSword = true;
                    Vector2 direction = Main.MouseWorld - (player.Center);
			        direction.Normalize();
			        direction *= 45f;
                    player.velocity = direction;
                    for (int i = 0; i < Main.npc.Length; i++)
                    {
                        NPC target = Main.npc[i];
                        if (Collision.CheckAABBvAABBCollision(target.position, new Vector2(target.width, target.height), player.position - new Vector2(10, 0), new Vector2(player.width + 20, player.height)) && index < 11)
                        {
                            bool inlist = false;
                            foreach (var npc in hit)
				                if (target == npc)
                                    inlist = true;
                            if (!inlist)
                                hit[index++] = target;
                        }
                    }
                }
                if (charge == MAXCHARGE)
                {
					player.GetModPlayer<MyPlayer>().AnimeSword = false;
					player.velocity = Vector2.Zero;
                }
            }
            else
            {
                if (charge > 60 && charge < MAXCHARGE)
                {
					player.GetModPlayer<MyPlayer>().AnimeSword = false;
					player.velocity = Vector2.Zero;
                    charge = MAXCHARGE + 1;
                }
                if (projectile.timeLeft % 5 == 0)
                {
                    float mindist = 0;
                    NPC closest = null;
                    foreach (var npc in hit)
                    {
                        if (npc != null)
                        {
                            if (npc.active && (!npc.townNPC || !npc.friendly))
                            {
                                float distance = (npc.Center - projectile.Center).Length();
                                if (mostrecent == null)
                                {
                                    if (distance > mindist)
                                    {
                                        closest = npc;
                                        mindist = distance;
                                    }
                                }
                                else
                                {
                                    float maxdistance = (mostrecent.Center - projectile.Center).Length();
                                    if (distance > mindist && distance < maxdistance)
                                    {
                                        closest = npc;
                                        mindist = distance;
                                    }
                                }
                            }
                        }
                    }
                    if (closest != null)
                    {
                        mostrecent = closest;
                        if (mostrecent.active)
                        {
                            SpiritMod.primitives.CreateTrail(new AnimePrimTrailTwo(mostrecent));
                        }
                    }
                    else if (projectile.timeLeft > 15)
                    {
                        projectile.timeLeft = 15;
                    }
                }
            }
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            Player player = Main.player[projectile.owner];
            if (charge > 60 && charge < MAXCHARGE)
            {
                return base.Colliding(projHitbox, targetHitbox);
            }
            if (!player.channel)
                return true;
            return false;

        }

        public override bool? CanHitNPC(NPC target)
		{
            Player player = Main.player[projectile.owner];
            if (player.channel || projectile.timeLeft > 5)
                return false;
            foreach (var npc in hit)
				if (target == npc)
					return base.CanHitNPC(target);
			return false;
        }

		public override void Kill(int timeLeft) => Main.player[projectile.owner].GetModPlayer<MyPlayer>().AnimeSword = false;

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Player player = Main.player[projectile.owner];
            Texture2D texture = Main.projectileTexture[projectile.type];
            if (player.direction == 1)
            {
                Main.spriteBatch.Draw(texture, (player.Center - new Vector2(-6, 6)) - Main.screenPosition, null, lightColor, 0, new Vector2(texture.Width,0), projectile.scale, SpriteEffects.None, 0.0f);
                if (charge > 60 && player.channel)
                {
                    Texture2D texture2 = ModContent.GetTexture("SpiritMod/Items/Weapon/Swung/AnimeSword/TwinkleXLarge");
                    Main.spriteBatch.Draw(texture2, (player.Center - new Vector2(-6, 6)) - Main.screenPosition, null, Color.White, charge / 40f, new Vector2(texture2.Width / 2,texture2.Height / 2), projectile.scale, SpriteEffects.None, 0.0f);
                }
            }
            else
            {
                Main.spriteBatch.Draw(texture, (player.Center - new Vector2(6, 6)) - Main.screenPosition, null, lightColor, 0, new Vector2(0,0), projectile.scale, SpriteEffects.FlipHorizontally, 0.0f);
                if (charge > 60 && player.channel)
                {
                    Texture2D texture2 = ModContent.GetTexture("SpiritMod/Items/Weapon/Swung/AnimeSword/TwinkleXLarge");
                    Main.spriteBatch.Draw(texture2, (player.Center - new Vector2(-2, 6)) - Main.screenPosition, null, Color.White, charge / 40f, new Vector2(texture2.Width / 2,texture2.Height / 2), projectile.scale, SpriteEffects.None, 0.0f);
                }
            }
            return false;
        }
    }
}