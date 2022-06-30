using Leopotam.EcsLite;
using UnityEngine;

public class GateRatationApplySystem : IEcsRunSystem
{
    private EcsWorld ecsWorld;

    public void Run(EcsSystems ecsSystem)
    {
        ecsWorld = ecsSystem.GetWorld();
        var gates = ecsWorld.Filter<GateTag>().Inc<Rotation>().Inc<TransformComponent>().End();
        var rotations = ecsWorld.GetPool<Rotation>();
        var transforms = ecsWorld.GetPool<TransformComponent>();

        foreach(var gate in gates)
        {
            ref var rotation = ref rotations.Get(gate);
            ref var transform = ref transforms.Get(gate);
            transform.Value.localRotation = Quaternion.Euler(rotation.Value);
        }
    }
}

