using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backGround : MonoBehaviour
{
    //��ũ�Ѹ�
    public float speed;
    public int startIndex;
    public int endIndex;
    public Transform[] sprites;

    float viewHeight;

    private void Awake()
    {
        viewHeight = Camera.main.orthographicSize * 2;
    }

    private void Update()
    {
        move();
        scrolling();

    }

    void move()
    {
        Vector3 curPos = transform.position;
        Vector3 nextPos = Vector3.down*speed*Time.deltaTime;
        transform.position = curPos + nextPos;

    }
    void scrolling()
    {
        if (sprites[endIndex].position.y < viewHeight * (-1))
        {
            //��ũ�Ѹ�
            Vector3 backSpritePos = sprites[startIndex].localPosition;
            Vector3 frontSpritePos = sprites[endIndex].localPosition;
            sprites[endIndex].transform.localPosition = backSpritePos + Vector3.up * viewHeight;

            //cursor index changed
            int temp=startIndex; //endindex�� ���� �ö����� startindex�� �ְ�
            startIndex = endIndex;
            endIndex = (temp - 1 == -1) ? sprites.Length-1 : temp - 1;
            /*
             *2 1 0 -> (0 2 1 end=1 start=0 )-> (1 0 2 end = 2 start = 1)             
             */
        }
    }
}
