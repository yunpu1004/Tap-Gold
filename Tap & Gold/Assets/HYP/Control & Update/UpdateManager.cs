using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Text;

/// 이 컴포넌트는 ObjectData 를 상속한 모든 컴포넌트의 업데이트 함수를 아래의 순서에 따라 관리합니다
/// 1. 게임 실행시, dataUpdateDict 에 등록된 모든 컴포넌트의 업데이트 함수를 depth 단계마다 dataUpdateTasks 에 분배합니다.
/// 2. 각 dataUpdateTask 를 depth 단계에 따라 순차적으로 실행합니다. (하나의 dataUpdateTask 는 내부적으로 병렬로 실행됩니다.)
/// 3. 모든 dataUpdateTask 가 실행되면, mainThreadQueue 에 등록된 모든 함수를 순차적으로 실행합니다. (이벤트 큐 패턴)
/// (주의 : 매프레임의 마지막에 실행되어야 하며, 딱 하나의 오브젝트에만 존재해야 합니다.)
public class UpdateManager : MonoBehaviour
{
    private static Camera mainCamera;
    private static Stopwatch sw;
    public bool checkTime = false;
    public bool onSingleThread = false;
    public int frameCount = 0;
    public static float totalTime = 0;
    public static float[] depthTime;

    public static Dictionary<int, List<Action>> dataUpdateDict;
    private static DataUpdateTask[] dataUpdateTasks;
    private static ConcurrentQueue<Action> mainThreadQueue;

    public static void Init()
    {
        Application.targetFrameRate = 60;
        mainCamera = Camera.main;
        sw = new Stopwatch();
        mainThreadQueue = new ConcurrentQueue<Action>();
        dataUpdateDict = new Dictionary<int, List<Action>>();
    }

    [LateInitialize]
    private static void LateInit()
    {
        int maxKey = -1;
        foreach(var key in dataUpdateDict.Keys)
        {
            if(key > maxKey) maxKey = key;
        }

        dataUpdateTasks = new DataUpdateTask[maxKey + 1];
        for (int i = 0; i < dataUpdateTasks.Length; i++)
        {
            Debug.Log($"depth {i} : {dataUpdateDict[i].Count}");
            dataUpdateTasks[i] = new DataUpdateTask(dataUpdateDict[i]);
        }

        depthTime = new float[dataUpdateTasks.Length];
    }

    /// MainThreadQueue 는 이벤트 큐 패턴을 구현하기 위해 사용됩니다.
    /// 이 메소드를 통해 추가된 함수는 매프레임의 마지막에 순차적으로 실행됩니다. 
    public static void AddMainThreadQueue(Action action)
    {
        mainThreadQueue.Enqueue(action);
    }

    /// 이 메소드는 아래의 순서에 따라 실행됩니다.
    /// 1. 최상위 depth 부터 dataUpdateTasks 를 순차적으로 실행합니다.
    /// 2. 이어서, mainThreadQueue 에 등록된 모든 함수를 순차적으로 실행합니다. (이벤트 큐 패턴)
    void Update()
    {
        if(!InitializerManager.IsInit()) return;
        frameCount++;
        if(checkTime) sw.Start();
        AppData.Update(mainCamera);   
        TouchData.Update();

        for (int i = 0; i < dataUpdateTasks.Length; i++)
        {
            if(checkTime) depthTime[i] = (float)sw.Elapsed.TotalMilliseconds;
            if(!onSingleThread)dataUpdateTasks[i].RunParallel();
            else dataUpdateTasks[i].Run();
            if(checkTime) depthTime[i] = (float)sw.Elapsed.TotalMilliseconds - depthTime[i];
        }

        while (mainThreadQueue.TryDequeue(out Action action))
        {
            action();
        }

        if(checkTime)
        {
            sw.Stop();
            totalTime = (float)sw.Elapsed.TotalMilliseconds;
            sw.Reset();

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < depthTime.Length; i++)
            {
                sb.Append($"depth {i} time in ms : {depthTime[i]}\n");
            }
            sb.Append($"total time in ms : {totalTime}");
            Debug.Log(sb.ToString());
        }
    }

 

    private void FixedUpdate() {
        if(!InitializerManager.IsInit()) return;
        AppData.FixedUpdate(mainCamera);   
        PhysicsSimulationUpdate.Update();
    }

    /// UpdateManager.dataUpdateDict 에 등록된 동일한 depth 를 가지는 업데이트 함수들을 세 개의 task 로 분배합니다.
    /// task1, task2 는 병렬로 실행되고, task3 는 메인 스레드에서 실행됩니다.
    private class DataUpdateTask
    {
        private Action[] actionsA;
        private Action[] actionsB;
        private Action[] actionsC;

        public DataUpdateTask(List<Action> taskList)
        {
            int count = taskList.Count;
            int count1 = count / 3;
            int count2 = count1 * 2;
            actionsA = new Action[count1];
            actionsB = new Action[count2 - count1];
            actionsC = new Action[count - count2];
            for (int i = 0; i < count1; i++)
            {
                actionsA[i] = taskList[i];
            }
            for (int i = count1; i < count2; i++)
            {
                actionsB[i - count1] = taskList[i];
            }
            for (int i = count2; i < count; i++)
            {
                actionsC[i - count2] = taskList[i];
            }
        }

        public void RunParallel()
        {
            Task taskA = null;
            Task taskB = null;

            if(actionsA.Length > 0)
            {
                taskA = Task.Run(() =>
                {
                    for (int i = 0; i < this.actionsA.Length; i++)
                    {
                        this.actionsA[i]();
                    }
                });
            }
            

            if(actionsB.Length > 0)
            {
                taskB = Task.Run(() =>
                {
                    for (int i = 0; i < this.actionsB.Length; i++)
                    {
                        this.actionsB[i]();
                    }
                });
            }
            
            
            if(actionsC.Length > 0)
            {
                for (int i = 0; i < this.actionsC.Length; i++)
                {
                    this.actionsC[i]();
                }
            }
        

            taskA?.Wait();
            taskB?.Wait();
        }


        public void Run()
        {
            for (int i = 0; i < this.actionsA.Length; i++)
            {
                this.actionsA[i]();
            }
            for (int i = 0; i < this.actionsB.Length; i++)
            {
                this.actionsB[i]();
            }
            for (int i = 0; i < this.actionsC.Length; i++)
            {
                this.actionsC[i]();
            }
        }
    }
}