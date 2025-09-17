using UnityEngine;

namespace Tailspin96.SmoothUILayouts
{
    public class ItemFollowClone : MonoBehaviour
    {
        public Transform cloneTransform;
        public SmoothGridLayout smoothLayout;

        private Vector3 velocity = Vector3.zero;

        private RectTransform _rectTransform;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        private void Update()
        {
            if (_rectTransform == null || cloneTransform == null) return;

            if (cloneTransform.position.sqrMagnitude < 0.01f) return;
            transform.position = Vector3.SmoothDamp(transform.position, cloneTransform.position, ref velocity, 1f / smoothLayout.MoveSpeed);
        }
    }
}
