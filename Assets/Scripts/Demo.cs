using DELTation.Persistence;
using UnityEngine;
using UnityEngine.UI;

public class Demo : MonoBehaviour
{
    [SerializeField] private InputField _text;

    private PersistentStateMonoBase<DemoModel> _modelContainer;

    private void Awake()
    {
        _modelContainer = GetComponentInChildren<PersistentStateMonoBase<DemoModel>>();
    }

    public void Read()
    {
        _text.text = _modelContainer.Model.PlayerName;
    }

    public void Write()
    {
        _modelContainer.Model.PlayerName = _text.text;
        _modelContainer.Save();
    }
}