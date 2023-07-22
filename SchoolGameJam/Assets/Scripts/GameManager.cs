using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Image Stage; // Image 컴포넌트를 가리킬 변수

    public Text Timer; //타이머 텍스트로 표시할꺼 받아옴

    public Transform SpawnPoint; //스폰포인트 위치 받아옴
    public GameObject MonsterPrf; //몬스터 프리팹 받아옴

    public Transform worldCanvas;

    private float curTime = 60f; //타이머 

    [SerializeField] private int currentStage = 1; //스테이지 값

    public float maxSpawnDelay = 1f; //최대 스폰 딜레이
    public float curSpawnDelay = 0; //현재 스폰 딜레이

    private float[] stageStartFillAmounts = { 0f, 0.147f, 0.222f, 0.36f, 0.431f, 0.569f, 0.641f, 0.783f, 0.854f  }; //스테이지 Fill 시작값
    private float[] stageEndFillAmounts = { 0.147f, 0.222f, 0.36f, 0.431f, 0.569f, 0.641f, 0.783f, 0.854f, 1f }; //스테이지 Fill 엔드값

    private float fillAmountVelocity = 0f;

    public static GameManager Instance { get; private set; }

    public List<Transform> curEnemys = new List<Transform> ();

    // Start is called before the first frame update

    public int upgradeDamage = 0;
    public Button foodArchi;
    public Sprite[] upgradeFoodArchi;
    [SerializeField] int upgradeFoodRequire;

    public int upgradeSkill = 0;
    public Button box;
    public Sprite[] upgradeBox;
    [SerializeField] int upgradeBoxRequire;

    public int upgradeSkill2 = 0;
    public Button house;
    public Sprite[] upgradeHouse;
    [SerializeField] int upgradeHouseRequire;

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
    

    private void Initialize()
    {
        SetCoin(99999);
        foodArchi.onClick.AddListener(() =>
        {
            if (coin >= upgradeFoodRequire * (upgradeDamage + 1) && upgradeDamage < 3)
            {
                upgradeDamage++;
                SetCoin(GetCoin() - upgradeFoodRequire * (upgradeDamage + 1));
                foodArchi.GetComponent<Image>().sprite = upgradeFoodArchi[upgradeDamage - 1];

            }
        });
        box.onClick.AddListener(() =>
        {
            if (coin >= upgradeBoxRequire * (upgradeSkill + 1) && upgradeSkill < 3)
            {
                upgradeSkill++;
                SetCoin(GetCoin() - upgradeBoxRequire * (upgradeSkill + 1));
                box.GetComponent<Image>().sprite = upgradeBox[upgradeSkill - 1];

            }
        });
        house.onClick.AddListener(() =>
        {
            if (coin >= upgradeHouseRequire * (upgradeSkill2 + 1) && upgradeSkill2 < 3)
            {
                upgradeSkill2++;
                SetCoin(GetCoin() - upgradeHouseRequire * (upgradeSkill2 + 1));
                house.GetComponent<Image>().sprite = upgradeHouse[upgradeSkill2 - 1];

            }
        });
    }
    void Awake()
    {
        if(Instance == null) Instance = this;
        else Destroy(this.gameObject);
        Initialize();
        StartCoroutine(Wave(currentStage));

        Stage.fillAmount = stageStartFillAmounts[currentStage];

    }

    // Update is called once per frame
    void Update()   
    {

        
        if (Stage == null)
            Stage = GetComponent<Image>();

        curTime -= Time.deltaTime; // 타이머 += 프레임
        if(currentStage % 2 != 0) curSpawnDelay += Time.deltaTime; // 현재 스폰 딜레이 프레임따라 흐르게
        if (curSpawnDelay > maxSpawnDelay && currentStage % 2 != 0) // 현재 스폰 딜레이가 최대 스폰 딜레이를 넘을때 몬스터 소환 및 스폰 딜레이 초기화 
        {
            
            curSpawnDelay = 0;
            
                   
            //MonsterSpawn();
        }

        // 스테이지가 진행되는 동안 Fill Amount가 증가하도록 설정
        float fillAmountValue = Mathf.Lerp(stageStartFillAmounts[currentStage - 1], stageEndFillAmounts[currentStage - 1], 1f - ((currentStage % 2 == 1) ? curTime / 60f : curTime / 10f));

        // 스테이지가 진행되고 Fill Amount가 끝값을 넘지 않도록 처리
        if (curTime <= 0f)
        {
            currentStage = Mathf.Clamp(currentStage + 1, 1, stageStartFillAmounts.Length);
            StartCoroutine(Wave(currentStage));
            curTime = (currentStage % 2 == 1) ? 60f : 10f; // 다음 스테이지로 넘어갔으므로 타이머를 다시 60초로 초기화
        }

        // Fill Amount 업데이트
        Stage.fillAmount = fillAmountValue;

        UpdateTimerText();

       

        
    }
    private void UpdateTimerText()
    {
        if (Timer != null)
        {
            int seconds = Mathf.FloorToInt(curTime % 60f);
            string formattedTime = string.Format("{0:00}:{1:00}", 0, seconds); // 분은 항상 00으로 고정
            Timer.text = formattedTime;
        }
    }
    void MonsterSpawn()
    {
        curEnemys.Add(Instantiate(MonsterPrf, SpawnPoint.position, SpawnPoint.rotation).transform); //몬스터프리팹 소환
    }

    IEnumerator Wave(int stage)
    {
        switch(stage)
        {
            case 1:
                float t = 3f;
                for(int i = 0; i < 5; i++)
                {
                    float rt = Random.Range(0.5f, t);
                    t -= rt;
                    yield return new WaitForSeconds(rt);
                    MonsterSpawn();
                }
                yield return new WaitForSeconds(3);
                t = 3f;
                for (int i = 0; i < 5; i++)
                {
                    float rt = Random.Range(0.5f, t);
                    t -= rt;
                    yield return new WaitForSeconds(rt);

                    MonsterSpawn();
                }
                break;
            default:
                break;
        }
    }
    



}
