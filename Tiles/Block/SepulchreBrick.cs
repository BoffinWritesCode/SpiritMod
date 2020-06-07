using Microsoft.Xna.Framework;
using Terraria.GameContent.Generation;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.World.Generation;
using SpiritMod;
using SpiritMod.Items.Placeable.Tiles;

namespace SpiritMod.Tiles.Block
{
    public class SepulchreBrick : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlendAll[Type] = true;
            soundType = 21;
            Main.tileBlockLight[Type] = true;
            AddMapEntry(new Color(87, 85, 81));
            drop = ModContent.ItemType<SepulchreBrickItem>();
            dustType = 54;
        }
	}
}

