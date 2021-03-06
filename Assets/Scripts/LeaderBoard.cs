using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LeaderBoard : ScrollText
{
    const string URL = "https://gamestream.stream/api/leaderboard";

    protected override void Start()
    {
        txt = GetComponent<TMPro.TMP_Text>();
        StartCoroutine(Delay());
    }
    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(.25f);
        yield return Mathx.GetRequest(URL+ "?number=100", makeBoard, () => txt.text = "Network Error");
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

    public static void SendScore(MonoBehaviour that, Score score, Action<uint> callback)
    {
        string data = JsonConvert.SerializeObject(score);
        MonoBehaviour m = FindObjectOfType<AudioManager>();
        m.StartCoroutine(Mathx.PutRequest(URL, data, e => {
            callback(JsonConvert.DeserializeObject<Place>(e).place);
        }, () => callback(uint.MaxValue)));
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
        public uint place;
    }
}
