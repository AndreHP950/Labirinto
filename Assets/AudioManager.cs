using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip pointSound;
    public AudioClip phaseTransitionSound;
    public AudioSource audioSource;

    public void PlayPointSound()
    {
        audioSource.PlayOneShot(pointSound);
    }

    public void PlayPhaseTransitionSound()
    {
        audioSource.PlayOneShot(phaseTransitionSound);
    }
}
