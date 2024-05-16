using System.Collections.Generic;
using UnityEngine;

public static class ComUtil
{
	public static bool AlmostEquals(this float standard, float target, float range = float.Epsilon)
	{
		return standard >= target - range && standard <= target + range;
	}

	public static bool AlmostEquals(this Vector2 standard, Vector2 target)
	{
		return standard.x.AlmostEquals(target.x) && standard.y.AlmostEquals(target.y);
	}

	static public void DestroyChildren(this Transform tf)
	{
		while (0 != tf.childCount)
		{
			Transform tfChild = tf.GetChild(0);
			tfChild.SetParent(null);

			if (null != tfChild.gameObject)
				GameObject.Destroy(tfChild.gameObject);
		}
	}

	public static string[] SplitCsvLine(string line)
	{
		bool inQuotes = false;
		var columns = new List<string>();
		var currentColumn = "";

		foreach (char c in line)
		{
			if (c == '"')
			{
				inQuotes = !inQuotes;
			}
			else if (c == ',' && !inQuotes)
			{
				columns.Add(currentColumn.Trim('"'));
				currentColumn = "";
			}
			else
			{
				currentColumn += c;
			}
		}

		columns.Add(currentColumn.Trim('"'));

		return columns.ToArray();
	}

	public static string[] DivideString(string word)
	{
		char[] tok = new char[1] { '|' };

		string[] arrWord = word.Split(tok);

		return arrWord;
	}
}