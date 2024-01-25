using Algoritcom.TechnicalTest.Ball;
using Algoritcom.TechnicalTest.Character;
using Algoritcom.TechnicalTest.Timer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Algoritcom.TechnicalTest.ThirPersonController
{
    public class ThirdPersonMovement : MonoBehaviour
    {
        [Header("REFERENCES")]
        [SerializeField] private CharacterController _controller;
        [SerializeField] private Transform _cam;
        [SerializeField] private Animator _animator;
        [SerializeField] private Transform _cameraFollowTarget;
        [SerializeField] private ThrowForceBar _throwForceBar;
        [SerializeField] private Transform _spawnPointTothrowBall;


        [SerializeField] private float _turnSmoothTime = 0.1f;

        private Vector3 _direction;
        private float _turnSmoothVelocity;
        private float _targetAngle;

        private float _speed;


        private GameObject ball;

        private bool _isHaveBall;

        // EVENTS
        public delegate void PlayerIsInstantiate(Transform _cameraFollowTarget);
        public static event PlayerIsInstantiate OnPlayerIsInstantiate;

        public delegate void PlayerShoot(Transform _throwPoint, float powerUp);
        public static event PlayerShoot OnPlayerShoot;

        public delegate void PlayerPositionEvent(GameObject player);
        public static event PlayerPositionEvent OnPlayerPositionEvent;

        public delegate void StartGameEvent();
        public static event StartGameEvent OnStartGameEvent;

        #region UNITY METHODS
        private void Start()
        {
            _cam = Camera.main.transform;

            SusbribeToEvents();

            OnPlayerIsInstantiate.Invoke(_cameraFollowTarget);
        }

        private void Update()
        {
            InputsToDirection();

            if (_direction.magnitude >= 0.1f)
            {
                Rotation();
                Movement();
            }

            InputoToThrowBall();

            ChargueThrowBar();
        }

        private void OnDestroy() => UnsusbribeToEvents();

        #endregion

        #region PRIVATE METHODS
        /// <summary>
        /// The direction of the vector is obtained through the inputs collected in _horizontal and _vertical.
        /// </summary>
        private void InputsToDirection()
        {
            float _horizontal = Input.GetAxisRaw("Horizontal");
            float _vertical = Input.GetAxisRaw("Vertical");
            _direction = new Vector3(_horizontal, 0f, _vertical).normalized;

            _animator.SetFloat("InputX", _horizontal, 0.0f, Time.deltaTime);
            _animator.SetFloat("InputZ", _vertical, 0.0f, Time.deltaTime);

            _speed = new Vector2(_horizontal, _vertical).sqrMagnitude;
        }

        /// <summary>
        /// The target angle is calculated based on the normalized direction vector.
        /// SmoothDampAngle is used to smoothly interpolate the current rotation angle to the target angle, taking into account a specified smooth time.
        /// The resulting rotation is then applied to the transform.
        /// </summary>
        private void Rotation()
        {
            _targetAngle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg + _cam.eulerAngles.y;
            float _angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetAngle, ref _turnSmoothVelocity, _turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, _angle, 0f);
        }

        /// <summary>
        /// Calculates the direction of movement based on the calculated _targetAngle.
        /// Then use Quaternion.Euler to transform the direct vector by angle. Then the direction, speed is applied to the character controller.
        /// </summary>
        private void Movement()
        {
            Vector3 _moveDir = Quaternion.Euler(0f, _targetAngle, 0f) * Vector3.forward;
            _controller.Move(_moveDir.normalized * _speed * Time.deltaTime);

            MovementAnimation();
        }

        private void ChargueThrowBar()
        {
            if (Input.GetButtonDown("Jump") && _isHaveBall)
                EnableOrDisableThrowBar(true);
        }

        private void InputoToThrowBall()
        {
            if (Input.GetButtonUp("Jump") && _isHaveBall)
                ThrowBall(true);
        }

        private void ThrowBall(bool isThrow)
        {
            _animator.SetBool("Shoot", isThrow);
            EnableOrDisableThrowBar(false);
        }

        private void EnableOrDisableThrowBar(bool state)
        {
            _throwForceBar.gameObject.SetActive(state);
        }

        private void MovementAnimation()
        {
            _animator.SetFloat("InputMagnitude", _speed, 0.0f, Time.deltaTime);
        }

        private void TouchBasketBall(GameObject ball)
        {
            this.ball = ball;
            OnStartGameEvent?.Invoke();
        }

        private void RemoveBallFromHandsTimerIsOver()
        {
            HaveBall(false);
            EnableOrDisableThrowBar(false);
        }

        private void SusbribeToEvents()
        {
            TriggerDetector.OnBallEnterEvent += TouchBasketBall;
            TimerController.OnGameOverEvent += RemoveBallFromHandsTimerIsOver;
        }

        private void UnsusbribeToEvents()
        {
            TriggerDetector.OnBallEnterEvent -= TouchBasketBall;
            TimerController.OnGameOverEvent -= RemoveBallFromHandsTimerIsOver;
        }
        #endregion

        #region PUBLIC METHODS
        /// <summary>
        /// It is activated by an animation event. Call Shoot(false) and HaveBall(false), to reset the shooting and ball possession states in the associated animation sequence.
        /// </summary>
        /// <param name="animationEvent"></param>
        public void TriggerAnimationEvent(AnimationEvent animationEvent)
        {
            ThrowBall(false);
            HaveBall(false);
        }

        /// <summary>
        /// Sets the "HaveBall" parameter in an animator and updates a boolean variable _isHaveBall based on the provided action parameter.
        /// </summary>
        /// <param name="action"></param>
        public void HaveBall(bool action)
        {
            _animator.SetBool("HaveBall", action);
            _isHaveBall = action;
        }

        /// <summary>
        /// Activates a ball, triggers events for player shoot with throw point and power, and player position event with the game object.
        /// </summary>
        public void AnimationEventThrowBall()
        {
            ball.SetActive(true);
            OnPlayerShoot.Invoke(_spawnPointTothrowBall, _throwForceBar.Power);
            OnPlayerPositionEvent?.Invoke(this.gameObject);
        }
        #endregion
    }
}

