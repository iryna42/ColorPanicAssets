using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Handles UI display of an objective, in the prefab
/// </summary>
public class ObjectiveDisplay : MonoBehaviour
{
	public const float FADE_DURATION = 0.3f;
	public const float COMPLETE_DURATION = 1f;

	[SerializeField] TextMeshProUGUI descText;
	[SerializeField] TextMeshProUGUI moneyText;

	[SerializeField] CanvasGroup globalCanvas;
	[SerializeField] CanvasGroup displayCanvas;
	[SerializeField] CanvasGroup completedCanvas;

	[SerializeField] GameObject completedPS;

	[SerializeField] AudioSource completedSound;

	/// <summary>
	/// Objective that I'm displaying
	/// </summary>
	public Objective myObj;

	/// <summary>
	/// Updates the values in the UI
	/// </summary>
	public void UpdateValues(Objective obj)
	{
		myObj = obj;
		descText.text = myObj.description;
		moneyText.text = myObj.reward.ToString();
	}

	public void PlaceUI(int index)
	{
		var myRect = gameObject.GetComponent<RectTransform>();
		myRect.anchoredPosition = (myRect.sizeDelta + 5 * Vector2.up) * -(index - 1);
	}

	/// <summary>
	/// Animates the UI and updates values for new objectives
	/// </summary>
    public void ShowObj(Objective obj, int placeIndex)
	{
		gameObject.transform.localScale = 2 * Vector3.one;
		globalCanvas.alpha = 0;
		UpdateValues(obj);
		PlaceUI(placeIndex);

		LeanTween.scale(gameObject, Vector3.one, FADE_DURATION).setIgnoreTimeScale(true);
		LeanTween.alphaCanvas(globalCanvas, 1, FADE_DURATION).setIgnoreTimeScale(true);
	}

	/// <summary>
	/// Animates the UI and updates values for skipped objectives
	/// </summary>
	public void SkipObj()
	{
		LeanTween.scale(gameObject, Vector3.one * 0.5f, FADE_DURATION);
		LeanTween.alphaCanvas(globalCanvas, 0, FADE_DURATION);
		Destroy(gameObject, FADE_DURATION);
	}

	public IEnumerator Completed()
	{
		completedSound.Play();

		LeanTween.alphaCanvas(displayCanvas, 0, FADE_DURATION);
		LeanTween.alphaCanvas(completedCanvas, 1, FADE_DURATION);
		Instantiate(completedPS, gameObject.transform.position, Quaternion.identity);

		yield return new WaitForSeconds(COMPLETE_DURATION);

		LeanTween.scale(gameObject, 2 * Vector3.one, FADE_DURATION);
		LeanTween.alphaCanvas(globalCanvas, 0, FADE_DURATION);

		Destroy(gameObject, FADE_DURATION);
	}
}
