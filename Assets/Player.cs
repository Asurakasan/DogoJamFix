using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    MainGame maingame;
   CameraManager cameramanager;

    private GameObject player;
    [Header("Attack Point")]
    public Transform Punch1;
    public float Punch1Range;
    public Transform Punch2;
    public float Punch2Range;
    public Transform UpperCut;
    public float UppperCutRange;
    public Transform LowKick;
    public float LowKickRange;
    [Header("---------------")]
    public Transform attackPoint;
    public Transform ult;

    [Header("Attack Settings")]
    public float attackRange = 0.5f;
    public float ultiRange = 5f;
    public LayerMask enemyLayers;

    [Header("Ulti Stats")]
    public int chargeUlt;
    public int addPoint;
    public int maxUlt;

    [Header("Life Stats")]
    public int currentLife;
    public int maxLife;

    [Header("Movement Settings")]
    public float speed;
    private float saveSpeed;
    public float jumpforce;
    public bool IsGrounded, Crouched, CanWalk;

    public SpriteRenderer SpritPlayer;
    public Sprite[] SpriteList;

    //[Header("Animation")]
    //public Animator animPlayer;

    private Rigidbody2D Rigid;

    public bool Invicible;

    public bool punch1, punch2;

    public float InvicibleTime = 0f;
    public float TimeStart;

    public Collider2D dammage;
   
    public float  InCrounch;
    private float Stand;
    private void Awake()
    {
        Rigid = GetComponent<Rigidbody2D>();
        player = this.gameObject;
        currentLife = maxLife;
        chargeUlt = 0;
        saveSpeed = speed;
    }
    private void Start()
    {
        Stand = dammage.offset.y;

        Invicible = false;
        maingame = MainGame.instance;
        cameramanager = CameraManager.instance;

       //HealthBar.Instance.MaxValue(maxLife); COMENT NICO
        //UltBar.Instance.MaxValue(maxUlt); COMENT NICO
        punch1 = true;
        CanWalk = true;
    }
    // Start is called before the first frame update
    void Update()
    {
        if (Crouched)
        {
            dammage.offset = new Vector2(dammage.offset.x, InCrounch);

        }

        else
        {
            dammage.offset = new Vector2(dammage.offset.x, Stand);


        }

        //HealthBar.Instance.SetHealth(currentLife); //COMENT NICO
        //UltBar.Instance.SetUlt(chargeUlt); //COMENT NICO
        if (Invicible)
        {

            SpritPlayer.sprite = SpriteList[5]; //Ajout NICO


            TimeStart -= 1 * Time.deltaTime;


            if (TimeStart <= 0)
            {

                TimeStart = InvicibleTime;
                Invicible = false;
                Physics2D.IgnoreLayerCollision(3, 6, false);
                SpritPlayer.sprite = SpriteList[0]; //Ajout NICO
            }
        }

        if (!Invicible)
        {
            if (Input.GetKey(KeyCode.Space))
            {
               if(punch1)
               {
                    Attack1();
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {

                        punch2 = true;
                        punch1 = false;
                    }
               }
               else if(punch2)
               {
                    Attack2();
                    punch1 = true;
                    punch2 = false;
               }
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                if (Crouched)
                    SpritPlayer.sprite = SpriteList[1];
                if (!IsGrounded)
                    SpritPlayer.sprite = SpriteList[0];
                if(IsGrounded)
                {
                    punch2 = false;
                    punch1 = true;
                }
                
            }
            if ((Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.D)) && CanWalk)
                Walk();
            if (Input.GetKeyDown(KeyCode.Z) && IsGrounded)
                Jump();
            if (Input.GetKeyDown(KeyCode.S) && IsGrounded)
                Crouch();
            if (Input.GetKeyUp(KeyCode.S) && IsGrounded)
                CanWalk = true;
            if (Input.GetKeyDown(KeyCode.R) && chargeUlt > maxUlt)
                Ulti();
            if (!Input.anyKey)
                Idle();

        }

       

    }

    void Attack1()
    {
        Debug.Log("punch1");
        speed = 0;
        Collider2D[] hitenemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitenemies)
        {
            EnemyDamage(enemy);
            chargeUlt += addPoint;
        }
        if (Crouched)
            SpritPlayer.sprite = SpriteList[2];
        else if (!IsGrounded)
            SpritPlayer.sprite = SpriteList[3];
        else if (IsGrounded && !Crouched)
        {
            attackPoint = Punch1;
            SpritPlayer.sprite = SpriteList[4];
        }
        
    }
    void Attack2()
    {
        Debug.Log("punch2");
       
        speed = 0;
        Collider2D[] hitenemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitenemies)
        {
            EnemyDamage(enemy);
            chargeUlt += addPoint;
        }
        if (Crouched)
            SpritPlayer.sprite = SpriteList[2];
        else if (!IsGrounded)
            SpritPlayer.sprite = SpriteList[3];
        else if (IsGrounded && !Crouched)
        {
            attackPoint = Punch2;
            SpritPlayer.sprite = SpriteList[5];
        }

    }
    void EnemyDamage(Collider2D enemy)
    {
        if (enemy.gameObject.tag == "Ennemis") //Ajout NICO
        {
            enemy.GetComponent<EnemyBase2>().IsDead = true; //Ajout NICO
            //Destroy(enemy.gameObject);

        }
       
    }
    void Idle()
    {
        Crouched = false;
        //animPlayer.SetBool("walking", false);
        if (IsGrounded)
        {
            attackPoint = Punch1;
            attackRange = Punch1Range;

        }
        else
        {
            attackPoint = UpperCut;
            attackRange = UppperCutRange;
        }
        SpritPlayer.sprite = SpriteList[0];
    }
    void Walk()
    {
        speed = saveSpeed;
        float horizontal = Input.GetAxis("Horizontal")*speed;
        horizontal *= Time.deltaTime;

        transform.Translate(horizontal, 0, 0);
        //animPlayer.SetBool("walking", true);
        if (horizontal < 0)
            transform.localScale = new Vector3(-1, 1, 1); 
        else
            transform.localScale = new Vector3(1, 1, 1);

        if(IsGrounded)
            attackPoint = Punch1;
    }
    void Jump()
    {
        Rigid.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
        attackPoint = UpperCut;
        IsGrounded = false;
        attackRange = UppperCutRange;
    }
    void Crouch()
    {
        CanWalk = false;
        speed = 0;
        Crouched = true;
        attackPoint = LowKick;
        attackRange = LowKickRange;
        SpritPlayer.sprite = SpriteList[1];
    }
    void Ulti()
    {
        Collider2D[] ultenemies = Physics2D.OverlapCircleAll(ult.position, ultiRange, enemyLayers);
        foreach (Collider2D enemy in ultenemies)
        {
            EnemyDamage(enemy);
        }
        chargeUlt = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        for (int i = 0; i < maingame.TriggerArene.Count; i++)
        {
            if (collision == maingame.TriggerArene[i])
            {
                maingame.index = i;
                cameramanager.target = collision.gameObject.transform;
                cameramanager.cameraIsfollow = false;
                maingame.isEnter = true;
                maingame.TriggerArene.Remove(maingame.TriggerArene[i]);

            }
        }

        if (collision.gameObject.tag == "Ennemis" || collision.gameObject.tag == "Rocket") //Ajout NICO
        {

            currentLife = currentLife - 1;
            if (Invicible == false)
            {
                TimeStart = InvicibleTime;
                Invicible = true;
                Physics2D.IgnoreLayerCollision(3, 6);
            }




        }
        for (int i = 0; i < maingame.TriggerArene.Count; i++)
        {
            if (collision == maingame.TriggerArene[i])
            {
                Debug.Log("oui");
                cameramanager.target = collision.gameObject.transform;
                cameramanager.cameraIsfollow = false;

            }
        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
       /* foreach (var item in maingame.ennemyObject)
        {
            if(item == null)
            {
                cameramanager.cameraIsfollow = true;
            }


        }*/
        

    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        if (ult == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        Gizmos.DrawWireSphere(ult.position, ultiRange);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Sol"))
            IsGrounded = true;
    }

   
}
