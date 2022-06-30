using Leopotam.EcsLite;
using UnityEngine;

public class GateOpeningSystem : IEcsRunSystem
{
    private EcsWorld ecsWorld;

    public void Run(EcsSystems ecsSystem)
    {
        ecsWorld = ecsSystem.GetWorld();
        var gameData = ecsSystem.GetShared<GameData>();
        var openingGates = ecsWorld.Filter<GateTag>().Inc<OpeningTag>().End();
        var rotations = ecsWorld.GetPool<Rotation>();

        foreach (var gate in openingGates)
        {
            ref var rotation = ref rotations.Get(gate);
            rotation.Value.y += gameData.gateOpeningSpeed * Time.fixedDeltaTime;
        }
    }
}
