using UnityEngine;

public class InteractiveObject : MonoBehaviour, ISelectable
{
    [SerializeField]
    private MeshRenderer _renderer;

    public void Deselect()
    {
        _renderer.material.color = Color.white;
    }

    public void Select()
    {
        _renderer.material.color = Color.cyan;
    }

    // Start is called before the first frame update
    void Start()
    {
        Select();
    }

}
