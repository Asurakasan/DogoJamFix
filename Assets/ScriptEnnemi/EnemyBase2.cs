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

    protected bool right = true; // right = !right;  // Change la direction

    public Collider2D detection;

    float baseOffset;

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

        baseOffset = detection.offset.x;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
        // Movement
        if (right == false)
        {
            rb.velocity = new Vector2(Speed, rb.velocity.y);
            //rb.AddForce(new Vector2(Speed, 0), ForceMode2D.Force);
            detection.offset = new Vector2(baseOffset, detection.offset.y);
            
        }
        else
        {
            rb.velocity = new Vector2(-Speed, rb.velocity.y);
            //rb.AddForce(new Vector2(-Speed, 0), ForceMode2D.Force) ;
            detection.offset = new Vector2(-baseOffset, detection.offset.y);
        }

        //Detect Player

        Collider2D[] hitplayer = Physics2D.OverlapCircleAll(transform.position, DetectionRange, playerLayer);
        if (hitplayer != null)
        {
            Attack();

        }




    }



    //Attack
    void Attack()
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


}