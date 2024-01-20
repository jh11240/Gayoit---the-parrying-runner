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
        //start버튼에 엔터게임 넣어주기 
        btn_start.onClick.AddListener(() => { sceneManager.Instance.enterGame(); });
        //start버튼에 소리 넣어주기
        btn_start.onClick.AddListener(() => { soundManager.Instance.effectPlayC(3); });

        btn_shop.onClick.AddListener(() => { sceneManager.Instance.enterShop(); });
        //shop버튼에 소리 넣어주기
        btn_start.onClick.AddListener(() => { soundManager.Instance.effectPlayC(3); });

        btn_quit.onClick.AddListener(() => { sceneManager.Instance.Quit(); });
    }
}
