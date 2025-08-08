using UnityEngine;
using UnityEngine.UI;
using Audio;

public class TitleScreenUI : MonoBehaviour
{
    [Header("🎵 Audio")]
    [SerializeField] private AudioCue startCue = AudioCue.ButtonClick;
    [SerializeField] private AudioCue quitCue = AudioCue.ButtonClick;
    [SerializeField] private AudioSource buttonAudio;

    [Header("🖱️ Buttons")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button quitButton;

    private void Awake()
    {
        if (startButton != null)
            startButton.onClick.AddListener(() => OnStartClicked());

        if (quitButton != null)
            quitButton.onClick.AddListener(() => OnQuitClicked());
    }

    private void OnStartClicked()
    {
        PlayAudio(startCue);
        // Add your start game logic here
        Debug.Log("[TitleScreenUI] ▶️ Start button clicked.");
    }

    private void OnQuitClicked()
    {
        PlayAudio(quitCue);
        // Add your quit logic here
        Debug.Log("[TitleScreenUI] ❌ Quit button clicked.");
    }

    private void PlayAudio(AudioCue cue)
    {
        if (buttonAudio != null && cue != AudioCue.ButtonClick) // fallback check
        {
            buttonAudio.PlayOneShot(GetClip(cue));
        }
    }

    private AudioClip GetClip(AudioCue cue)
    {
        // Replace this with your actual cue-to-clip logic
        // For now, return a placeholder or null
        return null;
    }
}
