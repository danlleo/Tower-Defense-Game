using Infrastructure.AssetManagment;
using Infrastructure.Factory;
using Services;
using Services.Input;

namespace Infrastructure.States
{
  public class BootstrapState : IState
  {
    private const string Initial = "Initial";
    private readonly GameStateMachine _stateMachine;
    private readonly InputService _inputService;
    private readonly SceneLoader _sceneLoader;
    private readonly AllServices _services;
    
      
    public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
    {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
      _services = services;
      _inputService = new StandaloneInputService();
      
      RegisterServices();
    }

    public void Enter() =>
      _sceneLoader.Load(Initial, onLoaded: EnterLoadLevel);

    public void Exit()
    {
    }

    /// <summary>
    /// Артур не бей, надо будет поговорить
    /// </summary>
    private void RegisterServices()
    {
      RegisterStaticData();
      
      _services.RegisterSingle<IGameStateMachine>(_stateMachine);
      _services.RegisterSingle<IInputService>(_inputService);
      _services.RegisterSingle<IAssetsProvider>(new AssetsProvider());
      _services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IAssetsProvider>(), _services.Single<IStaticDataService>())); 
    }
    private void RegisterStaticData()
    {
      IStaticDataService staticData = new StaticDataService();
      staticData.LoadEnemies();
      _services.RegisterSingle(staticData);
    }

    private void EnterLoadLevel() =>
      _stateMachine.Enter<LoadLevelState, string>("Main");
  }
}