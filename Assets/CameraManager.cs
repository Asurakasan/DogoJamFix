using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    MainGame maingame;

    public static CameraManager instance;
    [SerializeField] bool Shake;
    [SerializeField] AnimationCurve animationCurve;
    [SerializeField] float ShakeSpeed;
    [SerializeField] float TimeBetweenShake;

    public Transform target;

    [SerializeField] GameObject Player;
    bool isfollowing;
    public bool cameraIsfollow;

    public Cinemachine.CinemachineVirtualCamera c_VirtualCam;


    Vector3 position;
    Vector3 offset;


    private void Awake()
    {

        instance = this;
        //c_VirtualCam = GetComponent<Cinemachine.CinemachineVirtualCamera>();
    }
    // Start is called before the first frame update
    void Start()
    {
        maingame = MainGame.instance;
        position = transform.position;

    }

    // Update is called once per frame
    void Update()
    {

        transform.position = position + offset;
        if(Shake)
        {

            StartCameraShake();
          
        }

        if (!cameraIsfollow)
        {
            c_VirtualCam.LookAt = target.transform;
            c_VirtualCam.Follow = target.transform;

            position = transform.position;
        }
        if (cameraIsfollow)
        {
            c_VirtualCam.LookAt = Player.transform;
            c_VirtualCam.Follow = Player.transform;

            position = transform.position;
        }


    }
    #region CameraShake
    public void StartCameraShake()
    {

        StartCoroutine(screenShake());
        Shake = !Shake;
    }
    IEnumerator screenShake()
    {
        for (float i = 0; i < TimeBetweenShake; i+= Time.deltaTime)
        {

            float y = animationCurve.Evaluate(i * ShakeSpeed);
            offset = new Vector3(0, y, 0);
            yield return null;
            
        }
    }
    #endregion


    #region CameraLock





    #endregion

}
