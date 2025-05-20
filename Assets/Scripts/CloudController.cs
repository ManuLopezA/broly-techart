using UnityEngine;

public enum CloudLayer
{
    Distant,    
    Far,
    Mid,
    Near
}
public class CloudController : MonoBehaviour
{
    [SerializeField] private float baseSpeed = 1f;
    [SerializeField] private float speedMultiplier = 0.3f;
    [SerializeField] private float randomOffset = 0.15f; 

    public float SpeedMultiplier => speedMultiplier;

    public float SetSpeed(CloudLayer layer)
    {
        float layerMultiplier = GetLayerMultiplier(layer);
        float offset = Random.Range(-randomOffset, randomOffset);
        return (baseSpeed + offset) * layerMultiplier;
    }

    private float GetLayerMultiplier(CloudLayer layer)
    {
        switch (layer)
        {
            case CloudLayer.Distant: return 0.7f;
            case CloudLayer.Far: return 1f;
            case CloudLayer.Mid: return 1.6f;
            case CloudLayer.Near: return 2.5f;
            default: return 1f;
        }
    }

    public void RedrawCloud(Cloud cloud)
    {
        CloudLayer layer = cloud.Layer;
        Color color = cloud.GetComponent<SpriteRenderer>().color;
        float newScale;
        switch (layer)
        {
            case CloudLayer.Distant:
                newScale = Random.Range(0.4f, 0.65f);
                cloud.transform.localScale = new Vector3(newScale, newScale, 1f);
                color.a = Random.Range(0.45f, 0.6f);
                break;
            case CloudLayer.Far:                
                newScale = Random.Range(0.5f, 0.75f);
                cloud.transform.localScale = new Vector3(newScale, newScale, 1f);
                color.a = Random.Range(0.50f, 0.65f);
                break;
            case CloudLayer.Mid: 
                newScale = Random.Range(0.85f, 1.35f);
                cloud.transform.localScale = new Vector3(newScale, newScale, 1f);
                color.a = Random.Range(0.65f, 0.88f);
                break;
            default: 
                newScale = Random.Range(1.35f, 1.65f);
                cloud.transform.localScale = new Vector3(newScale, newScale, 1f);
                color.a = Random.Range(0.9f, 1f);
                break;
        }
    }
}