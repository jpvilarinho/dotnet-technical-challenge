using System;
using System.Threading.Tasks;

public class Program
{
    public static async Task Main(string[] args)
    {
        string teamName1 = "Paris Saint-Germain";
        int year1 = 2013;
        int totalGoals1 = await FootballDataService.GetTotalScoredGoalsAsync(teamName1, year1);
        Console.WriteLine($"Team {teamName1} scored {totalGoals1} goals in {year1}");

        string teamName2 = "Chelsea";
        int year2 = 2014;
        int totalGoals2 = await FootballDataService.GetTotalScoredGoalsAsync(teamName2, year2);
        Console.WriteLine($"Team {teamName2} scored {totalGoals2} goals in {year2}");
    }
}
