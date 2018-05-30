using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScript : MonoBehaviour {
    public AudioClip clipUpgrade;
    public AudioClip clipDowngrade;
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private AudioSource audio;
    private float jumpTimer = 0;
    private float previousSpeed = 1;
    private Animator anim;
    // Use this for initialization
    void Start()
    {
        anim = this.gameObject.GetComponent<Animator>();
        audio = this.gameObject.GetComponent<AudioSource>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
          
            previousSpeed = speed;
            speed = 200;
            jumpSpeed = 30;
            UpDownGradeSound();
        }
    }
    private void UpDownGradeSound()
    {
        if (previousSpeed > speed)
        {
            audio.clip = clipDowngrade;
            audio.Play();
        }
        else if (previousSpeed < speed)
        {
            audio.clip = clipUpgrade;
            audio.Play();
        }
    }
}
