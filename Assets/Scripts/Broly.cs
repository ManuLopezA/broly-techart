using System;
using System.Collections;
using BrolyTechArt;
using UnityEngine;

[RequireComponent(typeof(AnimationHandler))]
[RequireComponent(typeof(Rigidbody2D))]
public class Broly : MonoBehaviour
{
    [SerializeField] private bool isGrounded = true;
    private AnimationHandler _animationHandler;
    [SerializeField] private float speed;
    [SerializeField] private float time;
    
    [SerializeField] private Shadow shadow;
    [SerializeField] private CameraShake cameraShake;

    public Action OnForceStart;

    private void Awake()
    {
        _animationHandler = GetComponent<AnimationHandler>();
    }

    private void Update()
    {
        InputGetKeys();
    }

    private void InputGetKeys()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (isGrounded)
                StartCoroutine(FlySequence());
            else
            {
                StartCoroutine(LandSequence());
            }

            return;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _animationHandler.Play(BrolyState.SayingNoNo);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _animationHandler.Play(BrolyState.Force01Ground);
            StartCoroutine(cameraShake.Shake());
            return;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            _animationHandler.Play(BrolyState.Force02Ground);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CinematicController.Instance.PlayCinematic(0); 
        }
    }

    private IEnumerator FlySequence()
    {
        _animationHandler.Play(BrolyState.FlyStart);
        float elapsed = 0f;
        while (elapsed < time)
        {
            transform.position += new Vector3(0, speed, 0) * Time.deltaTime;
            elapsed += Time.deltaTime;
            shadow.ShadowResize(true);
            yield return null;
        }

        isGrounded = false;
    }

    private IEnumerator LandSequence()
    {
        float elapsed = 0f;
        while (elapsed < time)
        {
            transform.position += new Vector3(0, -speed, 0) * Time.deltaTime;
            elapsed += Time.deltaTime;
            shadow.ShadowResize(false);
            yield return null;
        }

        _animationHandler.Play(BrolyState.FlyEnd);
        isGrounded = true;
        shadow.ResetSize();
    }

    public void PlayAnimationSound(int position)
    {
        AudioController.Instance.PlaySound(position);
    }
}