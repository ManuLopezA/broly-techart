using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class AnimationEntry
{
    public BrolyState type;     
    public AnimationClip clip;          
}

[CreateAssetMenu(menuName = "Broly/Animation Set")]
public class AnimationSet : ScriptableObject
{
    public List<AnimationEntry> animationList = new List<AnimationEntry>();

    public AnimationClip GetClip(BrolyState type)
    {
        foreach (var entry in animationList)
        {
            if (entry.type == type)
                return entry.clip;
        }

        Debug.LogWarning($"No se ha encontrado animación para: {type}");
        return null;
    }
}

