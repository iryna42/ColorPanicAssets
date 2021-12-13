using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ThemeBtn : MonoBehaviour
{
    public Color color;

	private bool wasUnlocked = true;

	[SerializeField] GameObject lockIcon;
	[SerializeField] GameObject ps;

	private bool IsUnlocked
	{
		get
		{
			if (GameManager.instance.saveData == null)
				return false;

			return GameManager.instance.saveData.unlockedColors.Any(c => Utility.CompareColors(c, color));
		}
	}

	private void Start()
	{
		GameManager.instance.allColors.Add(color);
		GameManager.instance.colorBtns.Add(this);

		UpdateUI();
	}

	public void UpdateUI()
	{
		if (IsUnlocked)
		{
			if (!wasUnlocked)
			{
				var main = ps.GetComponent<ParticleSystem>().main;
				main.startColor = color;
				ps.GetComponent<ParticleSystem>().Play();
			}

			wasUnlocked = true;

			gameObject.transform.GetChild(0).GetComponent<Image>().color = color;
			lockIcon.SetActive(false);
		}
		else
		{
			wasUnlocked = false;

			float[] hsv = new float[3];
			Color.RGBToHSV(color, out hsv[0], out hsv[1], out hsv[2]);
			Color darkerColor = Color.HSVToRGB(hsv[0], 0.5f, 0.3f);
			gameObject.transform.GetChild(0).GetComponent<Image>().color = darkerColor;
			lockIcon.SetActive(true);
		}
	}

	public void click()
	{
		if (IsUnlocked)
		{
			GameManager.instance.saveData.colors[GameManager.instance.currentColorSelected] = color;
			SaveData.Save(GameManager.instance.saveData);
			GameManager.instance.UpdateTheme();
		}
	}
}
