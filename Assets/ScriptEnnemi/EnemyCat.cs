using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCat : EnemyBase2
{
    public float baseOffset;
    public Collider2D detection;
    public bool canJump;
    public int JumpForceCat;

    public GameObject visual;
    public Animator animator;
    public SpriteRenderer sprite;


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        animator = visual.gameObject.GetComponent<Animator>();
        sprite = visual.gameObject.GetComponent<SpriteRenderer>();

        //Permet de changer le positionnement de la box de détection


        if (right)
        {
            detection.offset = new Vector2(baseOffset, detection.offset.y);


            sprite.flipX = false;
        }
        else
        {
            detection.offset = new Vector2(-baseOffset, detection.offset.y);


            sprite.flipX = true;
        }

        baseOffset = detection.offset.x;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        //Lance l'attack jump 

        if (canJump && !IsDead)  
        {
            canJump = false;
            rb.AddForce(new Vector2(0, JumpForceCat), ForceMode2D.Impulse);
            animator.SetBool("Attack", true);

        }


        if (IsDead)
        {
            animator.SetTrigger("Hurt");
        }




    }



    protected override void PlayerDamage(Collider2D player)
    {
        base.PlayerDamage(player);

    }

    /* protected override void OnTriggerEnter2D(Collider2D collision)
     {
         base.OnTriggerEnter2D(collision);

         IsDead = true;
         if(IsDead)
         {
             Destroy(gameObject);
         }

     }*/
    protected override void OnCollisionEnter2D(Collision2D collision) //Changement de sens
    {
        base.OnCollisionEnter2D(collision);

        if(collision.gameObject.tag=="Sol")
        {
           
            animator.SetBool("Attack", false);

        }
           

    }


}