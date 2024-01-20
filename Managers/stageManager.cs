using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class stageManager : MonoBehaviour
{
    //스테이지 버튼들의 부모 오브젝트
    public GameObject buttonsGroup;
    //별모양들의 부모 오브젝트
    public GameObject starGroup;

    [SerializeField] private Button Difficulty;
    [SerializeField] private TextMeshProUGUI DifficultyText;
    //스테이지 버튼들
    Image[] buttons;

    //버튼들의 겟차일드를 통해 tmp값 가져오려고 선언
    Transform[] buttonTransforms;

    //버튼에 직접 드래그하기 귀찮아서 onclickListner로 넣어주려고 button컴퍼넌트
    [SerializeField]Button[] buttonButtons;

    //스테이지 버튼 밑 별들
    Image[] stars;

    //스테이지 버튼 안쪽의 TMP들
    List<TextMeshProUGUI> buttonTexts;

    //버튼 위아래로 움직이는 컨트롤러
    public RuntimeAnimatorController animController;

    //별 이미지. 0이 빈 별 1이 꽉 찬별
    public Sprite[] sprites;

    //어디 스테이지까지 깼나 정보
    public int stagesEasy = 1;
    public int stagesHard = 1;
    //어디 스테이지까지 별 채웠니
    public int stageStarEasy = 0;
    public int stageStarHard = 0;
    //현재 난이도 상태
    public bool isEasy = true;
    public GameObject warningSign;
    private void Awake()
    {
        Difficulty.onClick.AddListener(SetDifficulty);
        isEasy = coinManager.instance.IsEasy;
        //for legacy version users
        if (PlayerPrefs.HasKey("StageInt"))
        {
            stageStarEasy = PlayerPrefs.GetInt("StageInt");
            PlayerPrefs.DeleteKey("StageInt");
        }
        //playerprefs에 저장된 stageint값 불러오기 없으면 디폴트로 0값 
        if (PlayerPrefs.HasKey("StageEasyInt"))
        {
            stagesEasy = PlayerPrefs.GetInt("StageEasyInt") > sceneManager.Instance.EasyStages ? PlayerPrefs.GetInt("StageEasyInt") : sceneManager.Instance.EasyStages;
            if (stagesEasy < 1)
            {
                stagesEasy = 1;
            }
        }
        else
        {
            stagesEasy = 1;
            PlayerPrefs.SetInt("StageEasyInt", 1);
        }
        //playerprefs에 저장된 stageint값 불러오기 없으면 디폴트로 0값 
        if (PlayerPrefs.HasKey("StageHardInt"))
        {
            stagesHard = PlayerPrefs.GetInt("StageHardInt") > sceneManager.Instance.HardStages ? PlayerPrefs.GetInt("StageHardInt") : sceneManager.Instance.HardStages;
            if (stagesHard < 1)
            {
                stagesHard = 1;
            }
        }
        else
        {
            stagesHard = 1;
            PlayerPrefs.SetInt("StageHardInt", 1);
        }

        //나중에 별 먹으면 stagestar구현하고 일단은 -1로
        //stageStarEasy = stagesEasy - 1;
        //stageStarHard = stagesHard - 1;
        buttonTexts = new List<TextMeshProUGUI>();

        //buttons에 모든 스테이지 버튼 다 넣어줌
        buttons = buttonsGroup.GetComponentsInChildren<Image>();

        //각 버튼들의 tmp컴퍼넌트 가져오기 위함.
        buttonTransforms = buttonsGroup.GetComponentsInChildren<Transform>();

        //각 버튼들의 button컴퍼넌트 가져오깅
        buttonButtons = buttonsGroup.GetComponentsInChildren<Button>();

        //stars에 모든 별 오브젝트 다 넣어줌.
        stars = starGroup.GetComponentsInChildren<Image>();

        //buttons의 모든 tmp값 buttonTexts에 넣어줌.
        foreach(Transform elem in buttonTransforms)
        {
            if (elem.name == buttonsGroup.name || elem.parent.name!=buttonsGroup.name) continue;
            TextMeshProUGUI temp =elem.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
            buttonTexts.Add(temp);
        }

    }
    public void OnEnable()
    {
        //어차피 게임을 깨면 스테이지 매니저로 오게되므로 노상관.
        //stageStarEasy = sceneManager.Instance.Stars > stageStarEasy ? sceneManager.Instance.Stars : stageStarEasy ;
        //stageStarHard = sceneManager.Instance.Stars > stageStarHard ? sceneManager.Instance.Stars : stageStarHard ;
        stagesEasy = sceneManager.Instance.EasyStages > stagesEasy ? sceneManager.Instance.EasyStages : stagesEasy;
        stagesHard = sceneManager.Instance.HardStages > stagesHard ? sceneManager.Instance.HardStages : stagesHard;
        DifficultyText.text = (isEasy) ? "Stages-Easy" : "Stages-Hard";
        //처음에 다 투명화
        initialize();
        
    }

    //버튼들 스프라이트나 알파값 초기화 함수
    public void initialize()
    {
        int stages = isEasy ? stagesEasy : stagesHard;
        //버튼 스테이지 열린곳까지 선명하게
        for (int i = 0; i < stages; i++)
        {
            Color tempColor = buttons[i].color;
            tempColor.a = 1f;
            buttons[i].color = tempColor;
        }
        for(int i = stages; i < buttons.Length; i++)
        {
            Color tempColor = buttons[i].color;
            tempColor.a = 0.4f;
            buttons[i].color = tempColor;
        }

        //star 스테이지 열린곳까지 선명하게
        for (int i = 0; i < stages; i++)
        {
            if (i == 13) break;
            Color tempColor = stars[i].color;
            tempColor.a = 1f;
            stars[i].color = tempColor;
            stars[i].sprite = sprites[1];
        }
        for (int i = stages; i < stars.Length; i++)
        {
            if (i == 13) break;
            Color tempColor = stars[i].color;
            tempColor.a = 0.4f;
            stars[i].color = tempColor;
            stars[i].sprite = sprites[0];
        }
        //star는 딴곳까지 별로 채움
        for(int i = 0; i < stages-1; i++)
        {
            if (i == 13) break;
            stars[i].sprite = sprites[2];
        }

        //tmp글씨 조정
        for (int i = 0; i < stages; i++)
        {
            Color tempColor = buttonTexts[i].color;
            tempColor.a = 1f;
            buttonTexts[i].color = tempColor;
        }
        for (int i = stages; i < buttonTexts.Count; i++)
        {
            Color tempColor = buttonTexts[i].color;
            tempColor.a = 0.4f;
            buttonTexts[i].color = tempColor;
        }

        //buttons들에 버튼 온클릭리스너 달아주기
        for(int i=0;i<buttonButtons.Length;i++)
        {
            int index = i;
            //마지막 스테이지에서 엔드씬 보여줄 것이므로 
            if(index<stages && index<=buttonButtons.Length-2)
            //tutorial이 3번씬이므로
                buttonButtons[index].onClick.AddListener(delegate { loadGame(index+3); });
            //마지막 스테이지에서 
            else if (index<=stages && index == buttonButtons.Length - 1)
            {
                if(isEasy)
                {
                    warningMessege("보스러시는 하드모드에서만 \n 가능합니다!");
                    return;
                }
                buttonButtons[index].onClick.AddListener(delegate { loadGame(17); });
            }
            //stgaes이후 값 누르면
            else
                buttonButtons[index].onClick.AddListener(delegate { warningMessege(); });
        }
        

        if (stages == 1 || stages == 3 || stages == 6 || stages == 9 || stages == 12 || stages >= 14)
        {
            if (stages > 14) stages = 14;
            buttonButtons[stages-1].gameObject.GetComponent<Animator>().runtimeAnimatorController=animController;
            buttonButtons[stages-1].gameObject.GetComponent<Animator>().SetInteger("LMR", 1);
            //stars[stages - 1].gameObject.GetComponent<Animator>().runtimeAnimatorController=animController;
            //stars[stages - 1].gameObject.GetComponent<Animator>().SetInteger("LMR", 1);
        }
        else if(stages == 2 || stages == 5 || stages == 8 || stages == 11)
        {
            buttonButtons[stages-1].gameObject.GetComponent<Animator>().runtimeAnimatorController=animController;
            buttonButtons[stages-1].gameObject.GetComponent<Animator>().SetInteger("LMR", 0);
            //stars[stages - 1].gameObject.GetComponent<Animator>().runtimeAnimatorController=animController;
            //stars[stages - 1].gameObject.GetComponent<Animator>().SetInteger("LMR", 0);
        }
        else
        {
            buttonButtons[stages-1].gameObject.GetComponent<Animator>().runtimeAnimatorController=animController;
            buttonButtons[stages-1].gameObject.GetComponent<Animator>().SetInteger("LMR", 2);
           // stars[stages - 1].gameObject.GetComponent<Animator>().runtimeAnimatorController=animController;
            //stars[stages - 1].gameObject.GetComponent<Animator>().SetInteger("LMR", 2);
        }
    }

    public void SetDifficulty()
    {
        isEasy = !isEasy;
        DifficultyText.text = (isEasy)? "Stages-Easy" : "Stages-Hard" ;
        coinManager.instance.IsEasy = isEasy;
        if (isEasy)
            buttonButtons[stagesHard - 1].gameObject.GetComponent<Animator>().runtimeAnimatorController = null;
        else
            buttonButtons[stagesEasy - 1].gameObject.GetComponent<Animator>().runtimeAnimatorController = null;
        initialize();
    }

    //버튼들에 연결해주는 함수
    public void loadGame(int scene)
    {
        PlayerPrefs.SetInt("StageEasyInt", stagesEasy);
        PlayerPrefs.SetInt("StageHardInt", stagesHard);
        PlayerPrefs.Save();
        sceneManager.Instance.loadGame(scene);
    }
    public void warningMessege()
    {
        warningSign.SetActive(true);
        Invoke("warningMessegeoff", 1f);
    }
    public void warningMessege(string txt)
    {
        warningSign.GetComponent<TextMeshProUGUI>().text = txt;
        warningSign.SetActive(true);
        Invoke("warningMessegeoff", 1f);
    }
    public void warningMessegeoff()
    {
        warningSign.SetActive(false);
    }



    //일시정지시 저장
    //private void OnApplicationPause(bool pause)
    //{
    //    PlayerPrefs.SetInt("StageEasyInt", stagesEasy);
    //    PlayerPrefs.SetInt("StageHardInt", stagesHard);
    //    PlayerPrefs.Save();
    //}

    ////종료시 저장
    //private void OnApplicationQuit()
    //{
    //    PlayerPrefs.SetInt("StageEasyInt", stagesEasy);
    //    PlayerPrefs.SetInt("StageHardInt", stagesHard);
    //    PlayerPrefs.Save();
    //}
}
