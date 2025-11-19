using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    private Dialogue currentDialogue;
    private int currentLineIndex;
    private Coroutine lineRoutine;

    private void Awake()
    {
        Instance = this; 
    }

    public void StartDialogue(Dialogue dialogue)
    {
        currentDialogue = dialogue;
        currentLineIndex = 0;
        dialoguePanel.SetActive(true);
        ShowLine();
    }
    private IEnumerator AutoNext(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (currentLineIndex >= currentDialogue.lines.Length - 1)
        {
            EndDialogue();
        }
        else
        {
            ShowNextLine();
        }
    }

    private void ShowLine()
    {
        if (lineRoutine != null)
            StopCoroutine(lineRoutine);

        var line = currentDialogue.lines[currentLineIndex];

        dialogueText.color = line.color; 
        dialogueText.SetText(line.text);

        lineRoutine = StartCoroutine(AutoNext(line.duration));
    }

    private void ShowNextLine()
    {
        currentLineIndex++;
        if (currentLineIndex < currentDialogue.lines.Length)
        {
            ShowLine();
        }
        else
        {
            EndDialogue();
        }
    }
    private void EndDialogue()
    {
        if (lineRoutine != null)
            StopCoroutine(lineRoutine);

        dialoguePanel.SetActive(false);
        currentDialogue = null;
    }
}
