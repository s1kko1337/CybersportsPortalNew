using CyberSportsPortal.Data.Entities;
using CyberSportsPortal.Data.Model.Enums;
using CyberSportsPortal.Data.Model.Views;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace CyberSportsPortal.Core.OlympiadServices;

public class TournamentTasksService
{
    public string GetTournamentStatus(Tournament tournament)
    {
        var currentDate = DateTime.UtcNow;
        if (tournament.EndDate <= currentDate)
        {
            return "Окончен";
        }
        else if (tournament.StartDate > currentDate)
        {
            return "Не начался";
        }
        else
        {
            return "В процессе";
        }
    }

    public int GetPlayersFromCountryCount(List<Player> players, Country country)
    {
        if (players == null)
            return 0;

        int count = 0;
        foreach (var player in players)
        {
            if (player != null && player.Country == country)
                count++;
        }
        return count;
    }

    public string GetTeamParticipantsNameString(List<Team> teams)
    {
        if (teams == null || teams.Count == 0)
        {
            return string.Empty;
        }

        return string.Join(", ", teams.Select(team => team.Name));
    } 

    public int ComparePrizes(string prizeA, string prizeB)
    {
        decimal a = decimal.Parse(prizeA, NumberStyles.Any, CultureInfo.InvariantCulture);
        decimal b = decimal.Parse(prizeB, NumberStyles.Any, CultureInfo.InvariantCulture);

        return a.CompareTo(b);
    }

    public Dictionary<int, decimal> GetTournamentVictoryProbabilities(List<TeamWithVictoryProbabilities> teams, Dictionary<int, int> standings)
    {
        return new Dictionary<int, decimal>();
    }
}