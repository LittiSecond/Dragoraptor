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

        public GameStateManager GameStateManager { get; private set; }
        public UiManager UiManager { get; private set; }

        #endregion


        #region Methods

        private void Initialize()
        {
            GameStateManager = new GameStateManager();
            UiManager = new UiManager();


            GameStateManager.SetUiManager(UiManager);
        }

        #endregion
    }
}
