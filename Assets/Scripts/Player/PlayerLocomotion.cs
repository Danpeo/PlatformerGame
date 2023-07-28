using Infrastructure.Data;
using Infrastructure.Services;
using Infrastructure.Services.Input;
using Infrastructure.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class PlayerLocomotion : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _moveSpeed = 10.0f;
        [SerializeField] private SpriteRenderer _sprite;
        [SerializeField] private bool _reverseSpriteOrientation;

        private Camera _camera;
        private IInputService _inputService;

        private void Awake()
        {
            _inputService = AllServices.Container.Single<IInputService>();
        }

        private void Start()
        {
            _camera = Camera.main;
        }

        private void FixedUpdate()
        {
            var movementVector = Vector3.zero;

            if (_inputService.Axis.sqrMagnitude > Constants.Epsilon)
            {
                movementVector = _camera.transform.TransformDirection(_inputService.Axis);
                movementVector.Normalize();
                DetermineSpriteOrientation();
            }

            float xVelocity = movementVector.x * _moveSpeed;
            _rigidbody.velocity = new Vector2(xVelocity, _rigidbody.velocity.y);
        }

        private void DetermineSpriteOrientation()
        {
            if (_rigidbody.velocity.x > 0)
                _sprite.flipX = _reverseSpriteOrientation;
            else if (_rigidbody.velocity.x < 0)
                _sprite.flipX = !_reverseSpriteOrientation;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            if (GetCurrentLevel() == progress.WorldData.PositionOnLevel.Level)
            {
                Vector3Data savedPosition = progress.WorldData.PositionOnLevel.Position;

                if (savedPosition != null)
                    Warp(savedPosition);
            }
        }

        public void UpdateProgress(PlayerProgress progress) =>
            progress.WorldData.PositionOnLevel =
                new PositionOnLevel(GetCurrentLevel(), transform.position.AsVectorData());

        private void Warp(Vector3Data savedPosition)
        {
            transform.position = savedPosition.AsUnityVector();
        }

        private static string GetCurrentLevel() =>
            SceneManager.GetActiveScene().name;
    }
}