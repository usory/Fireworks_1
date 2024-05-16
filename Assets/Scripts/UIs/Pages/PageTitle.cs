using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public partial class PageTitle : MonoBehaviour
{
	GameManager _gameManager = null;
	GameAudioManager _audioManager = null;

	void Awake()
	{
		if ( null == _gameManager ) _gameManager = GameManager.GetInstance;
		if ( null == _audioManager ) _audioManager = GameAudioManager.Singleton;

		_audioManager.PlayBGM("Audios/BGMs/Title");
	}

	public void OnClick()
    {
		SceneManager.LoadScene(ESceneType.Animator.ToString());
	}
}
