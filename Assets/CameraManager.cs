using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    MainGame maingame;

    public static CameraManager instance;
    public bool Shake;
    [SerializeField] float ShakTimer;
    public float Intensity;
    public float Timing;


    public Transform target;

    [SerializeField] GameObject Player;
    bool isfollowing;
    public bool cameraIsfollow;

    public CinemachineVirtualCamera c_VirtualCam;


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


        if(ShakTimer > 0)
        {
            ShakTimer -= Time.deltaTime;
            if(ShakTimer <= 0f)
            {
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = c_VirtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;

            }
        }
       
        transform.position = position + offset;
        if(Shake)
        {
            ShakeCam(Intensity, Timing);
            //   StartCameraShake();
            Shake = !Shake;
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

    public void ShakeCam(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = c_VirtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        ShakTimer = time;
    }


    /*public void StartCameraShake()
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
    }*/
    #endregion


    #region CameraLock





    #endregion

}
