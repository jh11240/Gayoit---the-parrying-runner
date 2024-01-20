using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossRush : MonoBehaviour
{
    //����
    public TextMeshProUGUI score;
    //���� ��ġ
    public Transform tr;
    //�÷��̾�
    public GameObject player;
    public objectManager objManager;
    public GameObject healthBar;
    public GameObject healthBarReal;

    public GameObject Boss1P;
    public GameObject Boss2P;
    public GameObject Boss3P;
    public GameObject Boss4P;
    public GameObject Boss5P;
    public GameObject Boss6P;
    public GameObject Boss7P;
    public GameObject Boss8P;
    public GameObject Boss9P;
    public GameObject Boss10P;
    public GameObject Boss11P;
    public GameObject Boss12P;

    GameObject boss1;
    GameObject boss2;
    GameObject boss3;
    GameObject boss4;
    GameObject boss5;
    GameObject boss6;
    GameObject boss7;
    GameObject boss8;
    GameObject boss9;
    GameObject boss10;
    GameObject boss11;
    GameObject boss12;

    public List<GameObject> bosses;

    //������ �����Ŭ �׾����� üũ
    private int phase = 1;
    public int Phase { get => phase; set => phase = value; }

    private void Awake()
    {
        bosses = new List<GameObject>();

        boss1 = Instantiate(Boss1P);
        boss1.SetActive(false);
        bosses.Add(boss1);

        boss2 = Instantiate(Boss2P);
        boss2.SetActive(false);
        bosses.Add(boss2);

        boss3 = Instantiate(Boss3P);
        boss3.SetActive(false);
        bosses.Add(boss3);

        boss4 = Instantiate(Boss4P);
        boss4.SetActive(false);
        bosses.Add(boss4);

        boss5 = Instantiate(Boss5P);
        boss5.SetActive(false);
        bosses.Add(boss5);

        boss6 = Instantiate(Boss6P);
        boss6.SetActive(false);
        bosses.Add(boss6);

        boss7 = Instantiate(Boss7P);
        boss7.SetActive(false);
        bosses.Add(boss7);

        boss8 = Instantiate(Boss8P);
        boss8.SetActive(false);
        bosses.Add(boss8);

        boss9 = Instantiate(Boss9P);
        boss9.SetActive(false);
        bosses.Add(boss9);

        boss10 = Instantiate(Boss10P);
        boss10.SetActive(false);
        bosses.Add(boss10);

        boss11 = Instantiate(Boss11P);
        boss11.SetActive(false);
        bosses.Add(boss11);

        boss12 = Instantiate(Boss12P);
        boss12.SetActive(false); 
        bosses.Add(boss12);
    }

    public void OnEnable()
    {
        //�ʱ�ȭ
        sceneManager.Instance.BossKilled = 0;
        InvokeRepeating("checkDeadBossAmount",0f,0.5f);
        respawnBoss();
    }

    //������ ��� �׾����� üũ
    public void checkDeadBossAmount()
    {
        int bossKilled = sceneManager.Instance.BossKilled;
        score.text = bossKilled.ToString()+"����!";
        //�� ����Ŭ�� 12�����̹Ƿ� 12������ ���̸� ���� ������� �Ѿ
        phase=bossKilled/12 >phase? bossKilled/12:phase;
    }
    //boss ��ȯ�ϴ� �Լ�
    public void respawnBoss()
    {
        foreach(GameObject elem in bosses)
        {
            elem.SetActive(false);
        }
        //Ŭ����� �ҷ� ����
        GameObject[] bullets;
        bullets = GameObject.FindGameObjectsWithTag("enemyBullet");
        int bulletsNumber = bullets.Length;

        for (int i = 0; i < bulletsNumber; i++)
        {
            bullets[i].SetActive(false);
        }

        int randBoss = Random.Range(0,12);
        bosses[randBoss].SetActive(true);
        bosses[randBoss].transform.position = tr.position;
        //���� ���̽���ŭ ü�� ����
        bosses[randBoss].GetComponent<enemy>().bossHealth = phase;
        bosses[randBoss].GetComponent<enemy>().maxBossHealth = phase;

        Rigidbody2D rigid = bosses[randBoss].GetComponent<Rigidbody2D>();
        enemy enemyLogic = bosses[randBoss].GetComponent<enemy>();
        enemyLogic.player = player;
        enemyLogic.objManager = objManager;
        enemyLogic.healthBar = healthBar;
        enemyLogic.healthBarReal = healthBarReal;

        Player playerlogic = player.GetComponent<Player>();
        Debug.Log("respawnBoss����");
        enemyLogic.speed = 3;
        rigid.velocity = new Vector2(0, enemyLogic.speed * (-1));

        playerlogic.boss = bosses[randBoss];
        enemyLogic.boss = bosses[randBoss];
        //enemyLogic.spawnManager = playerlogic.spawnManager;
    }

}
