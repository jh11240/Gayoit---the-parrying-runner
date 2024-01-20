using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{

    public GameObject menuSet;
    public Image[] uiHealth;
    public GameObject player;
    public GameObject enemy;
    public GameObject gameOver;
    public GameObject stageBanner;
    
    public Image parryCoolImg;
    GameObject coinManager;
    Player playerLogic;
    coinManager coinLogic;
    public parryingManager pManager;

    public TextMeshProUGUI txt_coin;

    public float parryCoolTime;
    //���� ���� ��
    public string enemyname;
    public Sprite deadSprite;

    Rigidbody2D rigid;

    void Start()
    {
        parryCoolTime = pManager.parryCoolTime-player.GetComponent<playerStats>().coolTimeReduce;
        GameLoad();
        //if (SceneManager.GetActiveScene().buildIndex >= 3 && SceneManager.GetActiveScene().buildIndex <= 15)
        //{
        //    Debug.Log("���⵵ ã��");
        //    Invoke("gameStart", 1.0f);
        //    Debug.Log("�����");
        //}
    }

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();

        playerLogic = player.GetComponent<Player>();
        coinManager = GameObject.Find("coinManager");
        //pManager = GetComponent<parryingManager>();
        coinLogic = coinManager.GetComponent<coinManager>();
        coinLogic.txt_coin = txt_coin;

        if (SceneManager.GetActiveScene().buildIndex >= 3 && SceneManager.GetActiveScene().buildIndex <= 15)
        {
            stageBanner = GameObject.FindGameObjectWithTag("stageBanner");

            StartCoroutine(initialStarter());
        }
    }

    IEnumerator initialStarter()
    {
        yield return new WaitForSecondsRealtime(3.0f);
        stageBanner.SetActive(false);
    }

    //�и���Ÿ��UI & Bullet��������
    void Update()
    {
        ParryUIControl(parryCoolTime);

        markHealth();

        if (Input.GetButtonDown("Cancel"))
        {
            subMenuActive();
        } 
    }

    //Sub Menu Call
    public void subMenuActive()
    {
        if (menuSet.activeSelf)
        {
            Time.timeScale = 1f;
            menuSet.SetActive(false);
        }
        else
        {
            Time.timeScale = 0f;
            menuSet.SetActive(true);
        }
    }

    //�÷��̾� ü�� ĵ������ ǥ��
    public void markHealth()
    {
        for(int i=0; i< playerLogic.playerHealth; i++)
        {
            uiHealth[i].gameObject.SetActive(true);
        }
    }

    //�÷��̾� ������
    public void respawnPlayer()
    {
        Invoke("respawnPlayerDelay", 2f);

    }

    void respawnPlayerDelay()
    {
        player.transform.position = Vector3.down * 5.0f;
        player.SetActive(true);

        playerLogic.isGodMod = true;
        playerLogic.spriteInvisible();
        Invoke("unableGodMod", 2f);
    }

    void unableGodMod()
    {
        playerLogic.isGodMod = false;
        playerLogic.spriteVisible();
    }

    //��Ÿ�� ǥ�� ����
    public void ParryUIControl(float cool)
    {
        if (!pManager.canParry)
        {
            parryCoolImg.fillAmount -= 1 / cool * Time.deltaTime;

            if (parryCoolImg.fillAmount <= 0)
            {
                parryCoolImg.fillAmount = 1;
            }
        }else if (pManager.canParry)
        {
            parryCoolImg.fillAmount = 1;
        }
    }

    //���� �ý��� ���� ������
    public void GameOver()
    {
        Time.timeScale = 0;
        gameOver.SetActive(true);
        Text text = GameObject.Find("reason").GetComponent<Text>();
        text.text = enemyname.Split('(')[0]=="������"? "�������� �и��� \n �Ұ����մϴ٤�.��": enemyname.Split('(')[0];
        Image image = GameObject.Find("reasonImage").GetComponent<Image>();
        image.sprite = deadSprite;
    }

    public void Retry()
    {
        SceneManager.LoadScene(0);
    }

    public void GameSave()
    {
        enemy enemyData = enemy.GetComponent<enemy>();
        Player playerData = player.GetComponent<Player>();

        PlayerPrefs.SetFloat("PlayerX", player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", player.transform.position.y);
      //  PlayerPrefs.SetInt("EnemyHP", enemyData.health);
        PlayerPrefs.SetInt("PlayerHP", playerData.playerHealth);
        PlayerPrefs.Save();

        menuSet.SetActive(false);
    }

    public void GameLoad()
    {
        if (!PlayerPrefs.HasKey("PlayerX"))
        {
            return;
        }

        enemy enemyData = enemy.GetComponent<enemy>();
        Player playerData = player.GetComponent<Player>();

        float x = PlayerPrefs.GetFloat("PlayerX");
        float y = PlayerPrefs.GetFloat("PlayerY");
        int enemyHealth = PlayerPrefs.GetInt("EnemyHP");
        int playerHP = PlayerPrefs.GetInt("PlayerHP");

        player.transform.position = new Vector3(x, y, 0);
       // enemyData.health = enemyHealth;
        playerData.playerHealth = playerHP;

        for(int i = 2; i >= playerData.playerHealth; i--)
        {
            uiHealth[i].color = new Color(1, 1, 1, 0.2f);
        }
    }

    public void GameReset()
    {
        PlayerPrefs.DeleteAll();
    }

    public void GameExit()
    {
        Application.Quit();
    }
}
