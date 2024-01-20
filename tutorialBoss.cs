using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class tutorialBoss : MonoBehaviour
{
    public GameObject bulletPrefab;
    GameObject[] bullets;
    SpriteRenderer sprite;
    public GameObject healthBar;
    public GameObject healthBarReal;
    Camera mainCam;
    Image healthImg;



    public dialogueManager dManager;
    public int health=2; 
    public float maxBossHealth;

    private void Awake()
    {
        mainCam = Camera.main;
        sprite = GetComponent<SpriteRenderer>();
        bullets = new GameObject[10];
        for(int i = 0; i < 10; i++)
        {
            bullets[i] = Instantiate(bulletPrefab);
            bullets[i].gameObject.SetActive(false);
        }
    }

    
    private GameObject makeObj()
    {
        for(int i = 0; i < 10; i++)
        {
            if(!bullets[i].activeSelf)
            {
                bullets[i].SetActive(true);
                return bullets[i];
            }
        }
        return null;
    }

    private void OnEnable()
    {
        healthImg= healthBar.GetComponent<Image>();
        maxBossHealth = health;
        healthBar.SetActive(true);

        Fire();
    }
    private void Update()
    {
            healthUIControl();

    }
    void healthUIControl()
    {
        healthBar.transform.position = mainCam.WorldToScreenPoint( transform.position + Vector3.down * 1);
        
        healthBarReal.GetComponent<Image>().fillAmount = health / maxBossHealth;
    }

    void Fire()
    {
        GameObject stone = makeObj();
        stone.transform.position = transform.position + Vector3.down * 1;

        Rigidbody2D stoneRigid = stone.GetComponent<Rigidbody2D>();

        stoneRigid.AddForce(Vector2.down * 10f, ForceMode2D.Impulse);

        Invoke("Fire", 2f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "playerBullet")
        {
            collision.gameObject.SetActive(false);
            sprite.color = new Color(1, 1, 1, 0.4f);
            Invoke("returnColor",1f);
            health--;
 
            if (health <= 0)
            {
                dManager.nextStep();

            }
        }
    }
    void returnColor()
    {
        sprite.color = new Color(1, 1, 1, 1f);
    }

    private void OnDisable()
    {
        healthBar.SetActive(false);
        CancelInvoke();
    }
}
