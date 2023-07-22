using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public List<Transform> curEnemys = new List<Transform> ();
    
    // Start is called before the first frame update
    void Start()
    {
        if(Instance == null) Instance = this;
        else Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
