using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("Attack Settings")]
    public Transform attackPoint;
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
    }
    void Walk()
    {
        float horizontal = Input.GetAxis("Horizontal")*speed;
        horizontal *= Time.deltaTime;

        transform.Translate(horizontal, 0, 0);
        //animPlayer.SetBool("walking", true);
    }
    void Jump()
    {
        Rigid.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
    }
    void Crouch()
    {
        
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
