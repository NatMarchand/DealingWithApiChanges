# Dealing with API Changes
When creating NuGet packages or distributing assemblies, it is very important to ensure that the contract is not broken (or at least in a compatible manner).

This solution shows how to detect and ensure that the contract is preserved through a unit test.

## Implement it

In a unit test project (here xUnit but NUnit works also fine), add the following nuget packages :
```xml
<PackageReference Include="ApprovalTests" Version="3.0.14" />
<PackageReference Include="PublicApiGenerator" Version="7.0.0" />
```

Then configure ApprovalTests to use your test framework with an assembly attribute (in AssemblyInfo.cs for example).
```csharp
[assembly: UseReporter(typeof(XUnit2Reporter))]
```

You can then create a test that use a type of the assembly that you want to ensure the contract is not broken.
```csharp
public class ApprovalTests
{
    [Fact]
    public void TestApi()
    {
        var publicApiSnapshot = PublicApiGenerator.ApiGenerator.GeneratePublicApi(typeof(MyTypeInAssembly).Assembly);
        Approvals.Verify(publicApiSnapshot);
    }
}
```

## Making it work
When the test runs, it generates a text file next to the test file named `<TestClass>.<TestName>.received.txt` containing all the public members of the API.
It is then compared to `<TestClass>.<TestName>.approved.txt` (that must be commited/checked-in) which is the API reference.

When there is a mismatch (or at first run), the test fails and the differences between received and approved files must be examined. If the modification is desired, replace the approved file with the received file and commit/check-in it.

Now, all the modification to the public API can be audited with this file.
