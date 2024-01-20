using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerStats : MonoBehaviour
{

    public int skinNumber;
    public float moveSpeed; //이속
    public int health;  //체력
    public int goldRate;    //골드확률
    public int coolTimeReduce;  //패링 쿨타임 줄이기

    void init()
    {
        
        moveSpeed = 4;
        health = 1;
        goldRate = 1;
        coolTimeReduce = 0;
    }

    private void Start()
    {
        init();
        Debug.Log("playerstat"+skinNumber);
        switch (skinNumber)
        {
            case 100:
                init();
                break;
            case 1:
                init();
                moveSpeed += 1;
                break;
            case 2:
                init();
                moveSpeed += 2;
                goldRate += 1;
                break;
            case 3:
                init();
                moveSpeed += 3;
                goldRate += 2;
                break;
            case 4:
                init();
                moveSpeed += 3;
                goldRate += 4;
                health += 1;
                break;
            case 5:
                init();
                moveSpeed += 3;
                goldRate += 6;
                health += 1;
                break;
            case 6:
                init();
                moveSpeed += 3;
                goldRate += 8;
                health += 1;
                coolTimeReduce += 1;
                break;
            case 7:
                init();
                moveSpeed += 3;
                goldRate += 10;
                health += 2;
                coolTimeReduce += 2;

                break;
            case 8:
                moveSpeed += 3;
                goldRate += 12;
                health += 2;
                coolTimeReduce += 3;
                break;

        }
    }
}
