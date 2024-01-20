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
    //0이 누르기 전 1이 누른 후
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
            buyButtons[skinLogic.bought[i]].gameObject.SetActive(false);   //이 스크립트 실행될 때 이미 구매가 된 퍼튼들 다 제거 그런데 그냥 파괴시 가비지 생길수도있으므로 그냥 setactive조져야겠다
        }
        equipButtons[skinLogic.skinNumber - 1].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "장착 중";
        equipButtons[skinLogic.skinNumber- 1].GetComponent<Image>().sprite = equipSprites[1];
    }

    private void Update()
    {
        coinAmount = coinLogic.coinAmount;
        choosingWhichText();        //awake로 안넣는 이유가 사게되면 업데이트 해줘야하는데 awake로 넣으면 어차피 sex됨 
    }

    public void choosingWhichText()
    {
        if (whatIsOpen == 0)
        {
            txt_curMoney.text = coinAmount.ToString()+"코인";
        }
        else if (whatIsOpen == 1)
            txt_curMoney2.text = coinAmount.ToString()+"코인";
        
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
        Debug.Log("얘 들어감 "+ skinNumber);
        skinLogic.skinNumber = skinNumber;
        foreach(var elem in equipButtons)
        {
            elem.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "장착";
            elem.GetComponent<Image>().sprite = equipSprites[0];
        }
        equipButtons[skinNumber - 1].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "장착 중";
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
