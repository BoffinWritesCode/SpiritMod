﻿using SpiritMod.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace SpiritMod.Mechanics.QuestSystem
{
	public class TalkNPCTask : QuestTask
	{
		public override string ModCallName => "TalkNPC";

		private int _npcType;
		private string _objective;

		public TalkNPCTask() { }

		public TalkNPCTask(int npcType, string objective = null)
		{
			_npcType = npcType;
			_objective = objective;
		}

		public override QuestTask Parse(object[] args)
		{
			// get the npc type
			int npcID = -1;
			if (!QuestUtils.TryUnbox(args[1], out npcID))
			{
				if (QuestUtils.TryUnbox(args[1], out short IDasShort, "NPC Type"))
				{
					npcID = IDasShort;
				}
				else
				{
					return null;
				}
			}

			// get the name override, if there is one
			string objective = null;
			if (args.Length > 2)
			{
				if (!QuestUtils.TryUnbox(args[2], out objective, "Talk NPC Objective"))
				{
					return null;
				}
			}

			return new TalkNPCTask(npcID, objective);
		}

		public override bool CheckCompletion()
		{
			if (Main.netMode == Terraria.ID.NetmodeID.SinglePlayer)
			{
				if (Main.LocalPlayer.talkNPC != -1)
				{
					return Main.npc[Main.LocalPlayer.talkNPC].type == _npcType;
				}
			}
			else if (Main.netMode == Terraria.ID.NetmodeID.Server)
			{
				for (int i = 0; i < Main.player.Length; i++)
				{
					if (Main.player[i].active && Main.player[i].talkNPC >= 0 && Main.npc[Main.player[i].talkNPC].netID == _npcType)
					{
						return true;
					}
				}
			}
			return false;
		}

		public override void AutogeneratedBookText(List<string> lines)
		{
			lines.Add(GetObjectives(false));
		}

		public override void AutogeneratedHUDText(List<string> lines)
		{
			lines.Add(GetObjectives(true));
		}

		public string GetObjectives(bool showProgress)
		{
			StringBuilder builder = new StringBuilder();

			if (_objective != null)
			{
				builder.Append(_objective);
				return builder.ToString();
			}

			builder.Append("Talk to the ").Append(Lang.GetNPCNameValue(_npcType));
			return builder.ToString();
		}
	}
}
