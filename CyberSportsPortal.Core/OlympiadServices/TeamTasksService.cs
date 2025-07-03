using System;
using CyberSportsPortal.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace CyberSportsPortal.Core.OlympiadServices;

public class TeamTasksService
{
    public int GetTeamIncomeForYear(Team team, int year)
    {
        int totalIncome = 0;

        foreach (var result in team.TeamTournamentResults)
        {
            var tournament = result.Tournament;

            if (tournament.StartDate.Year == year || tournament.EndDate.Year == year)
            {
                if (tournament.TournamentPrizes != null)
                {
                    var prize = tournament.TournamentPrizes
                        .FirstOrDefault(p => p.Place == result.Place);

                    if (prize != null)
                    {
                        totalIncome += prize.Prize;
                    }
                }
            }
        }

        return totalIncome;
    }

    public List<Rating> GetNewRatings(List<MatchHistory> matches, List<Rating> oldRatings)
    {
        Dictionary<int, double> currentRatings = oldRatings.ToDictionary(
            r => r.TeamId,
            r => (double)r.Score
        );

        List<MatchHistory> sortedMatches = matches.OrderBy(m => m.Date).ToList();

        foreach (MatchHistory match in sortedMatches)
        {
            int winnerId = match.WinnerId;
            int loserId = match.LoserId;

            if (!currentRatings.ContainsKey(winnerId) || !currentRatings.ContainsKey(loserId))
                continue;

            double winnerRating = currentRatings[winnerId];
            double loserRating = currentRatings[loserId];

            double ratingChange;

            if (winnerRating > loserRating)
            {
                ratingChange = loserRating * 0.01;
            }
            else if (winnerRating < loserRating)
            {
                ratingChange = loserRating * 0.10;
            }
            else
            {
                ratingChange = loserRating * 0.10;
            }

            currentRatings[winnerId] += ratingChange;
            currentRatings[loserId] += ratingChange;
        }

        return oldRatings.Select(old => new Rating
        {
            TeamId = old.TeamId,
            Score = (int)Math.Round(currentRatings[old.TeamId]),
            Team = old.Team 
        }).ToList();
    }
}