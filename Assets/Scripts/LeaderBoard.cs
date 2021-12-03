using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LeaderBoard : ScrollText
{
    const string URL = "https://gamestream.stream/api/leaderboard?number=100";

    protected override void Start()
    {
        txt = GetComponent<TMPro.TMP_Text>();
        StartCoroutine(Mathx.GetRequest(URL, makeBoard, ()=>txt.text = "Network Error"));
    }
    private void makeBoard(string text)
    {
        Score[] scores = JsonConvert.DeserializeObject<Score[]>(text);
        txt.text = "Top 100 Leaderboard:";
        for (int j = 0; j < scores.Length; j++)
        {
            txt.text += $"\n{j + 1}: {scores[j].name} got {scores[j].score} points on {ConvertTime(scores[j].time)}";
        }
    }
    private static string ConvertTime(ulong? time) =>
           DateTimeOffset.FromUnixTimeMilliseconds((long)time).Date.ToShortDateString();

    public static void SendScore(MonoBehaviour that, Score score, Action<string> callback)
    {
        string data = JsonConvert.SerializeObject(score);
        that.StartCoroutine(Mathx.PutRequest(URL, data, e =>{
            callback($"You got slot {JsonConvert.DeserializeObject<Place>(e).place} on the leaderboard");
        }, () => callback("Network Error")));
    }
    [Serializable]
    public struct Score
    {
        public ulong score;
        public string name;
        public ulong? time;

        public Score(ulong score, string name)
        {
            this.score = score;
            this.name = name;
            time = null;
        }
    }
    [Serializable]
    public struct Place
    {
        public int place;
    }
}
