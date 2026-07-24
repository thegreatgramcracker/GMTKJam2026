using UnityEngine;

public class BGMManager : MonoBehaviour
{

    AudioSource audioSource;

    BGMSoundItem currentSound;
    public BGMSoundItem[] songList;
    public bool playOnAwake;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (playOnAwake)
        {
            PlaySoundByIndex(0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentSound != null)
        {
            audioSource.volume = currentSound.volume;// * OptionsManager.Instance.musicVolume; //* fadeGradient * volumeSlider.value;
            if (currentSound.loop)
            {
                if (audioSource.timeSamples > currentSound.loopEndSample)
                {
                    audioSource.timeSamples = audioSource.timeSamples - currentSound.loopEndSample + currentSound.loopStartSample;
                }
            }
        }
    }

    public void PlaySoundByIndex(int index)
    {
        if (currentSound == songList[index]) return;
        currentSound = songList[index];

        audioSource.volume = currentSound.volume;
        audioSource.loop = currentSound.loop;

        audioSource.clip = currentSound.clip;

        //fadeGradient = 1f;
        audioSource.Play();
    }

    public void Stop()
    {
        audioSource.Stop();
        currentSound = null;
    }

}

[System.Serializable]
public class BGMSoundItem
{
    public string name;
    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume;
    public bool loop;
    public int loopStartSample;
    public int loopEndSample;

}
