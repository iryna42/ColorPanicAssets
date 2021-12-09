using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Dropdown))]
public class DropdownLocalisation : MonoBehaviour
{
	private TMP_Dropdown dropdown;

	[SerializeField]
	private List<string> keys;

	private void Start()
	{
		dropdown = gameObject.GetComponent<TMP_Dropdown>();
		UpdateText();
		GameManager.instance.localDropdowns.Add(this);
	}

	public void ChangeKey(List<string> newKeys)
	{
		keys = newKeys;
		UpdateText();
	}

	public void UpdateText()
	{
		dropdown.ClearOptions();
		dropdown.AddOptions(keys.Select(key => LocalisationSystem.GetLocalisedText(key)).ToList());
	}

	private void OnDestroy()
	{
		GameManager.instance.localDropdowns.Remove(this);
	}
}
