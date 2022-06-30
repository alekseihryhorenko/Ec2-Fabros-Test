using Leopotam.EcsLite;

public class GateOpenedCheckSystem : IEcsRunSystem
{
    private EcsWorld ecsWorld;

    public void Run(EcsSystems ecsSystem)
    {
        ecsWorld = ecsSystem.GetWorld();
        var openingGates = ecsWorld.Filter<GateTag>().Inc<OpeningTag>().End();
        var openingPool = ecsWorld.GetPool<OpeningTag>();
        var openedPool = ecsWorld.GetPool<OpenedTag>();
        var rotationsPool = ecsWorld.GetPool<Rotation>();

        foreach (var entity in openingGates)
        {
            ref var rotation = ref rotationsPool.Get(entity);
            if (rotation.Value.y >= 90.0f)
            {
                openingPool.Del(entity);
                openedPool.Add(entity);
            }
        }
    }
}
