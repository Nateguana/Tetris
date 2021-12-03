using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public static class Mathx
{
    public static Vector3 ScreenPointRayCast(Vector2 screenPoint, Plane plane)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPoint);
        plane.Raycast(ray, out float dist);
        return ray.GetPoint(dist);
    }
    public static Vector3 ScreenPointRayCast(Vector2 screenPoint)
    {
        return ScreenPointRayCast(screenPoint, new Plane(Vector3.back, Vector3.zero));
    }
    public static IEnumerator Request(UnityWebRequest request, Action<string> success, Action error = null)
    {
        using (request)
        {
            yield return request.SendWebRequest();
            if (request.result != UnityWebRequest.Result.Success)
            {
                error();
                yield break;
            }
            success(request.downloadHandler.text);
        }
    }
    public static IEnumerator GetRequest(string uri, Action<string> success, Action error = null)
    {
        yield return Request(UnityWebRequest.Get(uri),success,error);
    }
    public static IEnumerator PutRequest(string uri, string data, Action<string> success, Action error = null)
    {
        yield return Request(UnityWebRequest.Put(uri, data), success, error);
    }
}
