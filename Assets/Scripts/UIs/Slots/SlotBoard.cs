using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SlotBoard : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _sName;
    [SerializeField] TextMeshProUGUI _sScore;

    public void SetName(string name)
    {
        _sName.text = name;
    }

    public void SetScore(int score)
    {
        _sScore.text = score.ToString();
    }
}
