using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using System.Threading;
using System;
using UnityEngine.SocialPlatforms.Impl;

public class ASM_MN : Singleton<ASM_MN>
{
    public List<Region> listRegion = new List<Region>();
    public static List<Players> listPlayer = new List<Players>();

    private void Start()
    {
        createRegion();
    }

    public void createRegion()
    {
        listRegion.Add(new Region(0, "VN"));
        listRegion.Add(new Region(1, "HK"));
        listRegion.Add(new Region(2, "EU"));
        listRegion.Add(new Region(3, "JP"));
        listRegion.Add(new Region(4, "NA"));
    }

    public string calculate_rank(int score)
    {
        if (score < 100) return "Hang dong";
        else if (score >= 100 && score < 500) return "Hang bac";
        else if (score >= 500 && score < 1000) return "Hang vang";
        else if (score >= 1000) return "Hang Kim cuong";
            return null;

    }

    public void YC1(ScoreKeeper scoreKeeper)
    {
        int playerID = scoreKeeper.GetID();
        string playerUserName = scoreKeeper.GetUserName();
        int playerScore = scoreKeeper.GetScore();
        int playerRegionID = scoreKeeper.GetIDregion();

        Region playerRegion = listRegion.FirstOrDefault(r => r.ID == playerRegionID);

        Players newPlayer = new Players(playerID, playerUserName, playerScore, playerRegion);
        listPlayer.Add(newPlayer);
        listPlayer.Add(new Players(1, "1st player", 100, new Region(1, "VN")));
        listPlayer.Add(new Players(2, "2nd player", 200, new Region(1, "VN")));
        listPlayer.Add(new Players(3, "3rd player", 300, new Region(1, "VN")));
        listPlayer.Add(new Players(4, "4th player", 400, new Region(1, "VN")));
        listPlayer.Add(new Players(5, "5th player", 500, new Region(1, "VN")));
    }

    public void YC2()
    {
        foreach (Players player in listPlayer)
        {
            Debug.Log($"ID: {player.ID}, Name: {player.Name}, Score: {player.Score}, Region: {player.Region.Name}, Rank: {calculate_rank(player.Score)}");
        }
    }

    public void YC3(int score)
    {
        Debug.Log(" Player có điểm bé hơn điểm hiện tại của người chơi:");
        var playersWithLessScore = listPlayer.Where(player => player.Score < score);

        foreach (Players player in playersWithLessScore)
        {
            Debug.Log($"ID: {player.ID}, Name: {player.Name}, Score: {player.Score}, Region: {player.Region.Name}, Rank: {calculate_rank(player.Score)}");
        }
    }
    public void YC4(int playerID)
    {
        Debug.Log("Player theo Id của người chơi hiện tại là:");
        Players player = listPlayer.FirstOrDefault(p => p.ID == playerID);
        if (player != null)
        {
            Debug.Log("Id:" + player.ID + " Name: " + player.Name + "score:" + player.Score + "hang:" + calculate_rank(player.Score));
        }
        else
        {
            Debug.Log("Player not found.");
        }
    }
    public void YC5()
    {
        Debug.Log("các Player trong listPlayer theo thứ tự điểm giảm dần là:");
        var sortedPlayers = listPlayer.OrderByDescending(p => p.Score);
        foreach (Players player in sortedPlayers)
        {
            Debug.Log("Id:" + player.ID + " Name: " + player.Name + " Score:" + player.Score + " Region: " + player.Region.Name + " Rank: " + calculate_rank(player.Score));
        }
    }
    public void YC6()
    {
        Debug.Log("5 player có score thấp nhất theo thứ tự tăng dần là:");
        var lowestScorePlayers = listPlayer.OrderBy(p => p.Score).Take(5);
        foreach (Players player in lowestScorePlayers)
        {
            Debug.Log("Id:" + player.ID + " Name: " + player.Name + " Score:" + player.Score + " Region: " + player.Region.Name + " Rank: " + calculate_rank(player.Score));
        }
    }
    public void YC7()
    {
        Thread thread = new Thread(CalculateAndSaveAverageScoreByRegion);
        thread.Name = "BXH";
        thread.Start();
    }
    void CalculateAndSaveAverageScoreByRegion()
    {
        var averageScoresByRegion = listPlayer
           .GroupBy(p => p.Region)
           .Select(g => new { Region = g.Key, AverageScore = g.Average(p => p.Score) })
           .ToList();

        using (StreamWriter writer = new StreamWriter("bxhRegion.txt"))
        {
            foreach (var entry in averageScoresByRegion)
            {
                writer.WriteLine($"Region: {entry.Region.Name}, Average Score: {entry.AverageScore}");
            }
        }
    }
}

[SerializeField]
public class Region
{
    public int ID;
    public string Name;
    public Region(int ID, string Name)
    {
        this.ID = ID;
        this.Name = Name;
    }
}

[SerializeField]
public class Players
{
    public int ID { get; set; }
    public string Name { get; set; }
    public int Score { get; set; }
    public Region Region { get; set; }

    public Players(int id, string name, int score, Region region)
    {
        ID = id;
        Name = name;
        Score = score;
        Region = region;
    }
}