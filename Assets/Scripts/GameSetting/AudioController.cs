using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [Serializable]
    private struct Clip
    {
        public SoundType SoundType;
        public AudioClip AudioClip;
    }

    private static readonly Dictionary<SoundType, AudioClip> _clipsDictionary = new Dictionary<SoundType, AudioClip>();

    [SerializeField] private List<Clip> _clips;

    [SerializeField] private AudioSource _audioSource;

    public static Action<bool> onSoundModeChange;

    private void Awake()
    {
        if (_clipsDictionary.Count != 0)
        {
            return;
        }

        foreach (Clip clip in _clips)
        {
            _clipsDictionary.Add(clip.SoundType, clip.AudioClip);
        }
    }

    private void OnEnable()
    {
        if(SaveManager.getInstance().getSoundMode())
        {
            enableSounds();
        }

        onSoundModeChange += changeSoundMode;
    }

    private void OnDisable()
    {
        CharacterController.onPlayerJump -= playSoundShoot;
        CharacterController.onPlayerHitInBorder -= playSoundHit;
        Basket.onCharacterHit -= playSoundHitInBasket;
        GameEvent.onGameOver -= playSoundFail;
        PointsCounter.onStarAdd -= playSoundStarHit;

        onSoundModeChange -= changeSoundMode;
    }

    private void enableSounds()
    {
        CharacterController.onPlayerJump += playSoundShoot;
        CharacterController.onPlayerHitInBorder += playSoundHit;
        Basket.onCharacterHit += playSoundHitInBasket;
        GameEvent.onGameOver += playSoundFail;
        PointsCounter.onStarAdd += playSoundStarHit;

        SaveManager.getInstance().saveSoundMode(true);
    }
    private void disableSounds()
    {
        CharacterController.onPlayerJump -= playSoundShoot;
        CharacterController.onPlayerHitInBorder -= playSoundHit;
        Basket.onCharacterHit -= playSoundHitInBasket;
        GameEvent.onGameOver -= playSoundFail;
        PointsCounter.onStarAdd -= playSoundStarHit;
        
        SaveManager.getInstance().saveSoundMode(false);
    }

    private void changeSoundMode(bool enabled)
    {
        if(enabled)
        {
            enableSounds();    
        }
        else
        {
            disableSounds();
        }
    }

    private void PlaySound(SoundType soundType)
    {
        _audioSource.clip = _clipsDictionary[soundType];
        _audioSource.Play();
    }

    private void playSoundHit()
    {
        PlaySound(SoundType.hit);
    }
    private void playSoundFail()
    {
        PlaySound(SoundType.fail);
    }
    private void playSoundHitInBasket()
    {
        PlaySound(SoundType.hitInBasket);
    }
    private void playSoundShoot(Vector2 vector2)
    {
        PlaySound(SoundType.shoot);
    }
    private void playSoundStarHit()
    {
        PlaySound(SoundType.starHit);
    }
}
