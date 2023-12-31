using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public SoundList library;
    public AudioSource soundFXSource;
    public AudioSource musicSource;

    public void PlayExactSounds(string soundName, string clipName, float volume)
    {
        soundFXSource.clip = library.GetExactClip(soundName, clipName);
        soundFXSource.volume = volume;
        soundFXSource.loop = false;
        soundFXSource.Play();
    }


    public void PlaySound(string soundName, float volume)    {
        soundFXSource.clip = library.GetClipFromName(soundName);
        soundFXSource.volume = volume;
        soundFXSource.loop = false;
        soundFXSource.Play();
        //soundFXSource.PlayOneShot(library.GetClipFromName(soundName), volume);
    }

    public void PlayMusic(string soundName, float volume)
    {
        musicSource.clip = library.GetClipFromName(soundName);
        musicSource.loop = true;
        musicSource.volume = volume;
        musicSource.Play();
    }

    public void Start()
    {
        if(GameManager.Instance.getCurrentState() == GameManager.GameStates.MainMenu)
        {
            PlayMusic("MainMenuMusic", 0.3f);
        }
        else if (GameManager.Instance.getCurrentState() == GameManager.GameStates.MultiplicationPuzzle)
        {
            PlayMusic("MultiplicationPuzzleMusic", 0.3f);
        }
        else if (GameManager.Instance.getCurrentState() == GameManager.GameStates.MultiplicationFun)
        {
            PlayMusic("MultiplicationFunMusic", 0.3f);
        }
        else if (GameManager.Instance.getCurrentState() == GameManager.GameStates.MultiplicationQuiz)
        {
            PlayMusic("MultiplicationQuizMusic", 0.3f);
        }
        else if (GameManager.Instance.getCurrentState() == GameManager.GameStates.MultiplicationPractice)
        {
            PlayMusic("MultiplicationPracticeMusic", 0.3f);
        }
    }

    public IEnumerator Transition()
    {
        yield return new WaitForSeconds(12.0f);
        GameManager.Instance.AudioManager.musicSource.Play();
    }

}
