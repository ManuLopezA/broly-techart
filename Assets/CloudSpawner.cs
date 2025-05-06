using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class CloudSpawner : MonoBehaviour
{
    [SerializeField] private List<Cloud>clouds = new List<Cloud>();
    

    private void Awake()
    {
        foreach (var cloud in clouds)
        {
            Debug.Log(cloud.name);
        }
    }

}
