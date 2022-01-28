using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Gestion des dégats de la rocket et destruction de la rocket

        if (other.gameObject.tag == "Player" || other.gameObject.tag=="Out")
        {
            Destroy(gameObject);
        }




    }
}
