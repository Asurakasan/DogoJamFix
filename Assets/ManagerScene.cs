using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerScene : MonoBehaviour
{
    public void OnButtonMenuStart()
    {
        SceneManager.LoadScene("DevNico");

    }
    public void OnButtonMenuAcceuil()
    {
        SceneManager.LoadScene("Start");

    }
    public void OnButtonMenuKey()
    {
        SceneManager.LoadScene("Touche");
    }
}
