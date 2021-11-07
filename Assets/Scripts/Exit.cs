using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight));
        Debug.DrawRay(ray.origin, ray.direction,Color.white,1000);
        Vector3 size=GetComponent<SpriteRenderer>().bounds.size/2;
        new Plane(Vector2.zero, Vector2.one,Vector2.right).Raycast(ray,out float dist);
        transform.position=ray.GetPoint(dist) - size;
        if (Input.GetKeyDown(KeyCode.Escape))
            Quit();
    }
    public void OnMouseDown()
    {
        Quit();
    }

    public void Quit()
    {
        Debug.Log("Quiting");
        Application.Quit();
    }
}
