using System.Collections.Generic;


namespace Dragoraptor
{
    public sealed class UpdateService
    {

        private List<IExecutable> _executeList;


        public void SetListToExecute(List<IExecutable> list)
        {
            _executeList = list;
        }

        public void AddToUpdate(IExecutable executable)
        {
            if (executable != null)
            {
                if (!_executeList.Contains(executable))
                {
                    _executeList.Add(executable);
                }
            }
        }

        public void RemoveFromUpdate(IExecutable executable)
        {
            if (executable != null)
            {
                _executeList.Remove(executable);
            }
        }

    }
}
