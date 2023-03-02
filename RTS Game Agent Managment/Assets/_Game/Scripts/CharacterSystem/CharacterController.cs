using NavigationSystem;
using UnityEngine;

namespace CharacterSystem
{
    public class CharacterController : MonoBehaviour
    {
        [SerializeField]
        private NavigationAgent _navigationAgent;

        private const float X_MIN = -20;

        private const float X_MAX = 20;

        private const float Z_MIN = -20;

        private const float Z_MAX = 20;

        private void OnEnable()
        {
            InitCharacter();
            GoToRandomPoint();
        }

        private void OnDisable()
        {
            DeInitCharacter();
        }

        private void InitCharacter()
        {
            _navigationAgent.OnTargetReached.AddListener(GoToRandomPoint);
        }

        private void DeInitCharacter()
        {
            _navigationAgent.OnTargetReached.RemoveAllListeners();
        }

        private void GoToRandomPoint()
        {
            _navigationAgent.GoTo(new Vector3(Random.Range(X_MIN, X_MAX), 0, Random.Range(Z_MIN, Z_MAX)));
        }


        public void CustomUpdate()
        {

        }
    }
}
