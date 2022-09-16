using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothVolumeChanger : MonoBehaviour
{
    [SerializeField] private AudioSource _alarmSound;
    [SerializeField] private float _volumeChangeRate;

    private Coroutine _currentCorutine;
    private readonly int _maxVolume = 1;
    private readonly int _minVolume = 0;

    public void SmoothVolumeUp()
    {
        if (_currentCorutine != null)
            StopCoroutine(_currentCorutine);
        else
            _alarmSound.Play();

        _currentCorutine = StartCoroutine(SmoothVolumeChange(_volumeChangeRate, _maxVolume));
    }

    public void SmoothVolumeDown()
    {
        if (_currentCorutine != null)
            StopCoroutine(_currentCorutine);

        _currentCorutine = StartCoroutine(SmoothVolumeChange(-_volumeChangeRate, _minVolume));
    }

    public IEnumerator SmoothVolumeChange(float volumeChangeRate, int target)
    {
        while (_alarmSound.volume != target)
        {
            _alarmSound.volume = Mathf.MoveTowards(_alarmSound.volume, _maxVolume, volumeChangeRate * Time.deltaTime);
            yield return null;
        }
    }
}
