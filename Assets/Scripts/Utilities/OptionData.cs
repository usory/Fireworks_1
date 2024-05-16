using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using TMPro;

[Serializable]
public class OptionData
{
	public OptionSound m_Audio = new OptionSound();
}

[Serializable]
public class OptionSound
{
	public float fBGM_Vol = 0.5f;
	public float fSFX_Vol = 0.5f;
}