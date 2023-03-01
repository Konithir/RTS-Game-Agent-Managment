using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [SerializeField]
    private CharacterController[] _characterControllers;

    private void Update()
    {
        ExecuteCharacterUpdate();
    }

    private void ExecuteCharacterUpdate()
    {
        for(int i = 0; i < _characterControllers.Length; i++)
        {
            _characterControllers[i].CustomUpdate();
        }
    }
}
