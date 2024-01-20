using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectManager : MonoBehaviour
{
    //enemy
    public GameObject enemyWallAPrefab;
    public GameObject enemyWallBPrefab;
    public GameObject enemyWallCPrefab;
    public GameObject enemyWallDPrefab;
    public GameObject enemyBossPrefab;
    //coin
    public GameObject itemCoinPrefab;
    //bullet
    public GameObject bossBulletAPrefab;
    public GameObject bossBulletBPrefab;
    public GameObject bossBulletCPrefab;
    public GameObject bossBulletDPrefab;
    

    GameObject[] targetPool;

    //enemy
    GameObject[] enemyWallA;
    GameObject[] enemyWallB;
    GameObject[] enemyWallC;
    GameObject[] enemyWallD;
    GameObject[] enemyBoss;
    //coin
    GameObject[] itemCoin;
    //bullet
    GameObject[] bossBulletA;
    GameObject[] bossBulletB;
    GameObject[] bossBulletC;
    GameObject[] bossBulletD;

    private void Awake()
    {
        enemyWallA = new GameObject[30];
        enemyWallB = new GameObject[30];
        enemyWallC = new GameObject[20];
        enemyWallD = new GameObject[10];
        enemyBoss = new GameObject[1];

        itemCoin = new GameObject[20];

        bossBulletA = new GameObject[40];
        bossBulletB = new GameObject[40];
        bossBulletC = new GameObject[40];
        bossBulletD = new GameObject[1];
        generate();
    }

    void generate()
    {
        for(int i = 0; i < enemyWallA.Length; i++)
        {
            if (enemyWallAPrefab == null) break;
            enemyWallA[i] = Instantiate(enemyWallAPrefab);
            enemyWallA[i].SetActive(false);
        }
        for(int i = 0; i < enemyWallB.Length; i++)
        {
            if (enemyWallBPrefab == null) break;

            enemyWallB[i] = Instantiate(enemyWallBPrefab);
            enemyWallB[i].SetActive(false);
        }
        for(int i = 0; i < enemyWallC.Length; i++)
        {
            if (enemyWallCPrefab == null) break;

            enemyWallC[i] = Instantiate(enemyWallCPrefab);
            enemyWallC[i].SetActive(false);
        }
        for(int i = 0; i < enemyWallD.Length; i++)
        {
            if (enemyWallDPrefab == null) break;

            enemyWallC[i] = Instantiate(enemyWallCPrefab);
            enemyWallC[i].SetActive(false);
        }
        for(int i = 0; i < enemyBoss.Length; i++)
        {
            if (enemyBossPrefab == null) break;
            
            enemyBoss[i] = Instantiate(enemyBossPrefab);
            enemyBoss[i].SetActive(false);
        }
        for(int i = 0; i < itemCoin.Length; i++)
        {
            if (itemCoinPrefab == null) break;
            itemCoin[i] = Instantiate(itemCoinPrefab);
            itemCoin[i].SetActive(false);
        }
        for(int i = 0; i < bossBulletA.Length; i++)
        {
            if (bossBulletAPrefab == null) break;
            bossBulletA[i] = Instantiate(bossBulletAPrefab);
            bossBulletA[i].SetActive(false);
        }
        for(int i = 0; i < bossBulletB.Length; i++)
        {
            if (bossBulletBPrefab == null) break;

            bossBulletB[i] = Instantiate(bossBulletBPrefab);
            bossBulletB[i].SetActive(false);
        }
        for(int i = 0; i < bossBulletC.Length; i++)
        {
            if (bossBulletCPrefab == null) break;

            bossBulletC[i] = Instantiate(bossBulletCPrefab);
            bossBulletC[i].SetActive(false);
        }
        for(int i = 0; i < bossBulletD.Length; i++)
        {
            if (bossBulletDPrefab == null) break;

            bossBulletD[i] = Instantiate(bossBulletDPrefab);
            bossBulletD[i].SetActive(false);
        }
    }

    public GameObject makeObj(string type)
    {
        switch (type)
        {
            case "enemyWallA":
                targetPool = enemyWallA;
                break;
            case "enemyWallB":
                targetPool = enemyWallB;
                break;
            case "enemyWallC":
                targetPool = enemyWallC;
                break;
            case "enemyWallD":
                targetPool = enemyWallC;
                break;
            case "enemyBoss":
                targetPool = enemyBoss;
                break;
            case "itemCoin":
                targetPool = itemCoin;
                break;
            case "bossBulletA":
                targetPool = bossBulletA;
                break;
            case "bossBulletB":
                targetPool = bossBulletB;
                break;
            case "bossBulletC":
                targetPool = bossBulletC;
                break;
            case "bossBulletD":
                targetPool = bossBulletD;
                break;
        }

        for(int i = 0; i < targetPool.Length; i++)
        {
            if (!targetPool[i].activeSelf)
            {
                targetPool[i].SetActive(true);
                return targetPool[i];
            }
        }
        return null;
    }

    public GameObject[] getPool(string type)
    {
        switch (type)
        {
            case "enemyWallA":
                targetPool = enemyWallA;
                break;
            case "enemyWallB":
                targetPool = enemyWallB;
                break;
            case "enemyWallC":
                targetPool = enemyWallC;
                break;
            case "enemyWallD":
                targetPool = enemyWallC;
                break;
            case "enemyBoss":
                targetPool = enemyBoss;
                break;
            case "itemCoin":
                targetPool = itemCoin;
                break;
            case "bossBulletA":
                targetPool = bossBulletA;
                break;
            case "bossBulletB":
                targetPool = bossBulletB;
                break;
            case "bossBulletC":
                targetPool = bossBulletC;
                break;
            case "bossBulletD":
                targetPool = bossBulletD;
                break;
        }

        return targetPool;
    }

}
