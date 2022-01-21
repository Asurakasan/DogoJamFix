using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCat : EnemyBase2
{
    public float baseOffset;

    public float DetectionRange2;
    

    public Collider2D detection;

    public float DetectionRangeCat;

    public bool canJump;

    // Start is called before the first frame update
    protected override void Start()
    {


        base.Start();

        

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

       

    }

    

    protected override void PlayerDamage(Collider2D player)
    {
        base.PlayerDamage(player);

    }

   


}