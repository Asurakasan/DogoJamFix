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
    
    // Start is called before the first frame update
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            Attack();
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.D))
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
            Destroy(enemy.gameObject);
        }
    }
    void Idle()
    {
        IdleState = true;
        Walking = false;
        Jumping = false;
        Crouching = false;
    }
    void Walk()
    {
        IdleState = false;
        Walking = true;
        Jumping = false;
        Crouching = false;
    }
    void Jump()
    {
        IdleState = false;
        Walking = false;
        Jumping = true;
        Crouching = false;
    }
    void Crouch()
    {
        IdleState = false;
        Walking = false;
        Jumping = false;
        Crouching = true;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
