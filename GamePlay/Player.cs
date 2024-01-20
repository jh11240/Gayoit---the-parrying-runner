using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    public bool isTouchLeft;
    public bool isTouchRight;
    public bool isGodMod;
    public int playerHealth;
    public GameManager manager;
    public GameObject bullet;
    public GameObject enemy;
    public GameObject boss;
    public GameObject menuSet;
    public GameObject parryingButton; // 패링버튼 숨기기
    playerStats stat;


    bool isTouchedLeft;
    bool isTouchedRight;
    public parryingManager pManager;
    public Effect effect;
    public spawnManager spawnManager;
    public soundManager soundManager;

    BoxCollider2D boxCollider;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigid;
    Vector3 dirVec;
    //코인매니저
    GameObject cManager;
    coinManager coinLogic;
    //애니메이터
    Animator anim;

    int left_Value;
    int right_Value;

    void Awake()
    {
        stat = GetComponent<playerStats>();
        effect = GetComponent<Effect>();
        rigid = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        cManager = GameObject.Find("coinManager");
        coinLogic = cManager.GetComponent<coinManager>();
        anim = GetComponent<Animator>();
        if(SceneManager.GetActiveScene().buildIndex !=17)
        isGodMod = coinManager.instance.IsEasy ? true : false;
    }
    private void Start()
    {
        Debug.Log(stat.health);
       // 체력 속도 붙여주기
        playerHealth = stat.health;
        Debug.Log(playerHealth);
        moveSpeed = stat.moveSpeed;

    }


    void Update()
    {
        move();
        checking();
        //Parrying();
    }

    void move()
    {
        //float h = Input.GetAxisRaw("Horizontal") + right_Value +left_Value;
        float h = right_Value +left_Value;

        if ((isTouchedRight && h ==1) || (isTouchedLeft && h == -1))
        {
            h = 0;
        }

        Vector2 curpos = transform.position;
        Vector2 nextpos = new Vector2(h, 0) * moveSpeed * Time.deltaTime;

        transform.position = curpos + nextpos;

        
    }
    void checking()
    {
        if (SceneManager.GetActiveScene().buildIndex == 17) return;
        
        parryingButton.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        if(spawnManager.bossSpawned)
        {
            parryingButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }

    }
    void Parrying()
    {
        dirVec = Vector3.up;
        if (!Input.GetButton("Jump") || !pManager.canParry || !spawnManager.bossSpawned)
        {
            return;
        }

        anim.SetBool("isParrying", true);

        RaycastHit2D boxCast = Physics2D.BoxCast(rigid.position, new Vector2(1.5f, 3.0f), rigid.rotation, dirVec, 0.7f, LayerMask.GetMask("Bullet"));

        if (boxCast.collider != null&& (boxCast.collider.tag == "enemyBullet") && pManager.canParry == true)
        {
            Rigidbody2D bRigid = boxCast.collider.gameObject.GetComponent<Rigidbody2D>();
            enemy parriedArrow = boxCast.collider.gameObject.GetComponent<enemy>();

            boxCast.collider.gameObject.tag = "playerBullet";
            effect.EffectPlay();
            Vector2 playerPos = transform.position;
            Vector2 bossPos = boss.transform.position;

            Vector2 dirVec = playerPos - bossPos;
            float rotAng = Vector2.Angle(dirVec, new Vector2(0, 1));
            if (dirVec.x < 0)
            {
                parriedArrow.transform.rotation = Quaternion.Euler(0, 0, rotAng);
            }
            else
            {
                parriedArrow.transform.rotation = Quaternion.Euler(0, 0, rotAng * (-1f));
            }
            soundManager.playerEffect(0);
            bRigid.velocity = new Vector2(bossPos.x - playerPos.x, bossPos.y - playerPos.y) * 1.0f;
        }
        else if (boxCast.collider == null)
        {
            soundManager.playerEffect(1);
            pManager.parryFailPenalty(pManager.parryCoolTime-stat.coolTimeReduce);
        }

        Invoke("parryingEnd", 0.5f);
    }

    public void parryingEnd()
    {
        anim.SetBool("isParrying", false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            if (collision.gameObject.name == "borderL")
            {
                isTouchedLeft = true;
            
            }
            if (collision.gameObject.name == "borderR")
            {
                isTouchedRight = true;
            }
        }

        if (collision.gameObject.tag == "coin")
        {
            soundManager.playerEffect(2);
           coinLogic.coinAmount += stat.goldRate;
            collision.gameObject.SetActive(false);
        } 
        else if ((collision.gameObject.tag == "enemyBullet" || collision.gameObject.tag == "enemy") && !isGodMod)
        {
            manager.deadSprite = collision.gameObject.GetComponent<SpriteRenderer>().sprite;
            manager.enemyname = collision.gameObject.name;
            healthDown();
            gameObject.SetActive(false);
            manager.respawnPlayer();
        }
    }

    public void healthDown()
    {
        if(playerHealth > 1)
        {
            playerHealth--;
            manager.uiHealth[playerHealth].gameObject.SetActive(false);
        }
        else if (playerHealth == 1)
        {
            soundManager.playerEffect(4);
            manager.GameOver();
        }
    }

    public void spriteInvisible()
    {
        spriteRenderer.color = new Color(1, 1, 1, 0.55f);
    }

    public void spriteVisible()
    {
        spriteRenderer.color = new Color(1, 1, 1, 1f);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            if (collision.gameObject.name == "borderL")
            {
                isTouchedLeft = false;

            }
            if (collision.gameObject.name == "borderR")
            {
                isTouchedRight = false;
            }
        }
    }

    public void ButtonDown(string type)
    {
        if (!menuSet.activeSelf)
        {
            switch (type)
            {
                case "L":
                    Debug.Log("L누름");
                    left_Value = -1;
                    break;
                case "C":

                    dirVec = Vector3.up;
                    if (!pManager.canParry || (SceneManager.GetActiveScene().buildIndex != 17 && !spawnManager.bossSpawned))
                    {
                        return;
                    }
                    anim.SetBool("isParrying", true);

                    RaycastHit2D boxCast = Physics2D.BoxCast(rigid.position, new Vector2(1.5f, 3.0f), rigid.rotation, dirVec, 0.7f, LayerMask.GetMask("Bullet"));

                    if (boxCast.collider!=null&&(boxCast.collider.gameObject.tag == "enemyBullet") && pManager.canParry == true) 
                    {
                        Rigidbody2D bRigid = boxCast.collider.gameObject.GetComponent<Rigidbody2D>();
                        enemy parriedArrow = boxCast.collider.gameObject.GetComponent<enemy>();

                        boxCast.collider.gameObject.tag = "playerBullet";

                        effect.EffectPlay();
                        Vector2 playerPos = transform.position;
                        Vector2 bossPos = boss.transform.position;

                        Vector2 dirVec = playerPos - bossPos;
                        float rotAng = Vector2.Angle(dirVec, new Vector2(0, 1));
                        if (dirVec.x < 0)
                        {
                            parriedArrow.transform.rotation = Quaternion.Euler(0, 0, rotAng);
                        }
                        else
                        {
                            parriedArrow.transform.rotation = Quaternion.Euler(0, 0, rotAng * (-1f));
                        }
                        soundManager.playerEffect(0);
                        bRigid.velocity = new Vector2(bossPos.x - playerPos.x, bossPos.y - playerPos.y) * 1.0f;
                    }
                    else if (boxCast.collider == null)
                    {
                        soundManager.playerEffect(1);
                        pManager.parryFailPenalty(pManager.parryCoolTime-stat.coolTimeReduce);
                    }

                    Invoke("parryingEnd", 0.5f);

                    break;
                case "R":
                    Debug.Log("R누름");

                    right_Value = 1;
                    break;
                case "M":
                    manager.subMenuActive();
                    break;
            }
        }
    }

    public void ButtonUp(string type)
    {
        switch (type)
        {
            case "L":
                Debug.Log("buttonUp_L");
                left_Value = 0;
                break;
            case "C":
                break;
            case "R":
                Debug.Log("buttonUp_R");
                right_Value = 0;
                break;
        }
    }


}

