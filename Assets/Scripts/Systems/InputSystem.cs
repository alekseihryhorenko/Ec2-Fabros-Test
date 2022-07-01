using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

public class InputSystem : IEcsRunSystem
{
    private EcsWorld ecsWorld;
    public void Run(EcsSystems ecsSystem)
    {
        ecsWorld = ecsSystem.GetWorld();
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (!Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                return;
            }
            var entity = ecsWorld.NewEntity();
            ref var mouseInputEvent = ref ecsWorld.GetPool<MouseInputEvent>().Add(entity);
            mouseInputEvent.Position = new Vector2(hit.point.x, hit.point.z);
        }
    }
}