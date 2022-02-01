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

    public bool IsDead;
 


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

        if (IsDead==true)
        {
            //Destroy(gameObject);
            

            mainGame.ennemylist.Remove(gameObject);
            Speed = 0;
            DetectionRange = 0;
            mainGame.Canexplore(); //J'ai commenter cette ligne car elle fesait buger le jeu et je ne comprenais pas pourquoi
            if (Player.GetComponent<Player>().Droite)
            {
                gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
               
                rb.AddForce(new Vector2(150,5));

            }
            else if(Player.GetComponent<Player>().Gauche)
            {
                gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
                rb.AddForce(new Vector2(-150,5));

            }

            

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

    protected void OnTriggerEnter2D(Collider2D Wall)
    {
        if (Wall.gameObject.tag == "wall" && IsDead)
        Destroy(gameObject);

    }



}