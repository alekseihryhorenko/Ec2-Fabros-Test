using Leopotam.EcsLite;

public class PlayerStartMovingSystem : IEcsRunSystem
{
    private EcsWorld ecsWorld;
    public void Run(EcsSystems ecsSystem)
    { 
        ecsWorld = ecsSystem.GetWorld();
        var datas = ecsWorld.Filter<PlayerMovingData>().End();
        if (datas.GetEntitiesCount() > 0)
        {
            return;
        }
        var mouseEvents = ecsWorld.Filter<MouseInputEvent>().End();
        var mouseEventsPool = ecsWorld.GetPool<MouseInputEvent>();
        foreach (var entity in mouseEvents)
        {
            var mouseEvent = mouseEventsPool.Get(entity);
            ref var data = ref ecsWorld.GetPool<PlayerMovingData>().Add(entity);
            data.Target = mouseEvent.Position;
        }
    }
}