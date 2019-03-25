using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class SkipBtn : MonoBehaviour
{
    public VideoPlayer player;
    public float readyTime = 32.5f;

    private VideoClip video;
    private double startFrame;

    public void OnClick()
    {
        startFrame = player.frameRate * readyTime;
        player.frame = (long)startFrame;
        gameObject.SetActive(false);
    }

}
