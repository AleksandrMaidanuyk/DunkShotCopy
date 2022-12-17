using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetting : MonoBehaviour
{
    [Header("Selected values range was not tested")]
    [Space]

    [Header("Character")]
    [Range(1f, 20)]
    [SerializeField] private float _jumpForce = 1f;

    [Range(1, 10)]
    [SerializeField] private float _maxDistanceJump = 5f;

    [Range(0, 0.5f)]
    [SerializeField] private float _minDistanceJump = 0.1f;
    [SerializeField] private float _maxStretchBasket = 1f;

    [Space]
    [Header("Trajectory")]
    [Range(3, 8)]
    [SerializeField] private int _countPointsTrajectory = 10;

    [Space]
    [Header("Time")]
    [Range(0.5f, 2)]
    [SerializeField] private float _speedGame = 1.1f;

    [Header("Spawn basket")]
    [Range(0, 1)]
    [SerializeField] private float _minHeight = 0.1f;
    [Range(1, 4)]
    [SerializeField] private float _maxHeight = 2f;

    [Range(0, 1)]
    [SerializeField] private float _maxWidth = 1f;
    [Range(0, 45)]
    [SerializeField] private float _maxAngleRotateBasket = 1f;

    [Header("Spawn star")]
    [Range(0, 10)]
    [SerializeField] private int _frequencySpawnStar = 4;

    [Range(0, 3)]
    [SerializeField] private float _heightSpawnStar = 1f;
    
    [Space]
    [Header("Background color")]
    [SerializeField] private Color _colorLight;
    [SerializeField] private Color _colorDark;


    private static GameSetting _instance;
    private void Awake()
    {
        _instance = this;
    }

    public static GameSetting getInstance()
    {
        if (_instance == null)
            _instance = new GameSetting();
        return _instance;
    }


    public float getJumpForce()
    {
        return _jumpForce;
    }

    public int getCountPointsTrajectory()
    {
        return _countPointsTrajectory;
    }

    public float getMaxDistanceJump()
    {
        return _maxDistanceJump;
    }

    public float getMinDistanceJump()
    {
        return _minDistanceJump * 10;
    }

    public float getSpeedGame()
    {
        return _speedGame;
    }

    public float getMaxStretchBasket()
    {
        return _maxStretchBasket;
    }

    public float getMinHeightSpawn()
    {
        return _minHeight;
    }

    public float getMaxHeightSpawn()
    {
        return _maxHeight;
    }

    public float getMaxWidthSpawn()
    {
        return _maxWidth;
    }

    public float getMaxAngleRotate()
    {
        return _maxAngleRotateBasket;
    }

    public int getFrequencySpawnStar()
    {
        return _frequencySpawnStar;
    }

    public float getHeightStarSpawn()
    {
        return _heightSpawnStar;
    }
    public Color getColorLight()
    {
        return _colorLight;
    }

    public Color getColorDark()
    {
        return _colorDark;
    }
}
