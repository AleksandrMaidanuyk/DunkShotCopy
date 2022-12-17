using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PointsCounter : MonoBehaviour
{
    private int points = 0;
    private int stars = 0;

    public static Action<bool, bool> onPointsCalculate;
    public static Action<int> onPointAdded;

    public static Action onStarAdd;
    public static Action<int> onStarAdded;

    private void Awake()
    {
        stars = SaveManager.getInstance().getSaveCountStars();
    }
    private void OnEnable()
    {
        onPointsCalculate += calculatePoints;
        onStarAdd += addStar;
    }

    private void OnDisable()
    {
        onPointsCalculate -= calculatePoints;
        onStarAdd -= addStar;
    }

    private void calculatePoints(bool isHitBasketBounds, bool isHitBorder)
    {
        int countPoints = 1;

        if(isHitBorder)
        {
            countPoints += 2;
        }
        if(isHitBasketBounds)
        {
            countPoints = 1;
        }
        else
        {
            countPoints += 2;
        }

        addPoints(countPoints);
    }

    private void addPoints(int value)
    {
        points += value;
        onPointAdded.Invoke(points);

        if (points > SaveManager.getInstance().getSavePoints())
        {
            SaveManager.getInstance().savePoints(points);
        }
    }

    private void addStar()
    {
        stars++;
        onStarAdded.Invoke(stars);
        SaveManager.getInstance().saveCountStars(stars);    
    }


}
