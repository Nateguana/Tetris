using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Mathx {
   public static Vector3 ScreenPointRayCast(Vector2 screenPoint,Plane plane)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPoint);
        plane.Raycast(ray, out float dist);
        return ray.GetPoint(dist);
    }
    public static Vector3 ScreenPointRayCast(Vector2 screenPoint)
    {
        return ScreenPointRayCast(screenPoint, new Plane(Vector3.back, Vector3.zero));
    }
}
