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
    public bool IsGrounded, Crouched;

    public SpriteRenderer SpritPlayer;
    public Sprite[] SpriteList;

    //[Header("Animation")]
    //public Animator animPlayer;

    private Rigidbody2D Rigid;

    public bool Invicible;

    public float InvicibleTime = 0f;
    public float TimeStart;

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
        Invicible = false;
        maingame = MainGame.instance;
        cameramanager = CameraManager.instance;

        //HealthBar.Instance.MaxValue(maxLife);
        //UltBar.Instance.MaxValue(maxUlt);
    }
    // Start is called before the first frame update
    void Update()
    {
       // HealthBar.Instance.SetHealth(currentLife);
        //UltBar.Instance.SetUlt(chargeUlt);
        if (Invicible)
        {
            //Debug.Log("Invicible");



            TimeStart -= 1 * Time.deltaTime;


            if (TimeStart <= 0)
            {

                TimeStart = InvicibleTime;
                Invicible = false;
                Physics2D.IgnoreLayerCollision(3, 6, false);

            }
        }

        if (!Invicible)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                Attack();
            if (Input.GetKeyUp(KeyCode.Space))
            {
                if (Crouched)
                    SpritPlayer.sprite = SpriteList[1];
                if (!IsGrounded)
                    SpritPlayer.sprite = SpriteList[0];

            }
            if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.D))
                Walk();
            if (Input.GetKeyDown(KeyCode.Z) && IsGrounded)
                Jump();
            if (Input.GetKeyDown(KeyCode.S) && IsGrounded)
                Crouch();
            if (Input.GetKeyDown(KeyCode.R) && chargeUlt > maxUlt)
                Ulti();
            if (!Input.anyKey)
                Idle();

        }

       

    }

    void Attack()
    {
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
            SpritPlayer.sprite = SpriteList[4];
    }
    void EnemyDamage(Collider2D enemy)
    {
        Destroy(enemy.gameObject);
    }
    void Idle()
    {
        Crouched = false;
        //animPlayer.SetBool("walking", false);
        attackPoint = Punch1;
        attackRange = Punch1Range;
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
                Debug.Log("oui");
                cameramanager.target = collision.gameObject.transform;
                cameramanager.cameraIsfollow = false;

            }
        }

        if (collision.gameObject.tag == "Ennemis")
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
        cameramanager.cameraIsfollow = true;

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
