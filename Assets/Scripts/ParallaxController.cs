using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ParallaxController : MonoBehaviour
{
    [SerializeField] private List<ParallaxElement> scenarySprites = new List<ParallaxElement>();
    [SerializeField] private float parallaxSpeed = 0.1f;
    public GameObject reference;
    private Vector3 referencePosition;
    private bool _cameraShaking;

    private void Awake()
    {
        ParallaxElement[] elements = FindObjectsOfType<ParallaxElement>();
        scenarySprites.AddRange(elements);
        referencePosition = reference.transform.position;
        _cameraShaking = false;
    }
    private void Update()
    {
        if (referencePosition == reference.transform.position)
            return;
        var movement = reference.transform.position - referencePosition;
        UpdatePosition(movement);
        referencePosition = reference.transform.position;
    }
    public void UpdatePosition(Vector2 position)
    {
        if (_cameraShaking) return;
        foreach (ParallaxElement element in scenarySprites)
        {
            element.UpdatePosition(position * parallaxSpeed);
        }
    }
    
    public void EnableCameraShake()
    {
        _cameraShaking = true;
    }

    public void DisableCameraShake()
    {
        _cameraShaking = false;
    }
}