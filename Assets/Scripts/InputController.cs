using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class InputController : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public static Action<Vector3> onPlayerTap;
    public static Action<Vector3> onPlayerDrag;
    public static Action onPlayerTapUp;

    private bool _gameStart = false;
    private bool _isReady = false;

    private void OnEnable() 
    {
        Basket.onCharacterHit += enableInput;  
        Basket.onCharacterExit += disableInput;   
    }

    private void OnDisable() 
    {
        Basket.onCharacterHit -= enableInput;
        Basket.onCharacterExit -= disableInput;   
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!_gameStart)
        {
            GameEvent.onGameStart.Invoke();
            _gameStart = true;
        }

        if (_isReady)
        {
            onPlayerTap.Invoke(getMousePosition());
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_isReady)
        {
            onPlayerDrag.Invoke(getMousePosition());
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_isReady)
        {
            onPlayerTapUp.Invoke();
        }
    }

    private Vector3 getMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void enableInput()
    {
        StartCoroutine(deleyBeforeEnabled());
    }

    private void disableInput()
    {
        _isReady = false;
    }

    IEnumerator deleyBeforeEnabled()
    {
        yield return new WaitForSeconds(0.3f);
        _isReady = true;
    }

}
