using UnityEngine;

namespace BrolyTechArt
{
    public class Shadow: MonoBehaviour
    {
        [SerializeField] private float shadowProportion = 0.01f;
        private Vector2 _initialShadowScale;

        private void Awake()
        {
            _initialShadowScale = transform.localScale;
        }
        
        public void ShadowResize(bool ascending)
        {
            Vector3 currentScale = transform.localScale;
            float factor = ascending ? 1 -shadowProportion : 1 + shadowProportion;
            transform.localScale = new Vector3(
                factor * currentScale.x,
                factor * currentScale.y,
                currentScale.z);
        }

        public void ResetSize()
        {
            transform.localScale = _initialShadowScale;
        }
        
    }
}