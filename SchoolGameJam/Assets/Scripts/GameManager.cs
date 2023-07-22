using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public List<Transform> curEnemys = new List<Transform> ();

    public int upgradeDamage = 0;

    public Button foodArchi;

    public Sprite[] upgradeFoodArchi;

    [SerializeField] int upgradeFoodRequire;

    private static int coin;

    public static int GetCoin()
    {
        return coin;
    }

    public static void SetCoin(int value)
    {
        coin = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(Instance == null) Instance = this;
        else Destroy(this.gameObject);
        Initialize();
    }

    private void Initialize()
    {
        SetCoin(99999);
        foodArchi.onClick.AddListener(() =>
        {
            if(coin >= upgradeFoodRequire * (upgradeDamage +1) && upgradeDamage < 3)
            {
                upgradeDamage++;
                SetCoin(GetCoin() - upgradeFoodRequire * (upgradeDamage + 1));
                foodArchi.GetComponent<Image>().sprite = upgradeFoodArchi[upgradeDamage-1];

            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
