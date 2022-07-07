using Persistence;
using UnityEngine;
using UnityEngine.UI;

public class Demo : MonoBehaviour
{
	[SerializeField] private InputField _text = default;
	
	public void Read()
	{
		_text.text = _modelContainer.Model.PlayerName;
	}

	public void Write()
	{
		_modelContainer.Model.PlayerName = _text.text;
		_modelContainer.SaveChanges();
	}

	private void Awake()
	{
		_modelContainer = GetComponentInChildren<IModelContainer<DemoModel>>();
	}

	private IModelContainer<DemoModel> _modelContainer;
}