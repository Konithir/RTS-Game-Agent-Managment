using UnityEngine;

public class KeybindManager : MonoBehaviour
{
    [SerializeField]
    private KeyCode _addCharacterKey;

    [SerializeField]
    private KeyCode _removeCharacterKey;

    [SerializeField]
    private KeyCode _clearSceneKey;

    public KeyCode AddCharacterKey
    {
        get { return _addCharacterKey; }
    }

    public KeyCode RemoveCharacterKey
    {
        get { return _removeCharacterKey; }
    }

    public KeyCode ClearSceneKey
    {
        get { return _clearSceneKey; }
    }
}
