# ResultOf

This project enables c# methods to return an indication of success or failure, for any method return type (including void).
It shouldn't be used instead of exceptions, but rather enable a method to return a failure indication in non-exceptional circumstances.

Use `Result` to enable void methods to return an indication of success or failure, 
and `Result<T>` to enable non-void methods to do the same.

Both `Result` and `Result<T>` overloads the `&` and `|` operators as well as the `true` and `false` operators, 
meaning you can easily combine results in a short-circute manner for easy validations.
Usage example:

```scharp
internal Result<SomeObject> Validate(SomeObject someObject)
{
    
    return Validate("someObject is null.", d => d is object) 
        && Validate("Raw data has no SomeProperty.", d => d.SomeProperty is object) 
        && Validate("SomeProperty is invalid.", d => d.SomeProperty.IsValid) 
        && Validate("SomeCollection is empty.", d => (d.SomeCollection?.Count ?? 0) > 0);

    Result<SomeObject> Validate(string errorMessage, Predicate<SomeObject> predicate)
    {
        var isValid = predicate(someObject);
        if (!isValid)
        {
            // Log non-exceptional error here...
        }
        return isValid ? Result<SomeObject>.Success(rbMatch) : Result<SomeObject>.Fail(errorMessage);
    }
}
```
