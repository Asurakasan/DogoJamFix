using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyCat : EnemyBase2
{
    public GameObject rocket;

    public float currentTime = 0f;
    public float ThrowTime;

    public GameObject visual;
    public Animator animator;
    public SpriteRenderer sprite;

    protected override void Start()
    {
        base.Start();

        animator = visual.gameObject.GetComponent<Animator>();
        sprite = visual.gameObject.GetComponent<SpriteRenderer>();


        if (right)
        {
          sprite.flipX = false;
        }
        else
        {
            sprite.flipX = true;
        }

        currentTime = ThrowTime;

    }

    protected override void Update()
    {
        base.Update();

        //Crée une rocket toute les X secondes

        currentTime -= 1 * Time.deltaTime;

        if (currentTime <= 0)
        {
            Instantiate(rocket, transform.position, Quaternion.identity);
            currentTime = ThrowTime;
        }
    }



    protected override void PlayerDamage(Collider2D player)
    {

        base.PlayerDamage(player);

    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        //Change le sens quand le chat entre en contact avec le mur

        if (collision.gameObject.tag == "wall")
        {
            right = !right;
            sprite.flipX = !sprite.flipX;
        }

    }

}