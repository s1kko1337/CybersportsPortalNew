using CyberSportsPortal.Data.Model.Views;
using System.Collections.Generic;

namespace CyberSportsPortal.Core.OlympiadServices;

public class PlayerTasksService
{
    public List<PlayerView> FilterPlayers(List<PlayerView> players, string filter)
    {
        var result = new List<PlayerView>();
        if (string.IsNullOrEmpty(filter))
        {
            return players;
        }
        var lowerCaseFilter = filter.ToLower();
        foreach (var player in players)
        {
            if (player.NickName != null && player.NickName.ToLower().Contains(lowerCaseFilter))
            {
                result.Add(player);
            }
            else if (player.CombinedName != null && player.CombinedName.ToLower().Contains(lowerCaseFilter))
            {
                result.Add(player);
            }
        }
        return result;
    }
}