using Leopotam.EcsLite;
using Leopotam.EcsLite.ExtendedSystems;
using UnityEngine;

public class Startup : MonoBehaviour
{
    private EcsWorld ecsWorld;
    private EcsSystems initSystems;
    private EcsSystems fixedUpdateSystems;
    private EcsSystems updateSystems;

    private void Start()
    {
        var gameData = new GameData();
        ecsWorld = new EcsWorld();

        initSystems = new EcsSystems(ecsWorld, gameData)
            .Add(new PlayerInitSystem())
            .Add(new GateInitSystem())
            .Add(new ButtonInitSystem());
            
        initSystems.Init();

        updateSystems = new EcsSystems(ecsWorld, gameData)
            .Add(new InputSystem());

        updateSystems.Init();

        fixedUpdateSystems = new EcsSystems(ecsWorld, gameData)
            .Add(new GateOpeningSystem())
            .Add(new PlayerMovementSystem())
            .Add(new ListeningGateStopOpeningEventSystem())
            .Add(new ListeningGateStartOpeningEventSystem())
            .Add(new GateRatationApplySystem())
            .Add(new PlayerPositionApplySystem())
            .Add(new GateOpenedCheckSystem())
            .Add(new ButtonUpdateSystem());
       
        fixedUpdateSystems.Init();

    }

    private void Update()
    {
        updateSystems.Run();
    }

    private void FixedUpdate()
    {
        fixedUpdateSystems.Run();
    }

    private void OnDestroy()
    {
        initSystems.Destroy();
        updateSystems.Destroy();
        fixedUpdateSystems.Destroy();
        ecsWorld.Destroy();
    }
}