using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class scoreManager : MonoBehaviour
{
    //int highScore;
    //[SerializeField] float score = 0;
    //[SerializeField] bool isPlaying = false;
    //public GameObject scoreBoard;
    //public GameObject highScoreBoard;

    //몇마리 보스 죽였는 지
    public TextMeshProUGUI scoreBossKilled;
    private int howManyBossKilled=0;

    public void OnEnable()
    {
        //메인 메뉴가 enable될때마다 bossKilled 갱신
        if (PlayerPrefs.HasKey("BossKilled"))
            howManyBossKilled = PlayerPrefs.GetInt("BossKilled");
        else
        {
            PlayerPrefs.SetInt("BossKilled", 0);
            howManyBossKilled = 0;
        }

        scoreBossKilled.text = "보스 러시 : "+howManyBossKilled.ToString() + "마리!";
        //chogihwa(); 

    }

    //나중에 스코어 표기 할때 사용
    //if (!PlayerPrefs.HasKey("HighScore"))
    //{
    //    PlayerPrefs.SetInt("HighScore", 0);
    //}
    //else
    //{
    //    highScore = PlayerPrefs.GetInt("HighScore");
    //}
    //highScoreBoard = GameObject.Find("Score");
    //TextMeshProUGUI highScoreText = highScoreBoard.GetComponent<TextMeshProUGUI>();
    //highScoreText.text = "High Score: " + highScore.ToString() + "점";
   
    
    public void chogihwa()
    {
        //PlayerPrefs.SetInt("HighScore", 0);
        PlayerPrefs.SetInt("BossKilled", 0);
    }

    //private void Start()
    //{ 
    //    SceneManager.sceneLoaded += sceneLoad_play;
    //}

    //private void sceneLoad_play(Scene arg0, LoadSceneMode arg1)
    //{
    //    //나중에 스코어 표기시 사용
    //    if (arg0.buildIndex == 0 || arg0.buildIndex == 1 || arg0.buildIndex == 2 || arg0.buildIndex == 3 || arg0.buildIndex == 16)
    //    {
    //        if (arg0.buildIndex == 0)
    //        {
    //            highScoreBoard = GameObject.Find("Score");
    //            TextMeshProUGUI highScoreText = highScoreBoard.GetComponent<TextMeshProUGUI>();
    //            highScoreText.text = "High Score: " + highScore.ToString() + "점";
    //        }
    //        Time.timeScale = 1f;
    //        isPlaying = false;
    //        score = 0;
    //        return;
    //    }
    //    else if (arg0.buildIndex == 17)
    //    {
    //        isPlaying = true;
    //        scoreBoard = GameObject.Find("Score");
    //    }


    //}

    //private void Update()
    //{
    //    나중에 장애물 피하기 모드 나왔을 때
    //    if (isPlaying)
    //    {
    //        score += Time.deltaTime;
    //        TextMeshProUGUI scoreText = scoreBoard.GetComponent<TextMeshProUGUI>();
    //        scoreText.text = ((int)score).ToString();
    //        if (score >= highScore)
    //            highScore = (int)score;
    //    }

    //}
    //private void OnDisable()
    //{
    //    SceneManager.sceneLoaded -= sceneLoad_play;
    //}
    private void OnApplicationPause(bool pause)
    {
        PlayerPrefs.SetInt("BossKilled",howManyBossKilled);
        PlayerPrefs.Save();
        
    }
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("BossKilled", howManyBossKilled);
        PlayerPrefs.Save();
        
    }
}
