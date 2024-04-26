using Unity.Mathematics;
using UnityEngine;
using static Unity.Mathematics.math;
using float2 = Unity.Mathematics.float2;



/// AppData는 어플리케이션 정보에 대한 정보를 담고 있습니다.
/// 이 클래스는 매프레임마다 UpdateManager 에서 업데이트됩니다.
public class AppData
{
    public static float2 screenSize { get; private set; }
    public static float deltaTime { get; private set; }
    public static float fixedDeltaTime { get; private set; }
    public static float totalDeltaTime { get; private set; }
    public static float orthographicSize { get; private set; }
    public static float3 cameraPos { get; private set; }


    public static void Init()
    {
        int cnt = 0;
        screenSize = 0;
        while(screenSize.Equals(float2(0,0)) && cnt < 10)
        {
            screenSize = ResolutionUtil.GetResolution();
            cnt++;
        }

        if(screenSize.Equals(float2(0,0))) throw new System.Exception("스크린사이즈가 0 입니다.");

        screenSize = ResolutionUtil.GetResolution();
        orthographicSize = Camera.main.orthographicSize;
        cameraPos = Camera.main.transform.position;
        fixedDeltaTime = 0.02f;
        deltaTime = 1/60f;
        totalDeltaTime = Time.realtimeSinceStartup;
    }


    
    public static void Update(Camera mainCamera)
    {
        deltaTime = Time.deltaTime;
        totalDeltaTime = Time.realtimeSinceStartup;
        cameraPos = mainCamera.transform.position;
    }

    
    public static void FixedUpdate(Camera mainCamera){
        fixedDeltaTime = Time.fixedDeltaTime;
        totalDeltaTime = Time.realtimeSinceStartup;
        cameraPos = mainCamera.transform.position;
    }

    
    public static void Log()
    {
        Debug.Log($"ScreenSize: {screenSize} \n DeltaTime: {deltaTime} \n FixedDeltaTime: {fixedDeltaTime} \n TotalDeltaTime: {totalDeltaTime} \n OrthographicSize: {orthographicSize} \n CameraPos: {cameraPos}");
    }
}
