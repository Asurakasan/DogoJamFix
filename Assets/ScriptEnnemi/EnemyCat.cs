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
        //RigidCat.AddForce(new Vector2(10, jumpforce), ForceMode2D.Impulse);
        //right = !right;
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("chat");
            //RigidCat.AddForce(new Vector2(10, jumpforce), ForceMode2D.Impulse);
        }
    }

    protected override void PlayerDamage(Collider2D player)
    {
        base.PlayerDamage(player);

    }





}