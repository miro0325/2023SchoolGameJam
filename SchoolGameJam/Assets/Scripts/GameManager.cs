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

    private float curTime = 60f; //Ÿ�̸� 

    private int currentStage = 1; //�������� ��

    public float maxSpawnDelay = 1f; //�ִ� ���� ������
    public float curSpawnDelay = 0; //���� ���� ������

    private float[] stageStartFillAmounts = { 0f, 0.106f, 0.328f, 0.551f, 0.773f }; //�������� Fill ���۰�
    private float[] stageEndFillAmounts = { 0.106f, 0.328f, 0.551f, 0.773f, 1f }; //�������� Fill ���尪

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

        curTime -= Time.deltaTime; // Ÿ�̸� += ������
        curSpawnDelay += Time.deltaTime; // ���� ���� ������ �����ӵ��� �帣��
        if (curSpawnDelay > maxSpawnDelay) // ���� ���� �����̰� �ִ� ���� �����̸� ������ ���� ��ȯ �� ���� ������ �ʱ�ȭ 
        {
            MonsterSpawn();
            curSpawnDelay = 0;
        }

        // ���������� ����Ǵ� ���� Fill Amount�� �����ϵ��� ����
        float fillAmountValue = Mathf.Lerp(stageStartFillAmounts[currentStage - 1], stageEndFillAmounts[currentStage - 1], 1f - (curTime / 60f));

        // ���������� ����ǰ� Fill Amount�� ������ ���� �ʵ��� ó��
        if (curTime <= 0f)
        {
            currentStage = Mathf.Clamp(currentStage + 1, 1, stageStartFillAmounts.Length);
            curTime = 60f; // ���� ���������� �Ѿ���Ƿ� Ÿ�̸Ӹ� �ٽ� 60�ʷ� �ʱ�ȭ
        }

        // Fill Amount ������Ʈ
        Stage.fillAmount = fillAmountValue;

        UpdateTimerText();

        curTime -= Time.deltaTime; //Ÿ�̸� += ������
        curSpawnDelay += Time.deltaTime; //���� ���� ������ �����ӵ��� �帣��
        if (curSpawnDelay > maxSpawnDelay) //���� ���� �����̰� �ִ� ���� �����̸� ������ ���� ��ȯ �� ���� ������ �ʱ�ȭ 
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
            string formattedTime = string.Format("{0:00}:{1:00}", 0, seconds); // ���� �׻� 00���� ����
            Timer.text = formattedTime;
        }
    }
    void MonsterSpawn()
    {
        Instantiate(MonsterPrf, SpawnPoint.position, SpawnPoint.rotation); //���������� ��ȯ
    }
    



}
