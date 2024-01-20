using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class enemy : MonoBehaviour
{
    Camera mainCam;
    public float speed;
    public float bossHealth;
    public float maxBossHealth;
    public int bossNum;
    public int nextMove;
    public int arrowSpeed;
    public int patternIndex;
    public int reloadingTime;
    public int curPatternCount;
    public int maxPatternCount;

    //�������ð���
    bool isCannonOn;
    float blinktime;
    GameObject prebeam;
    GameObject cannonshot;


    Rigidbody2D rigid;
    //Rigidbody2D playerRigid;
    public spawnManager spawnManager;
    //�ｺ�� ��ġ
    public Transform healthbarLoc;

    bool isTouchedLeft;
    bool isTouchedRight;

    [HideInInspector]public GameObject player;
    [HideInInspector]public objectManager objManager;

    GameObject soundObject;
    public soundManager sManager;
    public GameObject boss;
    public GameObject healthBar;
    public GameObject healthBarReal;
    public GameObject stageClear;
    SpriteRenderer spriteRenderer;
    Animator anim;

    void Awake()
    {
        soundObject= GameObject.Find("bgmManagers");
        sManager = soundObject.GetComponent<soundManager>();
        mainCam = Camera.main;
        reloadingTime = 2;
        nextMove = 0;
    }

    void OnEnable()
    {
        if (gameObject.tag == "boss")
        {
            Debug.Log("���� ����_ " + bossNum);
            if (gameObject.GetComponent<Animator>() != null)
            {
                anim = gameObject.GetComponent<Animator>();
            }
            maxBossHealth = bossHealth;
            spriteRenderer = GetComponent<SpriteRenderer>();
            rigid = GetComponent<Rigidbody2D>();

            //playerRigid = player.GetComponent<Rigidbody2D>();

            Invoke("bossAction", 1.2f);

        }
        else if (gameObject.tag == "playerBullet")
        {
            gameObject.tag = "enemyBullet";
        }
        else
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }

    void OnDisable()
    {
        gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        spriteRenderer.flipX = false;
        spriteRenderer.flipY = false;
        CancelInvoke("bossAction");
    }

    void Update()
    {
        if (gameObject.tag == "boss")
        {
            healthBar.SetActive(true);
            healthUIControl();
        }
        if (prebeam || cannonshot != null)
        {
            if (prebeam.activeInHierarchy)
            {
                prebeam.transform.position = transform.position + Vector3.down*8.2f;
            }
            else if (cannonshot.activeInHierarchy)
            {
                cannonshot.transform.position = transform.position + Vector3.down*8;
            }
        }
    }

    void onHit()
    {
        if (bossHealth > 1)
        {
            bossHealth--;

            spriteRenderer.color = new Color(1, 1, 1, 0.4f);

            Invoke("returnSprite", 0.2f);

        }
        else if (bossHealth <= 1) //=onDie
        {
            //���� ���
            if (SceneManager.GetActiveScene().buildIndex == 17)
            {
                //���� �� ���
                bossHealth--;
          
                //���� �� �÷��ְ�
                sceneManager.Instance.BossKilled++;
                //
                if (sceneManager.Instance.BossKilled > PlayerPrefs.GetInt("bossKilled"))
                {
                    PlayerPrefs.SetInt("bossKilled", sceneManager.Instance.BossKilled);
                }
                //Ŭ����� �ҷ� ����
                GameObject[] bullets;
                bullets = GameObject.FindGameObjectsWithTag("enemyBullet");
                int bulletsNumber = bullets.Length;

                for (int i = 0; i < bulletsNumber; i++)
                {
                    bullets[i].SetActive(false);
                }
                StartCoroutine(fadeOut());
            }
            else
            {
                bossHealth--;
                spawnManager.bossSpawned = false;

                //�������� �߰� Stages�� set�Լ��� ���������� ����
                if(coinManager.instance.IsEasy)
                sceneManager.Instance.EasyStages = SceneManager.GetActiveScene().buildIndex - 1;
                else
                sceneManager.Instance.HardStages = SceneManager.GetActiveScene().buildIndex - 1;
                Invoke("playerNextStageRun", 1.0f);

                Invoke("stageClearControl", 3f);

                StartCoroutine(fadeOut());
                Invoke("nextStageDelay", 4.0f);

                //Ŭ����� �ҷ� ����
                GameObject[] bullets;
                bullets = GameObject.FindGameObjectsWithTag("enemyBullet");
                int bulletsNumber = bullets.Length;

                for (int i = 0; i < bulletsNumber; i++)
                {
                    bullets[i].SetActive(false);
                }

               
            }
        }
    }

    void returnSprite()
    {
        spriteRenderer.color = new Color(1, 1, 1, 1.0f);
    }

    void playerNextStageRun()
    {
        Rigidbody2D playerRigid = player.GetComponent<Rigidbody2D>();
        playerRigid.velocity = new Vector2(0, 5);
    }

    IEnumerator fadeOut()
    {
        for (int i = 10; i>=0; i--)
        {
            float f = i / 10.0f;
            spriteRenderer.color = new Color(1, 1, 1, f);
            yield return new WaitForSeconds(0.25f);
        }
        //���� ����� ��
        if(SceneManager.GetActiveScene().buildIndex == 17)
        {
            gameObject.SetActive(false);
            spriteRenderer.color = new Color(1, 1, 1, 1f);
            BossRush boss = GameObject.Find("bossRushManager")?.GetComponent<BossRush>();
            if (boss == null)
                Debug.Log("boss ����");
            bossHealth = maxBossHealth;
            boss.respawnBoss();
        }
    }

    //���� �������� �Ѿ�� �Լ�
    void nextStageDelay()
    {
        int buildIndex = SceneManager.GetActiveScene().buildIndex;
        //���Ѹ��ƴϸ� �������� ����ȭ������ 
        if (buildIndex != 17&& buildIndex!=15)
            sceneManager.Instance.enterGame();
        //������ ���������� ���� ũ����
        else if(buildIndex==15)
        {
            sceneManager.Instance.loadGame(16);
        }
        //���� ����
        else
        {
            Debug.Log("enemy.cs-its infinity mode");
        }
    }

    //
    void stageClearControl()
    {
        stageClear.SetActive(true);
        Invoke("stageClearOff", 3f);
    }

    void stageClearOff()
    {
        stageClear.SetActive(false);
    }

    void healthUIControl()
    {
        healthBar.transform.position = (healthbarLoc.position);
        if (bossHealth < 1)
            healthBar.SetActive(false);
        healthBarReal.GetComponent<Image>().fillAmount = bossHealth/ maxBossHealth;
    }

    void bossAction()
    {
        if (!gameObject.activeSelf)
        {
            return;
        }
        
        bossMove();
        //���Ѹ�� �ƴҶ�

         Invoke("choosePattern", 2f);
    }

    void bossMove()
    {
        if (bossHealth <= 0)
        {
            rigid.velocity = new Vector2(0, 0);
        } 
        else
        {
            rigid.velocity = new Vector2(nextMove, 0);

            nextMove = Random.Range(-1, 2);
        }

        if (bossHealth > 0)
        {
            Invoke("bossMove", 2);
        }
    }

    //��輱 �ʸӷ� �̵� ���� �� SetActive.false
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.tag == "boss")
        {
            if (collision.gameObject.tag == "playerBullet")
            {
                onHit();

                collision.gameObject.SetActive(false);
            }
        }

        if (gameObject.tag == "enemyBullet" || gameObject.tag == "playerBullet")
        {
            if (collision.gameObject.tag == "disableBorder")
            {
                gameObject.SetActive(false);
            } else if (collision.gameObject.tag == "Player") {
                gameObject.SetActive(false);
            }
        } else
        {
            if (collision.gameObject.name == "borderL"&&gameObject.tag=="boss")
            {
                rigid.velocity = new Vector2(1, 0);
            }
            else if (collision.gameObject.name == "borderR"&&gameObject.tag == "boss")
            {
                rigid.velocity = new Vector2(-1, 0);
            }
            else if (collision.gameObject.tag == "disableBorder")
            {
                gameObject.SetActive(false);
            }         
        }
    }



    void choosePattern()
    {

        curPatternCount = 0;

        if (bossHealth>0)
        {
            switch (bossNum)
            {
                case 0: //����
                    patternIndex = 0;

                    switch (patternIndex)
                    {
                        case 0:
                            fireForward("bossBulletA");
                            break;
                    }
                    break;
                case 1: //���
                    patternIndex = Random.Range(0, 3);

                    switch (patternIndex)
                    {
                        case 0:
                            fireForward("bossBulletA");
                            break;
                        case 1:
                            fireLeft("bossBulletA");
                            break;
                        case 2:
                            guidedFire("bossBulletA");
                            break;
                    }
                    break;
                case 2: //����
                    patternIndex = Random.Range(0, 4);

                    switch (patternIndex)
                    {
                        case 0:
                            anim.SetTrigger("attack2");
                            arcFire("bossBulletB");
                            break;
                        case 1:
                            anim.SetTrigger("attack1");

                            guidedFire("bossBulletA");
                            break;
                        case 2:
                            anim.SetTrigger("attack2");

                            guidedFire("bossBulletB");
                            break;
                        case 3:
                            anim.SetTrigger("attack1");

                            tripleFire("bossBulletA");
                            break;
                    }
                    break;  
                case 3: //�÷��̾�
                    patternIndex = Random.Range(0, 4);

                    switch (patternIndex)
                    {
                        case 0:
                            anim.SetTrigger("shoot");

                            guidedFire("bossBulletA");
                            break;
                        case 1:
                            anim.SetTrigger("shoot");

                            arcFire("bossBulletA");
                            break;
                        case 2:
                            anim.SetTrigger("shoot");

                            tripleFire("bossBulletA");
                            break;
                        case 3:
                            anim.SetTrigger("shoot");

                            fireForward("bossBulletA");
                            break;
                    }
                    break;
                case 4: //�������
                    patternIndex = Random.Range(0, 5);

                    switch (patternIndex)
                    {
                        case 0:
                            fireForward("bossBulletA");
                            break;
                        case 1:
                            guidedFire("bossBulletA");
                            break;
                        case 2:
                            scatterFire("bossBulletA");
                            break;
                        case 3:
                            arcFire("bossBulletA");
                            break;
                        case 4:
                            tripleFire("bossBulletA");
                            break;
                    }
                    break;
                case 5: //���
                    patternIndex = Random.Range(0, 3);

                    switch (patternIndex)
                    {
                        case 0:
                            guidedFire("bossBulletA");
                            break;
                        case 1:
                            tripleFire("bossBulletA");
                            break;
                        case 2:
                            machineGun("bossBulletA");
                            break;
                    }
                    break;
                case 6: //����
                    patternIndex = Random.Range(0, 5);

                    switch (patternIndex)
                    {
                        case 0:
                            guidedFire("bossBulletA");
                            break;
                        case 1:
                            tripleFire("bossBulletA");
                            break;
                        case 2:
                            moreScatterFire("bossBulletA");
                            break;
                        case 3:
                            arcFire("bossBulletA");
                            break;
                        case 4:
                            scatterFire("bossBulletA");
                            break;
                    }
                    break;
                case 7: //ȣ����
                    patternIndex = Random.Range(0, 6);

                    switch (patternIndex)
                    {
                        case 0:
                            guidedFire("bossBulletA");
                            break;
                        case 1:
                            tripleFire("bossBulletA");
                            break;
                        case 2:
                            scatterFire("bossBulletB");
                            break;
                        case 3:
                            scatterFire("bossBulletA");
                            break;
                        case 4:
                            arcFire("bossBulletA");
                            break;
                        case 5:
                            arcFire("bossBulletA");
                            break;
                    }
                    break;
                case 8://����
                    patternIndex = Random.Range(0, 8);

                    switch (patternIndex)
                    {
                        case 0:
                            guidedFire("bossBulletA");
                            break;
                        case 1:
                            arcFire("bossBulletA");
                            break;
                        case 2:
                            scatterFire("bossBulletA");
                            break;
                        case 3:
                            tripleFire("bossBulletA");
                            break;
                        case 4:
                            machineGun("bossBulletA");
                            break;
                        case 5:
                            machineGun("bossBulletA");
                            break;
                        case 6:
                            scatterFire("bossBulletA");
                            break;
                        case 7:
                            machineGun("bossBulletA");
                            break;
                    }
                    break;
                case 9://���
                    patternIndex = Random.Range(0, 6);

                    switch (patternIndex)
                    {
                        case 0:
                            arcFire("bossBulletC");
                            break;
                        case 1:
                            arcFire("bossBulletA");
                            break;
                        case 2:
                            guidedFire("bossBulletB");
                            break;
                        case 3:
                            scatterFire("bossBulletC");
                            break;
                        case 4:
                            fireForward("bossBulletB");
                            break; 
                        case 5:
                            machineGun("bossBulletC");
                            break;
                    }
                    break;
                case 10://����
                    patternIndex = Random.Range(0, 7);

                    switch (patternIndex)
                    {
                        case 0:
                            arcFire("bossBulletA");
                            break;
                        case 1:
                            scatterFire("bossBulletB");
                            break;
                        case 2:
                            scatterFire("bossBulletA");
                            break;
                        case 3:
                            moreScatterFire("bossBulletA");
                            break;
                        case 4:
                            moreScatterFire("bossBulletB");
                            break;
                        case 5:
                            machineGun("bossBulletA");
                            break;
                        case 6:
                            machineGun("bossBulletB");
                            break;
                    }
                    break;
                case 11://������
                    patternIndex = Random.Range(0, 10);

                    switch (patternIndex)
                    {
                        case 0:
                            guidedFire("bossBulletA");
                            break;
                        case 1:
                            machineGun("bossBulletA");

                            break;
                        case 2:
                            machineGun("bossBulletB");
                            break;
                        case 3:
                            arcFire("bossBulletA");
                        break;
                        case 4:
                            arcFire("bossBulletA");
                            break;
                        case 5:
                            moreScatterFire("bossBulletA");
                            break;
                        case 6:
                            moreScatterFire("bossBulletA");
                            break;
                        case 7:
                            cannon();
                            break;
                        case 8:
                            cannon();
                            break;
                        case 9:
                            cannon();
                            break;

                    }
                    break;
            }
        }
    }

    void fireForward(string bulletType)
    {
        GameObject bossArrow = objManager.makeObj(bulletType);
        bossArrow.transform.position = transform.position + Vector3.forward * 0.1f;

        Rigidbody2D rigidArrow = bossArrow.GetComponent<Rigidbody2D>();

        rigidArrow.AddForce(Vector2.down * 10, ForceMode2D.Impulse);

        if (bossHealth > 0)
        {
            Invoke("choosePattern", reloadingTime);
        }

    }

    void fireLeft(string bulletType)
    {
        
        GameObject bossArrow = objManager.makeObj(bulletType);
        bossArrow.transform.position = transform.position + Vector3.forward * 0.1f;
        bossArrow.transform.rotation = Quaternion.Euler(0,0,345f);

        Rigidbody2D rigidArrow = bossArrow.GetComponent<Rigidbody2D>();

        rigidArrow.AddForce(new Vector2(-1,-4).normalized * 10, ForceMode2D.Impulse);

        if (bossHealth > 0)
        {
            Invoke("choosePattern", reloadingTime);
        }

    }

    void fireRight(string bulletType)
    {
        
        GameObject bossArrow = objManager.makeObj(bulletType);
        bossArrow.transform.position = transform.position + Vector3.forward * 0.1f;
        bossArrow.transform.rotation = Quaternion.Euler(0,0,15f);

        Rigidbody2D rigidArrow = bossArrow.GetComponent<Rigidbody2D>();

        rigidArrow.AddForce(new Vector2(1, -4).normalized * 10, ForceMode2D.Impulse);

        if (bossHealth > 0)
        {
            Invoke("choosePattern", reloadingTime);
        }

    }

    void tripleFire(string bulletType)
    {
        
        for (int i = -1; i < 2; i++)
        {
            GameObject bossArrow = objManager.makeObj(bulletType);
            bossArrow.transform.position = transform.position + Vector3.forward * 0.1f;
            bossArrow.transform.rotation = Quaternion.Euler(0, 0, 15f * i);

            Rigidbody2D rigidArrow = bossArrow.GetComponent<Rigidbody2D>();

            rigidArrow.AddForce(new Vector2(i,-4).normalized * 10, ForceMode2D.Impulse);
        }

        if (bossHealth > 0)
        {
            Invoke("choosePattern", reloadingTime);
        }

    }

    void guidedFire(string bulletType)
    {
       
        GameObject bossArrow = objManager.makeObj(bulletType);
        bossArrow.transform.position = transform.position + Vector3.forward * 0.1f;

        Rigidbody2D rigidArrow = bossArrow.GetComponent<Rigidbody2D>();

        rigidArrow.AddForce(new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y).normalized * 10, ForceMode2D.Impulse);

        float rotAng = 0f;
        
        if (boss.transform.position.x <= player.transform.position.x)
        {
            rotAng = Vector2.Angle(boss.transform.position - player.transform.position, new Vector2(0, 1));

            bossArrow.transform.rotation = Quaternion.Euler(0, 0, rotAng);
        } else
        {
            rotAng = Vector2.Angle(boss.transform.position - player.transform.position, new Vector2(0, -1));

            bossArrow.transform.rotation = Quaternion.Euler(0, 0, rotAng + 180f);
        }


        if (bossHealth > 0)
        {
            Invoke("choosePattern", reloadingTime);
        }

    }

    //��ä�� ���
    void arcFire(string bulletType)
    {
        maxPatternCount = 30;

        if (bossHealth > 0)
        {
            GameObject bossArrow = objManager.makeObj(bulletType);
            bossArrow.transform.position = transform.position + Vector3.forward * 0.1f;

            Rigidbody2D rigidArrow = bossArrow.GetComponent<Rigidbody2D>();

            Vector2 dirVec = new Vector2(Mathf.Sin(Mathf.PI * 2 * curPatternCount / maxPatternCount), -1);
            rigidArrow.AddForce(dirVec.normalized * 10, ForceMode2D.Impulse);

            float rotAng = 0f;

            rotAng = Vector2.Angle(dirVec, new Vector2(0, -1));

            if (dirVec.x > 0)
            {
                bossArrow.transform.rotation = Quaternion.Euler(0, 0, rotAng);
            }
            else
            {
                bossArrow.transform.rotation = Quaternion.Euler(0, 0, rotAng * (-1f));
            }



            curPatternCount++;
        }

        if (curPatternCount < maxPatternCount)
        {
            StartCoroutine(arcFireC(bulletType));
        }
        else if (curPatternCount >= maxPatternCount && bossHealth > 0)
        {
            Invoke("choosePattern", reloadingTime);
        }
    }
    IEnumerator arcFireC(string bulletType)
    {
        yield return new WaitForSecondsRealtime(0.25f);
        arcFire(bulletType);
    }

    //����ȭ��
    void scatterFire(string bulletType)
    {
        
        for (int i = -2; i < 3; i++)
        {
            GameObject bossArrow = objManager.makeObj(bulletType);
            bossArrow.transform.position = transform.position + Vector3.forward * 0.1f;

            Rigidbody2D rigidArrow = bossArrow.GetComponent<Rigidbody2D>();


            Vector2 dirVec = new Vector2(-i, -1);

            if (i == 0)
            {
                rigidArrow.AddForce(dirVec.normalized * 2.5f, ForceMode2D.Impulse);
            }
            else
            {
                rigidArrow.AddForce(dirVec.normalized * (Mathf.Abs(i * 3)), ForceMode2D.Impulse);
            }

            float rotAng = 0f;

            rotAng = Vector2.Angle(dirVec, new Vector2(0, -1));

            if (dirVec.x > 0)
            {
                bossArrow.transform.rotation = Quaternion.Euler(0, 0, rotAng);
            }
            else
            {
                bossArrow.transform.rotation = Quaternion.Euler(0, 0, rotAng * (-1f));
            }

            StartCoroutine(scatter(bossArrow));
        }


        if (bossHealth > 0)
        {
            Invoke("choosePattern", reloadingTime);
        }
    }

    IEnumerator scatter(GameObject gameObject)
    {
        yield return new WaitForSeconds(0.7f);
        Rigidbody2D obRigid = gameObject.GetComponent<Rigidbody2D>();
        gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

        obRigid.AddForce(new Vector2(0, -1) * 15, ForceMode2D.Impulse);
    }

    void moreScatterFire(string bulletType)
    {
        
        for (int i = -2; i < 3; i++)
        {
            GameObject bossArrow = objManager.makeObj(bulletType);
            bossArrow.transform.position = transform.position + Vector3.forward * 0.1f;

            Rigidbody2D rigidArrow = bossArrow.GetComponent<Rigidbody2D>();


            Vector2 dirVec = new Vector2(-i, -1);

            if (i == 0)
            {
                rigidArrow.AddForce(dirVec.normalized * 2.5f, ForceMode2D.Impulse);
            }
            else
            {
                rigidArrow.AddForce(dirVec.normalized * (Mathf.Abs(i * 3)), ForceMode2D.Impulse);
            }

            float rotAng = 0f;

            rotAng = Vector2.Angle(dirVec, new Vector2(0, -1));

            if (dirVec.x > 0)
            {
                bossArrow.transform.rotation = Quaternion.Euler(0, 0, rotAng);
            }
            else
            {
                bossArrow.transform.rotation = Quaternion.Euler(0, 0, rotAng * (-1f));
            }

            StartCoroutine(moreScatter(bossArrow));
        }


        if (bossHealth > 0)
        {
            Invoke("choosePattern", reloadingTime);
        }
    }

    IEnumerator moreScatter(GameObject gameObject)
    {
        yield return new WaitForSeconds(0.7f);
        Rigidbody2D obRigid = gameObject.GetComponent<Rigidbody2D>();

        float randomPoint = Random.Range(-1f, 1f);
        Vector2 dirVec = new Vector2(Mathf.Sin(randomPoint), -1);
        obRigid.AddForce(dirVec.normalized * 15, ForceMode2D.Impulse);

        float rotAng = Vector2.Angle(dirVec, new Vector2(0, -1));

        if (dirVec.x > 0)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, rotAng);
        }
        else
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, rotAng * (-1f));
        }
    }

    //����
    void machineGun(string bulletType)
    {
        maxPatternCount = 15;

        if (bossHealth > 0)
        {
            GameObject bossArrow = objManager.makeObj(bulletType);
            bossArrow.transform.position = transform.position + Vector3.forward * 0.1f;

            Rigidbody2D rigidArrow = bossArrow.GetComponent<Rigidbody2D>();
            float randomPoint = Random.Range(-1f, 1f);
            Vector2 dirVec = new Vector2(Mathf.Sin(randomPoint), -1);
            rigidArrow.AddForce(dirVec.normalized * 8, ForceMode2D.Impulse);

            float rotAng = 0f;

            rotAng = Vector2.Angle(dirVec, new Vector2(0, -1));

            if (dirVec.x > 0)
            {
                bossArrow.transform.rotation = Quaternion.Euler(0, 0, rotAng);
            }
            else
            {
                bossArrow.transform.rotation = Quaternion.Euler(0, 0, rotAng * (-1f));
            }

            curPatternCount++;
        }

        if (curPatternCount < maxPatternCount && bossHealth > 0)
        {
            StartCoroutine(machinGunC(bulletType));
        } else if (curPatternCount >= maxPatternCount && bossHealth > 0)
        {
            Invoke("choosePattern", reloadingTime);
        }
    }
    IEnumerator machinGunC(string bulletType)
    {
        yield return new WaitForSecondsRealtime(0.2f);
        machineGun(bulletType);
    }
    void cannon()
    {
        if (bossHealth > 0)
        {
            cannonWayShow();
        }
        else
            return;
    }
    void cannonWayShow()
    {
         prebeam = objManager.makeObj("bossBulletC");
        prebeam.transform.position = transform.position + Vector3.down*7;
        SpriteRenderer beamsprite = prebeam.GetComponent<SpriteRenderer>();
        beamsprite.color = Color.red ;
        Invoke("cannonFire",2);

    }


    void cannonFire()
    {
        prebeam.SetActive(false);
        cannonshot = objManager.makeObj("bossBulletD");
        cannonshot.transform.position = transform.position+ Vector3.down *7 ;
        
        Invoke("disableCannon", 1);
    }
    void disableCannon()
    {
        isCannonOn = false;
        cannonshot.SetActive(false);

        Invoke("choosePattern", 1);
    }

}
