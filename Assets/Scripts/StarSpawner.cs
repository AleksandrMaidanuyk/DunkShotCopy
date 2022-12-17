using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _starPrefab;

    [SerializeField] private Basket _firstBasket;
    [SerializeField] private Basket _secondBasket;

    private void OnEnable()
    {
        Basket.onBasketFirstHit += trySpawnStar;
    }
    private void OnDisable()
    {
        Basket.onBasketFirstHit -= trySpawnStar;
    }

    private void trySpawnStar(Basket basket)
    {
        int chance = Random.Range(0, 10); // taken range from GameSetting

        int coef = GameSetting.getInstance().getFrequencySpawnStar();
        if (chance <= coef)
        {
            if (basket == _firstBasket)
            {
                StartCoroutine(spawnStar(_secondBasket));
            }
            else
            {
                StartCoroutine(spawnStar(_firstBasket));
            }
        }
    }

    private IEnumerator spawnStar(Basket basket)
    {
        yield return new WaitForSeconds(0.2f);
        float heightSpawn = basket.gameObject.transform.position.y + GameSetting.getInstance().getHeightStarSpawn();
        Vector3 positionSpawn = new Vector3(basket.gameObject.transform.position.x, heightSpawn, basket.gameObject.transform.position.z);

        GameObject newStar = Instantiate(_starPrefab, positionSpawn, Quaternion.identity);
    }
}
