using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderController : MonoBehaviour
{
    [SerializeField] private Transform _leftBorder;
    [SerializeField] private Transform _rightBorder;
    [SerializeField] private Transform _bottomBorder;

    [Space]

    [SerializeField] private WorkFovFinder _workFovFinder;

    private void Start()
    {
        Vector2 _screenSize = _workFovFinder.getWorkFovMaxBorder() * 2;

        setBordersPosition(_screenSize);
        setBordersScale(_screenSize);
    }

    private void setBordersPosition(Vector2 screenLengths)
    {
        _leftBorder.position = new Vector3(-screenLengths.x / 2 - _leftBorder.localScale.x / 2, 0, 0);
        _rightBorder.position = new Vector3(screenLengths.x / 2 + _rightBorder.localScale.x / 2, 0, 0);
        _bottomBorder.position = new Vector3(0, -screenLengths.y / 2 - _bottomBorder.localScale.y / 2, 0);
    }

    private void setBordersScale(Vector2 _screenSize)
    {
        float heightY = _screenSize.y;

        _leftBorder.localScale = new Vector3(_leftBorder.localScale.x, heightY, _leftBorder.localScale.y);
        _rightBorder.localScale = new Vector3(_rightBorder.localScale.x, heightY, _rightBorder.localScale.y);
        _bottomBorder.localScale = new Vector3(_bottomBorder.localScale.x, heightY, _bottomBorder.localScale.y);
    }


}
