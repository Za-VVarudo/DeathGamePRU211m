using UnityEngine;

public class BackgroundMusicController : MonoBehaviour
{
    [SerializeField]
    public AudioClip normalTheme;

    [SerializeField]
    public AudioClip bossTheme;

    [SerializeField]
    public AudioClip gameOverTheme;

    AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        audioSrc.clip = normalTheme;
        audioSrc.loop = true;
        audioSrc.Play();
    }

    public void ChangeToBossTheme(bool isBossTheme)
    {
        if (isBossTheme) audioSrc.clip = bossTheme;
        else audioSrc.clip = normalTheme;

        audioSrc.loop = true;
        audioSrc.Play();
    }

    public void ChangeTheme(AudioClip theme)
    {
        audioSrc.clip = theme;

        audioSrc.loop = false;
        audioSrc.Play();
    }
}
