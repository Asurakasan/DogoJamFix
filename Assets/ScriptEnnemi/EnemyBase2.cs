using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase2 : MonoBehaviour
{
    public float Speed;
    public float Damage;
    public float DetectionRange;

  
    public LayerMask playerLayer;

    protected GameObject Player;
    protected MainGame mainGame; // script maingame


    protected Rigidbody2D rb;  //rigidbody
    public Vector2 playerposition; // position du player

    public bool right ; // right = !right;  // Change la direction

    protected int xPosition;
 

    // Start is called before the first frame update
    protected virtual void Start()
    {
       
        mainGame = MainGame.instance;
        Player = mainGame.Player;
        rb = gameObject.GetComponent<Rigidbody2D>();

        playerposition = Player.transform.position;

        if (transform.position.x < playerposition.x)
        {
            right = false;

        }
        else
        {
            right = true;
        }

       
    }

    protected virtual void Update()
    {
        
        // Movement
        if (right == false)
        {
            rb.velocity = new Vector2(Speed, rb.velocity.y);
            
            
        }
        else
        {
            rb.velocity = new Vector2(-Speed, rb.velocity.y);
            
        }

        //Detect Player

        Collider2D[] hitplayer = Physics2D.OverlapCircleAll(transform.position, DetectionRange, playerLayer);
        foreach (Collider2D player in hitplayer)
        {

            Attack();
        }

    }

    //Attack
    protected virtual void Attack()
    {

        Collider2D[] hitplayer = Physics2D.OverlapCircleAll(transform.position, DetectionRange, playerLayer);
        foreach (Collider2D player in hitplayer)
        {
            PlayerDamage(player);
        }
    }

    // Dommage
    protected virtual void PlayerDamage(Collider2D player)
    {

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, DetectionRange);
    }

    protected void OnTriggerEnter2D(Collider2D PlayerColl)
    {
        
    }

}