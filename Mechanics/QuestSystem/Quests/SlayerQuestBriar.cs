﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria;
using Terraria.ModLoader;
using SpiritMod.NPCs.Reach;

namespace SpiritMod.Mechanics.QuestSystem.Quests
{
    public class SlayerQuestBriar : Quest
    {
        public override string QuestName => "Flowery Fiends";
		public override string QuestClient => "The Adventurer";
		public override string QuestDescription => " I need ya to go down to the Briar and show those beasts who's boss. Kill some of those big hounds an' those Thorn Stalkers. Maybe we can make the Briar a safer place... Ah, who am I kiddin'? That place is bound to stay a hellhole.";
		public override int Difficulty => 3;
		public override string QuestCategory => "Slayer";

		public override (int, int)[] QuestRewards => _rewards;
		private (int, int)[] _rewards = new[]
		{
			(ModContent.ItemType<Items.Accessory.FeralConcoction>(), 1),
			(ModContent.ItemType<Items.Accessory.FloranCharm>(), 1),
			(ModContent.ItemType<Items.Material.EnchantedLeaf>(), 6),
			(Terraria.ID.ItemID.SilverCoin, 50)
		};

        public SlayerQuestBriar()
        {
            _tasks.AddTask(new SlayTask(new int[] { ModContent.NPCType<Reachman>(), ModContent.NPCType<ReachObserver>(), ModContent.NPCType<BlossomHound>(), ModContent.NPCType<ThornStalker>()}, 12, "Briar monsters"));
        }
        public override void OnQuestComplete()
		{
            bool showUnlocks = true;
			QuestManager.UnlockQuest<RootOfTheProblem>(showUnlocks);
            QuestManager.UnlockQuest<SlayerQuestValkyrie>(showUnlocks);
			QuestManager.UnlockQuest<SlayerQuestDrBones>(showUnlocks);
			QuestManager.UnlockQuest<SlayerQuestNymph>(showUnlocks);
			QuestManager.UnlockQuest<SlayerQuestUGDesert>(showUnlocks);
			QuestManager.UnlockQuest<SlayerQuestCavern>(showUnlocks);

			base.OnQuestComplete();
        }
    }
}