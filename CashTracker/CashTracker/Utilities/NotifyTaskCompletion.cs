using System;
using System.ComponentModel;
using System.Threading.Tasks;

/// <summary>
/// Class for wrapping <see cref="Task"/> objects and make them bindable
/// </summary>
/// <remarks>Adapted from https://docs.microsoft.com/en-us/archive/msdn-magazine/2014/march/async-programming-patterns-for-asynchronous-mvvm-applications-data-binding </remarks>
/// <typeparam name="TResult"></typeparam>
public sealed class NotifyTaskCompletion<TResult> : INotifyPropertyChanged
{
    public Task<TResult> Task { get; private set; }
    public TResult Result => (Task.Status == TaskStatus.RanToCompletion) ? Task.Result : default(TResult);
    public TaskStatus Status => Task.Status;
    public bool IsCompleted => Task.IsCompleted;
    public bool IsNotCompleted => !Task.IsCompleted;
    public bool IsSuccessfullyCompleted => Task.Status == TaskStatus.RanToCompletion;
    public bool IsCanceled => Task.IsCanceled;
    public bool IsFaulted => Task.IsFaulted;
    public AggregateException Exception => Task.Exception;
    public Exception InnerException => (Exception == null) ? null : Exception?.InnerException;
    public string ErrorMessage => (InnerException == null) ? null : InnerException.Message;

    public NotifyTaskCompletion(Task<TResult> task)
    {
        Task = task;
        if (!task.IsCompleted)
        {
            var _ = WatchTaskAsync(task);
        }
    }
    private async Task WatchTaskAsync(Task task)
    {
        try
        {
            await task;
        }
        catch
        {
        }
        var propertyChanged = PropertyChanged;
        if (propertyChanged == null)
            return;

        propertyChanged(this, new PropertyChangedEventArgs("Status"));
        propertyChanged(this, new PropertyChangedEventArgs("IsCompleted"));
        propertyChanged(this, new PropertyChangedEventArgs("IsNotCompleted"));

        if (task.IsCanceled)
        {
            propertyChanged(this, new PropertyChangedEventArgs("IsCanceled"));
        }
        else if (task.IsFaulted)
        {
            propertyChanged(this, new PropertyChangedEventArgs("IsFaulted"));
            propertyChanged(this, new PropertyChangedEventArgs("Exception"));
            propertyChanged(this,
              new PropertyChangedEventArgs("InnerException"));
            propertyChanged(this, new PropertyChangedEventArgs("ErrorMessage"));
        }
        else
        {
            propertyChanged(this,
              new PropertyChangedEventArgs("IsSuccessfullyCompleted"));
            propertyChanged(this, new PropertyChangedEventArgs("Result"));
        }
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;
}