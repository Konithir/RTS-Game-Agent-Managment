using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField]
    private NavigationAgent _navigationAgent;

    private const float X_MIN = -10;

    private const float X_MAX = 10;

    private const float Z_MIN = -10;

    private const float Z_MAX = 10;

    private void Start()
    {
        InitCharacter();
    }

    private void InitCharacter()
    {
        _navigationAgent.OnTargetReached.AddListener(GoToRandomPoint);
    }

    private void GoToRandomPoint()
    {
        _navigationAgent.GoTo(new Vector3(Random.Range(X_MIN, X_MAX), 0, Random.Range(Z_MIN, Z_MAX)));
    }


    public void CustomUpdate()
    {

    }
}
