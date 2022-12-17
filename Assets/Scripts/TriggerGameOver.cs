using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerGameOver : MonoBehaviour
{
    private bool _isReady = false;

    private void OnEnable()
    {
        GameEvent.onGameStart += setReady;
    }

    private void OnDisable()
    {
        GameEvent.onGameStart -= setReady;
    }

    private void setReady()
    {
        _isReady = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_isReady)
        {
            if (other.gameObject.tag == TagsList.player)
            {
                GameEvent.onGameOver.Invoke();
            }
        }
    }
}
