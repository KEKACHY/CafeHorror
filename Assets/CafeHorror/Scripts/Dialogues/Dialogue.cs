using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    public string speaker;
    public string text;
    public float duration = 2f;
    public Color color = Color.white;
}

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue/Dialogue")]
public class Dialogue : ScriptableObject
{
    public DialogueLine[] lines;
}