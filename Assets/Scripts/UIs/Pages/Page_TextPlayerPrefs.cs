using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Page_TextPlayerPrefs : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _txtNameInputTitle, _txtName;
    [SerializeField] TextMeshProUGUI _txtAgeInputTitle, _txtAge;
    [SerializeField] TextMeshProUGUI _txtHeightInputTitle, _txtHeight;
    [SerializeField] TMP_InputField _ifName, _ifAge, _ifHeight;

    private void Awake()
    {
        InitializeTextField();
    }

    void InitializeTextField()
    {
        SetOutputField(false);

        _ifName.text = string.Empty;
        _ifAge.text = string.Empty;
        _ifHeight.text = string.Empty;
    }

    void SetOutputField(bool state)
    {
        _txtName.gameObject.SetActive(state);
        _txtAge.gameObject.SetActive(state);
        _txtHeight.gameObject.SetActive(state);
    }

    public void OnChangeName()
    {
        PlayerPrefs.SetString("name", _ifName.text);
        _ifName.text = string.Empty;
        SetOutputField(false);
    }

    public void OnChangeAge()
    {
        if ( int.TryParse(_ifAge.text, out int result) )
        {
            PlayerPrefs.SetInt("age", result);
            _ifAge.text = string.Empty;
            SetOutputField(false);
        }
        else
        {
            Debug.LogWarning("Please enter as an integer");
            _ifAge.text = string.Empty;
        }
    }

    public void OnChangeHeight()
    {
        if (float.TryParse(_ifHeight.text, out float result))
        {
            PlayerPrefs.SetFloat("height", result);
            _ifHeight.text = string.Empty;
            SetOutputField(false);
        }
        else
        {
            Debug.LogWarning("Please enter as an float");
            _ifHeight.text = string.Empty;
        }
    }

    public void OnClickButton()
    {
        SetOutputField(true);

        _txtName.text = $"Your Name is {PlayerPrefs.GetString("name")}";
        _txtAge.text = $"your age is {PlayerPrefs.GetInt("age")}";
        _txtHeight.text = $"Your Height is {PlayerPrefs.GetFloat("height")}";
    }

    public void OnClickClear()
    {
        PlayerPrefs.DeleteAll();
    }
}
