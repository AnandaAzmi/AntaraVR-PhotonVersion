using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public TextMeshProUGUI speakerNameText;
    public TextMeshProUGUI dialogueText;
    public AudioSource audioSource;
    public List<DialogueLine> dialogueLines;

    private int currentLineIndex = 0;

    private void Awake()
    {
        Instance = this;

        // Cari berdasarkan tag jika belum di-assign
        if (speakerNameText == null)
        {
            GameObject obj = GameObject.FindGameObjectWithTag("SpeakerNameText");
            if (obj != null) speakerNameText = obj.GetComponent<TextMeshProUGUI>();
        }

        if (dialogueText == null)
        {
            GameObject obj = GameObject.FindGameObjectWithTag("DialogueText");
            if (obj != null) dialogueText = obj.GetComponent<TextMeshProUGUI>();
        }

        if (audioSource == null)
        {
            GameObject obj = GameObject.FindGameObjectWithTag("AudioSource");
            if (obj != null) audioSource = obj.GetComponent<AudioSource>();
        }
    }

    public void ShowNextLine()
    {
        if (currentLineIndex >= dialogueLines.Count)
        {
            Debug.Log("Dialog selesai.");
            return;
        }

        var line = dialogueLines[currentLineIndex];
        speakerNameText.text = line.speakerName;
        dialogueText.text = line.dialogueText;

        audioSource.Stop();
        if (line.voiceClip != null)
        {
            audioSource.clip = line.voiceClip;
            audioSource.Play();
        }

        currentLineIndex++;
    }
}
