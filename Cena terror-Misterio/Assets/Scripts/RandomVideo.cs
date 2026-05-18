using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class RandomVideo : MonoBehaviour
{
    [SerializeField] VideoPlayer videoPlayer;
    public string glitchVids = "videos";
    [SerializeField]float minInt;
    [SerializeField]float maxInt;
    void Start(){
        StartCoroutine(Randomizeplay());
    }

    IEnumerator Randomizeplay(){
        while(true){
            float wait= Random.Range(minInt, maxInt);
            PlayRandomVideo();
            videoPlayer.Play();
            yield return new WaitForSeconds(wait);
            videoPlayer.Stop();
            yield return new WaitForSeconds(wait);
        }
    }

     public void PlayRandomVideo()
    {
        VideoClip[] videos = Resources.LoadAll<VideoClip>(glitchVids);
        if (videos.Length > 0){
            int randomIndex = Random.Range(0, videos.Length);
            videoPlayer.clip = videos[randomIndex];
            //videoPlayer.Play();
        }
    }
}
