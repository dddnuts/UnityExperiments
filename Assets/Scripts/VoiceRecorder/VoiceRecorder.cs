using UnityEngine;
using System.Collections;

public class VoiceRecorder : MonoBehaviour
{
    [SerializeField]
    private int recordingTime = 60;

    [SerializeField]
    private int samplingRate = 44100;

    [SerializeField]
    private AudioSource voicePlayer;

    private AudioClip recordedClip;

    private VoiceRecorderState state;

    private string device;

    public VoiceRecorderState State
    {
        get { return state; }
    }

    void Awake()
    {
        if (Microphone.devices.Length == 0)
        {
            state = VoiceRecorderState.Unavailable;
            return;
        }

        state = VoiceRecorderState.Ready;
        device = Microphone.devices[0];
    }

    public void StartRecording()
    {
        if (state != VoiceRecorderState.Ready)
        {
            Debug.LogError("VoiceRecorder is not Ready");
            return;
        }

        recordedClip = Microphone.Start(device, false, recordingTime, samplingRate);
        if (recordedClip == null)
        {
            Debug.LogError("VoiceRecorder failed to start recording");
            state = VoiceRecorderState.Unavailable;
            return;
        }

        state = VoiceRecorderState.Recording;

        StartCoroutine(WaitForCompleteRecording());
    }

    private IEnumerator WaitForCompleteRecording()
    {
        while (Microphone.IsRecording(device))
        {
            yield return new WaitForSeconds(0.1F);
        }

        state = VoiceRecorderState.Ready;
    }

    public void StopRecording()
    {
        if (state != VoiceRecorderState.Recording)
        {
            Debug.LogError("VoiceRecorder is not Recording");
            return;
        }

        Microphone.End(device);
        state = VoiceRecorderState.Ready;
    }

    public void PlayVoice()
    {
        if (state != VoiceRecorderState.Ready)
        {
            Debug.LogError("VoiceRecorder is not Ready");
            return;
        }

        if (recordedClip == null)
        {
            Debug.LogError("Voice is not recorded");
            return;
        }

        if (voicePlayer == null)
        {
            Debug.LogError("VoicePlayer is not attached");
            return;
        }

        voicePlayer.clip = recordedClip;
        voicePlayer.Play();

        state = VoiceRecorderState.Playing;

        StartCoroutine(WaitForCompletePlayingVoice());
    }

    private IEnumerator WaitForCompletePlayingVoice()
    {
        while (voicePlayer.isPlaying)
        {
            yield return new WaitForSeconds(0.1F);
        }

        state = VoiceRecorderState.Ready;
    }

    public void StopVoice()
    {
        if (state != VoiceRecorderState.Playing)
        {
            Debug.LogError("VoiceRecorder is not Playing");
            return;
        }

        voicePlayer.Stop();
    }
}
