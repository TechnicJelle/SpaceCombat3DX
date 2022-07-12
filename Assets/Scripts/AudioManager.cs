using Menu;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
	public static AudioManager Instance;

	public AudioMixer audioMixer;

	public const string MASTER_VOLUME_KEY = "master_volume";
	public const string MUSIC_VOLUME_KEY = "music_volume";
	public const string SFX_VOLUME_KEY = "sfx_volume";

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;

			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}

		LoadVolume();
	}


	private void LoadVolume() // Saves in SettingsManager.cs
	{
		float masterVolume = PlayerPrefs.GetFloat(MASTER_VOLUME_KEY, 1f);
		audioMixer.SetFloat(SettingManager.MASTER_VOLUME, Mathf.Log10(masterVolume) * 20);

		float musicVolume = PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY, 1f);
		audioMixer.SetFloat(SettingManager.MUSIC_VOLUME, Mathf.Log10(musicVolume) * 20);

		float sfxVolume = PlayerPrefs.GetFloat(SFX_VOLUME_KEY, 1f);
		audioMixer.SetFloat(SettingManager.SFX_VOLUME, Mathf.Log10(sfxVolume) * 20);
	}
}
