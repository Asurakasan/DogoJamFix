using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : Enemy
{




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

        right = !right;

    }

    protected override void PlayerDamage(Collider2D player)
    {
        base.PlayerDamage(player);

    }
}
