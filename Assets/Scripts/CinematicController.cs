using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Playables;

public class CinematicController : MonoBehaviour
{
    public static CinematicController Instance { get; private set; }
    [SerializeField] private PlayableDirector[] playableDirectors;
    
    private void Awake()
    {
        if(Instance!= null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void PlayCinematic(int index)
    {
        playableDirectors[index].Play();
    }
    
}
