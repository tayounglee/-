using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeAmount;
    float shakeTime;
    Vector3 initialPosition;
    public static CameraShake camera;

    public void VibrateForTime(float time)
    {
        shakeTime = time;
    }

    // Start is called before the first frame update
    void Start()
    {
        shakeAmount = 1.5f;
        camera = FindObjectOfType<CameraShake>();
        camera.enabled = false;
        initialPosition = new Vector3(0f, 0f, -5f);
        camera = GameObject.FindWithTag("MainCamera").GetComponent<CameraShake>();
        camera.VibrateForTime(0.7f);
    }

    // Update is called once per frame
    void Update()
    {
        if (shakeTime > 0)
        {
            transform.position = Random.insideUnitSphere * shakeAmount + initialPosition;
            shakeTime -= Time.deltaTime;
        }
        else
        {
            shakeTime = 0.0f;
            transform.position = initialPosition;
        }
    }
}
