using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using Extensions;

namespace NavigationSystem
{

    public class NavigationAgent : MonoBehaviour
    {
        [SerializeField]
        private float _speed = 5f;

        [SerializeField]
        private float _rotationSpeed = 5f;

        private Vector3? _currentTarget;

        private Vector3 _temporaryRotation;

        public UnityEvent OnTargetReached;

        private void OnDisable()
        {
            StopMovement();
        } 

        private void CheckForTargetReached()
        {
            if (_currentTarget == null)
                return;

            if (transform.position == _currentTarget)
            {
                _currentTarget = null;

                OnTargetReached?.Invoke();
            }
        }

        private float CalculateTweenMovementDuration(Vector3 destinationPoint)
        {
            return Vector3.Distance(transform.position, destinationPoint) / _speed;
        }

        private float CalculateTweenRotationDuration(Vector3 finalRotation)
        {
            return finalRotation.UnsignedDifference(transform.eulerAngles).y / _rotationSpeed;
        }

        private Vector3 GetRotationTowardsPoint(Vector3 destinationPoint)
        {
            return Quaternion.LookRotation((destinationPoint - transform.position).normalized).eulerAngles;
        }

        private void HandleMovement(Vector3 point)
        {
            transform.DOMove(point, CalculateTweenMovementDuration(point)).SetEase(Ease.Linear).OnComplete(() => CheckForTargetReached());
        }

        private void HandleRotation(Vector3 point)
        {
            _temporaryRotation = GetRotationTowardsPoint(point);
            transform.DORotate(_temporaryRotation, CalculateTweenRotationDuration(_temporaryRotation));
        }

        public void GoTo(Transform targetTransform)
        {
            _currentTarget = targetTransform.position;

            HandleMovement(targetTransform.position);

            HandleRotation(targetTransform.position);
        }

        public void GoTo(Vector3 point)
        {
            _currentTarget = point;

            HandleMovement(point);

            HandleRotation(point);
        }

        public void StopMovement()
        {
            DOTween.Kill(transform);
        }
    }

}
