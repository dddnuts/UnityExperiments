using UnityEngine;

public class DDDTwitter
{
    public void Post(string message)
    {
        var dddTwitter = new AndroidJavaObject("com.kakeragames.dddtwitter.DDDTwitter");
        dddTwitter.Call("post", message);
    }
}
