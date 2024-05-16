using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioListenerController : MonoBehaviour
{
    [SerializeField]
    Transform _tPlayerCharacter;

    void Update()
    {
        transform.position = _tPlayerCharacter.position;
    }
}
