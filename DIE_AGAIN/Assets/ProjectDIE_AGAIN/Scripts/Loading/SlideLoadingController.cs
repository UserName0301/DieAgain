using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SlideLoadingController : MonoBehaviour
{
    [SerializeField] private Image _font;

    [SerializeField] private TMP_Text _textLoading;


    float _valueSlideFont = 0;

    private void UpdateFontSlide()
    {
        _valueSlideFont += Time.deltaTime/KeySave.TimeLoadingGame;
        _font.fillAmount = _valueSlideFont;
        _textLoading.text = "LOADING " + (int)(_valueSlideFont * 100) + "%";

        if (_valueSlideFont >= 1)
            SceneManager.LoadScene(KeySave.NameSceneGamePlay);
    }

    private void Update()
    {
        UpdateFontSlide();
    }
}
