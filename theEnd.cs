using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class theEnd : MonoBehaviour
{
    public unityBannerAds ads;

    public GameObject first;
    public GameObject second;
    public GameObject third;

    public Button btn_r;    //Ȥ�ó� �������µ��߿� �������ٰ� ������ ��� �ٳ������� Ȱ��ȭ
    public Button btn_Q;


    soundManager sound;
    // Start is called before the first frame update
    void Start()
    {
        first.SetActive(true);
        Invoke("secondActive", 5f);
    }

    void secondActive()
    {
        first.SetActive(false);
        second.SetActive(true);
        Invoke("thirdActive", 2.5f);
    }
    void thirdActive()
    {
        second.SetActive(false);
        third.SetActive(third);
        btn_r.gameObject.SetActive(false);
        btn_Q.gameObject.SetActive(false);
        ads.Show();
        Invoke("buttonActive", 5f);
        
    }
    void buttonActive()
    {
        btn_r.gameObject.SetActive(true);
        btn_Q.gameObject.SetActive(true);
    }

    public void returnMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void exit()
    {
        Application.Quit();
    }

    public void clickSound()
    {
        sound.effectPlayC(3);
    }

}
