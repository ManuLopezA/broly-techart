using UnityEngine;

public class ParallaxElement : MonoBehaviour
{
    [SerializeField] private float speed;
    private void Awake()
    {
        if (gameObject.CompareTag("sky"))
        {
            speed = transform.position.z * 0.5f;
            return;
        }
        if (gameObject.CompareTag("terrain"))
        {
            if (transform.position.z < 0)
                speed = transform.position.z * 2;
            else
                speed = transform.position.z;
        }

    }

    public void UpdatePosition(Vector2 position)
    {
        transform.position += new Vector3(0, position.y * speed, 0) * Time.deltaTime;
    }
}
