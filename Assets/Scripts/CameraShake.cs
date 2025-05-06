using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private ParallaxController parallaxController;
    public static CameraShake Instance {get; private set;}
    [SerializeField] private float shakeX;
    [SerializeField] private float shakeY;
    
    [Header("Camera Shake Atributes")]
    [SerializeField] private float duration;
    [SerializeField] private float magnitude;
    
    private void Awake()
    {
        Instance = this;
    }

    public IEnumerator Shake()
    {
        parallaxController.EnableCameraShake();
        
        yield return new WaitForSeconds(0.4f);
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
