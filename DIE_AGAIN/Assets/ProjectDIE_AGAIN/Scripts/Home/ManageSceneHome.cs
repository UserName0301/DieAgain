using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManageSceneHome : MonoBehaviour
{
    [Header("Button")]

    [SerializeField] private Button _music;
    [SerializeField] private Button _setting;
    [SerializeField] private Button _sound;
    [SerializeField] private Button _vibration;
    [SerializeField] private Button _fullScreenChangScene;
    [SerializeField] private Button _home;
    [SerializeField] private Button _restart;

    [Header("Panel")]
    [SerializeField] private GameObject _topHome;
    [SerializeField] private GameObject _topGame;
    [SerializeField] private GameObject _botHome;
    [SerializeField] private GameObject _botGame;

    [Header("ImageBtnMussic")]
    [SerializeField] private Sprite _onM;
    [SerializeField] private Sprite _offM;

    [Header("ImageBtnSound")]
    [SerializeField] private Sprite _onS;
    [SerializeField] private Sprite _offS;

    [Header("ImageBtnVibration")]
    [SerializeField] private Sprite _onV;
    [SerializeField] private Sprite _offV;

    private void Awake()
    {
        ProcessingImgSetting();
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey(KeySave.CheckLoadLevel))
        {
            if(PlayerPrefs.GetInt(KeySave.CheckLoadLevel) == 1)
            {
                ChangeGamepLay();
                PlayerPrefs.SetInt(KeySave.CheckLoadLevel, 0);
            }
        }

        //add lisener
        _setting.onClick.AddListener(AreaSetting);
        _music.onClick.AddListener(ProcessingMusic);
        _sound.onClick.AddListener(ProcessingSound);
        _vibration.onClick.AddListener(ProcessingVibration);
        _fullScreenChangScene.onClick.AddListener(ChangeGamepLay);
        _home.onClick.AddListener(ReLoadSceneGamePlay);
        _restart.onClick.AddListener(ReloadAndPlaySceneHome);
    }

    //open - close area setting
    private void AreaSetting()
    {
        AudioController.Ins.OpenSoundClickBtn();
        if (_sound.gameObject.activeSelf)
        {
            _sound.gameObject.SetActive(false);
            _music.gameObject.SetActive(false);
            _vibration.gameObject.SetActive(false);

            _fullScreenChangScene.gameObject.SetActive(true);
        }
        else
        {
            _sound.gameObject.SetActive(true);
            _music.gameObject.SetActive(true);
            _vibration.gameObject.SetActive(true);

            _fullScreenChangScene.gameObject.SetActive(false);
        }
    }

    // processing image sound music ... 
    private void ProcessingImgSetting()
    {
        switch (PlayerPrefs.GetInt(KeySave.NameMusic))
        {
            case 0:
                _music.gameObject.GetComponent<Image>().sprite = _offM; break;
            case 1:
                _music.gameObject.GetComponent<Image>().sprite = _onM; break;
        }

        switch(PlayerPrefs.GetInt(KeySave.NameSound))
        {
            case 0:
                _sound.gameObject.GetComponent<Image>().sprite = _offS; break;
            case 1:
                _sound.gameObject.GetComponent<Image>().sprite = _onS; break;
        }

        switch (PlayerPrefs.GetInt(KeySave.NameVibration))
        {
            case 0:
                _vibration.gameObject.GetComponent<Image>().sprite = _offV; break;
            case 1:
                _vibration.gameObject.GetComponent<Image>().sprite = _onV; break;
        }

    }

    // processing music
    private void ProcessingMusic()
    {
        AudioController.Ins.OpenSoundClickBtn();
        switch (PlayerPrefs.GetInt(KeySave.NameMusic))
        {    
            case 0:
                PlayerPrefs.SetInt(KeySave.NameMusic, 1);
                AudioController.Ins.OpenMusic();
                _music.gameObject.GetComponent<Image>().sprite = _onM;
                break;
            case 1:
                PlayerPrefs.SetInt(KeySave.NameMusic, 0);
                AudioController.Ins.MuteMusic();
                _music.gameObject.GetComponent<Image>().sprite = _offM;
                break;
        }
    }

    // processing sound
    private void ProcessingSound()
    {
        AudioController.Ins.OpenSoundClickBtn();
        switch (PlayerPrefs.GetInt(KeySave.NameSound))
        {
            case 0:
                PlayerPrefs.SetInt(KeySave.NameSound, 1);
                AudioController.Ins.OpenSound();
                _sound.gameObject.GetComponent<Image>().sprite = _onS;
                break;
            case 1:
                PlayerPrefs.SetInt(KeySave.NameSound, 0);
                AudioController.Ins.MuteSound();
                _sound.gameObject.GetComponent<Image>().sprite = _offS;
                break;
        }
    }

    // processing Vibration
    private void ProcessingVibration()
    {
        AudioController.Ins.OpenSoundClickBtn();
        switch (PlayerPrefs.GetInt(KeySave.NameVibration))
        {
            case 0:
                PlayerPrefs.SetInt(KeySave.NameVibration, 1);
                _vibration.gameObject.GetComponent<Image>().sprite = _onV;
                break;
            case 1:
                PlayerPrefs.SetInt(KeySave.NameVibration, 0);
                _vibration.gameObject.GetComponent<Image>().sprite = _offV;
                break;
        }
    }

    // change gameplay
    private void ChangeGamepLay()
    {
        if(PlayerPrefs.GetInt(KeySave.CheckLoadLevel) == 0)
            AudioController.Ins.OpenSoundClickBtn();

        _topHome.SetActive(false);
        _botHome.SetActive(false);
        _topGame.SetActive(true);
        _botGame.SetActive(true);
        _fullScreenChangScene.gameObject.SetActive(false);
    }

    // reload scene home
    private void ReLoadSceneGamePlay()
    {
        AudioController.Ins.OpenSoundClickBtn();
        Invoke(nameof(Reload), 0.25f);
    }
    private void Reload()
    {
        SceneManager.LoadScene(KeySave.NameSceneGamePlay);
    }

    // reload and play scene gameplay
    private void ReloadAndPlaySceneHome()
    {
        PlayerPrefs.SetInt(KeySave.CheckLoadLevel, 1);
        ReLoadSceneGamePlay();
    }
}
