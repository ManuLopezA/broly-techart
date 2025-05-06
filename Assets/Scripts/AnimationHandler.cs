using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    [SerializeField] private AnimationSet animationSet;
    private Animator _animator;

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
            Debug.LogWarning($"Clip no asignado para {state}");
        }
    }
}