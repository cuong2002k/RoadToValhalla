using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSFXManager : PersistentSingleton<WorldSFXManager>
{

    private AudioSource _audioSource;
    [Header("Hit SFX")]
    public AudioClip[] HitSFX;

    [Header("Foot Step")]
    public AudioClip FootStepSound;

    [Header("Running sound")]
    public AudioClip RunningSound;

    [Header("Die Sound")]
    public AudioClip DeathSound;

    public GameObject soundPlay;

    protected override void Awake()
    {
        base.Awake();
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlaySoundFXAtPoint(AudioClip audioClip, float volume = 1, float pitch = 1)
    {
        PlayClipAtPoint(audioClip, volume);
        //_audioSource.PlayOneShot(audioClip, volume);
        _audioSource.pitch = pitch;
    }

    public void PlaySoundFXOneShot(AudioClip audioClip, float volume = 1, float pitch = 1)
    {
        if (_audioSource.isPlaying != audioClip)
        {
            _audioSource.PlayOneShot(audioClip, volume);
            _audioSource.pitch = pitch;
        }

    }

    public void StopSFX()
    {
        _audioSource.Stop();
    }

    public void PlayRandomHitSFX()
    {
        int random = Random.Range(0, HitSFX.Length);
        if (HitSFX.Length > 0)
        {
            AudioClip hit = HitSFX[random];
            PlaySoundFXAtPoint(hit);
        }
    }

    public void PlayWalkSFX()
    {
        PlaySoundFXOneShot(FootStepSound);
    }

    public void PlaySprintSFX()
    {
        PlaySoundFXOneShot(RunningSound);
    }

    public void PlayDeadSFX()
    {
        PlaySoundFXAtPoint(DeathSound);
    }

    public void PlayClipAtPoint(AudioClip clip, float volume)
    {
        //  Create a temporary audio source object
        GameObject tempAudioSource = new GameObject("TempAudio");

        //  Add an audio source
        AudioSource audioSource = tempAudioSource.AddComponent<AudioSource>();

        //  Add the clip to the audio source
        audioSource.clip = clip;

        //  Set the volume
        audioSource.volume = volume;

        //  Set properties so it's 2D sound
        audioSource.spatialBlend = 0.0f;

        //  Play the audio
        audioSource.Play();

        //  Set it to self destroy
        Destroy(tempAudioSource, clip.length);

    }

}
