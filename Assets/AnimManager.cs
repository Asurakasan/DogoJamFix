using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimManager : MonoBehaviour
{
    void StartAnim()
    {
        Player.instance.bInAnim = true;
        Player.instance.bPunch1 = true;
    }
    void EndAnim()
    {
        Player.instance.bInAnim = false;
        Player.instance.bPunch1 = false;
    }
    
}
