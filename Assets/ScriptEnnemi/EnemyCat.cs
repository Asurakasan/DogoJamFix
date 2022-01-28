using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCat : EnemyBase2
{
    public float baseOffset;
    public Collider2D detection;
    public bool canJump;
    public int JumpForceCat;

    
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        //Permet de changer le positionnement de la box de détection


        if (right)
        {
            detection.offset = new Vector2(baseOffset, detection.offset.y);
        }
        else
        {
            detection.offset = new Vector2(-baseOffset, detection.offset.y);
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

        }
       

    }

    

    protected override void PlayerDamage(Collider2D player)
    {
        base.PlayerDamage(player);

    }

    private void OnCollisionEnter2D(Collision2D collision) //Changement de sens
    {

        //destruction du chat quand il sort de l'écran
        if (collision.gameObject.tag == "wall")
        {
            Destroy(gameObject);

        }

    }


}