using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using TMPro;


public class gameDialogueManager : MonoBehaviour
{
    private static gameDialogueManager instance;

    public GameObject talkset;  //��ȭâ
    public Button btn_show;
    public Button btn_skip;
    public TextMeshProUGUI txt_dialogue; //��ȭ ��ũ��Ʈ
    public Image player;    //���ΰ� �̹���
    public Image enemy;     // �� �̹���

    string[] scripts= new string[]
        {
            "���� ����..? ���� ù ����ΰ�.. ���ϴ� ���� �̱��ٵ�..","(�����)","���Ÿ� �ƴϸ� ��븦 ���ϴ� ��,, ","�� �� ",
            "���� ���� ���̾� �����ϳ�,, ������.. ���?","�ڳ�.. �� ���ϸ� ��ġ�� ���ΰ�?","��, ��� �� ƨ�ܳ��鼭 ��ġ�����ϴ�.","���� �� ���ϴ� �� �������µ� �̰� ����ٴ�.. ���� �ؼ� �ȵǰڱ�..",
            "�� ���� ����� �� �־�? ���� ��� ó���������.. ��;�! �౸��ƴϾ�?"," (����!) ���� ��������. "," ���� �����? �� ��","  ������ ���� �ִٴ� ���� �� ������",
            "�� ������ ����,, ī�尡 �����,, �����,,"," ���ο� ǥ���ΰ� �����̴°� ���ߴ� �Ʒ��� ó���ε� "," ��! �� ���������ݾƿ�"," ���ϴ� �κ��ΰ� �ű��ϳ�. �̷��Ա��� �������شٰ�?",
            "���� �� �౸�� �������°ǰ�.. ���� ��� ������.. ", "���� �� �����̴�. ó�� ���� ���ε�"," �� �౸�忡�� ������ �̸��� ������������?","�������. ħ����, �� ���غ����",
            "�ƴ� �� �ٵ� ���ڸ��� �ο��� �Ŵ°���"," �� ���ϴ���. �̻��ϵ� ���Ҽ� ������?"," �ƴ� �̰� ƨ�ܳ��� �ֳ�?"," ����ض�",
            "���.. �̻����� ��� Į�� ƨ�ܳ����°���?","������ ���� �Ժη� �����ôٴ�..","��? ����? �� �˼��մϴ�.. "," �����ϼ���!",
            "�� ȭ�� ƨ�ܳ���� ����.. ���� �Ŀ˾� �� ���� ������!"," ũ����"," ���� �ο��?  �������� ���Ÿ� �ƴϸ� ��� �����ش�"," ũ�����",
            "ȣ���̰� ���鵵 ������.. ���� �ٴ�? �������� ������ �پ�ٴ���"," ������! ������ �غ��ض�!"," ������..�� ƨ�ܳ�����?"," �ѹ��� ����!!!",
            "�� Į�� ��ü ����? �Ҿƹ����� ���������� �ֽ� ���ε�.. �� ƨ�ܳ��ٴ�"," ���� �� ���� �ٽ� ���� ��Ÿ�� ���̾�.","�� �Ҿƹ���..?"," ���� �Ҹ���. ��Ҹ��������� �� ���̳� �����Ŷ�!!",
            "�Ҿƹ����� �ʹ� ��Ҿ�.. �ĵ��� ƨ�ܳ����̾�.. �׳����� ���� ����� ","ũ�ַַ�"," ��� �ò��� �ʹ� ¡�׷��� �����"," ũ�ַַַ�",
            "�� ���� ���� ������ ��ư��±���.. �׳����� �� ���� ����ϱ�","@#&~@%#��(>_<)","��.. ���� �� ö����� �ο��� �Ѵٰ�?" ,"<<>> (~_~)<<>>"
        };
    //��ȭ ����
    int talkStep = 0;

    //��Ų����
    GameObject skin;
    skinManager skinLogic;

    [Header("ĳ���͵��̹���")]
    public Sprite[] characterImage;

