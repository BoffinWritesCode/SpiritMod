﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria;
using Terraria.ModLoader;

namespace SpiritMod.NPCs.Town.QuestSystem.Quests
{
    public class ExplorerQuestAurora : Quest
    {
        public override string QuestName => "Lights in the Sky";
		public override string QuestClient => "The Adventurer";
		public override string QuestDescription => "I'll level with you, lad. I don't really have a particular motive for this job except for sharin' the beauty of this world with more people. Have you seen a Boreal Aurora before? It might be the most magical thing in the world, and this world's actually got magic! You can find them at high altitudes and in the snowy tundra. Just go there and take in the sights. You'll thank me, lad.";
		public override int Difficulty => 1;
        public override QuestType QuestType =>  QuestType.Explorer;

        public ExplorerQuestAurora()
        {
            _questSections.Add(new ConcurrentSection(new KillSection(10, 10), new KillSection(15, 10), new KillSection(20, 10)));
        }
    }
}