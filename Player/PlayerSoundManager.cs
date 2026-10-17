using UnityEngine;
using UnityEngine.Audio;

public class PlayerSoundManager : MonoBehaviour
{
    public static PlayerSoundManager Instance { get; private set; }

    [SerializeField] private AudioSource bgmAudioSource;
    [SerializeField] private AudioSource seAudioSource;
    [SerializeField] private AudioSource playerMoveAudioSource;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void PlayBGM(AudioClip clip)
    {
        if (clip == null)
        {
            return;
        }

        bgmAudioSource.Play();
    }

    public void PlaySE(AudioClip clip)
    {
        seAudioSource.PlayOneShot(clip);
    }

    public void PlayerMoveSound()
    {
        if (playerMoveAudioSource.isPlaying) return;

        playerMoveAudioSource.Play();
    }
    public void StopPlayerMoveSound()
    {
        playerMoveAudioSource.Stop();
    }
}
