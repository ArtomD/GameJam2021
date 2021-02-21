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
        musicSource = Utils.AddAudioNoFalloff(gameObject, menuMuisc, true, false, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
