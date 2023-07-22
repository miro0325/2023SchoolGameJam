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

    private float curTime = 60f; //타이머 

    private int currentStage = 1; //스테이지 값

    public float maxSpawnDelay = 1f; //최대 스폰 딜레이
    public float curSpawnDelay = 0; //현재 스폰 딜레이

    private float[] stageStartFillAmounts = { 0f, 0.106f, 0.328f, 0.551f, 0.773f }; //스테이지 Fill 시작값
    private float[] stageEndFillAmounts = { 0.106f, 0.328f, 0.551f, 0.773f, 1f }; //스테이지 Fill 엔드값

    private float fillAmountVelocity = 0f;

    public static GameManager Instance { get; private set; }

    public List<Transform> curEnemys = new List<Transform> ();
    
    // Start is called before the first frame update
    void Start()
    {
        if(Instance == null) Instance = this;
        else Destroy(this.gameObject);

        Stage.fillAmount = stageStartFillAmounts[currentStage];

    }

    // Update is called once per frame
    void Update()   
    {

        
        if (Stage == null)
            Stage = GetComponent<Image>();

        curTime -= Time.deltaTime; // 타이머 += 프레임
        curSpawnDelay += Time.deltaTime; // 현재 스폰 딜레이 프레임따라 흐르게
        if (curSpawnDelay > maxSpawnDelay) // 현재 스폰 딜레이가 최대 스폰 딜레이를 넘을때 몬스터 소환 및 스폰 딜레이 초기화 
        {
            MonsterSpawn();
            curSpawnDelay = 0;
        }

        // 스테이지가 진행되는 동안 Fill Amount가 증가하도록 설정
        float fillAmountValue = Mathf.Lerp(stageStartFillAmounts[currentStage - 1], stageEndFillAmounts[currentStage - 1], 1f - (curTime / 60f));

        // 스테이지가 진행되고 Fill Amount가 끝값을 넘지 않도록 처리
        if (curTime <= 0f)
        {
            currentStage = Mathf.Clamp(currentStage + 1, 1, stageStartFillAmounts.Length);
            curTime = 60f; // 다음 스테이지로 넘어갔으므로 타이머를 다시 60초로 초기화
        }

        // Fill Amount 업데이트
        Stage.fillAmount = fillAmountValue;

        UpdateTimerText();

        curTime -= Time.deltaTime; //타이머 += 프레임
        curSpawnDelay += Time.deltaTime; //현재 스폰 딜레이 프레임따라 흐르게
        if (curSpawnDelay > maxSpawnDelay) //현재 스폰 딜레이가 최대 스폰 딜레이를 넘을때 몬스터 소환 및 스폰 딜레이 초기화 
        {
            MonsterSpawn();
            curSpawnDelay = 0;
        }

        
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
        Instantiate(MonsterPrf, SpawnPoint.position, SpawnPoint.rotation); //몬스터프리팹 소환
    }
    



}
