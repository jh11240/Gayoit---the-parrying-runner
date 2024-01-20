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

    public GameObject talkset;  //대화창
    public Button btn_show;
    public Button btn_skip;
    public TextMeshProUGUI txt_dialogue; //대화 스크립트
    public Image player;    //주인공 이미지
    public Image enemy;     // 적 이미지

    string[] scripts= new string[]
        {
            "저건 고라니..? 저게 첫 상대인가.. 고라니는 쉽게 이길텐데..","(비웃음)","원거리 아니면 상대를 안하는 데,, ","컹 컹 ",
            "똥을 날릴 줄이야 지독하네,, 다음은.. 농부?","자네.. 저 고라니를 해치운 것인가?","예, 방금 똥 튕겨내면서 해치웠습니다.","으윽 저 고라니는 내 원수였는데 이걸 뺏어가다니.. 말로 해선 안되겠군..",
            "뭐 저런 사람이 다 있어? 고라니 대신 처리해줬더니.. 우와악! 축구장아니야?"," (삐익!) 당장 나가세요. "," 지금 당장요? 네 네","  아직도 여기 있다니 믿을 수 없군요",
            "뭔 심판이 저래,, 카드가 무기네,, 무기야,,"," 새로운 표적인가 움직이는걸 맞추는 훈련은 처음인데 "," 악! 공 맞을뻔했잖아요"," 말하는 로봇인가 신기하네. 이렇게까지 지원해준다고?",
            "드디어 저 축구장 빠져나온건가.. 여긴 어디 도시지.. ", "여긴 내 구역이다. 처음 보는 얼굴인데"," 그 축구장에서 나오니 이리로 나와지던데요?","상관없다. 침입자, 잘 피해보라고",
            "아니 왜 다들 보자마자 싸움을 거는거지"," 잘 피하더군. 미사일도 피할수 있을까?"," 아니 이거 튕겨낼수 있나?"," 기대해라",
            "어우.. 미사일이 어떻게 칼로 튕겨내지는거지?","위대한 숲에 함부로 들어오시다니..","엥? 엘프? 아 죄송합니다.. "," 각오하세요!",
            "휴 화살 튕겨내기는 쉽지.. 뭐야 냐옹아 넌 이제 자유야!"," 크허어엉"," 뭐야 싸우게?  저리가라 원거리 아니면 상대 안해준다"," 크허어어엉",
            "호랑이가 발톱도 날리네.. 여긴 바다? 이정도는 가뿐히 뛰어다니지"," 흐하하! 대포를 준비해라!"," 대포알..도 튕겨내려나?"," 한바탕 하자!!!",
            "이 칼은 대체 뭐지? 할아버지가 떠나기전에 주신 검인데.. 다 튕겨내다니"," 허허 그 검이 다시 세상에 나타날 줄이야.","엥 할아버지..?"," 무슨 소리냐. 헛소리하지말고 얼른 검이나 내놓거라!!",
            "할아버지랑 너무 닮았어.. 파도도 튕겨낼줄이야.. 그나저나 여긴 어디지 ","크롸롸롸"," 어우 시끄러 너무 징그럽게 생겼다"," 크롸롸롸롸",
            "이 세상엔 저런 생물도 살아가는구나.. 그나저나 이 곳은 어디일까","@#&~@%#æ(>_<)","뭐.. 뭐야 저 철덩어리도 싸워야 한다고?" ,"<<>> (~_~)<<>>"
        };
    //대화 순서
    int talkStep = 0;

    //스킨관련
    GameObject skin;
    skinManager skinLogic;

    [Header("캐릭터들이미지")]
    public Sprite[] characterImage;

    [Header("적이미지(보스순서대로넣어주세용)")]
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
        talkset.SetActive(true);    //메뉴 띄우고
        nextStep(sceneNumber);
        btn_show.onClick.AddListener(() => {nextStep(sceneNumber);}); //만약 함수안에 매개변수 있을시 (delegate{nextstep("어쩌구");}); or (()=>nextstep("어쩌구"))
        btn_skip.onClick.AddListener(gameStart);
        enemy.sprite = enemyImage[sceneNumber]; //적 스프라이트
        if (skinLogic.skinNumber == 100)
        {
            player.sprite = characterImage[0]; //플레이어스프라이트

        }
        else
        player.sprite = characterImage[skinLogic.skinNumber-1]; //플레이어스프라이트
        //Invoke("gameStart",2f);
    }
    public void nextStep(int sceneNumber)
    {

        if (talkStep<4*sceneNumber && sceneNumber>0) talkStep = 4 * sceneNumber; //스킵되면 그전 대사 읽을거 염려

        if (talkStep != 4 * (sceneNumber + 1))
        {
            txt_dialogue.text = scripts[talkStep];
        }
        else
        {
            Debug.Log("대사끝  "+talkStep);
            gameStart();
        }


        if (talkStep % 2 == 0)  //2의 배수
        {
            enemy.gameObject.SetActive(false);
            player.gameObject.SetActive(true);
            hideButton();
            StartCoroutine(showButton());
        }
        else //홀수
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
        btn_show.enabled = false; //button 감추기

    }
    IEnumerator showButton()
    {
       
       yield return new WaitForSecondsRealtime(0.5f);
         
        Debug.Log("끝");
        btn_show.enabled = true; //button 감추기

    }
  
   
    public void gameStart() //대화창 끄고 전투
    {
        talkset.SetActive(false);

        Time.timeScale = 1;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -=dialogueLoad;
    }

}
