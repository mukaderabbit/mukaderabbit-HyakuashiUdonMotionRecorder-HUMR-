#if UDONSHARP
using UdonSharp;
using UnityEngine;

namespace Humr
{
    public class SmoothDamp : UdonSharpBehaviour
    {
        [SerializeField] private Transform targetTransform;
        [SerializeField] private float smoothTime = 0.3f;
        [SerializeField] private float smoothMaxDist = 0.2f;
        [SerializeField] private float smoothDistMult = 0.1f;
        private Vector3 _velocity = Vector3.zero;

        [SerializeField] private float rotSmoothTime = 0.2f;
        private Vector3 _rotVelocity = Vector3.zero;

        private void Update()
        {
            if (targetTransform == null) return;

            var distance = Vector3.Distance(transform.position, targetTransform.position);
            var dynamicSmoothTime = Mathf.Lerp(smoothTime, smoothTime * smoothDistMult, distance / smoothMaxDist);
        
            transform.position = Vector3.SmoothDamp(
                transform.position, targetTransform.position, ref _velocity, dynamicSmoothTime);

            var currentEuler = transform.rotation.eulerAngles;
            var targetEuler = targetTransform.rotation.eulerAngles;

            var x = Mathf.SmoothDampAngle(currentEuler.x, targetEuler.x, ref _rotVelocity.x, rotSmoothTime);
            var y = Mathf.SmoothDampAngle(currentEuler.y, targetEuler.y, ref _rotVelocity.y, rotSmoothTime);
            var z = Mathf.SmoothDampAngle(currentEuler.z, targetEuler.z, ref _rotVelocity.z, rotSmoothTime);

            transform.eulerAngles = new Vector3(x, y, z);

            var targetScale = targetTransform.lossyScale;
            if (transform.parent == null)
            {
                transform.localScale = targetScale;
            }
            else
            {
                var parentScale = transform.parent.lossyScale;
            
                transform.localScale = new Vector3(
                    targetScale.x / parentScale.x,
                    targetScale.y / parentScale.y,
                    targetScale.z / parentScale.z);
            }
        }
    }
}
#endif