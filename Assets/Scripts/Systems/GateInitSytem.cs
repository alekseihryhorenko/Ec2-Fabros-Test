using Leopotam.EcsLite;
using UnityEngine;

public class GateInitSystem : IEcsInitSystem
{
    private EcsWorld ecsWorld;

    public void Init(EcsSystems ecsSystem)
    {
        ecsWorld = ecsSystem.GetWorld();
        var gatePool = ecsWorld.GetPool<GateTag>();
        var coloredPool = ecsWorld.GetPool<Colored>();
        var unitTransformPool = ecsWorld.GetPool<TransformComponent>();
        var rotations = ecsWorld.GetPool<Rotation>();

        var gates = GameObject.FindGameObjectsWithTag(Tags.GATE);
        foreach(var gate in gates)
        {
            var entity = ecsWorld.NewEntity();
            gatePool.Add(entity);
            ref var colored = ref coloredPool.Add(entity);
            ref var unitTransform = ref unitTransformPool.Add(entity);
            unitTransform.Value = gate.transform;
            ref var rotation = ref rotations.Add(entity);
            rotation.Value = unitTransform.Value.localEulerAngles;
            colored.Color = gate.GetComponent<ColoredComponent>().color;
        }
    }
}