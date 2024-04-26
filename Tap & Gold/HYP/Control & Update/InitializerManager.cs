using UnityEngine;



/// InitializerManager 는 게임 실행시 초기화 작업을 실행합니다.
/// [LateInitialize] 라는 Attribute 를 가진 모든 메소드도 초기화 작업시 이어서 실행됩니다.
public class InitializerManager : MonoBehaviour
{
    private static bool init = false;
    public bool log = false;

    private void Awake()
    {
        if (init) return;
        init = true;
        Canvas.ForceUpdateCanvases();
        if(log) Debug.Log("InitializerStart");
        TouchData.Init();
        if(log) Debug.Log("TouchDataInit");
        AppData.Init();
        if(log) Debug.Log("AppDataInit");
        ComponentManager.Init();
        if(log) Debug.Log("ComponentManagerInit");
        UpdateManager.Init();
        if(log) Debug.Log("UpdateManagerInit");
        DefaultData.Init();
        if(log) Debug.Log("DefaultDataComponentInit");
        ObjectData.Init();
        if(log) Debug.Log("DataComponentInit");
        PhysicsSimulationUpdate.Init();
        if(log) Debug.Log("PhysicsSimulationUpdateInit");
        LateInit();
        if(log) Debug.Log("InitializerEnd");
    }

    private static void LateInit()
    {
        var methodInfos = ReflectionUtil.GetStaticMethodsWithAttribute<LateInitializeAttribute>();
        foreach (var methodInfo in methodInfos)
        {
            methodInfo.Invoke(null, null);
        }
    }

    public static bool IsInit()
    {
        return init;
    }
}



[System.AttributeUsage(System.AttributeTargets.Method)]
public class LateInitializeAttribute : System.Attribute  {  } 