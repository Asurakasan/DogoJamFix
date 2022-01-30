using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_anim_test : MonoBehaviour
{
    public Animator anim;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("Attack");
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            anim.SetTrigger("Hurt");
        }

    }
}
