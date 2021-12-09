using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalisationSystem
{
	public static Language CurrentLanguage { private set; get; }
	public static Dictionary<string, string> currentDictionary;

	public static void ChangeLanguage(Language language)
	{
		CSVLoader csvLoader = new CSVLoader();
		csvLoader.LoadCSV();

		if (language == Language.system)
			CurrentLanguage = Application.systemLanguage switch
			{
				SystemLanguage.English => Language.english,
				SystemLanguage.French => Language.french,
				SystemLanguage.Russian => Language.russian,
				_ => Language.english 
			};
		else
			CurrentLanguage = language;

		string langID = CurrentLanguage switch
		{
			Language.english => "en",
			Language.french => "fr",
			Language.russian => "ru",
			_ => "en"
		};

		currentDictionary = csvLoader.GetDictionaryValues(langID);
	}

	public static string GetLocalisedText(string key)
	{
		if (currentDictionary == null)
		{
			Debug.LogError("Translations not loaded!");
			return "Translations not loaded!";
		}

		currentDictionary.TryGetValue(key, out string result);
		if (result == "") result = "Translation not found!";
		return result;
	}
}
