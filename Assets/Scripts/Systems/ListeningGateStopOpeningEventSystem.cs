using Leopotam.EcsLite;

public class ListeningGateStopOpeningEventSystem : IEcsRunSystem
{
    private EcsWorld ecsWorld;

    public void Run(EcsSystems ecsSystem)
    {
        ecsWorld = ecsSystem.GetWorld();
        var buttonEvents = ecsWorld.Filter<UnPressedTag>().Inc<ButtonTag>().Inc<Colored>().End();
        var openingGates = ecsWorld.Filter<GateTag>().Inc<Colored>().Inc<OpeningTag>().Exc<OpenedTag>().End();
        var coloredPool = ecsWorld.GetPool<Colored>();

        foreach (var entity in buttonEvents)
        {
            ref var buttonEventColor = ref coloredPool.Get(entity);

            foreach (var gate in openingGates)
            {
                ref var gateColor = ref coloredPool.Get(gate);

                if (buttonEventColor.Color == gateColor.Color)
                {
                    ecsWorld.GetPool<OpeningTag>().Del(gate);
                }
            }
        }
    }
}

