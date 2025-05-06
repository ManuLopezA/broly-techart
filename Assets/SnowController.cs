using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[System.Serializable]
public class SnowMaker
{
    public ParticleSystem snowMaker;
    public float speed;
    public Vector3 initialNoise;
}

public class SnowController : MonoBehaviour
{

    [SerializeField] private bool snowControllerActive = true;
    [SerializeField] private List<SnowMaker> snowMakers = new List<SnowMaker>();
    [SerializeField] private Broly broly;
    [SerializeField] private float multiplier = 2f;

    [Header("Snow Shake Atributes")] 
    [SerializeField] private float magnitude;
    [SerializeField] private float shakeTime;
    [SerializeField] private float shakeX;
    [SerializeField] private float minShakeY;
    [SerializeField] private float maxShakeY;

    private void Awake()
    {
        foreach (var snowMaker in snowMakers)
        {
            broly.OnForceStart += ChangeSnowSpeed;
            var main = snowMaker.snowMaker.main;
            snowMaker.speed = main.simulationSpeed;
            
            var noise = snowMaker.snowMaker.noise;
            snowMaker.initialNoise = new Vector3(noise.strengthX.constant, noise.strengthY.constant, 0);
        }
    }

    public void ChangeSnowSpeed(bool ascendant)
    {
        if (!snowControllerActive) return;
        Debug.Log("Change Snow Speed");
        foreach (var snowMaker in snowMakers)
        {
            var main = snowMaker.snowMaker.main;
            if (ascendant)
            {
                main.simulationSpeed = snowMaker.speed * multiplier;
                StartCoroutine(SnowShake(snowMaker));
            }
            else
            {
                StartCoroutine(SnowSpeedRecover(snowMaker));
            }
        }
    }



    private IEnumerator SnowShake(SnowMaker snowMaker)
    {
        var noise = snowMaker.snowMaker.noise;
        float elapsed = 0f;
        while (elapsed < shakeTime)
        {
            float x = Random.Range(-shakeX, shakeX) * magnitude;
            float y = Random.Range(minShakeY, maxShakeY) * magnitude;
            noise.strengthX = x;
            noise.strengthY = y;
            elapsed += Time.deltaTime;
            yield return null;
        }
        Debug.Log("Snow Shake Complete");
        
        noise.strengthX = snowMaker.initialNoise.x;
        noise.strengthY = snowMaker.initialNoise.y;
    }

    private IEnumerator SnowSpeedRecover(SnowMaker snowMaker)
    {
        var main = snowMaker.snowMaker.main;
        while (main.simulationSpeed < snowMaker.speed)
        {
            main.simulationSpeed += Time.deltaTime;
            yield return null;
        }
    }
}