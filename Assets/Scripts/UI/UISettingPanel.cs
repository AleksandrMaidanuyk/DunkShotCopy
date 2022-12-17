using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISettingPanel : MonoBehaviour
{
    [SerializeField] private Toggle _nightModeToggle;

    [SerializeField] private Toggle _soundToggle;

    private void OnEnable()
    {
        setNightModeToggle(SaveManager.getInstance().getNightMode());
        setSoundMode(SaveManager.getInstance().getSoundMode());

        _nightModeToggle.onValueChanged.AddListener(switchNightMode);
        _soundToggle.onValueChanged.AddListener(switchSoundMode);
    }
    private void OnDisable()
    {
        _nightModeToggle.onValueChanged.RemoveListener(switchNightMode);
        _soundToggle.onValueChanged.RemoveListener(switchSoundMode);
    }

    private void switchNightMode(bool value)
    {
        BackgroundChanger.onNightModeChange.Invoke(value);
    }

    private void switchSoundMode(bool value)
    {
        AudioController.onSoundModeChange(value);
    }

    private void setNightModeToggle(bool enabled)
    {
         _nightModeToggle.SetIsOnWithoutNotify(enabled);
    }

    private void setSoundMode(bool value)
    {
        _soundToggle.SetIsOnWithoutNotify(value);
    }
}
