using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class dialogueManager : MonoBehaviour
{
    string[] dialogueMe= {"읏, 왜 항상 원거리야,,\n 이 칼로는 어떻게 상대 해야 하지..","엇! 할아버지?","그렇군요..! \n그런데 어떻게 움직이나요?" }; //1 3 5
    string[] dialogueGrandpa= {"손녀야,,,","칼로 공격들을 쳐내!","화면 왼쪽 오른쪽을 눌러서 움직여보거라","잘했다! 마침 저기 비겁한 원거리 몬스터가 나왔구나..","공격을 칼로 쳐내보거라!","타이밍 맞춰 가운데 버튼을 누르거라!","실패한다면 5초 뒤에 다시 쳐낼 수 있어!","좋아! 이제 저 비겁하게 원거리에서 \n공격하는 것들을 때려잡으러 가거라!" }; //2 4 6 7 8

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

    public Button btn_show;         //다이얼로그 버튼 
    public Button btn_left; 
    public Button btn_right;
    public Button btn_parrying;

    bool btn_leftOn;    //눌렀니
    bool btn_rightOn;   //눌렀니

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
                txt_dialogue.text = dialogueGrandpa[0];     //손녀야
                Me_image.gameObject.SetActive(false);
                Grandpa_image.gameObject.SetActive(true);
                hideButton();
                Invoke("showButton", 1f);

                step++;
                break;
            case 2:

                txt_dialogue.text = dialogueMe[1];          //엇 할아버지
                Me_image.gameObject.SetActive(true);
                Grandpa_image.gameObject.SetActive(false);
                hideButton();
                Invoke("showButton", 1f);
                step++;
                break;
            case 3:
                txt_dialogue.text = dialogueGrandpa[1];     //칼로쳐내!
                Me_image.gameObject.SetActive(false);
                Grandpa_image.gameObject.SetActive(true);
                hideButton();
                Invoke("showButton", 1f);
                step++;
                break;
            case 4:
                txt_dialogue.text = dialogueMe[2];          //네 그런데 다리가 안움직여요.
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
                dialogue.gameObject.SetActive(false);       //왼쪽 오른쪽 눌러서 움직여봐라
                simple.gameObject.SetActive(true);
                txt_simple.text = dialogueGrandpa[2];
                if (btn_leftOn && btn_rightOn)
                {
                    Debug.Log("눌림");
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
                txt_dialogue.text = dialogueGrandpa[3];     //잘했다 저기 비겁한거 나왔디
                Me_image.gameObject.SetActive(false);
                Grandpa_image.gameObject.SetActive(true);
                hideButton();
                Invoke("showButton", 1f);
                step++;
                break;
            case 7:
                txt_dialogue.text = dialogueGrandpa[4];     //공격을 칼로 쳐내보거라!
                Me_image.gameObject.SetActive(false);
                Grandpa_image.gameObject.SetActive(true);
                hideButton();
                Invoke("showButton", 1f);
                step++;
                break;
            case 8:
                boss.SetActive(true);
                dialogue.gameObject.SetActive(false);       //타이밍을 맞춰 가운데 버튼을 누르면 쳐낼수 있단다!
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
        btn_show.enabled=true; //button 띄우기
        
    }
    public void hideButton()
    {
        btn_show.enabled=false; //button 감추기
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
