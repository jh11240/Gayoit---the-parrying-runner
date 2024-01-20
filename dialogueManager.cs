using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class dialogueManager : MonoBehaviour
{
    string[] dialogueMe= {"��, �� �׻� ���Ÿ���,,\n �� Į�δ� ��� ��� �ؾ� ����..","��! �Ҿƹ���?","�׷�����..! \n�׷��� ��� �����̳���?" }; //1 3 5
    string[] dialogueGrandpa= {"�ճ��,,,","Į�� ���ݵ��� �ĳ�!","ȭ�� ���� �������� ������ ���������Ŷ�","���ߴ�! ��ħ ���� ����� ���Ÿ� ���Ͱ� ���Ա���..","������ Į�� �ĳ����Ŷ�!","Ÿ�̹� ���� ��� ��ư�� �����Ŷ�!","�����Ѵٸ� 5�� �ڿ� �ٽ� �ĳ� �� �־�!","����! ���� �� ����ϰ� ���Ÿ����� \n�����ϴ� �͵��� ���������� ���Ŷ�!" }; //2 4 6 7 8

    public Text txt_dialogue;
    public Text txt_simple;

    public GameObject dialogue;
    public GameObject simple;
    public GameObject boss;
    public GameObject player;
    public GameObject endTxt;
    public GameObject btn_left_G;
    public GameObject btn_right_G;
    public GameObject cManager;
    public coinManager coinLogic;

    public Image Me_image;
    public Image Grandpa_image;

    public Button btn_show;         //���̾�α� ��ư 
    public Button btn_left; 
    public Button btn_right;
    public Button btn_parrying;

    bool btn_leftOn;    //������
    bool btn_rightOn;   //������

    int step;
    private void Awake()
    {
        step = 0;
        btn_leftOn = false;
        btn_rightOn = false;
        txt_dialogue.text = dialogueMe[0];
        Me_image.gameObject.SetActive(true);
        step ++;
        cManager = GameObject.Find("coinManager");
        coinLogic = cManager.GetComponent<coinManager>();
    }

    public void test_right()
    {
        btn_rightOn = true;
    }  
    public void test_left()
    {
        btn_leftOn = true ;
    }

    public void nextStep()
    {
        switch (step)
        {
            case 1:
                txt_dialogue.text = dialogueGrandpa[0];     //�ճ��
                Me_image.gameObject.SetActive(false);
                Grandpa_image.gameObject.SetActive(true);
                hideButton();
                Invoke("showButton", 1f);

                step++;
                break;
            case 2:

                txt_dialogue.text = dialogueMe[1];          //�� �Ҿƹ���
                Me_image.gameObject.SetActive(true);
                Grandpa_image.gameObject.SetActive(false);
                hideButton();
                Invoke("showButton", 1f);
                step++;
                break;
            case 3:
                txt_dialogue.text = dialogueGrandpa[1];     //Į���ĳ�!
                Me_image.gameObject.SetActive(false);
                Grandpa_image.gameObject.SetActive(true);
                hideButton();
                Invoke("showButton", 1f);
                step++;
                break;
            case 4:
                txt_dialogue.text = dialogueMe[2];          //�� �׷��� �ٸ��� �ȿ�������.
                Me_image.gameObject.SetActive(true);
                Grandpa_image.gameObject.SetActive(false);
                hideButton();
                Invoke("showButton", 1f);
                step++;
                break;
            case 5:
                btn_left_G.gameObject.SetActive(true);
                btn_right_G.gameObject.SetActive(true);
                player.gameObject.SetActive(true);
                dialogue.gameObject.SetActive(false);       //���� ������ ������ ����������
                simple.gameObject.SetActive(true);
                txt_simple.text = dialogueGrandpa[2];
                if (btn_leftOn && btn_rightOn)
                {
                    Debug.Log("����");
                    step++;
                    Invoke("nextStep", 2f);
                    break;
                }
                else
                {
                    
                    Invoke("nextStep", 1f);
                    break;
                }
            case 6:
                simple.gameObject.SetActive(false);
                dialogue.gameObject.SetActive(true);
                txt_dialogue.text = dialogueGrandpa[3];     //���ߴ� ���� ����Ѱ� ���Ե�
                Me_image.gameObject.SetActive(false);
                Grandpa_image.gameObject.SetActive(true);
                hideButton();
                Invoke("showButton", 1f);
                step++;
                break;
            case 7:
                txt_dialogue.text = dialogueGrandpa[4];     //������ Į�� �ĳ����Ŷ�!
                Me_image.gameObject.SetActive(false);
                Grandpa_image.gameObject.SetActive(true);
                hideButton();
                Invoke("showButton", 1f);
                step++;
                break;
            case 8:
                boss.SetActive(true);
                dialogue.gameObject.SetActive(false);       //Ÿ�̹��� ���� ��� ��ư�� ������ �ĳ��� �ִܴ�!
                simple.gameObject.SetActive(true);

                txt_simple.text = dialogueGrandpa[5];
                Invoke("txtShow", 3f);
                Invoke("simpleHide", 5f);
                step++;
                break;
            case 9:
                boss.SetActive(false);
                dialogue.gameObject.SetActive(true);       
                simple.gameObject.SetActive(false);
                txt_dialogue.text = dialogueGrandpa[7];
                step++;
                break;
            case 10:
                dialogue.gameObject.SetActive(false);
                endTxt.gameObject.SetActive(true);
                step = 0;
                coinLogic.coinAmount += 5;
                Invoke("playStart", 2f);
                break;
                
        }
    }

    void txtShow()
    {
        txt_simple.text = dialogueGrandpa[6];
    }
    void simpleHide()
    {
        simple.gameObject.SetActive(false);
    }
    public void showButton()
    {
        btn_show.enabled=true; //button ����
        
    }
    public void hideButton()
    {
        btn_show.enabled=false; //button ���߱�
    }
    public void playStart()
    {
        if(coinManager.instance.IsEasy)
        sceneManager.Instance.EasyStages = SceneManager.GetActiveScene().buildIndex - 1;
        else
        sceneManager.Instance.HardStages = SceneManager.GetActiveScene().buildIndex - 1;
        sceneManager.Instance.enterGame();

    }
   

    
}
