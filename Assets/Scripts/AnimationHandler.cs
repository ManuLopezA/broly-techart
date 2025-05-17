using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    [SerializeField] private AnimationSet animationSet;
    private Animator _animator;
    public bool IsOnAnimation { get; private set; } = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Play(BrolyState state)
    {
        var clip = animationSet.GetClip(state);
        if (clip != null)
        {
            _animator.Play(clip.name);
        }
        else
        {
            Debug.LogWarning($"not assigned clip to {state}");
        }
    }

    // We want to know when the animation starts and ends
    // because we want to prevent other animations from playing while one is already playing.
    // We call these methods from the animation event.
    // Except in the case we have an animation with movement furthermore like flying or landing,
    // in that case we mix the call of the event by script + animation event.
    public void OnAnimationStart()
    {
        IsOnAnimation = true;
    }

    public void OnAnimationEnd()
    {
        IsOnAnimation = false;
    }
}