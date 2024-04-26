using UnityEngine;



/// AnimationData는 애니메이션에 대한 정보를 담고 있습니다.
/// 이 컴포넌트를 사용하는 애니메이션은 오로지 IDLE 방향으로만 트랜지션이 가능해야 합니다.
public class AnimationData : DefaultData
{
    private const string IDLE = "IDLE";
    private bool autoEnterIdle;

    private Animator animator;
    private (string before, string current) state;
    private ValueFlag<int> speed;
    

    public override void OnInit()
    {
        animator = GetComponent<Animator>();
        state = new (IDLE, IDLE);
        speed = new (1);
        autoEnterIdle = false;
        ComponentManager.AddComponent(hierarchyName, this);
    }


    /// 애니메이션의 재생속도를 반환합니다.
    public int GetSpeed()
    {
        return speed.value;
    }

    /// 애니메이션의 재생속도를 변경합니다. (0: 정지, 1: 정상속도)
    public void SetSpeed(int value)
    {
        speed.value = value;
    }

    public (string before, string current) GetState()
    {
        return state;
    }

    public void SetCurrentState(string value)
    {
        state.current = value;
    }

    public bool GetCurrentStateChanged()
    {
        return state.before != state.current;
    }

    public bool IsIDLE()
    {
        return state.current == IDLE;
    }


    /// IDLE Animation Clip 전용 함수 (해당 파일에서만 사용할 것!)
    public void EnterIDLE()
    {
        state.current = IDLE;
        if(GetCurrentStateChanged()) autoEnterIdle = true;
    }

    public bool IsChanged()
    {
        return GetCurrentStateChanged() || speed.isChanged;
    }

    public void Sync()
    {
        if(GetCurrentStateChanged())
        {
            if(!autoEnterIdle)
            {
                animator.Play(state.current);
            }

            state.before = state.current;
            autoEnterIdle = false;
        }

        if(speed.isChanged)
        {
            animator.speed = speed.value;
            speed.isChanged = false;
        }
    }
}