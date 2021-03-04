# ResultOf

This project enables dot net methods to return an indication of success or failure, for any method return type (including void).
It shouldn't be used instead of exceptions, but rather enable a method to return a failure indication in non-exceptional circumstances.

Use `Result` to enable void methods to return an indication of success or failure, 
and `Result<T>` to enable non-void methods to do the same.

Both `Result` and `Result<T>` overloads the `&` and `|` operators as well as the `true` and `false` operators, 
meaning you can easily combine results in a short-circute manner for easy validations.  
### Usage example:

**Return a *`Result`* from a method:**
```csharp
Result DoSomething()
{
    // some code...
    return condition
        ? Result.Success()
        : Result.Fail("Something went wrong...");
}
```

**Return a *`Result<T>`* from a method:**
```csharp
Result<int> DoSomethingAndReturnAnInt()
{
    // some code...
    return condition
        ? Result<int>.Success(5)
        : Result<int>.Fail("Something went wrong...");
}
```

**Consume a method that returns a *`Result`***
```csharp
void DoIfMethodSucceeded()
{
    var result = DoSomething();
    if(!result.Succeeded)
    {   
        // Something went wrong, do something with result.ErrorDescription 
        // log or show the user or whatever
        return;
    }
        // Everything is fine, you can go on with your code
    }
```

**Consume a method that returns a *`Result<T>`***
```csharp
bool DoIfMethodSucceeded()
{
    var result = DoSomethingAndReturnAnInt();
    if(!result.Succeeded)
    {   
        // Something went wrong, do something with result.ErrorDescription 
        // log or show the user or whateverlog or show the user or whatever
        return false;
    }
        var intValue = result.Value;
        // Everything is fine, you can go on with your code
    }
```

**And a validation usage example:**
```csharp
Result<SomeObject> Validate(SomeObject someObject)
{
    
    return Validate("someObject is null.", d => d is object) 
        && Validate("someObject has no SomeProperty.", d => d.SomeProperty is object) 
        && Validate("SomeProperty is invalid.", d => d.SomeProperty.IsValid) 
        && Validate("SomeCollection is empty.", d => (d.SomeCollection?.Count ?? 0) > 0);

    Result<SomeObject> Validate(string errorMessage, Predicate<SomeObject> predicate)
    {
        var isValid = predicate(someObject);
        if (!isValid)
        {
            // Log non-exceptional error here...
        }
        return isValid 
            ? Result<SomeObject>.Success(rbMatch) 
            : Result<SomeObject>.Fail(errorMessage);
    }
}
```
