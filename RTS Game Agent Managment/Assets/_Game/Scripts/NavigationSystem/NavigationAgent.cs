using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class NavigationAgent : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    private Vector3? _currentTarget;

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
            OnTargetReached?.Invoke();
            _currentTarget = null;
        }
    }

    private float CalculateTweenDuration(Vector3 destinationPoint)
    {
        return Vector3.Distance(transform.position, destinationPoint) * _speed;
    }

    public void GoTo(Transform transform)
    {
        _currentTarget = transform.position;

        transform.DOMove(transform.position, CalculateTweenDuration(transform.position));
    }

    public void GoTo(Vector3 point)
    {
        _currentTarget = point;

        transform.DOMove(transform.position, CalculateTweenDuration(point));
    }
}
