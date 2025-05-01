
using UnityEngine;

[CreateAssetMenu(fileName ="New Dialogue Line", menuName = "Dialogue/Line")]
public class DialogueLine : ScriptableObject
{
    public string speakerName; // Narator, Denis, Nenek
    [TextArea(2, 5)]
    public string dialogueText;
    public AudioClip voiceClip;
}
