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
    
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        currentTime = dashTime;

        baseOffset = detection.offset.x;

        base.Start();

        //Permet de changer le positionnement de la box de d�tection
        if (right)
        {
            detection.offset = new Vector2(-baseOffset, detection.offset.y);
        }
        else
        {
            detection.offset = new Vector2(baseOffset, detection.offset.y);
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

            }
            else
            {
                currentTime -= 1 * Time.deltaTime;

                rb.AddForce(new Vector2(DashForceCat, 0), ForceMode2D.Impulse);
            }
            

        }
        else if(canDash )
        {
            canDash = false;
            currentTime = dashTime;
            detection.GetComponent<Dash>().isDash = false;
        }   

    }
    protected override void PlayerDamage(Collider2D player)
    {
        base.PlayerDamage(player);

    }

    private void OnCollisionEnter2D(Collision2D collision) //Changement de sens
    {
        //Si le gros chat touche un mur il repart dans le sens oppos� 

        if (collision.gameObject.tag == "wall")
        {
            canDash = false;
            currentTime = dashTime;
            detection.GetComponent<Dash>().isDash = false;
            

            if (right)
            {
                right = false;
                detection.offset = new Vector2(baseOffset, detection.offset.y);

            }
            else
            {
                right = true;
                detection.offset = new Vector2(-baseOffset, detection.offset.y);

            }
        }
           

    }




}