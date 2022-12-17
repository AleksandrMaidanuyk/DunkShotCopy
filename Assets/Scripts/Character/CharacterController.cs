using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private TrajectoryRenderer _trajectoryRenderer;
    public static Action<Vector2> onPlayerJump;

    public static Action onPlayerHitInBorder;
    private Basket _basket;

    private Vector2 _firstTapPos;
    private Vector2 _nextDragPos;
    private Vector2 _direction;
    private float _forceJump;
    private float _maxDistanceJump;
    private float _minDistanceJump;

    private bool _canJump = false;

    private bool _isHitBorder = false;
    private bool _isHitBasketBounds = false;

    private void Start()
    {
        _forceJump = GameSetting.getInstance().getJumpForce();
        _maxDistanceJump = GameSetting.getInstance().getMaxDistanceJump();
        _minDistanceJump = GameSetting.getInstance().getMinDistanceJump();
    }
    private void OnEnable()
    {
        InputController.onPlayerTap += setFirstTapPos;
        InputController.onPlayerDrag += prepareToJump;
        InputController.onPlayerTapUp += startJump;
    }

    private void OnDisable()
    {
        InputController.onPlayerTap -= setFirstTapPos;
        InputController.onPlayerDrag -= prepareToJump;
        InputController.onPlayerTapUp -= startJump;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == TagsList.border)
        {
            if (onPlayerHitInBorder != null)
            {
                onPlayerHitInBorder.Invoke();
            }

            _isHitBorder = true;
        }
        if (other.gameObject.tag == TagsList.basketBounds)
        {
            _isHitBasketBounds = true;
        }
    }
    public void setCurrentBasket(Basket basket)
    {
        _basket = basket;
    }
    private void prepareToJump(Vector3 nextDragPos)
    {
        _nextDragPos = nextDragPos;

        _nextDragPos = checkDistance(_nextDragPos);
        _direction = _nextDragPos - _firstTapPos;

        Vector2 nextPoint = -_direction * _forceJump;

        if (Vector2.Distance(transform.localPosition, nextPoint) < _minDistanceJump)
        {
            _basket.turnDirection(_nextDragPos - _firstTapPos);
            _canJump = false;
            _trajectoryRenderer.hideLine();
            return;
        }
        else
        {
            _canJump = true;
            _trajectoryRenderer.ShowTrajectoryLine(transform.position, nextPoint);
            _basket.turnDirection(_direction);
        }
    }

    private Vector2 checkDistance(Vector2 nextDragPos)
    {
        if (Vector2.Distance(_firstTapPos, nextDragPos) > _maxDistanceJump)
        {
            float currentDistance = Vector2.Distance(_firstTapPos, nextDragPos);
            float difference = currentDistance - _maxDistanceJump;
            float rate = difference / currentDistance;
            rate = 1 - rate;

            nextDragPos = Vector2.Lerp(_firstTapPos, nextDragPos, rate);
        }
        return nextDragPos;
    }

    private void startJump()
    {
        if (_canJump)
        {
            onPlayerJump.Invoke(_direction);
        }
    }
    private void setFirstTapPos(Vector3 pos)
    {
        _firstTapPos = pos;
    }

    public void getCharacterHits(out bool isHitBasketBounds, out bool isHitBorder)
    {
        isHitBasketBounds = _isHitBasketBounds;
        isHitBorder = _isHitBorder;
    }

    public void resetCharacterHits()
    {
        _isHitBasketBounds = false;
        _isHitBorder = false;
    }
}
