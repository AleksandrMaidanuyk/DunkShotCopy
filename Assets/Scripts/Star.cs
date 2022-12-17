using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Star : MonoBehaviour
{
    public static Action onStarAdd;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == TagsList.player)
        {
            PointsCounter.onStarAdd.Invoke();
            Destroy(gameObject);
        }
    }
}
