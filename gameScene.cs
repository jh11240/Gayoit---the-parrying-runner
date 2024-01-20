using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameScene : MonoBehaviour
{
    public Button btn_con;
    public Button btn_main;
    public Button btn_quit;
    public Button btn_pause;
    public Button btn_Retry;

    private void OnEnable()
    {
        btn_con.onClick.AddListener(()=>sceneManager.Instance.contn());
        btn_main.onClick.AddListener(()=>sceneManager.Instance.enterGame());
        btn_quit.onClick.AddListener(()=>sceneManager.Instance.Quit());
        btn_pause.onClick.AddListener(()=>sceneManager.Instance.pause());
        btn_Retry?.onClick.AddListener(()=>sceneManager.Instance.enterMenu());
    }
}
