using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class LocalisedText : MonoBehaviour
{
    private TextMeshProUGUI textField;

	[SerializeField]
	private string key;

	private void Start()
	{
		textField = gameObject.GetComponent<TextMeshProUGUI>();
		UpdateText();
		GameManager.instance.localTexts.Add(this);
	}

	public void ChangeKey(string newKey)
	{
		key = newKey;
		UpdateText();
	}

	public void UpdateText()
	{
		textField.text = LocalisationSystem.GetLocalisedText(key);
	}

	private void OnDestroy()
	{
		GameManager.instance.localTexts.Remove(this);
	}
}
