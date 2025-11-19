using Cinemachine;
using UnityEngine;

public class AIEnterTrigger : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private PlayerController player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out AIController controller))
        {
            player.forceLook = true;
            player.targetPoint = controller.transform;
            audioSource.Play();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out AIController controller))
        {
            player.forceLook = false;
            player.targetPoint = null;
        }
    }
}