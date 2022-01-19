using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private GameObject player;
    [Header("Attack Point")]
    public Transform Punch1;
    public float Punch1Range;
    public Transform Punch2;
    public Transform UpperCut;
    public Transform LowKick;
    public float LowKickRange;
    
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


    //[Header("Animation")]
    //public Animator animPlayer;

    private Rigidbody2D Rigid;

    private void Awake()
    {
        Rigid = GetComponent<Rigidbody2D>();
        player = this.gameObject;
        currentLife = maxLife;
        chargeUlt = 0;
        saveSpeed = speed;
    }
    // Start is called before the first frame update
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            Attack();
        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.D))
            Walk();
        if (Input.GetKeyDown(KeyCode.Z))
            Jump();
        if (Input.GetKeyDown(KeyCode.S))
            Crouch();
        if (Input.GetKeyDown(KeyCode.R) && chargeUlt > maxUlt)
            Ulti();
        if(!Input.anyKey)
            Idle();

        if (Input.GetKeyUp(KeyCode.S))
            transform.localScale = new Vector3(-1, 1, 1);
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
    }
    void EnemyDamage(Collider2D enemy)
    {
        Destroy(enemy.gameObject);
    }
    void Idle()
    {
        //animPlayer.SetBool("walking", false);
        attackPoint = Punch1;
        attackRange = Punch1Range;
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
    }
    void Crouch()
    {
        transform.localScale = new Vector3(-1, 0.7f, 1);
        attackPoint = LowKick;
        attackRange = LowKickRange;
        
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
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        if (ult == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        Gizmos.DrawWireSphere(ult.position, ultiRange);
    }
}
