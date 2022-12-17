using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryRenderer : MonoBehaviour
{
    [SerializeField] private List<SpriteRenderer> _spritesRenderer;

    [Space]

    [SerializeField] private WorkFovFinder _workFovFinder;
    [SerializeField] private int timeOfFlight = 2;

    private int _countPoints = 1;

    private void OnEnable()
    {
        InputController.onPlayerTapUp += hideLine;
    }

    private void OnDisable()
    {
        InputController.onPlayerTapUp -= hideLine;
    }
    private void Start()
    {
        _countPoints = GameSetting.getInstance().getCountPointsTrajectory();
    }

    public void ShowTrajectoryLine(Vector2 firstPoint, Vector2 nextPoint)
    {
        float timeStep = (float)timeOfFlight / (float)_countPoints;

        Vector3[] lineRendererPoints = CalculateTrajectoryLine(firstPoint, nextPoint, timeStep);
        //_spritesRenderer.set = _countPoints;
        showLine();
        for(int i = 0; i < lineRendererPoints.Length; i++)
        {
            _spritesRenderer[i].transform.position = lineRendererPoints[i];
        }
    }
    public void hideLine()
    {
        Color shadow = new Color(_spritesRenderer[0].color.a, _spritesRenderer[0].color.g, _spritesRenderer[0].color.b, 0);
        for(int i = 0; i < _spritesRenderer.Count; i++)
        {
            _spritesRenderer[i].color = shadow;
        }
    }

    public void showLine()
    {
        Color shadow = new Color(_spritesRenderer[0].color.a, _spritesRenderer[0].color.g, _spritesRenderer[0].color.b, 1);
        for(int i = 0; i < _spritesRenderer.Count; i++)
        {
            _spritesRenderer[i].color = shadow;
        }
    }
    private Vector3[] CalculateTrajectoryLine(Vector2 firstPoint, Vector2 velocity, float timeStep)
    {
        Vector3[] lineRendererPoints = new Vector3[_countPoints];
        Vector3[] TapPoints = new Vector3[_countPoints];

        lineRendererPoints[0] = firstPoint;
        TapPoints[0] = firstPoint;

        for (int i = 1; i < _countPoints; i++)
        {
            float timeOffset = timeStep * i;

            Vector2 progressGravity = velocity * timeOffset;
            Vector2 gravityOffset = Vector2.up * -0.6f * Physics2D.gravity.y * timeOffset * timeOffset;
            Vector2 newPosition = firstPoint + progressGravity - gravityOffset;

            TapPoints[i] = newPosition;

            Vector2 position = CheckPositionPointInWorkSpace(newPosition, TapPoints[i - 1], lineRendererPoints[i - 1]);
            lineRendererPoints[i] = position;
        }

        return lineRendererPoints;
    }

    private Vector2 CheckPositionPointInWorkSpace(Vector2 pos, Vector2 lastTapPoint, Vector2 lastPointDraw)
    {
        float maxPos = _workFovFinder.getWorkFovMaxBorder().x;
        float minPos = _workFovFinder.getWorkFovMinBorder().x;

        if (pos.x >= maxPos || pos.x <= minPos)
        {
            float difference = pos.x - lastTapPoint.x;
            float newPosX = lastPointDraw.x - difference;

            pos.x = newPosX;

            return pos;
        }
        else
        {
            return pos;
        }
    }
}
