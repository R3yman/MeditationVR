using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{

    AudioSource source;
    float start;
    bool first;

    // Start is called before the first frame update
    void Start()
    {
        first = true;
        source = this.GetComponent<AudioSource>();
        start = Time.time;
        
    }

    // Update is called once per frame
    void Update()
    {   

        float curr = Time.time;
        if (curr-start >= 20 && first){
            first = false;
            source.Play(0);
        }
    }
}
