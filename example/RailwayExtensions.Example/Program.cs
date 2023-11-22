// See https://aka.ms/new-console-template for more information
using RailwayExtensions;
using RailwayExtensions.Extensions;

Console.WriteLine("Hello, World!");

var result1 = await true.ToResult()
        .OnSuccess(x => Deneme.DoSomethingReturnNothing(x))
        .OnSuccess(x => Deneme.DoSomethingReturnT(x))
        .OnBoth(x => Console.WriteLine(x))
        .OnSuccessAsync(async x =>
        {
            await Deneme.DoSomethingReturnTaskAsync(x);

            return x;
        })
        .OnSuccessAsync(async x => await Deneme.DoSomethingReturnTaskTAsync(x))
        .OnSuccessTry(x => Deneme.DoSomethingReturnNothingThrowException(x))
        .OnBoth(x => Console.WriteLine(x))
        .OnSuccessTry(x => Deneme.DoSomethingReturnTThrowException(x))
        .OnSuccessTryAsync(async x =>
        {
            await Deneme.DoSomethingReturnTaskThrowExceptionAsync(x);

            return x;
        })
        .OnSuccessTryAsync(async x => await Deneme.DoSomethingReturnTaskTThrowExceptionAsync(x))
        ;

if (result1.IsFailure)
{
    Console.WriteLine($"Result1: {result1.Error}");
}


var result2 = await true.ToResult()
        .OnSuccess(x => DoSomething(x))
        .OnBoth(x => Console.WriteLine(x))
        .OnSuccessTry(x => DoSomething2(x), y =>
        {
            return "Error Handled.";
        })
        .OnSuccessAsync(async (x) =>
        {
            await DoSomething3(x);

            return x;
        })
        .OnSuccessAsync(x => DoSomething5(x))
        .OnSuccessAsync(async x => await DoSomething4(x))
        ;

if (result2.IsFailure)
{
    Console.WriteLine($"Result2: {result2.Error}");
}

var result3 = await "murat".ToResult()
        .OnFailure(x => DoSomething(x))
        .OnBoth(x => Console.WriteLine(x))
        .OnFailureTry(x => DoSomething2(x))
        .OnFailureAsync(async x =>
        {
            await DoSomething3(x);

            return x;
        })
        .OnSuccessTry(x => DoSomething2(x))
        .OnBoth(x => Console.WriteLine(x))
        .OnBothAsync(async x =>
        {
            await Task.Delay(1000);

            return x;
        })
        .OnSuccessTry(x => DoSomething2(x))
        .OnFailureAsync(async x => await DoSomething4(x))
        ;

if (result3.IsFailure)
{
    Console.WriteLine($"Result3: {result3.Error}");
}

Result<Person> person = new Person();
Maybe<Person> person2 = new Person();

Console.ReadKey();

void DoSomething<T>(T value)
{

}

T DoSomething2<T>(T value)
{
    throw new Exception("Crashhhh...");

    return value;
}

async Task DoSomething3<T>(T value)
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

public static class Deneme
{
    public static void DoSomethingReturnNothing<T>(T value)
    {
        Console.WriteLine("Process DoSomethingReturnNothing");
    }

    public static T DoSomethingReturnT<T>(T value)
    {
        Console.WriteLine("Process DoSomethingReturnNothing");

        return value;
    }

    public static async Task DoSomethingReturnTaskAsync<T>(T value)
    {
        Console.WriteLine("Process DoSomethingReturnTaskAsync");

        await Task.Delay(1);
    }

    public static async Task<T> DoSomethingReturnTaskTAsync<T>(T value)
    {
        Console.WriteLine("Process DoSomethingReturnTaskTAsync");

        await Task.Delay(1);

        return value;
    }

    public static void DoSomethingReturnNothingThrowException<T>(T value)
    {
        Console.WriteLine("Process DoSomethingReturnNothingThrowException");

        throw new Exception("Crashhhh... on DoSomethingReturnNothingThrowException");
    }

    public static T DoSomethingReturnTThrowException<T>(T value)
    {
        Console.WriteLine("Process DoSomethingReturnTThrowException");

        throw new Exception("Crashhhh... on DoSomethingReturnTThrowException");

        return value;
    }

    public static async Task DoSomethingReturnTaskThrowExceptionAsync<T>(T value)
    {
        Console.WriteLine("Process DoSomethingReturnTaskThrowExceptionAsync");

        throw new Exception("Crashhhh... on DoSomethingReturnTaskThrowExceptionAsync");

        await Task.Delay(1);
    }

    public static async Task<T> DoSomethingReturnTaskTThrowExceptionAsync<T>(T value)
    {
        Console.WriteLine("Process DoSomethingReturnTaskTThrowExceptionAsync");

        await Task.Delay(1);

        throw new Exception("Crashhhh... on DoSomethingReturnTaskTThrowExceptionAsync");

        return value;
    }
}

public class Person
{

}