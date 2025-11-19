using UnityEngine;

public class AudioListenerState : MonoBehaviour
{
    [SerializeField] private AIController aiController;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip scaryHeartClip;
    [SerializeField] private AudioClip scaryChaseClip;

    private void OnEnable()
    {
        if (aiController != null)
            aiController.OnStateChanged += PlaySound;
    }

    private void OnDisable()
    {
        if (aiController != null)
            aiController.OnStateChanged -= PlaySound;
    }

    private void PlaySound(string stateName)
    {
        if (stateName == "GrabKnife")
        {
            audioSource.Stop();
            audioSource.volume = 0.05f;
            audioSource.PlayOneShot(scaryHeartClip);
        }
        else if(stateName == "ChaseState")
        {
            audioSource.Stop();
            audioSource.volume = 0.005f;
            audioSource.Play();
        }
    }
}
