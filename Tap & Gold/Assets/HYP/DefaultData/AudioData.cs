using UnityEngine;



/// TextData는 AudioSource Component에 대한 정보를 담고 있습니다.
public class AudioData : DefaultData
{
    [HideInInspector] public AudioSource audioSource;
    
    private ValueFlag<AudioClip> clip;
    private ValueFlag<float> volume;
    private ValueFlag<bool> loop;
 
    
    public override void OnInit()
    {
        audioSource = GetComponent<AudioSource>();
        clip = new (audioSource.clip);
        volume = new (audioSource.volume);
        loop = new (audioSource.loop);
        ComponentManager.AddComponent(hierarchyName, this);
    }


    /// Clip을 반환합니다.
    public AudioClip GetClip()
    {
        return clip.value;
    }

    /// Clip을 변경합니다.
    public void SetClip(in AudioClip value)
    {
        clip.value = value;
    }

    /// Volume을 반환합니다.
    public float GetVolume()
    {
        return volume.value;
    }

    /// Volume을 변경합니다.
    public void SetVolume(in float value)
    {
        volume.value = value;
    }

    /// Loop을 반환합니다.
    public bool GetLoop()
    {
        return loop.value;
    }

    /// Loop을 변경합니다.
    public void SetLoop(in bool value)
    {
        loop.value = value;
    }

    /// Clip을 재생합니다.
    public void Play()
    {
        audioSource.Play();
    }

    /// Clip을 정지합니다.
    public void Stop()
    {
        audioSource.Stop();
    }



    public bool IsChanged()
    {
        return clip.isChanged || volume.isChanged || loop.isChanged;
    }


    public void Sync()
    {
        if(clip.isChanged)
        {
            audioSource.clip = clip.value;
            clip.isChanged = false;
        }

        if(volume.isChanged)
        {
            audioSource.volume = volume.value;
            volume.isChanged = false;
        }

        if(loop.isChanged)
        {
            audioSource.loop = loop.value;
            loop.isChanged = false;
        }
    }
}