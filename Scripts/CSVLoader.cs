using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class CSVLoader
{
    private TextAsset file;

    private const char lineSeparator = '\n';
    private const char surround = '"';
    private readonly string[] fieldSeparator = { "\",\"" };

    public void LoadCSV()
	{
        file = Resources.Load<TextAsset>("localisation");
	}

    /// <summary>
    /// Get localisation dictionary for one language
    /// </summary>
    /// <param name="attributeId">The id of the language (en, fr)</param>
    public Dictionary<string, string> GetDictionaryValues(string attributeId)
	{
        if (file == null)
            LoadCSV();

        Dictionary<string, string> dictionnary = new Dictionary<string, string>();

        string[] lines = file.text.Split(lineSeparator);

        int attributeIndex = -1;

        string[] headers = lines[0].Split(fieldSeparator, System.StringSplitOptions.None);
		for (int i = 0; i < headers.Length; i++)
		{
            if (headers[i].Contains(attributeId))
			{
                attributeIndex = i;
                break;
			}
		}

        Regex CSVParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");

        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];
            string[] fields = CSVParser.Split(line);

			for (int j = 0; j < fields.Length; j++)
			{
                fields[j] = fields[j].TrimStart(' ', surround);
                fields[j] = fields[j].TrimEnd(surround);
            }

            if (fields.Length > attributeIndex)
			{
                string key = fields[0];
                string value = fields[attributeIndex].TrimEnd(surround, '\n', '\r');

                if (!dictionnary.ContainsKey(key))
				{
                    dictionnary.Add(key, value);
				}
			}
        }

        return dictionnary;
    }
}
