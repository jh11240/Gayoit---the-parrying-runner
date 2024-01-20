using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class btn_manager : MonoBehaviour
{
    public GameObject describe1;
    public GameObject describe2;
    public TextMeshProUGUI txt_describe1;
    public TextMeshProUGUI txt_describe2;

    public void clickBtn(int page)
    {
        if (page == 11 || page == 110 ||page == 150 ||page == 180)
        {
            describe1.SetActive(true);
            switch (page)
            {
                case 11:
                    txt_describe1.text = "이름은 가요잇  \n  \n" +
                        "비겁하게 뒤에 숨어서 뭐 날리는 애들을 제일 싫어한다.\n " +
                        "기본 스킨과 동일해 보이지만 착각이다.\n" +
                        "스피드 +1 \n" ;

                    break;
                case 110:
                    txt_describe1.text = "농부로 변장한 가요잇 \n \n" +
                        "농사가 취미에 맞는 지 계속 입고있다.\n \n" +
                        "스피드 +2 코인획득 +1 \n" ;
                    break;
                case 150:
                    txt_describe1.text = "답답해서 축구하러 나온 가요잇 \n \n" +
                        "집에서 티비보다 답답해서 뛰러나왔다.\n \n" +
                        "스피드 +3 코인획득+2 \n" ;
                    break;
                case 180:
                    txt_describe1.text = "폭주족이 되어버린 가요잇 \n \n" +
                        "적들을 더 빠르게 쫓아가기 위한 의상이다.\n \n" +
                        "골드획득+4 목숨+1 \n";
                    break;
            }
        }
        else
        {
            describe2.SetActive(true);
            switch (page)
            {
                case 21:
                    txt_describe2.text = "엘프가 되어버린 가요잇 \n \n " +
                        "원래 엘프로 태어났다고 주장하는데 귀가 불편해 보인다..\n \n" +
                        "목숨 +1 코인획득 +6 스피드+1 \n" ;

                    break;
                case 210:
                    txt_describe2.text = "해변가에 놀러간 가요잇 \n \n" +
                        "선탠을 하고 싶어한다. \n \n" +
                        "목숨+1 코인획득+8 패링쿨타임-1 \n" ;
                    break;
                case 250:
                    txt_describe2.text = "외계인 가요잇 \n \n" +
                        "gayoit 은하계에서 온 외계인이다 무섭다..\n \n" +
                        "목숨+2 코인획득+10 패링쿨타임-2 \n" ;
                    break;
                case 280:
                    txt_describe2.text = "만개한 꽃 가요잇 \n \n " +
                        "화사하게 꾸몄다. \n \n" +
                        "목숨 +2 코인획득+12 패링쿨타임-3 \n";
                    break;

            }

        }

    }
    public void close(int which)
    {
        if (which == 1)
            describe1.SetActive(false);
        else
            describe2.SetActive(false);
    }
}
