using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    public GameObject Cat;
    public bool isDash=false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && isDash==false)
        {
            Debug.Log("chat");
            Cat.GetComponent<EnemyBigCat>().canDash = true;
            isDash = true;

        }

        


    }
}
    
