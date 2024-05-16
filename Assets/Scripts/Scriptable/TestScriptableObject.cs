using System;
using UnityEngine;

[CreateAssetMenu]
public class TestScriptableObject : ScriptableObject
{
    public string _sName;
    public int _nAge;
    public float _fHeight;
    public DateTime _dtUpdateTime;

    public void Initialize()
    {
        _sName = default;
        _nAge = default;
        _fHeight = default;
        _dtUpdateTime = default;
    }
}
