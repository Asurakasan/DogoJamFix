using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScript : MonoBehaviour
{
    public GameObject Cat;
    private bool isJump=false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && isJump==false)
        {
            Debug.Log("chat");
            Cat.GetComponent<EnemyCat>().canJump = true;
            isJump = true;

        }

        if (other.gameObject.tag == "Sol")
            isJump = false;


    }
}
    
