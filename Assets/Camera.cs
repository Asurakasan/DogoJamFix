using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] bool Shake;
    [SerializeField] AnimationCurve animationCurve;
    [SerializeField] float ShakeSpeed;
    [SerializeField] float TimeBetweenShake;


    Vector3 position;
    Vector3 offset;



    // Start is called before the first frame update
    void Start()
    {
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

    }

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
}
