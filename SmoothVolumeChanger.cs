using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothVolumeChanger : MonoBehaviour
{
    [SerializeField] private AudioSource _alarmSound;
    [SerializeField] private float _volumeChangeRate;

    private Coroutine _currentCorutine;
    private readonly int _maxVolume = 1;

    public void SmoothVolumeUp()
    {
        if (_currentCorutine != null)
        {
            StopCoroutine(_currentCorutine);
            _currentCorutine = StartCoroutine(SmoothVolumeChange(_volumeChangeRate));
        }
        else
        {
            _alarmSound.Play();
            _currentCorutine = StartCoroutine(SmoothVolumeChange(_volumeChangeRate));
        }
    }

    public void SmoothVolumeDown()
    {
        if (_currentCorutine != null)
        {
            StopCoroutine(_currentCorutine);
            _currentCorutine = StartCoroutine(SmoothVolumeChange(-_volumeChangeRate));
        }
    }

    public IEnumerator SmoothVolumeChange(float volumeChangeRate)
    {
        while (_alarmSound.volume > 0 || _alarmSound.volume < _maxVolume)
        {
            _alarmSound.volume = Mathf.MoveTowards(_alarmSound.volume, _maxVolume, volumeChangeRate * Time.deltaTime);
            yield return null;
        }
    }
}
