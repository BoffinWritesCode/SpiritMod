﻿using SpiritMod.Utilities;
using System.Collections.Generic;
using System.Text;
using Terraria;
using Terraria.ID;

namespace SpiritMod.Mechanics.QuestSystem
{
	public class RetrievalTask : QuestTask
	{
		public override string ModCallName => "Retrieve";

		private int _itemID;
		private int _itemsNeeded;
		private string _wording;
		private int _lastCount;

		public RetrievalTask() { }

		public RetrievalTask(int itemID, int amount, string wordChoice = "Retrieve")
		{
			_itemID = itemID;
			_itemsNeeded = amount;
			_wording = wordChoice;
		}

		public override QuestTask Parse(object[] args)
		{
			// get the item ID
			int itemID = -1;
			if (!QuestUtils.TryUnbox(args[1], out itemID))
			{
				if (QuestUtils.TryUnbox(args[1], out short IDasShort, "Item ID"))
				{
					itemID = IDasShort;
				}
				else
				{
					return null;
				}
			}

			// get the amount
			if (!QuestUtils.TryUnbox(args[2], out int amount, "Item amount"))
			{
				return null;
			}

			// get the word choice, if there is one
			string wordChoice = "Retrieve";
			if (args.Length > 3)
			{
				if (!QuestUtils.TryUnbox(args[3], out wordChoice, "Word choice"))
				{
					return null;
				}
			}

			return new RetrievalTask(itemID, amount, wordChoice);
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

			string itemName = Lang.GetItemNameValue(_itemID);
			string count = _itemsNeeded > 1 ? _itemsNeeded.ToString() : "a";
			builder.Append(_wording).Append(" ").Append(count).Append(" ").Append(itemName);

			// pluralness
			builder.Append(QuestUtils.GetPluralEnding(_itemsNeeded, itemName));

			// add a progress bracket at the end like: (x/y)
			if (showProgress)
			{
				builder.Append(" (").Append(_lastCount).Append("/").Append(_itemsNeeded).Append(")");
			}

			return builder.ToString();
		}

		public override bool CheckCompletion()
		{
			if (Main.netMode == NetmodeID.SinglePlayer)
			{
				_lastCount = Main.LocalPlayer.CountItem(_itemID, _itemsNeeded);
				return _lastCount >= _itemsNeeded;
			}
			else if (Main.netMode == NetmodeID.Server)
			{
				return false;
			}
			return false;
		}
	}
}
