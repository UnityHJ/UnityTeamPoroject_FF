using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using System;

public class TimerCtrl : MonoBehaviour
{
    private const string start = "Start!!";
    private const string ready = "Ready..";
    private bool isStarted;

    public Text timeTxt;

    public AudioSource _audio; 
    public VideoPlayer player;
    public CanvasGroup scoreBoard;
    public Text bestScore;
    public Text yourScore;
    public GameObject btnGroup;
    public GameObject skipBtn;
    public float startTime = 35.5f;
    public float playTime = 120.0f;


    void Start()
    {
        timeTxt.GetComponent<Text>();
    }

    void Update()
    {
        //PlayerPrefs.DeleteAll();
        if (GameManager.Instance.isTimeOver) return;
        if(player.clockTime < startTime)
        {
            timeTxt.text = ready + " " + (int)(startTime - player.clockTime + 1);
            GameManager.Instance.itemState = ItemState.WAIT;
            if (startTime - player.clockTime + 1 <= 4 && skipBtn.activeSelf) skipBtn.SetActive(false);
        }
        else
        {
            timeTxt.text = start;
            if(!isStarted)
            {
                GameManager.Instance.itemState = ItemState.NORMAL;
                isStarted = true;
            }
            int leftTime = (int)((playTime - player.clockTime + startTime + 1) * 1000);
            if(leftTime <= 0)
            //if(leftTime <= 110000)
            {
                timeTxt.text = "Times Up!";
                GameManager.Instance.isTimeOver = true;
                StartCoroutine(ScoreBoard());
            }
            else if (leftTime < playTime * 1000)
            {
                var timeSpan = TimeSpan.FromMilliseconds(leftTime);
                timeTxt.text = timeSpan.ToString(@"mm\:ss\:ff");
            }
        }
        
    }

    private IEnumerator ScoreBoard()
    {
        float currVolume = player.GetDirectAudioVolume(0);
        while (!Mathf.Approximately(scoreBoard.alpha, 1.0f))
        {
            player.SetDirectAudioVolume(0,  (1 - scoreBoard.alpha) * currVolume);
            scoreBoard.alpha = Mathf.MoveTowards(scoreBoard.alpha, 1.0f, Time.deltaTime);
            yield return null;
        }
        _audio.Play();
        int bScore = PlayerPrefs.GetInt("BestScore", 0);
        string best = "Your Best Score " + bScore + " Kcal";
        bestScore.text = best;
        yield return new WaitForSeconds(1.0f);
        string yours = "";
        if(bScore < GameManager.Instance.Kcal)
        {
            yours = "The New Record!! " + GameManager.Instance.Kcal + " Kcal";
            PlayerPrefs.SetInt("BestScore", GameManager.Instance.Kcal);
            PlayerPrefs.Save();
        }
        else
        {
            yours = "Oops Try Again!!";
        }
        yourScore.text = yours;
        yield return new WaitForSeconds(1.0f);
        btnGroup.SetActive(true);
    }
}
