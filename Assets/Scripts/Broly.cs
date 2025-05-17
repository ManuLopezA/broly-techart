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
    
    private void Awake()
    {
        _animationHandler = GetComponent<AnimationHandler>();
        enabled = false;
    }

    private void Update()
    {
        InputGetKeys();
    }

    private void InputGetKeys()
    {
        if (_animationHandler.IsOnAnimation) return;
        
        // start or finish flight
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
        
        // get out of the program
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CinematicController.Instance.PlayCinematic(0); 
        }

        
        if (!isGrounded) return;
        
        // say no-no
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _animationHandler.Play(BrolyState.SayingNoNo);
            return;
        }

        // force explosion 
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _animationHandler.Play(BrolyState.Force01Ground);
            StartCoroutine(cameraShake.Shake());
            return;
        }

        // force concentration
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            _animationHandler.Play(BrolyState.Force02Ground);
        }
    }

    private IEnumerator FlySequence()
    {
        isGrounded = false;
        _animationHandler.Play(BrolyState.FlyStart);
        float elapsed = 0f;
        while (elapsed < time)
        {
            transform.position += new Vector3(0, speed, 0) * Time.deltaTime;
            elapsed += Time.deltaTime;
            shadow.ShadowResize(true);
            yield return null;
        }
        // we call the event by script because the animation stops before the player stops moving
        _animationHandler.OnAnimationEnd();
    }

    private IEnumerator LandSequence()
    {
        // we call the event by script because we move the player before the animation starts
        _animationHandler.OnAnimationStart();
        
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
    
    public void EnablePlayerControl()
    {
        enabled = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}