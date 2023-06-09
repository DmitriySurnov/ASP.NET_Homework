﻿using Homework.Server;
using Homework.ServerDatabase;

namespace Homework.Models
{
	public class Table
	{
		public int NumberTable { get; private set; } // Номер стола

		public Guid TableGuid { get; private set; }

		public string[] NamesPlayers { get; private set; } // Имена играков

		public int NumberFilledCells { get; private set; } // количество ходов от старта 

		public string WalkingPlayerName { get; private set; } // Имя игрока который сейчас ходит

		public FieldModel GameField { get; private set; } //  игровое полее

		public Table(Guid tableGuid, int numberTable, Game game)
		{
			NumberTable = numberTable;
			TableGuid = tableGuid;
			NamesPlayers = new string[2];
			NamesPlayers[0] = GetPlayerName(game.PlayerXGuid);
			NamesPlayers[1] = GetPlayerName(game.PlayerOGuid);
			WalkingPlayerName = GetPlayerName(game.WhichPlayerWalkingGuid);
			Field fieldGame;
			if (game.Field == string.Empty)
			{
				fieldGame = new Field();
				fieldGame.FillTheField();
			}
			else
			{
				fieldGame = new Field(game.Field);
			}
			NumberFilledCells = fieldGame.NumberFilledCells;
			GameField = new FieldModel(fieldGame.FieldGame);
		}

		private static string GetPlayerName(Guid PlayerGuid)
		{
			return PlayerGuid == Guid.Empty ?
					new Player().Name :
					Database.Players[PlayerGuid].Name;
		}
	}
}
