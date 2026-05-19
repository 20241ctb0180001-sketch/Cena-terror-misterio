using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class RandomVideo : MonoBehaviour
{
    [Header("Video Player")]
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] private VideoClip[] videos;

    [Header("Resource Folder // Material")]
    [SerializeField] private string glitchVids = "videos";
    [SerializeField] Renderer rend;
    [SerializeField] Material origMat;
    [SerializeField] Material Mat1;

    [Header("Timing")]
    [SerializeField] private float minInt;
    [SerializeField] private float maxInt;
    void Start()
    {
        videos = Resources.LoadAll<VideoClip>(glitchVids);
        StartCoroutine(RandomizePlay());
        rend = GetComponent<MeshRenderer>();
        rend.material = origMat;
    }

    #region Methods
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

    #endregion

    #region Corrotinas
    IEnumerator RandomizePlay()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.5f);
            rend.material = Mat1;
            PlayRandomVideo();
            float wait = Random.Range(minInt, maxInt);
            yield return new WaitForSeconds(wait);
            videoPlayer?.Stop();
            rend.material = origMat;
            yield return new WaitForSeconds(wait);
        }
    }

    IEnumerator PlayNextFrame(VideoPlayer vp)
    {
        yield return null;

        vp.Play();
    }

    #endregion
}

/*public void SetTrans()
    {
        Material mat = rend.material;
        // 1. Change surface type to Transparent (1)
        mat.SetFloat("_Surface", 1.0f);
        
        // 2. Set blending modes for Alpha transparency
        mat.SetInt("_SrcBlend", (int)BlendMode.SrcAlpha);
        mat.SetInt("_DstBlend", (int)BlendMode.OneMinusSrcAlpha);
        mat.SetInt("_ZWrite", 0); // Disable depth writing for transparency
        
        // 3. Set the appropriate render queue
        mat.renderQueue = (int)RenderQueue.Transparent;
        
        // 4. Update shader keywords (if applicable)
        mat.SetShaderPassEnabled("ShadowCaster", false); 

    }

    public void SetOpaq()
    {
        Material mat = rend.material;
        
        // 1. Change surface type back to Opaque (0)
        mat.SetFloat("_Surface", 0.0f);
        
        // 2. Reset blending to default opaque
        mat.SetInt("_SrcBlend", (int)BlendMode.One);
        mat.SetInt("_DstBlend", (int)BlendMode.Zero);
        mat.SetInt("_ZWrite", 1);
        
        // 3. Reset render queue
        mat.renderQueue = -1; // -1 resets to default for the shader
        
        // 4. Re-enable shadow casting
        mat.SetShaderPassEnabled("ShadowCaster", true);
    }*/
