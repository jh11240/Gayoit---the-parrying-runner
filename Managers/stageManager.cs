using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class stageManager : MonoBehaviour
{
    //�������� ��ư���� �θ� ������Ʈ
    public GameObject buttonsGroup;
    //�������� �θ� ������Ʈ
    public GameObject starGroup;

    [SerializeField] private Button Difficulty;
    [SerializeField] private TextMeshProUGUI DifficultyText;
    //�������� ��ư��
    Image[] buttons;

    //��ư���� �����ϵ带 ���� tmp�� ���������� ����
    Transform[] buttonTransforms;

    //��ư�� ���� �巡���ϱ� �����Ƽ� onclickListner�� �־��ַ��� button���۳�Ʈ
    [SerializeField]Button[] buttonButtons;

    //�������� ��ư �� ����
    Image[] stars;

    //�������� ��ư ������ TMP��
    List<TextMeshProUGUI> buttonTexts;

    //��ư ���Ʒ��� �����̴� ��Ʈ�ѷ�
    public RuntimeAnimatorController animController;

    //�� �̹���. 0�� �� �� 1�� �� ����
    public Sprite[] sprites;

    //��� ������������ ���� ����
    public int stagesEasy = 1;
    public int stagesHard = 1;
    //��� ������������ �� ä����
    public int stageStarEasy = 0;
    public int stageStarHard = 0;
    //���� ���̵� ����
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
        //playerprefs�� ����� stageint�� �ҷ����� ������ ����Ʈ�� 0�� 
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
        //playerprefs�� ����� stageint�� �ҷ����� ������ ����Ʈ�� 0�� 
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

        //���߿� �� ������ stagestar�����ϰ� �ϴ��� -1��
        //stageStarEasy = stagesEasy - 1;
        //stageStarHard = stagesHard - 1;
        buttonTexts = new List<TextMeshProUGUI>();

        //buttons�� ��� �������� ��ư �� �־���
        buttons = buttonsGroup.GetComponentsInChildren<Image>();

        //�� ��ư���� tmp���۳�Ʈ �������� ����.
        buttonTransforms = buttonsGroup.GetComponentsInChildren<Transform>();

        //�� ��ư���� button���۳�Ʈ ��������
        buttonButtons = buttonsGroup.GetComponentsInChildren<Button>();

        //stars�� ��� �� ������Ʈ �� �־���.
        stars = starGroup.GetComponentsInChildren<Image>();

        //buttons�� ��� tmp�� buttonTexts�� �־���.
        foreach(Transform elem in buttonTransforms)
        {
            if (elem.name == buttonsGroup.name || elem.parent.name!=buttonsGroup.name) continue;
            TextMeshProUGUI temp =elem.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
            buttonTexts.Add(temp);
        }

    }
    public void OnEnable()
    {
        //������ ������ ���� �������� �Ŵ����� ���ԵǹǷ� ����.
        //stageStarEasy = sceneManager.Instance.Stars > stageStarEasy ? sceneManager.Instance.Stars : stageStarEasy ;
        //stageStarHard = sceneManager.Instance.Stars > stageStarHard ? sceneManager.Instance.Stars : stageStarHard ;
        stagesEasy = sceneManager.Instance.EasyStages > stagesEasy ? sceneManager.Instance.EasyStages : stagesEasy;
        stagesHard = sceneManager.Instance.HardStages > stagesHard ? sceneManager.Instance.HardStages : stagesHard;
        DifficultyText.text = (isEasy) ? "Stages-Easy" : "Stages-Hard";
        //ó���� �� ����ȭ
        initialize();
        
    }

    //��ư�� ��������Ʈ�� ���İ� �ʱ�ȭ �Լ�
    public void initialize()
    {
        int stages = isEasy ? stagesEasy : stagesHard;
        //��ư �������� ���������� �����ϰ�
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

        //star �������� ���������� �����ϰ�
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
        //star�� �������� ���� ä��
        for(int i = 0; i < stages-1; i++)
        {
            if (i == 13) break;
            stars[i].sprite = sprites[2];
        }

        //tmp�۾� ����
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

        //buttons�鿡 ��ư ��Ŭ�������� �޾��ֱ�
        for(int i=0;i<buttonButtons.Length;i++)
        {
            int index = i;
            //������ ������������ ����� ������ ���̹Ƿ� 
            if(index<stages && index<=buttonButtons.Length-2)
            //tutorial�� 3�����̹Ƿ�
                buttonButtons[index].onClick.AddListener(delegate { loadGame(index+3); });
            //������ ������������ 
            else if (index<=stages && index == buttonButtons.Length - 1)
            {
                if(isEasy)
                {
                    warningMessege("�������ô� �ϵ��忡���� \n �����մϴ�!");
                    return;
                }
                buttonButtons[index].onClick.AddListener(delegate { loadGame(17); });
            }
            //stgaes���� �� ������
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

    //��ư�鿡 �������ִ� �Լ�
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



    //�Ͻ������� ����
    //private void OnApplicationPause(bool pause)
    //{
    //    PlayerPrefs.SetInt("StageEasyInt", stagesEasy);
    //    PlayerPrefs.SetInt("StageHardInt", stagesHard);
    //    PlayerPrefs.Save();
    //}

    ////����� ����
    //private void OnApplicationQuit()
    //{
    //    PlayerPrefs.SetInt("StageEasyInt", stagesEasy);
    //    PlayerPrefs.SetInt("StageHardInt", stagesHard);
    //    PlayerPrefs.Save();
    //}
}
