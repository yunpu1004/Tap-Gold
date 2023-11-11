using System;
using System.Collections.Generic;
using System.Linq;

using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public class JobHandleUtil
{
    public static Func<JobHandle> CombineJobFunc(params Func<JobHandle>[] jobFuncs)
    {
        var copyArray = jobFuncs.ToArray();
        return () =>
        {
            JobHandle jobHandle = copyArray[0]();
            int len = copyArray.Length;

            for(int i = 1; i < len; i++)
            {
                JobHandle temp = copyArray[i]();
                jobHandle = JobHandle.CombineDependencies(jobHandle, temp);
            }
            return jobHandle;
        };
    }

    public static Func<JobHandle> CombineJobFunc(List<Func<JobHandle>> jobFuncs)
    {
        var copyArray = jobFuncs.ToArray();
        return () =>
        {
            JobHandle jobHandle = copyArray[0]();
            int len = copyArray.Length;

            for(int i = 1; i < len; i++)
            {
                JobHandle temp = copyArray[i]();
                jobHandle = JobHandle.CombineDependencies(jobHandle, temp);
            }
            return jobHandle;
        };
    }

    public static Func<JobHandle> ConnectJobFunc(params Func<JobHandle>[] jobFuncs)
    {
        var copyArray = jobFuncs.ToArray();

        return () =>
        {
            JobHandle jobHandle = copyArray[0]();
            int len = copyArray.Length;
            for(int i = 1; i < len; i++)
            {
                jobHandle.Complete();
                jobHandle = jobFuncs[i]();
            }
            return jobHandle;
        };
    }

    public static Func<JobHandle> ConnectJobFunc(List<Func<JobHandle>> jobFuncs)
    {
        var copyArray = jobFuncs.ToArray();

        return () =>
        {
            JobHandle jobHandle = copyArray[0]();
            int len  = copyArray.Length;
            for(int i = 1; i < len; i++)
            {
                jobHandle.Complete();
                jobHandle = jobFuncs[i]();
            }
            return jobHandle;
        };
    }

    public static JobHandle ScheduleDebugJob(string msg)
    {
        var job = new DebugJob
        {
            msg = msg
        };
        return job.Schedule();
    }

    private struct DebugJob : IJob
    {
        public FixedString32Bytes msg;

        
        public void Execute()
        {
            Debug.Log(msg);
        }
    }
}
