using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefDecetor : MonoBehaviour
{
    [SerializeField] private Transform _thief;
    [SerializeField] private SmoothVolumeChanger _smoothVolumeChanger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Thief> (out Thief thief))
            _smoothVolumeChanger.SmoothVolumeUp();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Thief>(out Thief thief))
            _smoothVolumeChanger.SmoothVolumeDown();
    }
}
