using System;

namespace TopDownAction
{
    public interface IUpdatable
    {
        public event Action OnUpdate;
    }
}