using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyCat : EnemyBase2
{
    public GameObject rocket;

    public float currentTime = 0f;
    public float ThrowTime;


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        

        currentTime = ThrowTime;

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

       

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

    private void OnCollisionEnter2D(Collision2D collision) //Changement de sens
    {
        right = !right;



    }

}