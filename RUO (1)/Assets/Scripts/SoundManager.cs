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

    private string[] enemySounds = { "EnemyDamage1", "EnemyDamage2", "EnemyDamage3", "EnemyDamage4" };
    
    public Sound[] sounds;
    public UnityEngine.UI.Slider slider;

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

    public void PlayRandomSqueak()
    {
        PlaySound(enemySounds[Random.Range(0, enemySounds.Length)]);
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

    public void StopAllSounds()
    {
        foreach (Sound s in sounds)
        {
            s.source.Stop();
        }
    }

    public void OnValueChanged()
    {
        PlaySound("uiClickS");
        foreach (Sound s in sounds)
        {
            s.source.volume = slider.value;
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
