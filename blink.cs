using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blink : MonoBehaviour
{
    SpriteRenderer sprite;
    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeInHierarchy)
        {
            wingwing();
        }
    }

    void wingwing()
    {
        sprite.color = new Color(1, 0, 0, 0.4f);    //�� ������ 1,1,1�̶� ���� �׳� ����̳�
        StartCoroutine(wing());
    }

    IEnumerator wing()
    {
        yield return new WaitForSeconds(0.5f);
        sprite.color = new Color(1, 0, 0, 1);
    }
}
