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
        sprite.color = new Color(1, 0, 0, 0.4f);    //개 좆같이 1,1,1이라서 색이 그냥 흰색이네
        StartCoroutine(wing());
    }

    IEnumerator wing()
    {
        yield return new WaitForSeconds(0.5f);
        sprite.color = new Color(1, 0, 0, 1);
    }
}
