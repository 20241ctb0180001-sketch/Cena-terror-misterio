using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class RandomVideo : MonoBehaviour
{
    [Header("Video Player")]
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] private VideoClip[] videos;

    [Header("Resource Folder")]
    [SerializeField] private string glitchVids = "videos";

    [Header("Timing")]
    [SerializeField] private float minInt;
    [SerializeField] private float maxInt;
    void Start()
    {
        videos = Resources.LoadAll<VideoClip>(glitchVids);
        StartCoroutine(RandomizePlay());
    }

    IEnumerator RandomizePlay()
    {
        while (true)
        {
            PlayRandomVideo();
            float wait = Random.Range(minInt, maxInt);
            yield return new WaitForSeconds(wait);

            videoPlayer?.Stop();
            yield return new WaitForSeconds(wait);
        }
    }

    public void PlayRandomVideo()
    {
        int randomIndex = Random.Range(0, videos.Length);

        VideoClip selectedClip = videos[randomIndex];
        videoPlayer?.Stop();
        videoPlayer.clip = selectedClip;

        videoPlayer.prepareCompleted -= OnVideoPrepared;
        videoPlayer.prepareCompleted += OnVideoPrepared;
        videoPlayer.Prepare();

    }

    void OnVideoPrepared(VideoPlayer vp)
    {
        vp.prepareCompleted -= OnVideoPrepared;

        StartCoroutine(PlayNextFrame(vp));
    }

    IEnumerator PlayNextFrame(VideoPlayer vp)
    {
        yield return null;

        vp.Play();
    }
}