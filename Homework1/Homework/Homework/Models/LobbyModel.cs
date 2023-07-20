using Homework.Server;
using Homework.ServerDatabasa;

namespace Homework.Models
{
    public class LobbyModel
    {
        public string СurrentРlayerName { get; private set; }

        public Table[] Tables { get; private set; }

        public LobbyModel(Player player, Database database)
        {
            СurrentРlayerName = player.Name;
            Tables = new Table[database.Tables.Count];
            int currenTable = 0;
            foreach (var table in database.Tables)
            {
                Tables[currenTable] = new Table(table.Key, currenTable, table.Value, database);
                currenTable++;
            }
        }
    }
}
