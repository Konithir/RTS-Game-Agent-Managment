using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class NavigationAgent : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    private Vector3? _currentTarget;
    private float _initialTimeToTarget;

    public UnityEvent OnTargetReached;

    private void Update()
    {
        CheckForTargetReached();
    }

    private void CheckForTargetReached()
    {
        if (_currentTarget == null)
            return;

        if(transform.position == _currentTarget)
        {
            _initialTimeToTarget = 0;
            _currentTarget = null;

            OnTargetReached?.Invoke();         
        }
    }

    private float CalculateTweenDuration(Vector3 destinationPoint)
    {
        return Vector3.Distance(transform.position, destinationPoint) / _speed;
    }

    public void GoTo(Transform targetTransform)
    {
        _currentTarget = targetTransform.position;

        _initialTimeToTarget = CalculateTweenDuration(targetTransform.position);

        transform.DOMove(targetTransform.position, _initialTimeToTarget);
    }

    public void GoTo(Vector3 point)
    {
        _currentTarget = point;

        _initialTimeToTarget = CalculateTweenDuration(point);

        transform.DOMove(point, _initialTimeToTarget);
    }
}
