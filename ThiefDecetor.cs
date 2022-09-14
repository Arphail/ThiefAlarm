using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefDecetor : MonoBehaviour
{
    [SerializeField] private Transform _alarm;
    [SerializeField] private Transform _thief;
    [SerializeField] private AudioSource _alarmSound;
    [SerializeField] private float _volumeChangeRate;

    private readonly int _maxVolume = 1;
    private Coroutine _currentCorutine;
    private bool _corutineIsRunning;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Thief> (out Thief thief))
        {
            _alarmSound.Play();

            if (_corutineIsRunning)
            {
                StopCoroutine(_currentCorutine);
                _currentCorutine = StartCoroutine(SmoothVolumeChange(_volumeChangeRate));
            }
            else
            {
                _currentCorutine = StartCoroutine(SmoothVolumeChange(_volumeChangeRate));
                _corutineIsRunning = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Thief>(out Thief thief))
        {
            if(_corutineIsRunning)
            {
                StopCoroutine(_currentCorutine);
                _currentCorutine = StartCoroutine(SmoothVolumeChange(-_volumeChangeRate));
            }
        }
    }

    private IEnumerator SmoothVolumeChange(float volumeChangeRate)
    {
        while(_alarmSound.volume > 0 || _alarmSound.volume < 1)
        {
            _alarmSound.volume = Mathf.MoveTowards(_alarmSound.volume, _maxVolume, volumeChangeRate * Time.deltaTime);
            yield return null;
        }
    }
}
