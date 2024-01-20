using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    Camera mainCam;
    GameObject effectObj;
    public GameObject effectPrefab;
    public Transform effectGroup;
    ParticleSystem effect;
    private void Awake()
    {
        mainCam = Camera.main;
        effectObj = Instantiate(effectPrefab, effectGroup);
        effect = effectObj.GetComponent<ParticleSystem>();
    }

    public void EffectPlay()
    {
        mainCam.orthographicSize = 6;
        Time.timeScale = 0.4f;
        Invoke("returnCam", 0.3f);
        effect.transform.position = transform.position+Vector3.up;
        effect.transform.localScale= transform.localScale;
        effect.Play();
    }

    void returnCam()
    {
        Time.timeScale = 1f;
        mainCam.orthographicSize = 8;
    }
}
