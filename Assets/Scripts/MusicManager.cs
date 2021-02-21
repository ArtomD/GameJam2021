using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    private static MusicManager _instance = null;
    [SerializeField]
    private AudioClip menuMuisc;


    private AudioSource musicSource;
    // Start is called before the first frame update
    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            _instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
        
    }

    void Start()
    {
        musicSource = Utils.AddAudioNoFalloff(gameObject, menuMuisc, true, false, 0.4f, 1);
        musicSource.Play();
    }

    public static MusicManager getInstance()
    {
        return _instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
