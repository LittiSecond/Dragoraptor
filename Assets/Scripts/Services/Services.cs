using System;

using Dragoraptor.Ui;


namespace Dragoraptor
{
    public sealed class Services
    {
        #region Fields

        private static readonly Lazy<Services> _instance = new Lazy<Services>();

        #endregion


        #region ClassLifeCycles

        public Services()
        {
            Initialize();
        }

        #endregion


        #region Properties

        public static Services Instance => _instance.Value;

        public SceneGeometry SceneGeometry { get; private set; }
        public GameStateManager GameStateManager { get; private set; }
        public UiManager UiManager { get; private set; }
        public GameProgress GameProgress { get; private set; }
        public ObjectPool ObjectPool { get; private set; }
        public CharacterIntermediary CharacterIntermediary { get; private set; }
        public UiFactory UiFactory { get; private set; }

        #endregion


        #region Methods

        private void Initialize()
        {
            SceneGeometry = new SceneGeometry();
            GameStateManager = new GameStateManager();
            UiManager = new UiManager();
            GameProgress = new GameProgress();
            ObjectPool = new ObjectPool();
            CharacterIntermediary = new CharacterIntermediary();
            UiFactory = new UiFactory();

            GameStateManager.SetUiManager(UiManager);
        }

        #endregion
    }
}
