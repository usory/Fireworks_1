using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Page_TextScriptableObject : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _txtNameInputTitle, _txtName;
    [SerializeField] TextMeshProUGUI _txtAgeInputTitle, _txtAge;
    [SerializeField] TextMeshProUGUI _txtHeightInputTitle, _txtHeight;
    [SerializeField] TextMeshProUGUI _txtUpdateTime;
    [SerializeField] TMP_InputField _ifName, _ifAge, _ifHeight;

    public TestScriptableObject _soRepository;

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
        _txtUpdateTime.text = string.Empty;
    }

    void SetOutputField(bool state)
    {
        _txtName.gameObject.SetActive(state);
        _txtAge.gameObject.SetActive(state);
        _txtHeight.gameObject.SetActive(state);
        _txtUpdateTime.gameObject.SetActive(state);
    }

    public void OnChangeName()
    {
        _soRepository._sName = _ifName.text;
        _ifName.text = string.Empty;
        RecordUpdateTime();
        SetOutputField(false);
    }

    public void OnChangeAge()
    {
        if ( int.TryParse(_ifAge.text, out int result) )
        {
            _soRepository._nAge = result;
            _ifAge.text = string.Empty;
            RecordUpdateTime();
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
            _soRepository._fHeight = result;
            _ifHeight.text = string.Empty;
            RecordUpdateTime();
            SetOutputField(false);
        }
        else
        {
            Debug.LogWarning("Please enter as an float");
            _ifHeight.text = string.Empty;
        }
    }

    void RecordUpdateTime()
    {
        _soRepository._dtUpdateTime = DateTime.Now;
    }

    public void OnClickButton()
    {
        SetOutputField(true);

        _txtName.text = $"Your Name is {_soRepository._sName}";
        _txtAge.text = $"your age is {_soRepository._nAge}";
        _txtHeight.text = $"Your Height is {_soRepository._fHeight}";
        _txtUpdateTime.text = $"Last update time is {_soRepository._dtUpdateTime.ToString("yyyy-MM-dd HH:mm:ss")}";
    }

    public void OnClickClear()
    {
        _soRepository.Initialize();
    }
}
