/*
* Name: Zack Racz
* Course: CSCI 1250, Section 001
* Assignment: Lab 08, Game Night
* Date: November 22, 2026
* Description: Keeps a game night leaderboard in three text files. Shows the
* season so far, records tonight's points, saves the new totals,
* and adds tonight's result to a history that grows every week.
*/

//Part 1

using System.Drawing;

const string PLAYERS_FILE = "players.txt";
const string POINTS_FILE = "points.txt";
const string HISTORY_FILE = "history.txt";
const int MAX_POINTS = 20;

// Look for the data files next to Program.cs, however the program is started.
string projectFolder = Path.GetFullPath(
Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));
Directory.SetCurrentDirectory(projectFolder);


string[] points = File.ReadAllLines(projectFolder + "//points.txt");
string[] players = File.ReadAllLines(projectFolder + "//players.txt");
string[] history = File.ReadAllLines(projectFolder + "//history.txt");

Console.WriteLine("=== GAME NIGHT LEADERBOARD ===");

if (history.Length == 0)
{
    Console.WriteLine("No nights played yet.");
}
else
{
    Console.WriteLine("Nights Played: " + history.Length);

    for (int i = 0; i < history.Length; i++)
    {
        Console.WriteLine(history[i]);
    }
}
Console.WriteLine("Tonight is night " + (history.Length + 1));

Console.WriteLine("Players".PadRight(11) + "Points");

for (int i = 0; i < points.Length; i++) //Used this loop becasue I needed to go through the index at least once.
{
    Console.WriteLine(players[i].PadRight(11) + points[i]);
}

int leaderIndex = LeaderIndex(points);


Console.WriteLine("Leader: " + players[leaderIndex] + " with " + points[leaderIndex]);

Console.WriteLine("Season total: " + TotalPoints(points));

//Part 2

int highestPoints = 0;
String currentLeader = " ";

for (int i = 0; i < points.Length; i++) //I used this loop to look at each individual item in the index
{
    Console.Write("Points tonight for " + players[i] + " ");
    
    string playerPoints = Console.ReadLine();
    int playerpointsInt = Convert.ToInt32(playerPoints);

    while (playerpointsInt < 0 || playerpointsInt > 20) //This loop sets the exceptions, and only takes inputs that are within a range.
    {
        Console.WriteLine("Points must be from 0 to 20.");
        Console.Write("Points tonight for " + players[i] + " ");
        playerPoints = Console.ReadLine();
        playerpointsInt = Convert.ToInt32(playerPoints);
    }

    if (playerpointsInt > highestPoints)
    {
        highestPoints = playerpointsInt;
        currentLeader = players[i];
    }

    int playerTotal = Convert.ToInt32(points[i]) + playerpointsInt;
    points[i] = playerTotal.ToString();
}

File.WriteAllLines(POINTS_FILE, points);

Console.WriteLine("Tonights winner: " + currentLeader + " with " + highestPoints);
Console.WriteLine("Saved points.txt.");

//Part 3

string[] entry = { HistoryEntry(history.Length + 1, currentLeader, highestPoints)};
File.AppendAllLines(HISTORY_FILE, entry);

//Meathods

static int LeaderIndex(string[] points)
{
    int mostPoints = 0;
    int mostPointsIndex = 0;

    for (int i = 0; i < points.Length; i++) //This loop is used to find and look at each point in this index
    {
        int currentPoints = Convert.ToInt32(points[i]);
        if (currentPoints > mostPoints)
        {
            mostPoints = currentPoints;
            mostPointsIndex = i;
        }
    }
    return mostPointsIndex;
}



static int TotalPoints(string[] points)
{
    int totalPoints = 0;

    foreach(string current in points) //This loop was used becasue this needed to read every item
    {
        totalPoints += Convert.ToInt32(current);
    }
    return totalPoints;
}

static string HistoryEntry(int night, string winner, int points)
{
    return "Night " + night + ": " + winner + " Won with " + points + " points";
}
