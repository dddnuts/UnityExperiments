using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class TweetPoster : MonoBehaviour
{
    [SerializeField]
    private InputField message;

    [SerializeField]
    private string screenshotName = "screenshot.png";

    private DDDTwitter twitter;

    void Awake()
    {
        twitter = new DDDTwitter();
    }

    public void OnClickPost()
    {
        StartCoroutine(Post());
    }

    private IEnumerator Post()
    {
        Application.CaptureScreenshot(screenshotName);
        yield return null;

        twitter.Post(message.text, Path.Combine(Application.persistentDataPath, screenshotName));
    }
}
