using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCat : EnemyBase2
{

    public float DetectionRange2;
    public int jumpforce;

     

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        

    }

    // Update is called once per frame
    protected override void Update()
    {
        
        base.Update();
        
    }

    private void OnCollisionEnter2D(Collision2D collision) //Changement de sens
    {

        rb.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse); //changer le x en positif et negatif 
        //rb.velocity = new Vector2(0, jumpforce);
        Debug.Log("test");
        
        //right = !right;
        if (collision.gameObject.tag == "Player")
        {
            
            Debug.Log("chat");
            
        }
    }

    protected override void PlayerDamage(Collider2D player)
    {
        base.PlayerDamage(player);

    }





}