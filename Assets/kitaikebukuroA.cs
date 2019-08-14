using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kitaikebukuroA : MonoBehaviour
{
    public AudioClip tj02;
    public AudioSource tj02audio;
    // Start is called before the first frame update
    void Start()
    {
        tj02audio = gameObject.GetComponent<AudioSource>();
        tj02audio.clip = tj02;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
