// See https://aka.ms/new-console-template for more information
using RailwayExtensions;
using RailwayExtensions.Extensions;

Console.WriteLine("Hello, World!");


_ = await Result.Create(true)
        .OnSuccess(x => DoSomething(x))
        .OnBoth(x => Console.WriteLine(x))
        .OnSuccessAsync(x => DoSomething2(x))
        .OnSuccessAsync(x => DoSomething3(x))
        .OnSuccessAsync(x => DoSomething5(x))
        .OnSuccessAsync(async x => await DoSomething4(x))
        ;

_ = await Result.Create("murat")
        .OnFailure(x => DoSomething(x))
        .OnBoth(x => Console.WriteLine(x))
        .OnFailureAsync(x => DoSomething2(x))
        .OnFailureAsync(x => DoSomething3(x))
        .OnFailureAsync(async x => await DoSomething4(x))
        ;


void DoSomething<T>(T value)
{
    
}

T DoSomething2<T>(T value)
{
    return value;
}

async void DoSomething3<T>(T value)
{
    await Task.Delay(1);
}

async Task<T> DoSomething4<T>(T value)
{
    return await Task.FromResult(value);
}

async Task<T> DoSomething5<T>(T value)
{
    await Task.Delay(1);

    return value;
}