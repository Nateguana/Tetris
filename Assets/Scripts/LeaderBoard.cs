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
        StartCoroutine(GetRequest(URL));
    }
    private IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(uri))
        {
            yield return request.SendWebRequest();
            if(request.result != UnityWebRequest.Result.Success)
            {
                txt.text = "Network Error";
                yield break;
            }
            string text = request.downloadHandler.text;
            Score[] scores = JsonConvert.DeserializeObject<Score[]>(text);
            txt.text = "Top 100 Leaderboard:";
            for (int j = 0; j < scores.Length; j++)
            {
                txt.text += $"\n{j+1}: {scores[j].name} got {scores[j].score} points on {ConvertTime(scores[j].time)}";
            }
        }
    }
    private static string ConvertTime(ulong time) =>
           DateTimeOffset.FromUnixTimeMilliseconds((long)time).Date.ToShortDateString();
    [Serializable]
    public class Score
    {
        public ulong score;
        public string name;
        public ulong time;
    }
}
