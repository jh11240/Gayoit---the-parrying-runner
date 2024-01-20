using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scrollManager : MonoBehaviour
{
    public int Page;
    public passedCoinManager pcManager;
    public GameObject[] shopPages;
    public void prevBtn()
    {
        shopPages[Page].gameObject.SetActive(false);
        Page--;
        shopPages[Page].gameObject.SetActive(true);
        pcManager.whatIsOpen = Page;

    }
    public void nextBtn()
    {
        shopPages[Page].gameObject.SetActive(false);
        Page++;
        shopPages[Page].gameObject.SetActive(true);
        pcManager.whatIsOpen = Page;
    }
 
   
   
}
