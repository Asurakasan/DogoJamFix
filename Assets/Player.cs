using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private GameObject player;
    /*[Header("Attack Point")]
    public Transform Punch1;
    public Transform Punch2;
    public Transform UpperCut;
    public Transform LowKick;
    */
    public Transform attackPoint;

    [Header("Attack Settings")]
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    [Header("Movement Settings")]
    public float speed;
    public float jumpforce;


    [Header("State")]
    public bool IdleState;
    public bool Walking;
    public bool Jumping;
    public bool Crouching;

    //[Header("Animation")]
    //public Animator animPlayer;

    private Rigidbody2D Rigid;

    private void Awake()
    {
        Rigid = GetComponent<Rigidbody2D>();
        player = this.gameObject;
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

        if(!Input.anyKey)
            Idle();
    }

    void Attack()
    {
        Collider2D[] hitenemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitenemies)
        {
            EnemyDamage(enemy);
        }
    }
    void EnemyDamage(Collider2D enemy)
    {
        Destroy(enemy.gameObject);
    }
    void Idle()
    {
        //animPlayer.SetBool("walking", false);
        //attackPoint = Punch1;
    }
    void Walk()
    {
        float horizontal = Input.GetAxis("Horizontal")*speed;
        horizontal *= Time.deltaTime;

        transform.Translate(horizontal, 0, 0);
        //animPlayer.SetBool("walking", true);
        if (horizontal < 0)
            transform.localScale = new Vector3(-1, 1, 1); 
        else
            transform.localScale = new Vector3(1, 1, 1);


        //attackPoint = Punch1;
    }
    void Jump()
    {
        Rigid.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
        //attackPoint = UpperCut;
    }
    void Crouch()
    {
        //attackPoint = LowKick;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
