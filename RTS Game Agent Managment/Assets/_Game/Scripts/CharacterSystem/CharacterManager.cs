using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [SerializeField]
    private CharacterController[] _characterControllers;

    private CharacterController _temporaryCharacter;
    private Vector3 _characterStartPosition = new Vector3(0, 0, 0);
    private Vector3 _characterStartRotation = new Vector3(0, 0, 0);

    private void Update()
    {
        //ExecuteCharacterUpdate();
    }

    private void ExecuteCharacterUpdate()
    {
        for(int i = 0; i < _characterControllers.Length; i++)
        {
            if(_characterControllers[i].gameObject.activeInHierarchy)
            {
                _characterControllers[i].CustomUpdate();
            }
        }
    }

    private CharacterController FindCharacter(bool active)
    {
        for (int i = 0; i < _characterControllers.Length; i++)
        {
            if (_characterControllers[i].gameObject.activeInHierarchy == active)
            {
                return _characterControllers[i];
            }
        }

        return null;
    }

    public void ActivateCharacter()
    {
        _temporaryCharacter = null;
        _temporaryCharacter = FindCharacter(true);

        if(_temporaryCharacter != null)
        {
            _temporaryCharacter.transform.position = _characterStartPosition;
            _temporaryCharacter.transform.eulerAngles = _characterStartRotation;
            _temporaryCharacter.gameObject.SetActive(true);
        }

    }

    public void DeactivateCharacter()
    {
        _temporaryCharacter = null;
        _temporaryCharacter = FindCharacter(false);

        if (_temporaryCharacter != null)
        {
            _temporaryCharacter.gameObject.SetActive(false);
        }
    }
}
