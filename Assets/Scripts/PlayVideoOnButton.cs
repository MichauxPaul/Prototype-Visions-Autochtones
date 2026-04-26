using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class PlayVideoOnButton : MonoBehaviour
{
    public VideoPlayer myVideoPlayer;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(PlayVideo);
    }

    void PlayVideo()
    {
        myVideoPlayer.Stop();
        myVideoPlayer.Play();
    }
}