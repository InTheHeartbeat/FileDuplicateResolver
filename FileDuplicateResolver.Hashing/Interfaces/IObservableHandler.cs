using System;
using FileDuplicateResolver.Hashing.Models;

namespace FileDuplicateResolver.Hashing.Interfaces
{
    public interface IObservableHandler<out TObserveModel>
    {
        event Action<TObserveModel> ActionCompleted;
    }
}