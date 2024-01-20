using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


    [System.Serializable]
    public class Sounds
    {
        public string songName;
        public AudioClip clip;
    }

public class soundManager : MonoBehaviour
{
    public int stageInt=3;

    private static soundManager instance;
    [Header("사운드 등록")]
    [SerializeField] Sounds[] bgmSounds;            //이게 cd 역할

    [Header("효과음 등록")]
    [SerializeField] Sounds[] effects;

    [Header("사운드 재생")]
    [SerializeField] AudioSource bgmPlay;           //플레이어
    
    [Header("효과음 재생")]
    [SerializeField] AudioSource effectPlay;


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

        
        bgmPlay.clip = bgmSounds[0].clip;
        bgmPlay.loop = true;
        bgmPlay.Play();
    }
    public static soundManager Instance {
        get {
            if (instance == null)
                return null;
            else return instance;
        }
    }

    private void Start()
    {
        SceneManager.sceneLoaded += soundLoad;
    }


    private void soundLoad(Scene arg0, LoadSceneMode arg1)
    {
        if(GameObject.Find("player")!=null)
        {
            GameObject player = GameObject.Find("player");
            Player playerlogic = player.GetComponent<Player>();
            playerlogic.soundManager = this;
        }
        if (arg0.buildIndex == 0)
        {
            stageInt = 3;
            bgmPlay.clip = bgmSounds[0].clip;       //clip이 음원 소스
        }
        else if (arg0.buildIndex == 1)
        {

            bgmPlay.clip = bgmSounds[1].clip;
        }
        else if (arg0.buildIndex == 2)
        {
            bgmPlay.clip = bgmSounds[10].clip;
        }
        else if (arg0.buildIndex == 3)
        {
            bgmPlay.clip = bgmSounds[10].clip;
        }

        else if (arg0.buildIndex == 4 || arg0.buildIndex == 5)
        {
            bgmPlay.clip = bgmSounds[2].clip;
        }
        else if (arg0.buildIndex == 6 || arg0.buildIndex == 7)
        {
            bgmPlay.clip = bgmSounds[3].clip;
        }
        else if(arg0.buildIndex==8 ||arg0.buildIndex == 9)
        {
            bgmPlay.clip = bgmSounds[4].clip;
        }
        else if(arg0.buildIndex == 10 || arg0.buildIndex == 11)
        {
            bgmPlay.clip = bgmSounds[5].clip;
        }
        else if(arg0.buildIndex == 12)
        {
            bgmPlay.clip = bgmSounds[6].clip;
        }
        else if(arg0.buildIndex == 13)
        {
            bgmPlay.clip = bgmSounds[7].clip;

        }
        else if(arg0.buildIndex == 14)
        {
            bgmPlay.clip = bgmSounds[8].clip;

        }
        else if(arg0.buildIndex == 15)
        {
            bgmPlay.clip = bgmSounds[9].clip;

        }
        else if(arg0.buildIndex == 16)
        {
            bgmPlay.clip = bgmSounds[11].clip;
        }



        bgmPlay.Play();         //스피커에서 플레이
    }

    public void effectPlayC(int sound)
    {
        GameObject seo = new GameObject();
        AudioSource audioSource = seo.AddComponent<AudioSource>();
        audioSource.clip = effects[sound].clip;
        audioSource.Play();

        Destroy(seo, effects[sound].clip.length);
    }
    public void playerEffect(int success)
    {
        if (success == 0) effectPlay.clip = effects[1].clip;    //패링
        else if (success == 1) effectPlay.clip = effects[2].clip;   //패링실패
        else if (success == 2) effectPlay.clip = effects[3].clip;   //코인
        else if (success == 3) effectPlay.clip = effects[4].clip;   //딸깍
        else if (success == 4) effectPlay.clip = effects[5].clip;   //게임오버
        else if (success == 5) effectPlay.clip = effects[6].clip;   //보스처치
        

        effectPlay.loop = false;
        effectPlay.Play();

    }

    public void nextStage() //누가씀..? enemy가쓴다 병신아
    {
        SceneManager.LoadScene(stageInt);
    }

 

    private void OnDisable()
    {   

        SceneManager.sceneLoaded -= soundLoad;
    }
}
