using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundEffects { BITE, CHEW, BURP, DRINK, SWALLOW }

public class SoundCtrl : MonoBehaviour
{
    public static SoundCtrl Instance { get; set; }

    private AudioSource _audio;

    public AudioClip[] biteClips;
    public AudioClip[] chewClips;
    public AudioClip[] burpClips;
    public AudioClip[] drinkClips;
    public AudioClip[] swallowClips;
    public AudioClip[] trashClips;
    

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != null) Destroy(gameObject);
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
    }
    
    public float MakeSounds(SoundEffects kinds) 
    {
        int idx;
        AudioClip clip = biteClips[0];
        switch (kinds)
        {
            case SoundEffects.BITE:
                idx = Random.Range(0, biteClips.Length);
                clip = biteClips[idx];
                break;
            case SoundEffects.CHEW:
                idx = Random.Range(0, chewClips.Length);
                clip = chewClips[idx];
                break;
            case SoundEffects.BURP:
                idx = Random.Range(0, burpClips.Length);
                clip = burpClips[idx];
                break;
            case SoundEffects.DRINK:
                idx = Random.Range(0, drinkClips.Length);
                clip = drinkClips[idx];
                break;
            case SoundEffects.SWALLOW:
                idx = Random.Range(0, swallowClips.Length);
                clip = swallowClips[idx];
                break;
        }
        _audio.PlayOneShot(clip, 1.0f);
        return clip.length;
    }





}
