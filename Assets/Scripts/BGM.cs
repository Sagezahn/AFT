using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BGM : MonoBehaviour
{
    public AudioClip music;
    public Slider slider;
    private AudioSource back;
    void Start()
    {
        back = this.GetComponent<AudioSource>();
        back.loop = true; 
        back.volume = 0.5f;
        back.clip = music;
        back.Play(); 
    }

    void Update()
    {
        back.volume = slider.value;
    }
}
