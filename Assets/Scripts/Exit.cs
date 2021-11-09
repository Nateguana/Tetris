using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public void Update()
    {
        Vector3 pos = Mathx.ScreenPointRayCast(Camera.main.pixelRect.size);
        Vector3 size = GetComponent<SpriteRenderer>().bounds.size / 2;
        transform.position=pos - size;
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
