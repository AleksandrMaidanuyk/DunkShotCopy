using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BasketController : MonoBehaviour
{
    [SerializeField] private WorkFovFinder _workFovFinder;

    [Space]
    [SerializeField] private Basket _firstBasket;
    [SerializeField] private Basket _secondBasket;
    private void OnEnable()
    {
        Basket.onBasketFirstHit += setNewPositionForBasket;
    }
    private void OnDisable()
    {
        Basket.onBasketFirstHit -= setNewPositionForBasket;
    }

    private void calculateTransform(Basket basket, Basket secondBasket, out Vector3 position, out float angleZ)
    {
        float minPositionX = _workFovFinder.getWorkFovMinBorder().x;
        float maxPositionX = _workFovFinder.getWorkFovMaxBorder().x;
        float centerPositionX = _workFovFinder.getWorkFovCenter().x;
        float maxIncreasePositionY = GameSetting.getInstance().getMaxHeightSpawn();
        float minIncreasePositionY = GameSetting.getInstance().getMinHeightSpawn();
        float maxRotationZ = GameSetting.getInstance().getMaxAngleRotate();

        float newPosX;

        if (basket == _secondBasket)
        {
            newPosX = Random.Range(centerPositionX, maxPositionX - basket.getBasketWidth() / 2);
        }
        else
        {
            newPosX = Random.Range(minPositionX + basket.getBasketWidth() / 2, centerPositionX - basket.getBasketWidth() / 2);
        }

        float newPosY = Random.Range(secondBasket.transform.position.y + minIncreasePositionY, secondBasket.transform.position.y + maxIncreasePositionY);

        position = new Vector3(newPosX, newPosY, 0);

        angleZ = Random.Range(-maxRotationZ, maxRotationZ);

    }
    private void setNewPositionForBasket(Basket basket)
    {
        Basket nextBasket;
        Basket secondBasket;

        if (basket == _firstBasket)
        {
            nextBasket = _secondBasket;
            secondBasket = _firstBasket;
        }
        else
        {
            nextBasket = _firstBasket;
            secondBasket = _secondBasket;
        }


        calculateTransform(nextBasket, secondBasket, out Vector3 position, out float angleZ);

        animateMoveToPos(nextBasket.gameObject.transform, position);

        setAngleZ(nextBasket.gameObject.transform, angleZ);

        nextBasket.resetBasket();
    }

    private void animateMoveToPos(Transform objTransform, Vector3 pos)
    {
        var tween = DOTween.Sequence();

        Vector3 baseScale = objTransform.localScale;

        tween.Append(objTransform.DOScale(Vector3.zero, 0f));
        tween.Append(objTransform.DOScale(baseScale, 0.3f));

        objTransform.position = pos;
      
    }

    private void setAngleZ(Transform objTransform, float angleZ)
    {
        objTransform.rotation = Quaternion.Euler(0, 0, angleZ);
    }

    private float CalculatePositionY(Transform hitBasket)
    {
        return Mathf.Clamp(hitBasket.position.y + Random.Range(
                (int)(GameSetting.getInstance().getMaxHeightSpawn() * 100),
                (int)(hitBasket.position.y + (GameSetting.getInstance().getMaxHeightSpawn() * 100))) / 100f,
            hitBasket.position.y + (int)GameSetting.getInstance().getMaxHeightSpawn(),
            hitBasket.position.y + (int)GameSetting.getInstance().getMaxHeightSpawn());
    }
}
