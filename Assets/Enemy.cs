using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Speed;
    public float Damage;
    public float DetectionRange;

    public LayerMask playerLayer;

    private GameObject Player;

    private MainGame mainGame;

    private Vector2 movement; 
    private Rigidbody2D rb;
    public Vector2 playerposition;

    private bool right = true;

    // Start is called before the first frame update
    void Start()
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

    // Update is called once per frame
    void Update()
    {

        if (right == false)
        {
            rb.velocity = new Vector2(Speed, 0);
        }
        else
        {
            rb.velocity = new Vector2(-Speed, 0);
        }
       
       

        Collider2D[] hitplayer = Physics2D.OverlapCircleAll(transform.position, DetectionRange, playerLayer);
        if(hitplayer !=null)
        {
            Attack();

        }

    }
    void Attack()
    {
        
        Collider2D[] hitplayer = Physics2D.OverlapCircleAll(transform.position, DetectionRange, playerLayer);
        foreach (Collider2D player in hitplayer)
        {
            PlayerDamage(player);
        }
    }
    void PlayerDamage(Collider2D player)
    {
        

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, DetectionRange);
    }
}
