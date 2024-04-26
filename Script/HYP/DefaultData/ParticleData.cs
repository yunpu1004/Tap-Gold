using System;
using UnityEngine;

/// ParticleData는 Particle System Component에 대한 정보를 담고 있습니다.
/// (Particle System의 Stop Action은 Callback으로 설정되어야 합니다.)
public class ParticleData : DefaultData
{
    private ParticleSystem particle;
    private ValueFlag<bool> play;
    private ValueFlag<bool> loop;


    public override void OnInit()
    {
        particle = GetComponent<ParticleSystem>();
        if(particle.main.stopAction != ParticleSystemStopAction.Callback) {throw new Exception("파티클 시스템의 Stop Action이 Callback이 아닙니다.");}
        if(particle.main.playOnAwake) {throw new Exception("파티클 시스템의 Play On Awake가 활성화되어 있습니다.");}
        play = new(particle.isPlaying);
        loop = new(particle.main.loop);
        ComponentManager.AddComponent(hierarchyName, this);
    }

    private void OnParticleSystemStopped()
    {
        play.value = false;
        play.isChanged = false;
    }
    
    /// 파티클이 재생되고 있는지 여부를 반환합니다.
    public bool GetPlay()
    {
        return play.value;
    }


    /// 파티클의 재생 여부를 설정합니다.
    public void SetPlay(bool value, bool notifyChanged = true)
    {
        var isChanged = play.isChanged;
        play.value = value;
        if(!notifyChanged) { play.isChanged = isChanged; }
    }



    /// 파티클이 루프되고 있는지 여부를 반환합니다.
    public bool GetLoop()
    {
        return loop.value;
    }

    /// 파티클의 루프 여부를 설정합니다.
    public void SetLoop(bool value)
    {
        loop.value = value;
    }


    public bool IsChanged()
    {
        return play.isChanged || loop.isChanged;
    }


    public void Sync()
    {
        if(play.isChanged) 
        { 
            if(play.value) { particle.Play(true); } else { particle.Stop(true); }
            play.isChanged = false; 
        }

        if(loop.isChanged) 
        { 
            var main = particle.main; main.loop = loop.value; 
            loop.isChanged = false;
        }
    }
}