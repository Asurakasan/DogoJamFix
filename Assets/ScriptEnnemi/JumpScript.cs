using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScript : MonoBehaviour
{
    public GameObject Cat;


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("chat");
        Cat.GetComponent<EnemyCat>().canJump = true;
    }
}
    
