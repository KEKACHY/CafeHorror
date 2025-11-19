using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLoseUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI mainText;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        ChangeText(StartSceneParam.IsWin);
    }

    private void ChangeText(bool value)
    {
        if(value)
            mainText.SetText("Вы сбежали!");
        else
            mainText.SetText("Вас убили!");
    }

    public void TryAgain()
    {
        SceneManager.LoadScene("CafeHorrorScene");
    }
}
