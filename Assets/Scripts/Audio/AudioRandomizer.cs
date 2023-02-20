using UnityEngine;

public class AudioRandomizer : MonoBehaviour
{

    [SerializeField] public AudioClip[] audioClips;
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayRandomClip()
    {
        int randomIndex = Random.Range(0, audioClips.Length);
        audioSource.clip = audioClips[randomIndex];
        audioSource.Play();
    }
}

