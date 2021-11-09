using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollText : MonoBehaviour
{
    public float maxSpeed;
    public float extraZoom;
    public TextAsset text;
    private TMPro.TMP_Text txt;
    public int reset = 2;
    void Start()
    {
        txt = GetComponent<TMPro.TMP_Text>();
        txt.text = text != null ? text.text : null ?? "Could not be loaded";
    }

    void Update()
    {
        Vector3 currentTop = Mathx.ScreenPointRayCast(Vector2.zero);
        Vector2 extents = txt.bounds.extents;
        Scroll(currentTop, extents);
        Zoom(currentTop, extents);
    }
    private void Scroll(Vector3 currentTop, Vector2 extents)
    {
        float pos = Mathf.Clamp(Input.mousePosition.y / Screen.height * 2 - 1, -1, 1);
        float sqrtPos = pos * pos;

        float move = Mathf.Sign(pos) * sqrtPos * maxSpeed;

        float maxY = extents.y + currentTop.y + (extents.x * extraZoom- extents.x);
        transform.position = Vector3.up * Mathf.Clamp(transform.position.y + move, - maxY, maxY);
        if (reset>0) reset--;
        else if(reset==0)
        {
            transform.position = Vector3.down * maxY;
            reset--;
        }
        Debug.Log(maxY);
    }

    private void Zoom(Vector3 currentTop, Vector2 extents)
    {
        float targetX = extents.x * extraZoom;
        Camera.main.orthographicSize += (targetX + currentTop.x) / 2;
    }
}
