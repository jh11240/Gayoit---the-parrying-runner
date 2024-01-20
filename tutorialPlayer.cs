using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tutorialPlayer : MonoBehaviour
{
    public float moveSpeed;
    public bool isTouchLeft;
    public bool isTouchRight;
    public int playerHealth;

    public GameObject bullet;
    public GameObject enemy;
    public GameObject boss;
    public GameObject menuSet;

    public Effect effect;
    public Image parryCoolImg;
    playerStats stat;


    bool isTouchedLeft;
    bool isTouchedRight;
    public parryingManager pManager;
    public soundManager soundManager;

    BoxCollider2D boxCollider;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigid;
    Animator anim;
    Vector3 dirVec;

   
    int left_Value;
    int right_Value;

    void Awake()
    {
        soundManager = GameObject.Find("bgmManagers").GetComponent<soundManager>();
        //선언
        effect = GetComponent<Effect>();
        stat = GetComponent<playerStats>();
        rigid = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        //체력 속도 붙여주기 
        moveSpeed = stat.moveSpeed;
    }

    void Update()
    {
        move();
        Parrying();

        ParryUiControl(5f);
    }

    void ParryUiControl(float cool)
    {
        if (!pManager.canParry)
        {
            parryCoolImg.fillAmount -= 1 / cool * Time.deltaTime;

            if (parryCoolImg.fillAmount <= 0)
            {
                parryCoolImg.fillAmount = 1;
            }
        }
        else if (pManager.canParry)
        {
            
            parryCoolImg.fillAmount = 1;
        }
    }
    void move()
    {
        float h = Input.GetAxisRaw("Horizontal") + right_Value + left_Value;

        if ((isTouchedRight && h == 1) || (isTouchedLeft && h == -1))
        {
            h = 0;
        }

        Vector2 curpos = transform.position;
        Vector2 nextpos = new Vector2(h, 0) * moveSpeed * Time.deltaTime;

        transform.position = curpos + nextpos;


    }

    void Parrying()
    {
        dirVec = Vector3.up;
        if (!Input.GetButton("Jump") || !pManager.canParry)
        {
            return;
        }

        anim.SetBool("isParrying", true);

        RaycastHit2D boxCast = Physics2D.BoxCast(rigid.position, new Vector2(1.5f, 3.0f), rigid.rotation, dirVec, 0.7f, LayerMask.GetMask("Bullet"));

        if ((boxCast.collider != null) && (boxCast.collider.gameObject.tag == "enemyBullet") && pManager.canParry == true)
        {
            Rigidbody2D bRigid = boxCast.collider.gameObject.GetComponent<Rigidbody2D>();
            enemy parriedArrow = boxCast.collider.gameObject.GetComponent<enemy>();
            
            SpriteRenderer arrowSprite = parriedArrow.GetComponent<SpriteRenderer>();

            parriedArrow.tag = "playerBullet";
            effect.EffectPlay();
            arrowSprite.flipY = true;

            bRigid.velocity = Vector2.up * 10.0f;
        }
        else if (boxCast.collider == null)
        {
            pManager.parryFailPenalty(pManager.parryCoolTime);
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
        
        if (collision.gameObject.tag == "enemyBullet" )
        {
            Debug.Log("맞았당");
            healthDown();
        }
    }

    public void healthDown()
    {
        spriteInvisible();
        Invoke("spriteVisible",1.5f);

    }

    public void spriteInvisible()
    {
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
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
                    left_Value = -1;
                    break;
                case "C":
                    dirVec = Vector3.up;
                    if (!pManager.canParry)
                    {
                        return;
                    }

                    anim.SetBool("isParrying", true);

                    RaycastHit2D boxCast = Physics2D.BoxCast(rigid.position, new Vector2(1.5f, 3.0f), rigid.rotation, dirVec, 0.7f, LayerMask.GetMask("Bullet"));

                    if ((boxCast.collider != null) && (boxCast.collider.gameObject.tag == "enemyBullet")&& pManager.canParry == true)
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
                        //Rigidbody2D bRigid = boxCast.collider.gameObject.GetComponent<Rigidbody2D>();
                        //enemy parriedArrow = boxCast.collider.gameObject.GetComponent<enemy>();

                        //SpriteRenderer arrowSprite = parriedArrow.GetComponent<SpriteRenderer>();

                        //parriedArrow.tag = "playerBullet";
                        //effect.EffectPlay();
                        //arrowSprite.flipY = true;

                        //soundManager.playerEffect(0);
                        //bRigid.velocity = Vector2.up * 10.0f;
                    }
                    else if (boxCast.collider == null)
                    {
                        soundManager.playerEffect(1);
                        pManager.parryFailPenalty(pManager.parryCoolTime);
                    }
                    

                    Invoke("parryingEnd", 0.5f);

                    break;

                case "R":
                    right_Value = 1;
                    break;
                case "M":
                    
                        if (menuSet.activeSelf)
                        {
                            Time.timeScale = 1f;
                            menuSet.SetActive(false);
                        }
                        else
                        {
                             Time.timeScale = 0f;
                            menuSet.SetActive(true);
                        }
                    
                    break;
            }
        }
    }

    public void ButtonUp(string type)
    {
        switch (type)
        {
            case "L":
                left_Value = 0;
                break;
            case "C":
                break;
            case "R":
                right_Value = 0;
                break;
        }
    }


}

