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
                    txt_describe1.text = "�̸��� ������  \n  \n" +
                        "����ϰ� �ڿ� ��� �� ������ �ֵ��� ���� �Ⱦ��Ѵ�.\n " +
                        "�⺻ ��Ų�� ������ �������� �����̴�.\n" +
                        "���ǵ� +1 \n" ;

                    break;
                case 110:
                    txt_describe1.text = "��η� ������ ������ \n \n" +
                        "��簡 ��̿� �´� �� ��� �԰��ִ�.\n \n" +
                        "���ǵ� +2 ����ȹ�� +1 \n" ;
                    break;
                case 150:
                    txt_describe1.text = "����ؼ� �౸�Ϸ� ���� ������ \n \n" +
                        "������ Ƽ�񺸴� ����ؼ� �ٷ����Դ�.\n \n" +
                        "���ǵ� +3 ����ȹ��+2 \n" ;
                    break;
                case 180:
                    txt_describe1.text = "�������� �Ǿ���� ������ \n \n" +
                        "������ �� ������ �Ѿư��� ���� �ǻ��̴�.\n \n" +
                        "���ȹ��+4 ���+1 \n";
                    break;
            }
        }
        else
        {
            describe2.SetActive(true);
            switch (page)
            {
                case 21:
                    txt_describe2.text = "������ �Ǿ���� ������ \n \n " +
                        "���� ������ �¾�ٰ� �����ϴµ� �Ͱ� ������ ���δ�..\n \n" +
                        "��� +1 ����ȹ�� +6 ���ǵ�+1 \n" ;

                    break;
                case 210:
                    txt_describe2.text = "�غ����� ��� ������ \n \n" +
                        "������ �ϰ� �;��Ѵ�. \n \n" +
                        "���+1 ����ȹ��+8 �и���Ÿ��-1 \n" ;
                    break;
                case 250:
                    txt_describe2.text = "�ܰ��� ������ \n \n" +
                        "gayoit ���ϰ迡�� �� �ܰ����̴� ������..\n \n" +
                        "���+2 ����ȹ��+10 �и���Ÿ��-2 \n" ;
                    break;
                case 280:
                    txt_describe2.text = "������ �� ������ \n \n " +
                        "ȭ���ϰ� �ٸ��. \n \n" +
                        "��� +2 ����ȹ��+12 �и���Ÿ��-3 \n";
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
