using NavigationSystem;
using UnityEngine;

namespace CharacterSystem
{
    public class Base_CharacterController : MonoBehaviour, ISelectable
    {
        [SerializeField]
        protected NavigationAgent _navigationAgent;

        [SerializeField]
        protected SkinnedMeshRenderer _renderer;

        protected const float X_MIN = -20;

        protected const float X_MAX = 20;

        protected const float Z_MIN = -20;

        protected const float Z_MAX = 20;

        protected void OnEnable()
        {
            InitCharacter();
            GoToRandomPoint();
        }

        protected void OnDisable()
        {
            DeInitCharacter();
        }

        protected virtual void InitCharacter()
        {
            _navigationAgent.OnTargetReached.AddListener(GoToRandomPoint);
        }

        protected void DeInitCharacter()
        {
            _navigationAgent.OnTargetReached.RemoveAllListeners();
        }

        protected void GoToRandomPoint()
        {
            _navigationAgent.GoTo(new Vector3(Random.Range(X_MIN, X_MAX), 0, Random.Range(Z_MIN, Z_MAX)));
        }


        public void CustomUpdate()
        {

        }

        public virtual void Select()
        {
            _renderer.material.color = Color.green;
        }

        public virtual void Deselect()
        {
            _renderer.material.color = Color.white;
        }
    }
}
