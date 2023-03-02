using InputSystem;
using UnityEngine;

namespace CharacterSystem
{

    public class CharacterManager : MonoBehaviour
    {
        [SerializeField]
        private CharacterController[] _characterControllers;

        [SerializeField]
        private KeybindManager _keybindManager;

        private CharacterController _temporaryCharacter;
        private Vector3 _characterStartPosition = new Vector3(0, 0, 0);
        private Vector3 _characterStartRotation = new Vector3(0, 0, 0);

        private void Update()
        {
            //Currently not used by CharacteController, may uncomment later
            //ExecuteCharacterUpdate();

            CheckForInput();
        }

        private void CheckForInput()
        {
            if (Input.GetKeyUp(_keybindManager.AddCharacterKey))
            {
                ActivateCharacter();
            }

            if (Input.GetKeyUp(_keybindManager.RemoveCharacterKey))
            {
                DeactivateCharacter();
            }

            if (Input.GetKeyUp(_keybindManager.ClearSceneKey))
            {
                DeactivateAllCharacters();
            }
        }

        private void ExecuteCharacterUpdate()
        {
            for (int i = 0; i < _characterControllers.Length; i++)
            {
                if (_characterControllers[i].gameObject.activeInHierarchy)
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
            _temporaryCharacter = FindCharacter(false);

            if (_temporaryCharacter != null)
            {
                _temporaryCharacter.transform.position = _characterStartPosition;
                _temporaryCharacter.transform.eulerAngles = _characterStartRotation;
                _temporaryCharacter.gameObject.SetActive(true);
            }

        }

        public void DeactivateCharacter()
        {
            _temporaryCharacter = null;
            _temporaryCharacter = FindCharacter(true);

            if (_temporaryCharacter != null)
            {
                _temporaryCharacter.gameObject.SetActive(false);
            }
        }

        public void DeactivateAllCharacters()
        {
            for (int i = 0; i < _characterControllers.Length; i++)
            {
                _characterControllers[i].gameObject.SetActive(false);
            }
        }
    }
}

