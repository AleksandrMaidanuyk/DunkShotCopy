using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager
{
    private const string COUNT_POINTS = "COUNT_POINTS";
    private const string COUNT_STAR = "COUNT_STAR";

    private const string NIGHT_MODE = "NIGHT_MODE";
    private const string SOUND_MODE = "SOUND_MODE";

    private static SaveManager _instance;

    public static SaveManager getInstance()
    {
        if (_instance == null)
            _instance = new SaveManager();
        return _instance;
    }
    public void savePoints(int value)
    {
        PlayerPrefs.SetInt(COUNT_POINTS, value);
    }

    public int getSavePoints()
    {
        return PlayerPrefs.GetInt(COUNT_POINTS);
    }

    public void saveCountStars(int value)
    {
        PlayerPrefs.SetInt(COUNT_STAR, value);
    }

    public int getSaveCountStars()
    {
        return PlayerPrefs.GetInt(COUNT_STAR);
    }

    public void saveNightMode(bool enabled)
    {
        if (enabled)
        {
            PlayerPrefs.SetInt(NIGHT_MODE, 1);
        }
        else
        {
            PlayerPrefs.SetInt(NIGHT_MODE, 0);
        }

    }

    public bool getNightMode()
    {
        if (PlayerPrefs.GetInt(NIGHT_MODE) == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void saveSoundMode(bool enabled)
    {
        if (enabled)
        {
            PlayerPrefs.SetInt(SOUND_MODE, 1);
        }
        else
        {
            PlayerPrefs.SetInt(SOUND_MODE, 0);
        }

    }

    public bool getSoundMode()
    {
        if (PlayerPrefs.GetInt(SOUND_MODE, 1) == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
