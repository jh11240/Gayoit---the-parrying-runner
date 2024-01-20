using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parryingManager : MonoBehaviour
{
    public bool canParry;
    public GameObject player;
    public float parryCoolTime;

    void Start()
    {
        canParry = true;
    }

    public void parryFailPenalty(float cool)
    {
        canParry = false;
        Invoke("parryReturn", cool);
    }

    public void parryReturn()
    {
        canParry = true;
    }
}
