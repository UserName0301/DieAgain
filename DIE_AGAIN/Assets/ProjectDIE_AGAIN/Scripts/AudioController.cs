using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [Header("Default")]
    public AudioSource ausMusic;
    public AudioSource ausSound;

    [Header("Music")]
    [SerializeField][CanBeNull] AudioClip MusicBg0;
    [SerializeField][CanBeNull] AudioClip MusicBg1;
    [SerializeField][CanBeNull] AudioClip MusicBg2;


    [Header("Sound")]
    [Header("SoundBtnClick")]
    [SerializeField][CanBeNull] AudioClip SoundClickBtn;


    public static AudioController Ins;

    private void Awake()
    {
        Ins = this;
        DontDestroyOnLoad(this.gameObject);

        ProcessingSettingGlobal();
    }

    private void ProcessingSettingGlobal()
    {
        if (!PlayerPrefs.HasKey(KeySave.NameMusic))
            PlayerPrefs.SetInt(KeySave.NameMusic, 1); // 1:on - 0:off

        if(!PlayerPrefs.HasKey(KeySave.NameSound))
            PlayerPrefs.SetInt(KeySave.NameSound, 1);  //1:on - 0:off

        if(!PlayerPrefs.HasKey(KeySave.NameVibration))
            PlayerPrefs.SetInt(KeySave.NameVibration, 1);  //1:on - 0:off


        if (PlayerPrefs.GetInt(KeySave.NameMusic) == 1)
            OpenMusic();
        else
            MuteMusic();

        if (PlayerPrefs.GetInt(KeySave.NameSound) == 1)
            OpenSound();
        else
            MuteSound();
    }

    // vibration
    public void OpenVibration()
    {
        if (PlayerPrefs.GetInt(KeySave.NameVibration) == 1)
            Handheld.Vibrate();
        else
            return;
    }


    // music bg
    public void MuteMusic()
    {
        ausMusic.mute = true;
    }

    public void OpenMusic()
    {
        ausMusic.mute = false;
    }

    public void OpenMusicBg()
    {
        if (ausMusic && MusicBg0)
        {
            ausMusic.clip = MusicBg0;
            ausMusic.Play();
        }
    }

    //sound 
    public void MuteSound()
    {
        ausSound.mute = true;
    }

    public void OpenSound()
    {
        ausSound.mute = false;
    }

    //sound Btn
    public void OpenSoundClickBtn()
    {
        if (ausSound && SoundClickBtn)
        {
            ausSound.PlayOneShot(SoundClickBtn);
        }
    }
}
