using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollText : MonoBehaviour
{
    public float maxSpeed;
    public TextAsset text;
    private TMPro.TMP_Text txt;
    void Start()
    {
        txt = GetComponent<TMPro.TMP_Text>();
        txt.text = text != null ? text.text : null ?? "Could not be loaded";
    }

    void Update()
    {
        //txt.bounds.extents
        float pos = Mathf.Clamp(Input.mousePosition.y / Screen.height*2 - 1,-1,1);
        float sqrtPos = pos *pos;
        float move = Mathf.Sign(pos) * sqrtPos* maxSpeed;
        transform.position += Vector3.up * move;
    }
}
