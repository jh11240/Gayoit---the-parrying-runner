using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//��� ������ �ҷ��� �� ������ �̱������� ������.
public class sceneManager : MonoBehaviour
{
    private static sceneManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
        //chogihwa();
    }


    public static sceneManager Instance
    { 
        get
        {
            if(null == instance)
            {
                return null;
            }
            return instance;
        }
            
    }

    public int EasyStages { get => easyStages;
        set
        {
            if (value > 14)
            {
                easyStages = 14;
            }
            else
            {
                easyStages = easyStages > value ? easyStages : value;

            }
        }
    }
    public int HardStages
    {
        get=>hardStages;
        set
        {
            if (value > 14)
            {
                hardStages = 14;
            }
            else
            {
                hardStages = hardStages > value ? hardStages : value;

            }
        }
    }

    public int BossKilled { get => bossKilled; set => bossKilled = value; }
    //�������� ������ ������
   // private int easyStars;
    //private int HardStars;
    private int easyStages;
    private int hardStages;
    private int bossKilled;

    //menuSet
    public GameObject menuSet;

    private void Start()
    {
        //�Ž��۽� �ʱ�ȭ
        BossKilled = 0;
        SceneManager.sceneLoaded += findMenuSet;
    }
    public void findMenuSet(Scene arg0,LoadSceneMode arg1)
    {
        if(arg0.buildIndex != 0&& arg0.buildIndex != 1&& arg0.buildIndex != 2&& arg0.buildIndex != 16)
        menuSet = GameObject.Find("Canvas").transform.Find("Menu Set").gameObject;
    }

    //pause��ư
    public void pause()
    {
        Time.timeScale = 0;
        menuSet.SetActive(true);
    }
    //coninue��ư
    public void contn()
    {
        menuSet.SetActive(false);
        Time.timeScale = 1;
    }

    //Main Menu���� �Լ�
    public void enterMenu()
    {
        SceneManager.LoadScene(0);

    }
    //Ʃ�丮�� ���� �Լ�
    public void enterTut()
    {
        SceneManager.LoadScene(3);
    }
    //���� ���� �Լ�
    public void enterGame()
    {
        SceneManager.LoadScene(2);
    }
    //���� ���� �Լ�
    public void enterShop()
    {
        SceneManager.LoadScene(1);
    }
    //quit��ư
    public void Quit()
    {
        Application.Quit();
    }
    public void loadGame(int scene)
    {
        SceneManager.LoadScene(scene);

    }
    private void chogihwa()
    {
        PlayerPrefs.SetInt("StageEasyInt", 0);
        PlayerPrefs.SetInt("StageHardInt", 0);
        PlayerPrefs.Save();
    }

    private void OnApplicationPause(bool pause)
    {
        EasyStages = EasyStages > PlayerPrefs.GetInt("StageEasyInt") ? EasyStages : PlayerPrefs.GetInt("StageEasyInt");
        if (EasyStages < 1) EasyStages = 1;
        PlayerPrefs.SetInt("StageEasyInt", EasyStages);
        HardStages = HardStages > PlayerPrefs.GetInt("StageHardInt") ? HardStages : PlayerPrefs.GetInt("StageHardInt");
        if (HardStages < 1) HardStages = 1;
        PlayerPrefs.SetInt("StageHardInt", HardStages);
        PlayerPrefs.Save();
    }

    //����� ����
    private void OnApplicationQuit()
    {
        EasyStages = EasyStages > PlayerPrefs.GetInt("StageEasyInt") ? EasyStages : PlayerPrefs.GetInt("StageEasyInt");
        if (EasyStages < 1) EasyStages = 1;
        PlayerPrefs.SetInt("StageEasyInt", EasyStages);
        HardStages = HardStages > PlayerPrefs.GetInt("StageHardInt") ? HardStages : PlayerPrefs.GetInt("StageHardInt");
        if (HardStages < 1) HardStages = 1;
        PlayerPrefs.SetInt("StageHardInt", HardStages);
        PlayerPrefs.Save();

    }
}
