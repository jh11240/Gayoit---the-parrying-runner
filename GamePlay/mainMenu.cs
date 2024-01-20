using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mainMenu : MonoBehaviour
{
    public Button btn_start;
    public Button btn_shop;
    public Button btn_quit;
    public void OnEnable()
    {
        //start��ư�� ���Ͱ��� �־��ֱ� 
        btn_start.onClick.AddListener(() => { sceneManager.Instance.enterGame(); });
        //start��ư�� �Ҹ� �־��ֱ�
        btn_start.onClick.AddListener(() => { soundManager.Instance.effectPlayC(3); });

        btn_shop.onClick.AddListener(() => { sceneManager.Instance.enterShop(); });
        //shop��ư�� �Ҹ� �־��ֱ�
        btn_start.onClick.AddListener(() => { soundManager.Instance.effectPlayC(3); });

        btn_quit.onClick.AddListener(() => { sceneManager.Instance.Quit(); });
    }
}
