using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Menu
{
	public class SettingManager : MonoBehaviour
	{
		public AudioMixer audioMixer;

		public Slider masterSlider;
		public const string MASTER_VOLUME = "MasterVolume";

		public Slider musicSlider;
		public const string MUSIC_VOLUME = "MusicVolume";

		public Slider sfxSlider;
		public const string SFX_VOLUME = "SFXVolume";

		private void Awake()
		{
			masterSlider.onValueChanged.AddListener(SetMasterVolume);
			musicSlider.onValueChanged.AddListener(SetMusicVolume);
			sfxSlider.onValueChanged.AddListener(SetSFXVolume);
		}

		private void Start()
		{
			masterSlider.value = PlayerPrefs.GetFloat(AudioManager.MASTER_VOLUME_KEY, 1);
			musicSlider.value = PlayerPrefs.GetFloat(AudioManager.MUSIC_VOLUME_KEY, 1);
			sfxSlider.value = PlayerPrefs.GetFloat(AudioManager.SFX_VOLUME_KEY, 1);
		}

		private void OnDisable() // Loads in AudioManager.cs
		{
			PlayerPrefs.SetFloat(AudioManager.MASTER_VOLUME_KEY, masterSlider.value);
			PlayerPrefs.SetFloat(AudioManager.MUSIC_VOLUME_KEY, musicSlider.value);
			PlayerPrefs.SetFloat(AudioManager.SFX_VOLUME_KEY, sfxSlider.value);

		}

		private void SetMasterVolume(float volume) => audioMixer.SetFloat(MASTER_VOLUME, Mathf.Log10(volume) * 20);
		private void SetMusicVolume(float volume) => audioMixer.SetFloat(MUSIC_VOLUME, Mathf.Log10(volume) * 20);
		private void SetSFXVolume(float volume) => audioMixer.SetFloat(SFX_VOLUME, Mathf.Log10(volume) * 20);

		public void QuitGame()
		{
#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
#endif
			Application.Quit();
		}
	}
}
