namespace Dragoraptor
{
    public interface ILiveCycleHolder
    {
        void AddExecutable(IExecutable executable);
        void AddInitializable(IInitializable initializable);
        void AddCleanable(ICleanable cleanable);
    }
}