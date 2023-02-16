
namespace Dragoraptor
{
    public sealed class Controllers
    {
        #region Fields

        private readonly IExecuteble[] _executeControllers;

        #endregion


        #region Properties
        public int Length => _executeControllers.Length;
        public IExecuteble this[int index] => _executeControllers[index];

        #endregion


        #region ClassLifeCycles

        public Controllers()
        {
            _executeControllers = new IExecuteble[0];


        }

        #endregion

    }
}
