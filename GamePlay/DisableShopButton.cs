using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisableShopButton : MonoBehaviour
{
    [SerializeField] private Button ShowAds;
    public void ShowButton()
    {
        ShowAds.gameObject.GetComponent<Image>().color= new Color(1,1,1,1f);
    }

    public void HideButton()
    {
        ShowAds.gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0.25f);
    }
}
