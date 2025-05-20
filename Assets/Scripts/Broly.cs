using System.Collections;
using BrolyTechArt;
using UnityEngine;

[RequireComponent(typeof(AnimationHandler))]
[RequireComponent(typeof(Rigidbody2D))]
public class Broly : MonoBehaviour
{
    [SerializeField] private bool isGrounded = true;
    private AnimationHandler animationHandler;
    [SerializeField] private float speed;
    [SerializeField] private float time;
    
    [SerializeField] private Shadow shadow;
    [SerializeField] private CameraShake cameraShake;
    
    private void Awake()
    {
        animationHandler = GetComponent<AnimationHandler>();
    }

    private void Start()
    {
        DisablePlayerControl();
    }

    private void Update()
    {
        InputGetKeys();
    }

    private void InputGetKeys()
    {
        if (animationHandler.IsOnAnimation) return;
        
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
            animationHandler.Play(BrolyState.SayingNoNo);
            return;
        }

        // force explosion 
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            animationHandler.Play(BrolyState.Force01Ground);
            StartCoroutine(cameraShake.Shake());
            return;
        }

        // force concentration
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            animationHandler.Play(BrolyState.Force02Ground);
        }
    }

    private IEnumerator FlySequence()
    {
        isGrounded = false;
        animationHandler.Play(BrolyState.FlyStart);
        float elapsed = 0f;
        while (elapsed < time)
        {
            transform.position += new Vector3(0, speed, 0) * Time.deltaTime;
            elapsed += Time.deltaTime;
            shadow.ShadowResize(true);
            yield return null;
        }
        // we call the event by script because the animation stops before the player stops moving
        animationHandler.OnAnimationEnd();
    }

    private IEnumerator LandSequence()
    {
        // we call the event by script because we move the player before the animation starts
        animationHandler.OnAnimationStart();
        
        float elapsed = 0f;
        while (elapsed < time)
        {
            transform.position += new Vector3(0, -speed, 0) * Time.deltaTime;
            elapsed += Time.deltaTime;
            shadow.ShadowResize(false);
            yield return null;
        }
        
        animationHandler.Play(BrolyState.FlyEnd);
        isGrounded = true;
        shadow.ResetSize();
    }

    public void PlayAnimationSound(int position)
    {
        AudioController.Instance.PlaySound(position);
    }
    
    // this function is executed by signal emitter in Intro Cinematic Timeline
    public void EnablePlayerControl()
    {
        enabled = true;
    }

    public void DisablePlayerControl()
    {
        enabled = false;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}