using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource fonteSFX;
    
    public void AudioPlay(AudioClip som)
    {
        fonteSFX.PlayOneShot(som);
    }

}
