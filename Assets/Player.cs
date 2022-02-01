using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static Player instance;
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

    [Header("Animation")]
    public Animator animPlayer;
    public bool bPunch1, bPunch2, bInAnim;
    public GameObject FxUlti;
    [Header("Other")]
    private Rigidbody2D Rigid;

    public bool Invicible;

    public bool punch1, punch2;

    public float InvicibleTime = 0f;
    public float TimeStart;

    public Collider2D dammage;
   
    public float  InCrounch;
    private float Stand;

    [Header("Direction")]
    public bool Gauche, Droite;
    private void Awake()
    {
        Rigid = GetComponent<Rigidbody2D>();
        player = this.gameObject;
        currentLife = maxLife;
        chargeUlt = 0;
        saveSpeed = speed;
        instance = this;
    }
    private void Start()
    {
        Stand = dammage.offset.y;

        Invicible = false;
        maingame = MainGame.instance;
        cameramanager = CameraManager.instance;

       HealthBar.Instance.MaxValue(maxLife);
       UltBar.Instance.MaxValue(maxUlt); 
        punch1 = true;
        CanWalk = true;
    }
    // Start is called before the first frame update
    void Update()
    {
        if(currentLife <= 0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        if (Crouched)
        {
            dammage.offset = new Vector2(dammage.offset.x, InCrounch);

        }

        else
        {
            dammage.offset = new Vector2(dammage.offset.x, Stand);


        }

        HealthBar.Instance.SetHealth(currentLife); 
        UltBar.Instance.SetUlt(chargeUlt); 
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
            animPlayer.SetBool("Hurt", true);
        }

        if (!Invicible)
        {
            animPlayer.SetBool("Hurt", false);

            

            
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
            {
                CanWalk = true;
                animPlayer.SetBool("IsCrounching", false);
            }

            if (Input.GetKeyDown(KeyCode.R) && chargeUlt >= maxUlt)
                Ulti();
            if (!Input.anyKey)
                Idle();

        }

        if(IsGrounded)
            animPlayer.SetBool("IsJumping", false);

        if(!IsGrounded && Input.GetKeyDown(KeyCode.Space))
            animPlayer.SetTrigger("AttackAir");

        if (Crouched && Input.GetKeyDown(KeyCode.Space))
            animPlayer.SetTrigger("Kick");

        if (Input.GetKeyDown(KeyCode.Space) && !bInAnim && !bPunch1 && !bPunch2)
            Attack1();

        if (Input.GetKeyUp(KeyCode.Space) && bInAnim && bPunch1 && bPunch2)
        {
            bInAnim = false;
            bPunch1 = false;
            bPunch2 = false;
        }
        if (!bPunch2)
            animPlayer.SetBool("Punch2", false);


        if (bPunch2 && !bPunch1 && !bInAnim)
            Attack2();
    }

    void Attack1()
    {
        animPlayer.SetTrigger("Attack1");
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
        animPlayer.SetTrigger("Attack2");
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
        bPunch2 = false;
    }

    void EnemyDamage(Collider2D enemy)
    {
        if (enemy.gameObject.tag == "Ennemis") //Ajout NICO
        {
            enemy.GetComponent<EnemyBase2>().IsDead = true; //Ajout NICO
            cameramanager.ShakeCam(cameramanager.Intensity, cameramanager.Timing);
            //Destroy(enemy.gameObject);

        }
       
    }
    void Idle()
    {
        Crouched = false;
        animPlayer.SetBool("IsWalking", false);
        if (IsGrounded)
        {
            attackPoint = Punch1;
            attackRange = Punch1Range;
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
        animPlayer.SetBool("IsWalking", true);
        if (horizontal < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            Gauche = true;
            Droite = false;
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
            Gauche = false;
            Droite = true;
        }

        if(IsGrounded)
            attackPoint = Punch1;
    }
    void Jump()
    {
        animPlayer.SetTrigger("TakeOf");
        animPlayer.SetBool("IsJumping", true);
        Rigid.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
        attackPoint = UpperCut;
        IsGrounded = false;
        attackRange = UppperCutRange;
    }
    void Crouch()
    {
        animPlayer.SetBool("IsCrounching", true); 
        CanWalk = false;
        speed = 0;
        Crouched = true;
        attackPoint = LowKick;
        attackRange = LowKickRange;
        SpritPlayer.sprite = SpriteList[1];
    }
    void Ulti()
    {
        Instantiate(FxUlti, transform.position, Quaternion.identity);
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
