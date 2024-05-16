using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ComType
{
	public static string[] AudioMixPaths = new string[]
	{
		"Master/SFX/UI",
		"Master/SFX/InGame",
		"Master/Music/BGM",
	};

	public static readonly string BGM_MIX = "Master/Music/BGM";
	public static readonly string UI_MIX = "Master/SFX/UI";
	public static readonly string INGAME_MIX = "Master/SFX/InGame";

	public const string DATA_PATH = "Datatables";
}
