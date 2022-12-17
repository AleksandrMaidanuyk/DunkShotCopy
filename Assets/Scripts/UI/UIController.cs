using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private TMP_Text _pointRecord;

    [SerializeField] private TMP_Text _points;

    [SerializeField] private TMP_Text _stars;

    [Header("Panels")]
    [SerializeField] private GameObject _inGamePanel;
    [SerializeField] private GameObject _failPanel;
    [SerializeField] private GameObject _settingPanel;
    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private GameObject _pausePanel;

    private void Start()
    {
        showCountStars();
    }

    private void OnEnable()
    {
        PointsCounter.onPointAdded += updatePoints;
        PointsCounter.onStarAdded += showCountStars;
        GameEvent.onGameStart += hideMenuPanel;
        GameEvent.onGameStart += showInGamePanel;
        GameEvent.onGameOver += showFailPanel;
    }
    private void OnDisable()
    {
        PointsCounter.onPointAdded -= updatePoints;
        PointsCounter.onStarAdded -= showCountStars;
        GameEvent.onGameStart -= hideMenuPanel;
        GameEvent.onGameStart -= showInGamePanel;
        GameEvent.onGameOver -= showFailPanel;
    }
    private void updatePoints(int value)
    {
        _points.text = value.ToString();
    }

    private void showPointRecord()
    {
        _pointRecord.text = SaveManager.getInstance().getSavePoints().ToString();
    }

    private void showCountStars(int value)
    {
        _stars.text = value.ToString();
    }

    private void showCountStars()
    {
        _stars.text = SaveManager.getInstance().getSaveCountStars().ToString();
    }

    //-------------Method Panels ----------------

    public void showInGamePanel()
    {
        _inGamePanel.SetActive(true);
    }
    public void hideInGamePanel()
    {
        _inGamePanel.SetActive(false);
    }
    public void showMenuPanel()
    {
        _menuPanel.SetActive(true);
    }

    private void hideMenuPanel()
    {
        _menuPanel.SetActive(false);
    }
    private void showFailPanel()
    {
        showPointRecord();
        _failPanel.SetActive(true);
    }

    public void showSettingPanel()
    {
        _settingPanel.SetActive(true);
    }

    public void hideSettingPanel()
    {
        _settingPanel.SetActive(false);
    }

    public void showPausePanel()
    {
        TimeManager.stopGame();
        _pausePanel.SetActive(true);
    }

    public void hidePausePanel()
    {
        TimeManager.setBaseSpeedGame();
        _pausePanel.SetActive(false);
    }
}
