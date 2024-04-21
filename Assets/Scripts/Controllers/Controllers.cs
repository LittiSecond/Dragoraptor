
namespace Dragoraptor
{
    public sealed class Controllers
    {

        private readonly IExecutable[] _executeControllers;


        public int Length => _executeControllers.Length;
        public IExecutable this[int index] => _executeControllers[index];


        public Controllers(GamePlaySettings gamePlaySettings)
        {
            CharacterStateHolder characterStateHolder = new CharacterStateHolder();
            EnergyController energyController = new EnergyController(gamePlaySettings, characterStateHolder);
            JumpCalculator jumpCalculator = new JumpCalculator(gamePlaySettings);
            WalkController walkController = new WalkController(characterStateHolder, gamePlaySettings);
            JumpPainter jumpPainter = new JumpPainter(characterStateHolder, gamePlaySettings, jumpCalculator);
            JumpController jumpController = new JumpController(characterStateHolder, jumpCalculator, energyController);
            FlightObserver flightObserver = new FlightObserver(characterStateHolder);
            AnimationController animationController = new AnimationController(characterStateHolder);
            PlayerHorizontalDirection horizontalDirection = new PlayerHorizontalDirection();
            AttackController attackController = new AttackController(characterStateHolder, gamePlaySettings, 
                horizontalDirection, energyController);
            PlayerHealth playerHealth = new PlayerHealth(gamePlaySettings);
            PlayerSatiety playerSatiety = new PlayerSatiety(gamePlaySettings);
            CharOnGroundChecker onGroundChecker = new CharOnGroundChecker(characterStateHolder);
            ScoreController scoreController = new ScoreController();
            PickUpController pickUpController = new PickUpController(playerSatiety, scoreController, energyController);
            OnTakeDamageVisualiser damageVisualiser = new OnTakeDamageVisualiser(playerHealth);
            TimeController timeController = new TimeController();
            VictoryController victoryController = new VictoryController(playerSatiety, playerHealth, timeController);
            LevelProgressController levelProgressControler = new LevelProgressController(gamePlaySettings, playerHealth,
                playerSatiety, scoreController, timeController, victoryController);

            TouchInputController touchInputController = new TouchInputController(characterStateHolder, 
                walkController, jumpController, jumpPainter, horizontalDirection, attackController);

            IBodyUser[] bodyUsers = new IBodyUser[]
            {
                walkController,
                jumpController,
                jumpPainter,
                flightObserver,
                animationController, 
                horizontalDirection,
                attackController,
                playerHealth
            };

            PlayerCharacterController playerCharacterController = new PlayerCharacterController(characterStateHolder, 
                gamePlaySettings, touchInputController, playerHealth, playerSatiety, energyController, bodyUsers);

            NpcManager npcManager = new NpcManager();
            TimeRemainingController timeRemainingController = new TimeRemainingController();

            _executeControllers = new IExecutable[]
            {
                touchInputController,
                walkController,
                jumpPainter,
                flightObserver,
                npcManager,
                energyController,
                damageVisualiser,


                timeRemainingController
            };

            SceneController sceneController = new SceneController();

            Services.Instance.GameStateManager.SetControllers(playerCharacterController, sceneController, npcManager,
                levelProgressControler, timeController, victoryController);
            Services.Instance.CharacterIntermediary.SetControllers(pickUpController, scoreController, onGroundChecker);
            Ui.HuntScreenBehaviour huntScreenBehaviour = Services.Instance.UiFactory.GetHuntScreen();
            huntScreenBehaviour.SetControllers(playerHealth, energyController, playerSatiety, timeController, 
                scoreController, levelProgressControler, victoryController);
        }

    }
}
