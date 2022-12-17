using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
public class Character : MonoBehaviour
{
    public static Action onCharacterReady;
    [SerializeField] private Rigidbody2D _rigidbody;

    [SerializeField] private Transform _characterBody;
    private void OnEnable()
    {
        CharacterController.onPlayerJump += enableMovement;
        CharacterController.onPlayerJump += jump;

        Basket.onCharacterHit += disableMovement;
    }
    private void OnDisable()
    {
        CharacterController.onPlayerJump -= enableMovement;
        CharacterController.onPlayerJump -= jump;

        Basket.onCharacterHit -= disableMovement;
    }
    private void jump(Vector2 velocity)
    {
        float force = GameSetting.getInstance().getJumpForce();
        _rigidbody.AddForce(-velocity * force, ForceMode2D.Impulse);
        rotate(-velocity);
    }

    private void rotate(Vector2 direction)
    {
        Vector3 endvalue = Vector3.forward *  180f * direction.x;
        _characterBody.DORotate(endvalue, 2f);
    }

    public void moveToPoint(Vector2 point)
    {
        float forceDO = (1 / GameSetting.getInstance().getJumpForce());
        transform.DOMove(point, forceDO);
    }
    private void disableMovement()
    {
        _rigidbody.bodyType = RigidbodyType2D.Static;
    }

    private void enableMovement(Vector2 direction)
    {
        _rigidbody.bodyType = RigidbodyType2D.Dynamic;
    }
    public void setParentTransform(Transform parent)
    {
        transform.parent = parent;
    }
}
