using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;


public class spawnManager : MonoBehaviour
{
    public string[] enemyObjs;

    public int stageInt;
    public int spawnIndex;
    public int nextMove;
    public float nextSpawnDelay;
    public float curSpawnDelay;
    public GameObject boss;
    public GameObject healthBar;
    public GameObject healthBarReal;
    public GameObject stageClear;

    public bool spawnEnd;
    public bool bossSpawned;

    public GameObject player;
    public Transform[] spawnPoint;
    public objectManager objManager;
    List<spawn> spawnList;

    private void Awake()
    {
        spawnList = new List<spawn>();
        enemyObjs = new string[] {"enemyWallA","enemyWallB","enemyWallC","enemyWallD","enemyBoss","itemCoin"};
        stageStart();
        nextMove = 0;
    }

    void stageStart()
    {
        readSpawnFile();
    }

    void readSpawnFile()
    {
        spawnList.Clear();
        spawnIndex = 0;
        TextAsset textFile = Resources.Load("stage" + stageInt.ToString()) as TextAsset;
        StringReader stringReader = new StringReader(textFile.text);

        while(stringReader != null)
        {
            string line = stringReader.ReadLine();
            if (line == null)
                break;

            spawn spawnData = new spawn();
            spawnData.delay = float.Parse(line.Split(',')[0]);
            spawnData.type = line.Split(',')[1];
            spawnData.spawnPoint = int.Parse(line.Split(',')[2]);
            spawnList.Add(spawnData);
        }
        stringReader.Close();

        nextSpawnDelay = spawnList[0].delay;

    }

    void Update()
    {
        
        curSpawnDelay += Time.deltaTime;

        if(curSpawnDelay>nextSpawnDelay && !spawnEnd)
        {
    
            spawnEnemy();

            curSpawnDelay = 0;
        }
    }


    void spawnEnemy()
    {
        int enemyIndex = 0;
        switch (spawnList[spawnIndex].type)
        {
            case "A":
                enemyIndex = 0;
                break;
            case "B":
                enemyIndex = 1;
                break;
            case "C":
                enemyIndex = 2;
                break;
            case "D":
                enemyIndex = 3;
                break;
            case "Boss":
                enemyIndex = 4;
                break;
            case "Coin":
                enemyIndex = 5;
                break;


        }
        int enemyPoint = spawnList[spawnIndex].spawnPoint;
        GameObject enemy = objManager.makeObj(enemyObjs[enemyIndex]);
        enemy.transform.position = spawnPoint[enemyPoint].position;
        Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
        enemy enemyLogic = enemy.GetComponent<enemy>();
        enemyLogic.player = player;
        enemyLogic.objManager = objManager;
        enemyLogic.healthBar = healthBar;
        enemyLogic.healthBarReal = healthBarReal;
        enemyLogic.stageClear = stageClear;

        Player playerlogic = player.GetComponent<Player>();

        //speed control
        enemyLogic.speed = Random.Range(5.0f, 7.0f);
        rigid.velocity = new Vector2(0, enemyLogic.speed * (-1));
        


        if (enemy.tag == "boss")
        {
            enemyLogic.speed = 3;
            rigid.velocity = new Vector2(0, enemyLogic.speed * (-1));

            bossSpawned = true;
            playerlogic.boss = enemy;
            enemyLogic.boss = enemy;
            boss = enemy;
            enemyLogic.spawnManager = playerlogic.spawnManager;
        }
        
        spawnIndex++;
        if(spawnIndex == spawnList.Count)
        {
            spawnEnd = true;
            return;
        }
        nextSpawnDelay = spawnList[spawnIndex].delay;
    }
}
