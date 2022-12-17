using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private void Awake()
    {
        setBaseSpeedGame();
    }
    private void OnEnable()
    {
        CharacterController.onPlayerJump += increaseSpeedGame;
        Basket.onCharacterHit += setBaseSpeedGame;
    }
    private void OnDisable()
    {
        CharacterController.onPlayerJump -= increaseSpeedGame;
        Basket.onCharacterHit -= setBaseSpeedGame;
    }

    private void increaseSpeedGame(Vector2 vector2)
    {
        Time.timeScale = GameSetting.getInstance().getSpeedGame();
    }
    public static void setBaseSpeedGame()
    {
        Time.timeScale = 1;
    }

    public static void stopGame()
    {
        Time.timeScale = 0;
    }
}
