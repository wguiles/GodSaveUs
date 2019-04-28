using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    public AudioSource source;

    [Range(0f, 1f)]
    public float volume;

    [Range(0f, 1f)]
    public float pitch;
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    
    public Sound[] sounds;


    private void Awake()
    {
        if (instance == null)
            instance = this;

        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    public void PlaySound(string clipName)
    {
        foreach (Sound s in sounds)
        {
            if (s.name == clipName)
            {
                s.source.PlayOneShot(s.clip);
                break;
            }
        }
    }

    public void StopSound(string clipName)
    {
        foreach (Sound s in sounds)
        {
            if (s.name == clipName)
            {
                s.source.Stop();
                break;
            }
        }
    }

    public void StopAllSounds(string clipName)
    {
        foreach (Sound s in sounds)
        {
            if (s.name == clipName)
            {
                s.source.Stop();
                break;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
