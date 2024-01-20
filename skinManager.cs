using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class skinManager : MonoBehaviour
{
    private static skinManager instance;

    private void Awake()
    {
        
        if (instance != null)
        {
            Destroy(gameObject);
            return;

        }
        else
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }

        //chogihwa();     //초기화
     
        bought = new List<int>();   //파괴가 안되니깐 맨처음 겜 실행할때 한번 new로 바꿔주겠지? 히히
        if (PlayerPrefs.HasKey("Bought0") && PlayerPrefs.HasKey("skinNumber"))
        {
            howmany = PlayerPrefs.GetInt("howmany");
            for(int i = 0; i < howmany; i++)
            {
                bought.Add(PlayerPrefs.GetInt("Bought" + i.ToString()));

            }
            skinNumber = PlayerPrefs.GetInt("skinNumber");
        }

    }

    public List<int> bought;    //구매 리스트
    public int howmany;     //저장 불러오기때 용이하게 몇개 샀냐
    public Sprite[] sprites;
    public RuntimeAnimatorController[] animator; //스킨 애니메이터
    public int skinNumber;


    GameObject player;
    Player playerLogic;
    playerStats statLogic;

    private void Start()
    {
        //chogihwa();
        SceneManager.sceneLoaded += sceneLoad_example;
    }

    void sceneLoad_example(Scene arg0,LoadSceneMode arg1)
    {
     
        if (GameObject.Find("player") != null)
        {
            player = GameObject.Find("player");
            playerLogic = player.GetComponent<Player>();
            statLogic = player.GetComponent<playerStats>();
            statLogic.skinNumber = skinNumber;
            if (skinNumber == 100)
            {

                return;
            }
            SpriteRenderer playerSprite = player.GetComponent<SpriteRenderer>();
            Animator playerAnimator = player.GetComponent<Animator>();

            playerSprite.sprite = sprites[skinNumber-1];
            playerAnimator.runtimeAnimatorController = animator[skinNumber - 1];
        }
        else
            return;
    }

    private void OnApplicationPause(bool pause)
    {
        PlayerPrefs.SetInt("howmany", howmany);
        PlayerPrefs.SetInt("skinNumber",skinNumber);
        for(int i = 0; i < howmany; i++)
        {
            PlayerPrefs.SetInt("Bought" + i.ToString(),bought[i]);
        }
        PlayerPrefs.Save();
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("howmany", howmany);
        PlayerPrefs.SetInt("skinNumber",skinNumber);
        for(int i = 0; i < howmany; i++)
        {
            PlayerPrefs.SetInt("Bought" + i.ToString(),bought[i]);
        }
        PlayerPrefs.Save();
        
    }
    public  void chogihwa()
    {
        PlayerPrefs.SetInt("howmany", 0);
        PlayerPrefs.SetInt("skinNumber", 100);
        for(int i=0;i<8;i++)
        PlayerPrefs.SetInt("Bought"+i.ToString() ,0 );

    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= sceneLoad_example;
    }
}
