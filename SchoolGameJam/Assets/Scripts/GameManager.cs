using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    [Header("스테이지")]

    public Image Stage; // Image 컴포넌트를 가리킬 변수

    public Text Timer; //타이머 텍스트로 표시할꺼 받아옴
    [SerializeField] Transform blackPanel;

    public Transform SpawnPoint; //스폰포인트 위치 받아옴
    public GameObject[] MonsterPrf; //몬스터 프리팹 받아옴

    public GameObject[] Skills;

    public Sprite[] bgs;

    public SpriteRenderer bg;


    [Header("월드 캔버스")]

    public Transform worldCanvas;
    [Header("코인 텍스트")]

    public Text coinText;

    private float curTime = 60f; //타이머 
    [Header("현재 스테이지")]
    [SerializeField] private int currentStage = 1; //스테이지 값

    public float maxSpawnDelay = 1f; //최대 스폰 딜레이
    public float curSpawnDelay = 0; //현재 스폰 딜레이
    
    private float[] stageStartFillAmounts = { 0f, 0.147f, 0.222f, 0.36f, 0.431f, 0.569f, 0.641f, 0.783f, 0.854f  }; //스테이지 Fill 시작값
    private float[] stageEndFillAmounts = { 0.147f, 0.222f, 0.36f, 0.431f, 0.569f, 0.641f, 0.783f, 0.854f, 1f }; //스테이지 Fill 엔드값

    private float fillAmountVelocity = 0f;

    [SerializeField] Text text;
    [SerializeField] Text text2;
    [SerializeField] Text text3;

    [Header("목표 몬스터 수")]


    public int[] targetValue;
    public int currentValue;

    public static GameManager Instance { get; private set; }
    [Header("생존 몬스터")]

    public List<Transform> curEnemys = new List<Transform> ();

    // Start is called before the first frame update
    [Header("스킬")]

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
    [Header("몬스터")]
    [SerializeField] Sprite[] mobSprite;
    Coroutine coroutine;

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
        
        text.gameObject.SetActive(false);
        text2.gameObject.SetActive(false);

        text3.gameObject.SetActive(false);

        foodArchi.onClick.AddListener(() =>
        {
            if (coin >= upgradeFoodRequire + (upgradeDamage ) * 100 && upgradeDamage < 3)
            {
                if (upgradeDamage == 0)
                {
                    Skills[2].SetActive(true);
                    text3.gameObject.SetActive(true);

                    Player.Instance.requireSkills[0] = false;
                }
                upgradeDamage++;

                text3.text = "Lv" + upgradeDamage.ToString();

                SetCoin(GetCoin() - (upgradeFoodRequire + (upgradeDamage) * 100));
                foodArchi.GetComponent<Image>().sprite = upgradeFoodArchi[upgradeDamage - 1];

            }
        });
        box.onClick.AddListener(() =>
        {
            if (coin >= (upgradeBoxRequire + (upgradeSkill) * 100) && upgradeSkill < 3)
            {
                if (upgradeSkill == 0)
                {
                    Skills[1].SetActive(true);
                    text2.gameObject.SetActive(true);

                    Player.Instance.requireSkills[1] = false;
                }

                upgradeSkill++;
                text2.text = "Lv" + upgradeSkill.ToString();

                SetCoin(GetCoin() - (upgradeBoxRequire + (upgradeSkill ) * 100));
                box.GetComponent<Image>().sprite = upgradeBox[upgradeSkill - 1];

            }
        });
        house.onClick.AddListener(() =>
        {
            if (coin >= upgradeHouseRequire + (upgradeSkill2 ) * 100 && upgradeSkill2 < 3)
            {
                if (upgradeSkill2 == 0)
                {
                    Skills[0].SetActive(true);
                    text.gameObject.SetActive(true);

                    Player.Instance.requireSkills[2] = false;
                }

                upgradeSkill2++;
                text.text = "Lv" + upgradeSkill2.ToString();

                SetCoin(GetCoin() - (upgradeHouseRequire + (upgradeSkill2 ) * 100));
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
        if (currentStage % 2 != 0)
        {
            curSpawnDelay += Time.deltaTime; // 현재 스폰 딜레이 프레임따라 흐르게
            if(currentValue ==  targetValue[currentStage-1] )
            {
                currentValue = 0;
                currentStage = Mathf.Clamp(currentStage + 1, 1, stageStartFillAmounts.Length);
                StartCoroutine(Wave(currentStage));
                curTime = (currentStage % 2 == 1) ? 60f : 10f;
            }
        }
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
            if (currentValue < targetValue[currentStage - 1] && currentStage % 2 != 0)
            {
                currentStage = 1;
                blackPanel.transform.DOMoveX(5000, 0f);
                curTime = (currentStage % 2 == 1) ? 60f : 10f; // 다음 스테이지로 넘어갔으므로 타이머를 다시 60초로 초기화

                blackPanel.transform.DOMoveX(-3000, 5f);
                StartCoroutine(Delayed());
                
                return;
            }
            currentStage = Mathf.Clamp(currentStage + 1, 1, stageStartFillAmounts.Length);
            if (coroutine != null) StartCoroutine(Wave(currentStage));
            if(coroutine == null)
                coroutine = StartCoroutine(Wave(currentStage));
            curTime = (currentStage % 2 == 1) ? 60f : 10f; // 다음 스테이지로 넘어갔으므로 타이머를 다시 60초로 초기화
            if(currentStage == 9) curTime = 90f;
        }

        // Fill Amount 업데이트
        Stage.fillAmount = fillAmountValue;

        UpdateTimerText();

       UpdateCoinUI();  

        
    }

    void UpdateCoinUI()
    {
        coinText.text = GetCoin().ToString();
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
    void MonsterSpawn(int index)
    {
        
        var enemy = Instantiate(MonsterPrf[index], SpawnPoint.position, SpawnPoint.rotation);
        //if(index == 0) enemy.GetComponent<SpriteRenderer>().sprite = mobSprite[Random.Range(0, mobSprite.Length)];
        curEnemys.Add(enemy.transform); //몬스터프리팹 소환
    }

    IEnumerator Delayed()
    {
        yield return new WaitForSeconds(1.5f);
        for (int i = 0; i < GameManager.Instance.curEnemys.Count; i++)
        {
            if (curEnemys[i] != null)
                curEnemys[i].GetComponent<MonsterBase>().Damaged(9999);

                
        }
        currentValue = 0;
        foodArchi.GetComponent<Image>().sprite = upgradeFoodArchi[3];
        box.GetComponent<Image>().sprite = upgradeFoodArchi[3];
        house.GetComponent<Image>().sprite = upgradeFoodArchi[3];
        upgradeDamage = 0;
        Player.Instance.curSkillCooltimes[0] = 0;
        Player.Instance.curSkillCooltimes[1] = 0;

        Player.Instance.curSkillCooltimes[2] = 0;

        Player.Instance.useSkills[0] = false;
        Player.Instance.useSkills[1] = false;

        Player.Instance.useSkills[2] = false;

        upgradeSkill = 0;
        upgradeSkill2 = 0;
        text.gameObject.SetActive(false);
        text2.gameObject.SetActive(false);
        Player.Instance.requireSkills[0] = true;
        Player.Instance.requireSkills[1] = true;
        Player.Instance.requireSkills[2] = true;

        StartCoroutine(Delay(0, 1));
        text3.gameObject.SetActive(false);
        Skills[0].SetActive(false);
        Skills[1].SetActive(false);

        Skills[2].SetActive(false);

        Player.Instance.hp = Player.Instance.maxhp;
        currentStage = 1;
        SetCoin(0);

        if (coroutine == null)
            coroutine = StartCoroutine(Wave(currentStage));
    }

    IEnumerator Wave(int stage)
    {
        float t = 0;
        switch (stage)
        {
            case 1:
                 t = 3f;
                for(int i = 0; i < 5; i++)
                {
                    float rt = Random.Range(0.5f, t);
                    t -= rt;
                    yield return new WaitForSeconds(rt);
                    MonsterSpawn(Random.Range(0, 3));
                }
                yield return new WaitForSeconds(3);
                t = 6f;
                for (int i = 0; i < 10; i++)
                {
                    float rt = Random.Range(0.5f, t);
                    t -= rt;
                    yield return new WaitForSeconds(rt);

                    MonsterSpawn(Random.Range(0, 3));
                }
                yield return new WaitForSeconds(1.5f);
                t = 12f;
                for (int i = 0; i < 15; i++)
                {
                    float rt = Random.Range(0.5f, t);
                    t -= rt;
                    yield return new WaitForSeconds(rt);

                    MonsterSpawn(Random.Range(0, 3));
                }
                break;
            case 2:
                blackPanel.transform.DOMoveX(5000, 0f);

                blackPanel.transform.DOMoveX(-3000, 5f);
                break;
            case 3:
                t = 9f;
                for (int i = 0; i < 10; i++)
                {
                    float rt = Random.Range(0.5f, t);
                    t -= rt;
                    yield return new WaitForSeconds(rt);
                    MonsterSpawn(Random.Range(0, 3));
                }
                yield return new WaitForSeconds(3);
                t = 12f;
                for (int i = 0; i < 20; i++)
                {
                    float rt = Random.Range(0.5f, t);
                    t -= rt;
                    yield return new WaitForSeconds(rt);

                    MonsterSpawn(Random.Range(0,3));
                }
                yield return new WaitForSeconds(3);
                t = 12f;
                for (int i = 0; i < 5; i++)
                {
                    float rt = Random.Range(0.5f, t);
                    t -= rt;
                    yield return new WaitForSeconds(rt);

                    MonsterSpawn(3);
                }
                break;
            case 4:
                blackPanel.transform.DOMoveX(5000, 0f);

                blackPanel.transform.DOMoveX(-3000, 5f);
                StartCoroutine(Delay(1, 1));
                break;
            case 5:
                t = 9f;
                Debug.Log("Wa");
                for (int i = 0; i < 10; i++)
                {
                    float rt = Random.Range(0.5f, t);
                    t -= rt;
                    yield return new WaitForSeconds(rt);
                    MonsterSpawn(3);
                }
                yield return new WaitForSeconds(3);
                t = 12f;
                for (int i = 0; i < 15; i++)
                {
                    float rt = Random.Range(0.5f, t);
                    t -= rt;
                    yield return new WaitForSeconds(rt);

                    MonsterSpawn(Random.Range(0, 3));
                }
                break;
            case 6:
                blackPanel.transform.DOMoveX(50000, 0f);

                blackPanel.transform.DOMoveX(-3000, 5f);
                break;
            case 7:
                t =  20f;
                for (int i = 0; i < 30; i++)
                {
                    float rt = Random.Range(0.5f, t);
                    t -= rt;
                    yield return new WaitForSeconds(rt);
                    MonsterSpawn(3);
                }
                yield return new WaitForSeconds(2);
                t = 17f;
                for (int i = 0; i < 30; i++)
                {
                    float rt = Random.Range(0.5f, t);
                    t -= rt;
                    yield return new WaitForSeconds(rt);

                    MonsterSpawn(3);
                }
                
                break;
            case 8:
                blackPanel.transform.DOMoveX(50000, 0f);
                blackPanel.transform.DOMoveX(-3000, 5f);
                StartCoroutine(Delay(2, 1));
                break;
            case 9:
                t = 9f;
                for (int i = 0; i < 15; i++)
                {
                    float rt = Random.Range(0.5f, t);
                    t -= rt;
                    yield return new WaitForSeconds(rt);
                    MonsterSpawn(Random.Range(0, 3));
                }
                yield return new WaitForSeconds(3);
                t = 12f;
                for (int i = 0; i < 20; i++)
                {
                    float rt = Random.Range(0.5f, t);
                    t -= rt;
                    yield return new WaitForSeconds(rt);

                    MonsterSpawn(3);
                }
                yield return new WaitForSeconds(3);
                t = 18f;
                for (int i = 0; i < 30; i++)
                {
                    float rt = Random.Range(0.5f, t);
                    t -= rt;
                    yield return new WaitForSeconds(rt);

                    MonsterSpawn(3);
                }
                yield return new WaitForSeconds(3);
                MonsterSpawn(4);
                break;
            case 10:
                break;
            default:
                break;
        }

        
    }


    IEnumerator Delay(int index, float delay)
    {
        yield return new WaitForSeconds(delay);
        bg.sprite = bgs[index];
    }

}
