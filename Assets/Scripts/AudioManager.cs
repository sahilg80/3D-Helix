using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioManager : SingletonBehaviour<AudioManager>
{
    public Sound[] Sounds;
    private AudioSource audioSource;
    // Start is called before the first frame update
    public override void Awake()
    {
        base.Awake();
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {

    }

    // Update is called once per frame
    public void PlaySound(string name)
    {
        Sound s = Sounds.Where(s=> s.Name.Equals(name)).FirstOrDefault();
        audioSource.clip = s.AudioClip;
        audioSource.Play();
        audioSource.loop = s.OnLoop;
    }

}
