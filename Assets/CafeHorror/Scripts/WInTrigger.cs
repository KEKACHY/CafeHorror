using UnityEngine;
using UnityEngine.SceneManagement;

public class WinTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerController player))
        {
            StartSceneParam.IsWin = true;
            SceneManager.LoadScene("WinOrLoseScene");
        }
    }
}
