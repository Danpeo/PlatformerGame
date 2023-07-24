using UnityEngine;

namespace CameraLogic
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform _objectToFollow;
        [SerializeField] private float _distanceZ = 10f;
        [SerializeField] private float _offsetY;
        [SerializeField] private float _offsetX;
        [SerializeField] private float _deadZoneX = 0.1f;
        [SerializeField] private float _deadZoneY = 0.1f;
        [SerializeField] private float _smoothDampTime = 0.1f;
        private Vector3 _smoothDampVelocity;

        private void LateUpdate()
        {
            if (_objectToFollow == null)
                return;

            var targetPosition = new Vector3(0f, 0f, -_distanceZ) + FollowPointPosition();

            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _smoothDampVelocity, _smoothDampTime);
        }

        public void Follow(GameObject objectToFollow)
        {
            _objectToFollow = objectToFollow.transform;
        }

        private Vector3 FollowPointPosition()
        {
            var objectToFollowPosition = _objectToFollow.position;

            objectToFollowPosition.x += _offsetX;
            objectToFollowPosition.y += _offsetY;

            if (Mathf.Abs(objectToFollowPosition.x) < _deadZoneX)
            {
                objectToFollowPosition.x = 0f;
            }

            if (Mathf.Abs(objectToFollowPosition.y) < _deadZoneY)
            {
                objectToFollowPosition.y = 0f;
            }

            return objectToFollowPosition;
        }

    }
}
