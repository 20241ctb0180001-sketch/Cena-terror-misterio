using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class RandomVideo : MonoBehaviour
{
    [SerializeField] VideoPlayer videoPlayer;
    float minInt = 1f;
    float maxInt = 12f;
    void Start(){
        StartCoroutine(Randomizeplay());
    }

    IEnumerator Randomizeplay(){
        while(true){
            videoPlayer.Play();
            float wait= Random.Range(minInt, maxInt);
            yield return new WaitForSeconds(wait);
            videoPlayer.Stop();
            yield return new WaitForSeconds(wait);
            }
    }
}
