using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    [SerializeField] private CloudLayer layer;
    public CloudLayer Layer => layer;
    
    [SerializeField] private float leftLimit = -12f;
    [SerializeField] private float rightResetX = 12f;
    [SerializeField] private List<Sprite> sprites;

    private float speed;
    private CloudController cloudController;
    
    private void Awake()
    {
        cloudController = GetComponentInParent<CloudController>();
        speed = cloudController.SetSpeed(layer);
    }

    private void Update()
    {
        Move();
        CheckReset();
    }

    private void Move()
    {
        transform.position += Vector3.left * speed * cloudController.SpeedMultiplier * Time.deltaTime;
    }

    private void CheckReset()
    {
        if (transform.position.x > leftLimit) return;
        speed = cloudController.SetSpeed(layer);
        cloudController.RedrawCloud(this);
        transform.position = new Vector3(rightResetX, transform.position.y, transform.position.z);
    }
}