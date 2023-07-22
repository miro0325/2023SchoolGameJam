using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Image Stage; // Image ������Ʈ�� ����ų ����

    public Text Timer; //Ÿ�̸� �ؽ�Ʈ�� ǥ���Ҳ� �޾ƿ�

    public Transform SpawnPoint; //��������Ʈ ��ġ �޾ƿ�
    public GameObject MonsterPrf; //���� ������ �޾ƿ�

    public Transform worldCanvas;

    private float curTime = 60f; //Ÿ�̸� 

    [SerializeField] private int currentStage = 1; //�������� ��

    public float maxSpawnDelay = 1f; //�ִ� ���� ������
    public float curSpawnDelay = 0; //���� ���� ������

    private float[] stageStartFillAmounts = { 0f, 0.147f, 0.222f, 0.36f, 0.431f, 0.569f, 0.641f, 0.783f, 0.854f  }; //�������� Fill ���۰�
    private float[] stageEndFillAmounts = { 0.147f, 0.222f, 0.36f, 0.431f, 0.569f, 0.641f, 0.783f, 0.854f, 1f }; //�������� Fill ���尪

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

        curTime -= Time.deltaTime; // Ÿ�̸� += ������
        if(currentStage % 2 != 0) curSpawnDelay += Time.deltaTime; // ���� ���� ������ �����ӵ��� �帣��
        if (curSpawnDelay > maxSpawnDelay && currentStage % 2 != 0) // ���� ���� �����̰� �ִ� ���� �����̸� ������ ���� ��ȯ �� ���� ������ �ʱ�ȭ 
        {
            
            curSpawnDelay = 0;
            
                   
            //MonsterSpawn();
        }

        // ���������� ����Ǵ� ���� Fill Amount�� �����ϵ��� ����
        float fillAmountValue = Mathf.Lerp(stageStartFillAmounts[currentStage - 1], stageEndFillAmounts[currentStage - 1], 1f - ((currentStage % 2 == 1) ? curTime / 60f : curTime / 10f));

        // ���������� ����ǰ� Fill Amount�� ������ ���� �ʵ��� ó��
        if (curTime <= 0f)
        {
            currentStage = Mathf.Clamp(currentStage + 1, 1, stageStartFillAmounts.Length);
            StartCoroutine(Wave(currentStage));
            curTime = (currentStage % 2 == 1) ? 60f : 10f; // ���� ���������� �Ѿ���Ƿ� Ÿ�̸Ӹ� �ٽ� 60�ʷ� �ʱ�ȭ
        }

        // Fill Amount ������Ʈ
        Stage.fillAmount = fillAmountValue;

        UpdateTimerText();

       

        
    }
    private void UpdateTimerText()
    {
        if (Timer != null)
        {
            int seconds = Mathf.FloorToInt(curTime % 60f);
            string formattedTime = string.Format("{0:00}:{1:00}", 0, seconds); // ���� �׻� 00���� ����
            Timer.text = formattedTime;
        }
    }
    void MonsterSpawn()
    {
        curEnemys.Add(Instantiate(MonsterPrf, SpawnPoint.position, SpawnPoint.rotation).transform); //���������� ��ȯ
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
