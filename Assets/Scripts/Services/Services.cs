using System;

using Dragoraptor.Ui;


namespace Dragoraptor
{
    public sealed class Services
    {

        private static readonly Lazy<Services> _instance = new Lazy<Services>();


        public Services()
        {
            Initialize();
        }


        public static Services Instance => _instance.Value;

        public SceneGeometry SceneGeometry { get; private set; }
        public GameStateManager GameStateManager { get; private set; }
        public UiManager UiManager { get; private set; }
        public GameProgress GameProgress { get; private set; }
        public ObjectPool2 ObjectPool { get; private set; }
        public CharacterIntermediary CharacterIntermediary { get; private set; }
        public UiFactory UiFactory { get; private set; }
        public UpdateService UpdateService { get; private set; }


        private void Initialize()
        {
            SceneGeometry = new SceneGeometry();
            GameStateManager = new GameStateManager();
            UiManager = new UiManager();
            GameProgress = new GameProgress();
            ObjectPool = new ObjectPool2();
            CharacterIntermediary = new CharacterIntermediary();
            UiFactory = new UiFactory();
            UpdateService = new UpdateService();

            GameStateManager.SetUiManager(UiManager);
        }

    }
}
