using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DamageText : MonoBehaviour
{
    public float y;
    public float duration;
    // Start is called before the first frame update
    void Start()
    {
        transform.DOMoveY(transform.position.y + y, duration);
        transform.GetComponent<Text>().DOFade(0, duration);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
