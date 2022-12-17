using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkFovFinder : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    private float _maxWidth;
    private float _maxHeight;
    public int MaxWidth
    {
        get
        {
            return _mainCamera.pixelWidth;
        }
    }

    public int MaxHeight
    {
        get
        {
            return _mainCamera.pixelHeight;
        }
    }

    public Vector2 getWorkFovMaxBorder()
    {
        Vector3 maxPos = new Vector3(MaxWidth, MaxHeight, _mainCamera.transform.position.z);
        maxPos = Camera.main.ScreenToWorldPoint(maxPos);

        return new Vector2(maxPos.x, maxPos.y);
    }

    public Vector2 getWorkFovMinBorder()
    {
        Vector3 minPos = new Vector3(0, 0, _mainCamera.transform.position.z);
        minPos = Camera.main.ScreenToWorldPoint(minPos);

        return new Vector2(minPos.x, minPos.y);
    }

    public Vector2 getWorkFovCenter()
    {
        Vector3 centerPos = new Vector3(MaxWidth / 2, MaxHeight / 2, _mainCamera.transform.position.z);
        centerPos = Camera.main.ScreenToWorldPoint(centerPos);

        return new Vector2(centerPos.x, centerPos.y);

    }

    

}
