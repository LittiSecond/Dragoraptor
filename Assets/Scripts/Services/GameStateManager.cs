using Dragoraptor.Ui;

namespace Dragoraptor
{
    public sealed class GameStateManager
    {
        #region PrivateData

        private enum GameState
        {
            None        = 0,
            MainScreen  = 1,
            Hunt        = 2
        }

        #endregion


        #region Fields

        private UiManager _uiManager;
        private PlayerCharacterController _characterController;
        private SceneController _sceneController;
        private NpcManager _npcManager;

        private GameState _state;

        #endregion


        #region Methods

        public void SetControllers(PlayerCharacterController pcc, SceneController sc, NpcManager nm)
        {
            _characterController = pcc;
            _sceneController = sc;
            _npcManager = nm;
        }

        public void SetMainScreenAtStartGame()
        {
            if (_state == GameState.None)
            {
                _uiManager.SwichToMainScreen();
                _state = GameState.MainScreen;
                _sceneController.SetMainScreenScene();
            }
        }

        public void SetUiManager(UiManager manager)
        {
            _uiManager = manager;
        }

        public void SwitchToMainScreen()
        {
            if (_state == GameState.Hunt)
            {
                _state = GameState.MainScreen;


                _npcManager.StopNpcSpawn();
                _npcManager.ClearNpc();
                DeactivateCharacterControll();
                _characterController.RemoveCharacter();
                _sceneController.SetMainScreenScene();
                _uiManager.SwichToMainScreen();
            }
        }

        public void SwitchToHunt()
        {
            if (_state == GameState.MainScreen)
            {
                _state = GameState.Hunt;
                Services.Instance.GameProgress.ChooseNextLevel();

                _uiManager.SwichToHuntScreen();

                _sceneController.BuildLevel();
                _characterController.CreateCharacter();
                ActivateCharacterControll();
                _npcManager.PrepareNpcSpawn();
                _npcManager.RestartNpcSpawn();
            }
        }

        public void CharacterKilled()
        {
            SwitchToMainScreen();
        }

        private void ActivateCharacterControll()
        {
            _characterController.CharacterControllOn();
        }

        private void DeactivateCharacterControll()
        {
            _characterController.CharacterControllOff();
        }

        #endregion

    }
}
