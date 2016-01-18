using UnityEngine;
using UnityEngine.UI;

public class TweetPoster : MonoBehaviour
{
    [SerializeField]
    private InputField message;

    private DDDTwitter twitter;

    void Awake()
    {
        twitter = new DDDTwitter();
    }

    public void OnClickPost()
    {
        twitter.Post(message.text);
    }
}
