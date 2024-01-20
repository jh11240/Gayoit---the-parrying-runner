using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class passedButtonEffect : MonoBehaviour
{
    GameObject buttonEffect;
    public soundManager source;
    private void Awake()
    {
        buttonEffect = GameObject.Find("bgmManagers");
        source = buttonEffect.GetComponent<soundManager>();

    }

    public void clickSound()
    {
        source.playerEffect(3);
    }
}
