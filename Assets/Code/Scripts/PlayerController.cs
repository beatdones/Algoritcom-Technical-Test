using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Algoritcom.TechnicalTest.Character
{
    public class PlayerController : MonoBehaviour
    {
        #region VARIABLES
        private float _inputZ;
        private float _inputX;

        private float _desiredRotationSpeed = 0.08f;
        private Vector3 _desiredMoveDirection;

        private CapsuleCollider _collider;

        private Animator _animator;
        private float _speed = 1.0f;
        private float _allowPlayerRotation = 0.0f;
        private bool _isShooting;
        private bool _isCharging;
        private bool _isHaveBall;

        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private bool _grounded;

        [SerializeField] private Transform _followTarget;

        public delegate void PlayerIsInstantiate(Transform followTarget);
        public static event PlayerIsInstantiate OnPlayerIsInstantiate;

        public delegate void PlayerShoot();
        public static event PlayerShoot OnPlayerShoot;
        #endregion

        #region UNITY METHODS
        private void Start()
        {
            _animator = GetComponent<Animator>();
            _collider = GetComponent<CapsuleCollider>();

            OnPlayerIsInstantiate.Invoke(_followTarget);
        }

        private void Update()
        {
            InputMagnitude(); //Movements & rotations
            if (Input.GetButton("Fire2") && !_isShooting && _isHaveBall) ChargeShoot(true);
            if (Input.GetButton("Fire1") && _isCharging && _isHaveBall) Shoot(true);
        }

        private void FixedUpdate()
        {
            _grounded = IsGrounded();
        }
        #endregion

        #region PRIVATE METHODS

        /// <summary>
        /// Checks if an object is grounded using mesh collision rather than a raycast.
        /// It employs Physics.CheckCapsule with the collider's parameters and a specified ground layer, returning a boolean result.
        /// </summary>
        /// <returns></returns>
        private bool IsGrounded()
        {
            return Physics.CheckCapsule(_collider.bounds.center,
                                        new Vector3(_collider.bounds.center.x, _collider.bounds.center.y - (_collider.height / 2.0f), _collider.bounds.center.z),
                                        _collider.radius,
                                        _groundLayer);
        }

        /// <summary>
        /// Manages player input for movement and rotation.
        /// It sets animator parameters based on input values, calculates speed, and triggers player rotation if the speed exceeds a threshold.
        /// If the character is charging, it resets input parameters and exits the method.
        /// </summary>
        private void InputMagnitude()
        {
            if (_isCharging)
            {
                _animator.SetFloat("InputX", 0, 0.0f, Time.deltaTime);
                _animator.SetFloat("InputZ", 0, 0.0f, Time.deltaTime);
                return;
            }

            _inputX = Input.GetAxis("Horizontal");
            _inputZ = Input.GetAxis("Vertical");

            _animator.SetFloat("InputX", _inputX, 0.0f, Time.deltaTime);
            _animator.SetFloat("InputZ", _inputZ, 0.0f, Time.deltaTime);

            _speed = new Vector2(_inputX, _inputZ).sqrMagnitude;

            if (_speed > _allowPlayerRotation)
            {
                _animator.SetFloat("InputMagnitude", _speed, 0.0f, Time.deltaTime);
                PlayerRotation();
            }
            else if (_speed < _allowPlayerRotation)
            {
                _animator.SetFloat("InputMagnitude", _speed, 0.0f, Time.deltaTime);
            }
        }

        /// <summary>
        /// Aligns the player's orientation with the input direction.
        /// It utilizes the main camera's forward and right vectors, creating a desired movement direction.
        /// The character smoothly rotates using Quaternion.Slerp towards the calculated direction at a specified rotation speed.
        /// </summary>
        private void PlayerRotation()
        {
            Camera camera = Camera.main;
            Vector3 forward = camera.transform.forward;
            Vector3 right = camera.transform.right;
            forward.y = 0f;
            right.y = 0f;
            _desiredMoveDirection = forward * _inputZ + right * _inputX;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_desiredMoveDirection), _desiredRotationSpeed);
        }

        private void ChargeShoot(bool action)
        {
            _animator.SetBool("Charge", action);
            _isCharging = action;
        }

        private void Shoot(bool action)
        {
            _animator.SetBool("Shoot", action);
            _isCharging = action;
            _isHaveBall = action;
            OnPlayerShoot.Invoke();

        }
        #endregion

        #region PUBLIC METHODS
        public void TriggerAnimationEvent(AnimationEvent animationEvent)
        {
            Shoot(false);
            ChargeShoot(false);
            HaveBall(false);
        }

        public void HaveBall(bool action)
        {
            _animator.SetBool("HaveBall", action);
            _isHaveBall = action;
        }
        #endregion
    }
}

