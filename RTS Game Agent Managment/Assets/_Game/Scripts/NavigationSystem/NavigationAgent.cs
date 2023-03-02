using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using Extensions;
using Pathfinding;
using System.Collections;

namespace NavigationSystem
{

    public class NavigationAgent : MonoBehaviour
    {
        [SerializeField]
        private float _speed = 5f;

        [SerializeField]
        private float _rotationSpeed = 5f;

        [SerializeField]
        private float _acceptableDistanceToTarget = 0.5f;

        [SerializeField]
        private Seeker _seeker;

        private Vector3? _currentTarget;
        private Path _currentPath;
        private Vector3 _temporaryRotation;
        private bool _pathPointReached;
        private Coroutine movementCoroutine;
        private Vector3 _temporaryPoint;
        private NNInfo _nodeInformation;

        public UnityEvent OnTargetReached;

        private void OnDisable()
        {
            StopMovement();
        } 

        private void CheckForTargetReached()
        {
            if (_currentTarget == null)
                return;

            if (Vector3.Distance(transform.position, (Vector3)_currentTarget) < _acceptableDistanceToTarget)
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

        private void InitiatePath(Vector3 point)
        {
            _seeker.StartPath(transform.position, point, OnPathComplete);
        }

        private void OnPathComplete(Path p)
        {
            _currentPath = p;
            movementCoroutine = StartCoroutine(TraversePath());          
        }

        private IEnumerator TraversePath()
        {
            for(int i = 0; i < _currentPath.vectorPath.Count; i++)
            {
                _pathPointReached = false;
                transform.DOMove(_currentPath.vectorPath[i], CalculateTweenMovementDuration(_currentPath.vectorPath[i])).SetEase(Ease.Linear).OnComplete( () => { _pathPointReached = true; });

                HandleRotation(_currentPath.vectorPath[i]);

                while (_pathPointReached == false)
                {
                    yield return null;
                }
            }

            CheckForTargetReached();
        }

        private void HandleRotation(Vector3 point)
        {
            _temporaryRotation = GetRotationTowardsPoint(point);
            transform.DORotate(_temporaryRotation, CalculateTweenRotationDuration(_temporaryRotation));
        }

        public bool GoTo(Transform targetTransform)
        {
            _nodeInformation = AstarPath.active.GetNearest(targetTransform.position, NNConstraint.Default);

             if (!_nodeInformation.node.Walkable)
            {
                Debug.LogError("Wrong Path Node Chosen");
                return false;
            }

            _temporaryPoint = _nodeInformation.position;

            _currentTarget = _temporaryPoint;

            InitiatePath(_temporaryPoint);

            return true;
        }

        public bool GoTo(Vector3 point)
        {
            _nodeInformation = AstarPath.active.GetNearest(point, NNConstraint.Default);

            if (!_nodeInformation.node.Walkable)
            {
                Debug.LogError("Wrong Path Node Chosen");
                return false;
            }

            point = _nodeInformation.position;

            _currentTarget = point;

            InitiatePath(point);

            return true;
        }

        public void StopMovement()
        {
            DOTween.Kill(transform);

            if (movementCoroutine != null)
                StopCoroutine(movementCoroutine);

            _currentPath = null;
        }
    }

}
