using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSwitch: MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _camera;

    private void OnEnable()
    {
        GameEvent.onGameOver += stopCamera;
    }
    private void OnDisable()
    {
         GameEvent.onGameOver -= stopCamera;
    }
    private void stopCamera()
    {
        _camera.enabled = false;
    }
}
