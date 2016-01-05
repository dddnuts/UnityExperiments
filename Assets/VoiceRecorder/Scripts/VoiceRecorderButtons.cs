using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VoiceRecorderButtons : MonoBehaviour
{
    [SerializeField]
    private VoiceRecorder recorder;

    [SerializeField]
    private Button startRecordButton;

    [SerializeField]
    private Button stopRecordButton;

    [SerializeField]
    private Button playVoiceButton;

    [SerializeField]
    private Button stopVoiceButton;

    void Update()
    {
        startRecordButton.interactable = recorder.State == VoiceRecorderState.Ready;
        stopRecordButton.interactable = recorder.State == VoiceRecorderState.Recording;
        playVoiceButton.interactable = recorder.State == VoiceRecorderState.Ready;
        stopVoiceButton.interactable = recorder.State == VoiceRecorderState.Playing;
    }
}
