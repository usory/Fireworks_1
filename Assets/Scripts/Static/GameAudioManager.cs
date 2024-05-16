using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GameAudioManager : SingletonMono<GameAudioManager>
{
	private AudioSource _asBGM;
	private AudioSource[] _asSFX;
	private string nowBGM = string.Empty;

	private float m_fBgmVolume = 0.5f;
	private float m_fSfxVolume = 0.5f;

	private static AudioMixer _audioMix = null;

	private int MAX_COUNT_SFX = 20;

	private static List<AudioSource> _sfxReadyAoudioSources = new List<AudioSource>();
	private static List<AudioSource> _sfxPlayAoudioSources = new List<AudioSource>();

	private static Dictionary<string, AudioMixerGroup> _dicAudioMixGroup = new Dictionary<string, AudioMixerGroup>();

	private void Awake()
	{
		Create();
	}

	public void Create()
	{
		_sfxReadyAoudioSources.Clear();
		_sfxPlayAoudioSources.Clear();

		if (null == _asBGM)
		{
			_asBGM = new AudioSource();
			_asBGM = gameObject.AddComponent<AudioSource>();
			_asBGM.playOnAwake = false;
			_asBGM.loop = true;
			_asBGM.volume = m_fBgmVolume;
		}

		for (int i = 0; i < MAX_COUNT_SFX; i++)
		{
			var oAudioGameObj = new GameObject("SFXAudio");
			oAudioGameObj.transform.SetParent(this.gameObject.transform, false);

			var audios = oAudioGameObj.AddComponent<AudioSource>();
			audios.playOnAwake = false;
			audios.loop = false;
			audios.volume = m_fSfxVolume;
			audios.rolloffMode = AudioRolloffMode.Linear;
			_sfxReadyAoudioSources.Add(audios);
		}

		_dicAudioMixGroup.Clear();
		if (_audioMix == null)
			_audioMix = Resources.Load<AudioMixer>("audios/AudioMixer");

		_audioMix.FindMatchingGroups("Master");
		_dicAudioMixGroup.Add("Master", _audioMix.FindMatchingGroups("Master")[0]);

		for (int i = 0; i < ComType.AudioMixPaths.Length; i++)
		{
			string path = ComType.AudioMixPaths[i];
			var mix = _audioMix.FindMatchingGroups(path);

			if (mix != null)
			{
				_dicAudioMixGroup.Add(path, _audioMix.FindMatchingGroups(path)[0]);
			}
		}
	}

	public static void ChangeAudioMixSnapShot(string scene)
	{
		AudioMixerSnapshot snapShot = _audioMix.FindSnapshot(scene);

		if (snapShot != null)
			_audioMix.TransitionToSnapshots(new AudioMixerSnapshot[] { snapShot }, new float[] { .5f }, Time.deltaTime * 5.0f);
		else
		{
			snapShot = _audioMix.FindSnapshot("Default");
			_audioMix.TransitionToSnapshots(new AudioMixerSnapshot[] { snapShot }, new float[] { .5f }, Time.deltaTime * 5.0f);
		}
	}

	private AudioSource GetSFXReadyAudioSource()
	{
		for (int i = 0; i < _sfxPlayAoudioSources.Count; i++)
		{
			var tmp = _sfxPlayAoudioSources[i];

			if (tmp.isPlaying == false)
			{
				_sfxReadyAoudioSources.Add(tmp);
				_sfxPlayAoudioSources.RemoveAt(i);
			}
		}

		if (_sfxReadyAoudioSources.Count > 0)
		{
			var tmp = _sfxReadyAoudioSources[0];

			_sfxReadyAoudioSources.RemoveAt(0);
			_sfxPlayAoudioSources.Add(tmp);

			return tmp;
		}
		else
		{
			var tmp = _sfxPlayAoudioSources[0];
			_sfxPlayAoudioSources.Add(tmp);
			_sfxPlayAoudioSources.RemoveAt(0);

			return tmp;
		}
	}

	public void PlaySFX(string audioName, bool loop = false, string mixKey = "Master/SFX/InGame", float pitch = 1.0f)
	{
		if (false == string.IsNullOrEmpty(audioName))
		{
			AudioClip clip = Resources.Load<AudioClip>(audioName);

			if (null == clip)
			{
				Debug.LogError("audioClip is null : PlaySFX3D(), " + audioName);
			}
			else
            {
				if (_dicAudioMixGroup.ContainsKey(mixKey) == false)
				{
					Debug.LogError($"Mixer Group Not found : {mixKey}");
					return;
				}

				if (0f >= m_fSfxVolume) return;

				AudioSource selectAudio = GetSFXReadyAudioSource();
				selectAudio.Stop();

				selectAudio.volume = Singleton.m_fSfxVolume;
				selectAudio.clip = clip;
				selectAudio.loop = loop;
				selectAudio.spatialBlend = 0.0f;
				selectAudio.outputAudioMixerGroup = _dicAudioMixGroup[mixKey];
				selectAudio.pitch = pitch;
				selectAudio.Play();
			}
		}
	}

	public void PlaySFX3D(string audioName, Vector3 vec, float minDistance = 1, float maxDistance = 30, string mixKey = "Master/SFX/InGame", bool loop = false)
	{
		if (false == string.IsNullOrEmpty(audioName))
		{
			AudioClip clip = Resources.Load<AudioClip>(audioName);

			if (null == clip)
			{
				Debug.LogError("audioClip is null : PlaySFX3D(), " + audioName);
				return;
			}
			else
            {
				if (_dicAudioMixGroup.ContainsKey(mixKey) == false)
				{
					Debug.LogError($"Mixer Group Not found : {mixKey}");
					return;
				}

				if (0f >= m_fSfxVolume) return;

				PlaySFX3D(clip, vec, minDistance, maxDistance, mixKey, loop);
			}
		}
	}

	public void PlaySFX3D(AudioClip audioClip, Vector3 vec, float minDistance = 1, float maxDistance = 30, string mixerGroup = "Master/SFX/InGame", bool loop = false)
	{
		AudioSource audioSource = GetSFXReadyAudioSource();

		audioSource.clip = audioClip;
		audioSource.volume = m_fSfxVolume;
		audioSource.loop = loop;
		audioSource.outputAudioMixerGroup = _dicAudioMixGroup[mixerGroup];
		audioSource.transform.position = vec;
		audioSource.spatialBlend = 1f;
		audioSource.minDistance = minDistance;
		audioSource.maxDistance = maxDistance;
		audioSource.Play();
	}

	public void PlayBGM(string audioName)
	{
		if (false == string.IsNullOrEmpty(audioName))
		{
			if (true == _asBGM.isPlaying)
			{
				if (true == nowBGM.Equals(audioName))
					return;
				else
				{
					Singleton.StartCoroutine(ChangeBGMRoutine(audioName));
					return;
				}
			}

			AudioClip clip = Resources.Load<AudioClip>(audioName);

			if (null != clip)
			{
				nowBGM = audioName;

				_asBGM.volume = m_fBgmVolume;
				_asBGM.loop = true;
				_asBGM.clip = clip;
				_asBGM.outputAudioMixerGroup = _dicAudioMixGroup[ComType.BGM_MIX];
				_asBGM.Play();
			}
		}
	}

	private IEnumerator ChangeBGMRoutine(string audioName)
	{
		float fTime = 1f;

		while (0f < fTime)
		{
			fTime -= Time.deltaTime;
			_asBGM.volume = fTime / 1f * Singleton.m_fBgmVolume;
			yield return new WaitForEndOfFrame();
		}

		_asBGM.volume = 0f;
		_asBGM.Stop();

		AudioClip clip = Resources.Load<AudioClip>(audioName);

		if (null != clip)
		{
			nowBGM = audioName;
			_asBGM.clip = clip;
			_asBGM.loop = true;
			_asBGM.volume = 0f;
			_asBGM.outputAudioMixerGroup = _dicAudioMixGroup[ComType.BGM_MIX];
			_asBGM.Play();
		}

		if (0f != m_fBgmVolume)
		{
			fTime = 0f;

			while (1f > fTime)
			{
				fTime += Time.deltaTime;
				_asBGM.volume = fTime * m_fBgmVolume;
				yield return new WaitForEndOfFrame();
			}

			_asBGM.volume = m_fBgmVolume;
		}
	}
}
