using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rainbow : MonoBehaviour
{
    [SerializeField, Range(0,1f)] float lerpTime = 0f;
    
    [SerializeField] Color[] colors;

    
    SpriteRenderer spriteRenderer;

    public int index = 0;

    float t = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Color color = (Color)spriteRenderer.material.GetColor("GlowColor");
         color = Color.Lerp(color, colors[index], lerpTime* Time.deltaTime);
        spriteRenderer.material.SetColor("GlowColor", color);
        t = Mathf.Lerp(t, 1f, Time.deltaTime *lerpTime);
        if(t>.9f)
        {
            t = 0f;
            index++;
            index = (index>=colors.Length) ? 0 : index;
        }
    }
}
