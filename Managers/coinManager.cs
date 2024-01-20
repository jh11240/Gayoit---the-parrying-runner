using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class coinManager : MonoBehaviour
{
    public static coinManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;

        }
        else
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }

       //chogihwa();     //√‚Ω√¿¸

        if (!PlayerPrefs.HasKey("Coin"))
        {
            PlayerPrefs.SetInt("Coin", 0);
            coinAmount = 0;
        }
        else
        {
            coinAmount = PlayerPrefs.GetInt("Coin");
        }
        IsEasy = true;
    }
    public bool IsEasy { get; set; }
    public int coinAmount;
    public TextMeshProUGUI txt_coin;

    public void chogihwa()
    {
        PlayerPrefs.SetInt("Coin", 0);
    }
    public void adRewards(string watch)
    {
        if(watch=="Finished")
        coinAmount += 10;
        else if(watch=="Skipped")
        coinAmount += 1;

    }
    
    private void Update()
    {
        if (txt_coin == null)
          return;
        txt_coin.text = coinAmount.ToString();
    }
    private void OnApplicationPause(bool pause)
    {
        PlayerPrefs.SetInt("Coin", coinAmount);
        PlayerPrefs.Save();
        
    }
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("Coin", coinAmount);
        PlayerPrefs.Save();
        
    }

}
