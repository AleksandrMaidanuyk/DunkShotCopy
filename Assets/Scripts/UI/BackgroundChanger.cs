using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BackgroundChanger : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    public static Action<bool> onNightModeChange;

    private void OnEnable()
    {
        onNightModeChange += changeColor;
    }

    private void OnDisable()
    {
        onNightModeChange -= changeColor;
    }
    private void Start()
    {
        if (SaveManager.getInstance().getNightMode())
        {
            setBlackBackground();
        }
        else
        {
            setGreyBackground();
        }

    }
    public void changeColor()
    {
        if (_camera.backgroundColor == GameSetting.getInstance().getColorDark())
        {
            setGreyBackground();
        }
        else
        {
            setBlackBackground();
        }
    }


    private void changeColor(bool enabled)
    {
        if (enabled)
        {
            setBlackBackground();
        }
        else
        {
            setGreyBackground();
        }
    }

    private void setBlackBackground()
    {
        _camera.backgroundColor = GameSetting.getInstance().getColorDark();
        SaveManager.getInstance().saveNightMode(true);
    }

    private void setGreyBackground()
    {
        _camera.backgroundColor = GameSetting.getInstance().getColorLight();
        SaveManager.getInstance().saveNightMode(false);
    }

    public Color getCurrentColor()
    {
        return _camera.backgroundColor;
    }
}
