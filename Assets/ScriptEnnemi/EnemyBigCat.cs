using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBigCat : EnemyBase2
{
    public float baseOffset;
    public Collider2D detection;
    public bool canDash;
    public int DashForceCat;

    public float currentTime = 0f;
    public float dashTime ;

    public GameObject visual;
    public Animator animator;
    public SpriteRenderer sprite; 
    
    
    // Start is called before the first frame update
    protected override void Start()
    {
        animator = visual.gameObject.GetComponent<Animator>();
        sprite = visual.gameObject.GetComponent<SpriteRenderer>();

        base.Start();
        currentTime = dashTime;

        baseOffset = detection.offset.x;

        

        //Permet de changer le positionnement de la box de détection
        if (right)
        {
            detection.offset = new Vector2(-baseOffset, detection.offset.y);

            sprite.flipX = false;
        }
        else
        {
            detection.offset = new Vector2(baseOffset, detection.offset.y);

            sprite.flipX = true;
        }

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        //Lance l'attack dash

        if (canDash && currentTime>=0 && !IsDead)
        {
            if (right)
            {
                currentTime -= 1 * Time.deltaTime;

                rb.AddForce(new Vector2(-DashForceCat, 0), ForceMode2D.Impulse);

                animator.SetBool("Attack", true);

            }
            else
            {
                currentTime -= 1 * Time.deltaTime;

                rb.AddForce(new Vector2(DashForceCat, 0), ForceMode2D.Impulse);

                animator.SetBool("Attack", true);

            }
            

        }
        else if(canDash )
        {
            animator.SetBool("Attack", false);
            canDash = false;
            currentTime = dashTime;
            detection.GetComponent<Dash>().isDash = false;
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

    protected override void OnCollisionEnter2D(Collision2D collision) //Changement de sens
    {
        base.OnCollisionEnter2D(collision);

        //Si le gros chat touche un mur il repart dans le sens opposé 


        /*if (IsDead)
              Destroy(gameObject);*/

        if (collision.gameObject.tag == "wall")
        {
            animator.SetBool("Attack", false);
            canDash = false;
            currentTime = dashTime;
            detection.GetComponent<Dash>().isDash = false;
            

            if (right)
            {
                right = false;
                detection.offset = new Vector2(baseOffset, detection.offset.y);
                sprite.flipX = true;

            }
            else
            {
                right = true;
                detection.offset = new Vector2(-baseOffset, detection.offset.y);
                sprite.flipX = false;

            }
        }
           

    }




}