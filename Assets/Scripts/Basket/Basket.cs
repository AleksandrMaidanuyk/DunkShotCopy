using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
public class Basket : MonoBehaviour
{
    [SerializeField] private Transform _pointWaiting;
    [SerializeField] private Collider2D _trigger;
    [SerializeField] private BoxCollider2D _border;

    [Space]

    [Header("Anim element")]
    [SerializeField] private Transform _stretchElement;
    [SerializeField] private Transform _ringOut;
    [SerializeField] private List<SpriteRenderer> _ringSprites;

    public static Action onCharacterHit;

    public static Action onCharacterExit;
    public static Action<Basket> onBasketFirstHit;


    private Vector3 _baseSizeRingOut;

    private bool _isReady = true;


    private bool _isFirstHit = true;


    //--------------------------------------------------------

    private void Start()
    {
        _baseSizeRingOut = _ringOut.transform.localScale;
    }

    private void OnDisable()
    {
        InputController.onPlayerTapUp -= dropCharacter;
        StopAllCoroutines();
    }

    public float getBasketWidth()
    {
        return _border.bounds.size.x;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (_isReady)
        {
            if (other.gameObject.TryGetComponent<Character>(out Character character))
            {
                character.setParentTransform(transform);
                character.moveToPoint(_pointWaiting.position);

                hideRingOut();
            }
            if (other.gameObject.TryGetComponent<CharacterController>(out CharacterController characterController))
            {
                characterController.setCurrentBasket(this);

                characterController.getCharacterHits(out bool isHitBasketBounds, out bool isHitBorder);

                catchCharacter(isHitBasketBounds, isHitBorder);

                _trigger.enabled = false;
                _border.enabled = false;

                InputController.onPlayerTapUp += dropCharacter;
            }
        }

        _isReady = false;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!_isReady)
        {
            if (other.gameObject.TryGetComponent<Character>(out Character character))
            {
                InputController.onPlayerTapUp -= dropCharacter;
                onCharacterExit.Invoke();
                _isReady = true;
                character.setParentTransform(transform.parent);
            }
        }
    }
    public void turnDirection(Vector3 direction)
    {
        transform.up = direction * -1;
    }

    public void resetBasket()
    {
        showRingOut();
        _isFirstHit = true;
    }

    private void dropCharacter()
    {
        StartCoroutine(WaitBeforeReady());
        _stretchElement.DOShakeScale(0.2f, 0.5f);
    }

    private void hideRingOut()
    {
        _ringOut.DOScaleX(_baseSizeRingOut.x * 2, 0.2f);
        _ringOut.DOScaleY(_baseSizeRingOut.x * 1.2f, 0.2f);
        foreach (SpriteRenderer sprite in _ringSprites)
        {
            Color color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0);
            sprite.DOColor(color, 0.3f);
        }
    }

    private void showRingOut()
    {
        _ringOut.transform.localScale = _baseSizeRingOut;
        foreach (SpriteRenderer sprite in _ringSprites)
        {
            Color color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1f);
            sprite.color = color;
        }
    }
    private void catchCharacter(bool isHitBasketBounds, bool isHitBorder)
    {
        Vector3 baseScale = _stretchElement.localScale;
        var tween = DOTween.Sequence();
        tween.Append(_stretchElement.DOScaleY(baseScale.y * 1.3f, 0.2f));
        tween.Append(_stretchElement.DOScaleY(baseScale.y, 0.2f));

        onCharacterHit.Invoke();

        if (_isFirstHit)
        {
            PointsCounter.onPointsCalculate.Invoke(isHitBasketBounds, isHitBorder);
            onBasketFirstHit.Invoke(this);
            _isFirstHit = false;
        }
    }
    IEnumerator WaitBeforeReady()
    {
        _trigger.enabled = true;
        _border.enabled = true;

        yield return new WaitForSeconds(1f);
        _isReady = true;
    }
}
