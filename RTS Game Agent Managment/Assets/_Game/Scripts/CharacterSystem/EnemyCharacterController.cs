using NavigationSystem;
using UnityEngine;

namespace CharacterSystem
{
    public class EnemyCharacterController : Base_CharacterController
    {
        protected override void InitCharacter()
        {
            _navigationAgent.OnTargetReached.AddListener(GoToRandomPoint);
            Select();
        }

        public override void Select()
        {
            _renderer.material.color = Color.red;
        }

        public override void Deselect()
        {
            _renderer.material.color = Color.white;
        }
    }
}
