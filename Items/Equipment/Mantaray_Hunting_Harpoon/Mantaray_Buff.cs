﻿using Terraria.ModLoader;
using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader.IO;
using Terraria.GameInput;
 
namespace SpiritMod.Items.Equipment.Mantaray_Hunting_Harpoon
{
    public class Mantaray_Buff : ModBuff
    {
        public override void SetDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffNoSave[Type] = true;
			DisplayName.SetDefault("Manta Ray");
			Description.SetDefault("Swift as the tides !");
        }
 
        public override void Update(Player player, ref int buffIndex)
        {
            player.mount.SetMount(mod.MountType("Mantaray_Mount"), player);
            player.buffTime[buffIndex] = 10;
        }
    }
}