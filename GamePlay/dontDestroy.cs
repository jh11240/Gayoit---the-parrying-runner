using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dontDestroy: MonoBehaviour
{
    public AudioSource audio;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(audio);
    }

   
}
