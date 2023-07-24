using CameraLogic;
using Infrastructure;
using Services;
using UnityEngine;

namespace Hero
{
    public class HeroLocomotion : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _moveSpeed = 10.0f;

        private UnityEngine.Camera _camera;
        private IInputService _inputService;

        private void Awake()
        {
            _inputService = Game.InputService;
        }

        private void Start()
        {
            _camera = Camera.main;
            CameraFollow();
        }

        private void FixedUpdate()
        {
            var movementVector = Vector3.zero;
            
            if (_inputService.Axis.sqrMagnitude > Constants.Epsilon)
            {
                movementVector = _camera.transform.TransformDirection(_inputService.Axis);
                //movementVector.y = gameObject.transform.position.y;
                movementVector.Normalize();
            }


            _rigidbody.velocity = movementVector * (_moveSpeed);
        }

        private void CameraFollow() => _camera.GetComponent<CameraFollow>().Follow(gameObject);
    }
}