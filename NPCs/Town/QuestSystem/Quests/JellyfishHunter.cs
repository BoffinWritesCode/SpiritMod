﻿using SpiritMod.NPCs.Boss.MoonWizard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria;
using Terraria.ModLoader;

namespace SpiritMod.NPCs.Town.QuestSystem.Quests
{
    public class JellyfishHunter : Quest
    {
		public override string QuestName => "Jellyfish Hunter";
		public override string QuestClient => "The Adventurer";
		public override string QuestDescription => "Hey there, lad! I was doing some diggin' into what makes these cute jellies tick, and I think they're all part of some kinda hivemind that feed on mystical energy. In fact, that one jellyfish you caught seems to be emitting a distress signal to its buddies. You may need to take on a mighty strong jellyfish soon, so stay prepared!";
		public override int Difficulty => 3;
        public override QuestType QuestType => QuestType.Main | QuestType.Slayer;

        public JellyfishHunter()
        {
            _questSections.Add(new ConcurrentSection(new KillSection(ModContent.NPCType<MoonWizard>(), 1)));
        }
    }
}