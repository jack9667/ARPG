using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager instance;
    public AudioClip[] audioClipArray;
    public AudioSource audioSource;

    public bool isQuiet = false;
    private Dictionary<string, AudioClip> audioDict = new Dictionary<string, AudioClip>();

    void Awake()
    {
        instance = this;
    }

	// Use this for initialization
	void Start () {
		foreach(var a in audioClipArray)
        {
            audioDict.Add(a.name, a);
        }
	}
	

    /// <summary>
    /// 播放声音
    /// </summary>
    /// <param name="audioname"></param>
    public void Play(string audioname)
    {
        if (isQuiet) return;
        AudioClip ac;
       if(audioDict.TryGetValue(audioname, out ac))
        {
            //AudioSource.PlayClipAtPoint(ac, Vector3.zero);
            this.audioSource.PlayOneShot(ac);
        }
    }

    public void Play(string audioName,AudioSource audioSouce)
    {
        if (isQuiet) return;
        AudioClip ac;
        if (audioDict.TryGetValue(audioName, out ac))
        {
            //AudioSource.PlayClipAtPoint(ac, Vector3.zero);
            audioSource.PlayOneShot(ac);
        }
    }


}
