using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class passedCoinManager : MonoBehaviour
{
    GameObject cManager;
    coinManager coinLogic;
    GameObject sManager;
    skinManager skinLogic;

    public int coinAmount;
    public int whatIsOpen;

    public GameObject[] buyButtons;
    public GameObject[] equipButtons;
    //0�� ������ �� 1�� ���� ��
    public Sprite[] equipSprites;
    public TextMeshProUGUI txt_curMoney;
    public TextMeshProUGUI txt_curMoney2;
    public TextMeshProUGUI txt_notEnoughMoney;
    public TextMeshProUGUI txt_notEnoughMoney2;

    private void Awake()
    {
        cManager = GameObject.Find("coinManager");
        sManager = GameObject.Find("skinManager");
        coinLogic = cManager.GetComponent<coinManager>();
        skinLogic = sManager.GetComponent<skinManager>();
    }

    private void OnEnable()
    {
        for(int i = 0; i < skinLogic.bought.Count; i++)
        {
            buyButtons[skinLogic.bought[i]].gameObject.SetActive(false);   //�� ��ũ��Ʈ ����� �� �̹� ���Ű� �� ��ư�� �� ���� �׷��� �׳� �ı��� ������ ������������Ƿ� �׳� setactive�����߰ڴ�
        }
        equipButtons[skinLogic.skinNumber - 1].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "���� ��";
        equipButtons[skinLogic.skinNumber- 1].GetComponent<Image>().sprite = equipSprites[1];
    }

    private void Update()
    {
        coinAmount = coinLogic.coinAmount;
        choosingWhichText();        //awake�� �ȳִ� ������ ��ԵǸ� ������Ʈ ������ϴµ� awake�� ������ ������ sex�� 
    }

    public void choosingWhichText()
    {
        if (whatIsOpen == 0)
        {
            txt_curMoney.text = coinAmount.ToString()+"����";
        }
        else if (whatIsOpen == 1)
            txt_curMoney2.text = coinAmount.ToString()+"����";
        
    }

    public void buy(int price)
    {
        
        if (canBuy(price))
        {
            switch (price)
            {
                case 1:
                    skinLogic.bought.Add(0);
                    skinLogic.howmany++;
                    buyButtons[0].SetActive(false);
                    break;
                case 10:
                    skinLogic.bought.Add(1);
                    skinLogic.howmany++;
                    buyButtons[1].SetActive(false);

                    break;
                case 50:
                    skinLogic.bought.Add(2);
                    skinLogic.howmany++;
                    buyButtons[2].SetActive(false);

                    break;
                case 80:
                    skinLogic.bought.Add(3);
                    skinLogic.howmany++;
                    buyButtons[3].SetActive(false);

                    break;
                case 100:
                    skinLogic.bought.Add(4);
                    skinLogic.howmany++;
                    buyButtons[4].SetActive(false);

                    break;
                case 200:
                    skinLogic.bought.Add(5);
                    skinLogic.howmany++;
                    buyButtons[5].SetActive(false);

                    break;
                case 300:
                    skinLogic.bought.Add(6);
                    skinLogic.howmany++;
                    buyButtons[6].SetActive(false);

                    break;
                case 1000:
                    skinLogic.bought.Add(7);
                    skinLogic.howmany++;
                    buyButtons[7].SetActive(false);

                    break;
            }
            coinLogic.coinAmount-= price;
        }

        else
        {
            if (whatIsOpen == 0)
                txt_notEnoughMoney.gameObject.SetActive(true);
            else if (whatIsOpen == 1)
                txt_notEnoughMoney2.gameObject.SetActive(true);
            Invoke("txt_hide", 0.4f);
        }
    }

    public void equip(int skinNumber)
    {
        Debug.Log("�� �� "+ skinNumber);
        skinLogic.skinNumber = skinNumber;
        foreach(var elem in equipButtons)
        {
            elem.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "����";
            elem.GetComponent<Image>().sprite = equipSprites[0];
        }
        equipButtons[skinNumber - 1].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "���� ��";
        equipButtons[skinNumber - 1].GetComponent<Image>().sprite = equipSprites[1];
    }

    public bool canBuy(int price)
    {
        if (coinAmount >= price)
            return true;
        else
            return false;

    }

    public void txt_hide()
    {
        txt_notEnoughMoney.gameObject.SetActive(false);
        txt_notEnoughMoney2.gameObject.SetActive(false);

    }

    public void Loadscene()
    {
        coinLogic.coinAmount = coinAmount; 
        SceneManager.LoadScene(0);
    }
}
