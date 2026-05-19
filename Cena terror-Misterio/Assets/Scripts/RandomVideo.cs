using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class RandomVideo : MonoBehaviour
{
    [SerializeField] VideoPlayer videoPlayer;

    //folder
    [SerializeField] private string glitchVids = "videos";

    [SerializeField] private float minInt;
    [SerializeField] private float maxInt;

    [SerializeField]private VideoClip[] videos;
    void Start(){
        videos = Resources.LoadAll<VideoClip>(glitchVids);
        videoPlayer.clip = videos[0];
        videoPlayer.Play();
        StartCoroutine(RandomizePlay());
    }

    IEnumerator RandomizePlay(){
        while(true){
            float wait= Random.Range(minInt, maxInt);

            PlayRandomVideo();

            yield return new WaitForSeconds(wait);

            videoPlayer?.Stop();

            yield return new WaitForSeconds(wait);
        }
    }

    public void PlayRandomVideo()
    {
        int randomIndex = Random.Range(0, videos.Length);
        videoPlayer?.Stop();
        videoPlayer.clip = videos[randomIndex];
        videoPlayer?.Play();
    }
}