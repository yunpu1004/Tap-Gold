using System.Collections.Generic;
using UnityEngine;


/// PhysicsSimulationUpdate는 Transform 과 Rigidbody2D 의 정보를 TransformData, RigidbodyData 에 업데이트합니다.
/// 이 클래스는 매프레임마다 UpdataManager 에서 업데이트됩니다.
public class PhysicsSimulationUpdate
{
    private static (Transform, TransformData, Rigidbody2D, RigidbodyData)[] physicsTargets;

    public static void Init()
    {
        List<(Transform, TransformData, Rigidbody2D, RigidbodyData)> physicsTargetList = new List<(Transform, TransformData, Rigidbody2D, RigidbodyData)>();
        var rigidbodies = ComponentManager.GetComponents<RigidbodyData>();
        int len = rigidbodies.Length;
        for (int i = 0; i < len; i++)
        {
            if(rigidbodies[i].rigidbodyType == RigidbodyType2D.Dynamic)
            {
                var transform = rigidbodies[i].GetComponent<Transform>();
                var transformData = ComponentManager.GetComponent<TransformData>(rigidbodies[i].hierarchyName);
                var rigidbody = rigidbodies[i].GetComponent<Rigidbody2D>();
                physicsTargetList.Add((transform, transformData, rigidbody, rigidbodies[i]));
            }
        }
        physicsTargets = physicsTargetList.ToArray();
    }

    public static void Update()
    {
        int len = physicsTargets.Length;
        for (int i = 0; i < len; i++)
        {
            physicsTargets[i].Item2.ReadTransform(physicsTargets[i].Item1);
            physicsTargets[i].Item4.ReadRigidbody2D(physicsTargets[i].Item3);
        }
    }
}