    [Header("���̹���(����������γ־��ּ���)")]
    public Sprite[] enemyImage;




    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        skin = GameObject.Find("skinManager");
        skinLogic = skin.GetComponent<skinManager>();
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += dialogueLoad;
        
    }

     


    private void dialogueLoad(Scene arg0, LoadSceneMode arg1)
    {

        if (arg0.buildIndex <= 3)
        {
            Time.timeScale = 1f;
            talkStep = 0;
            return;
        }
        else if(arg0.buildIndex == 16 || arg0.buildIndex==17)
        {
            Time.timeScale = 1f;
            return;
        }
        else {
            Time.timeScale = 0;
            GameObject canvas = GameObject.Find("Canvas");
            talkset = canvas.transform.Find("talkSet").gameObject;
            enemy = canvas.transform.Find("talkSet").Find("portrait_enemy").gameObject.GetComponent<Image>();
            player = canvas.transform.Find("talkSet").Find("portrait_player").gameObject.GetComponent<Image>();
            txt_dialogue = canvas.transform.Find("talkSet").Find("dialogue").Find("txt_dialogue").gameObject.GetComponent<TextMeshProUGUI>();
            btn_show = canvas.transform.Find("talkSet").Find("dialogue").gameObject.GetComponent<Button>();
            btn_skip = canvas.transform.Find("talkSet").Find("btn_skip").gameObject.GetComponent<Button>();

        }
        switch (arg0.buildIndex)
        {
            case 4:
                dialogue(0);
                break;
            case 5:
                dialogue(1);
                break;
            case 6:
                dialogue(2);
                break;
            case 7:
                dialogue(3);
                break;
            case 8:
                dialogue(4);
                break;
            case 9:
                dialogue(5);
                break;
            case 10:
                dialogue(6);
                break;
            case 11:
                dialogue(7);
                break;
            case 12:
                dialogue(8);
                break;
             case 13:
                dialogue(9);
                break;
             case 14:
                dialogue(10);
                break;
             case 15:
                dialogue(11);
                break;
            default:
                return;
            
            
        }
    }

    private void dialogue(int sceneNumber)
    {
        talkset.SetActive(true);    //�޴� ����
        nextStep(sceneNumber);
        btn_show.onClick.AddListener(() => {nextStep(sceneNumber);}); //���� �Լ��ȿ� �Ű����� ������ (delegate{nextstep("��¼��");}); or (()=>nextstep("��¼��"))
        btn_skip.onClick.AddListener(gameStart);
        enemy.sprite = enemyImage[sceneNumber]; //�� ��������Ʈ
        if (skinLogic.skinNumber == 100)
        {
            player.sprite = characterImage[0]; //�÷��̾������Ʈ

        }
        else
        player.sprite = characterImage[skinLogic.skinNumber-1]; //�÷��̾������Ʈ
        //Invoke("gameStart",2f);
    }
    public void nextStep(int sceneNumber)
    {

        if (talkStep<4*sceneNumber && sceneNumber>0) talkStep = 4 * sceneNumber; //��ŵ�Ǹ� ���� ��� ������ ����

        if (talkStep != 4 * (sceneNumber + 1))
        {
            txt_dialogue.text = scripts[talkStep];
        }
        else
        {
            Debug.Log("��糡  "+talkStep);
            gameStart();
        }


        if (talkStep % 2 == 0)  //2�� ���
        {
            enemy.gameObject.SetActive(false);
            player.gameObject.SetActive(true);
            hideButton();
            StartCoroutine(showButton());
        }
        else //Ȧ��
        {
            player.gameObject.SetActive(false);
            enemy.gameObject.SetActive(true);
            hideButton();
            StartCoroutine(showButton());

        }
        talkStep++;

    }
    void hideButton()
    {
        btn_show.enabled = false; //button ���߱�

    }
    IEnumerator showButton()
    {
       
       yield return new WaitForSecondsRealtime(0.5f);
         
        Debug.Log("��");
        btn_show.enabled = true; //button ���߱�

    }
  
   
    public void gameStart() //��ȭâ ���� ����
    {
        talkset.SetActive(false);

        Time.timeScale = 1;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -=dialogueLoad;
    }

}
