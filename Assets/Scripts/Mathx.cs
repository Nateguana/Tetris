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
        yield return new WaitForSeconds(.25f);
        yield return request.SendWebRequest();
        if (request.result != UnityWebRequest.Result.Success)
        {
            error();
            request.Dispose();
            yield break;
        }
        success(request.downloadHandler.text);
        request.Dispose();
    }
    public static IEnumerator GetRequest(string uri, Action<string> success, Action error = null)
    {
        yield return Request(UnityWebRequest.Get(uri), success, error);
    }
    public static IEnumerator PutRequest(string uri, string data, Action<string> success, Action error = null)
    {
        UnityWebRequest request = UnityWebRequest.Put(uri, data);
        request.SetRequestHeader("Content-Type", "text/json");
        yield return Request(request, success, error);
    }
}
