using System;
using System.Threading.Tasks;


public static class TaskExtensions
{
    public async static Task<T> WithTimeout<T>(this Task<T> task, int timeoutInMilliseconds)
    {
        var retTask = await Task.WhenAny(task, Task.Delay(timeoutInMilliseconds))
            .ConfigureAwait(false);

        return retTask is Task<T> ? task.Result : default(T);
    }

    public static Task<T> WithTimeout<T>(this Task<T> task, TimeSpan timeout) =>
        WithTimeout(task, (int) timeout.TotalMilliseconds);
}
