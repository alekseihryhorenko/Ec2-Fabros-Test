using Leopotam.EcsLite;
using UnityEngine;

public class ListeningGateStartOpeningEventSystem : IEcsRunSystem
{
    private EcsWorld ecsWorld;

    public void Run(EcsSystems ecsSystem)
    {
        ecsWorld = ecsSystem.GetWorld();
        var buttonEvents = ecsWorld.Filter<PressedTag>().Inc<ButtonTag>().Inc<Colored>().End();
        var closedGates = ecsWorld.Filter<GateTag>().Inc<Colored>().Exc<OpeningTag>().Exc<OpenedTag>().End();
        var coloredPool = ecsWorld.GetPool<Colored>();

        foreach (var entity in buttonEvents)
        {
            ref var buttonEventColor = ref coloredPool.Get(entity);

            foreach (var gateEntity in closedGates)
            {
                ref var gateColor = ref coloredPool.Get(gateEntity);

                if (buttonEventColor.Color == gateColor.Color)
                {
                    Debug.Log("opening : " + gateColor.Color);
                    ecsWorld.GetPool<OpeningTag>().Add(gateEntity);
                }
            }
        }
    }
}
