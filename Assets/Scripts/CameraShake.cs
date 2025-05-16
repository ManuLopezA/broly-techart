using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private ParallaxController parallaxController;
    [SerializeField] private float shakeX;
    [SerializeField] private float shakeY;
    
    [Header("Camera Shake Atributes")]
    [SerializeField] private float duration;
    [SerializeField] private float magnitude;
    [SerializeField] private float shakeStartTime;
  
    public IEnumerator Shake()
    {
        parallaxController.EnableCameraShake();
        
        // waiting anticipation animation before explosion
        yield return new WaitForSeconds(shakeStartTime);
        
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-shakeX, shakeX) * magnitude;
            float y = Random.Range(-shakeY, shakeY) * magnitude;

            transform.localPosition = originalPos + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }
        
        transform.localPosition = originalPos;
        
        yield return new WaitForSeconds(0.5f);
        parallaxController.DisableCameraShake();
    }
}
