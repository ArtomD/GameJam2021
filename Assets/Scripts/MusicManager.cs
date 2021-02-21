using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip menuMuisc;


    private AudioSource musicSource;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        musicSource = Utils.AddAudioNoFalloff(gameObject, menuMuisc, true, false, 0.4f, 1);
        musicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
