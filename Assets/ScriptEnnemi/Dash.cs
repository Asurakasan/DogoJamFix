using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    public GameObject Cat;
    public bool isDash=false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Quand le player rentre dans la zone de détection, on lance le dash

        if (other.gameObject.tag == "Player" && isDash==false)
        {
           
            Cat.GetComponent<EnemyBigCat>().canDash = true;
            isDash = true;

        }

        


    }
}
    
