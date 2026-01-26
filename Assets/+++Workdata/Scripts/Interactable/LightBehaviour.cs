using System;
using UnityEngine;

public class LightBehaviour : MonoBehaviour
{
    public GameObject lightObj;

    private bool _isTurnedOn;

    private void Awake()
    {
        _isTurnedOn = lightObj.activeSelf;
    }

    public void ChangeLight()
    {
        _isTurnedOn = !_isTurnedOn;
        lightObj.SetActive(_isTurnedOn);
    }
}
