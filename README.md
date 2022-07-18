# Toolbelt.SystemResourceManager [![NuGet Package](https://img.shields.io/nuget/v/Toolbelt.SystemResourceManager.svg)](https://www.nuget.org/packages/Toolbelt.SystemResourceManager/)

This is the library for .NET that allows you to inject your custom resource manager into your .NET apps, such as Blazor, as the System Resource Manager.

That means **you can localize DataAnnotations validation error messages provided by the .NET runtime** with this library.

> **Warning**  
> ⚠️ This library touches undocumented areas and private implementations of the .NET runtime, using the "Reflection" technology. So please remember that **it might not be working on future .NET versions.**

## Usage

1. Install this library as a NuGet package.

```shell
dotnet add package Toolbelt.SystemResourceManager
```

2. Call the `AddSystemResourceManager()` extension method for a service collection at the startup of your apps with specifying the resource name you want to inject and its assembly.

```csharp
// Program.cs
...
builder.Services.AddSystemResourceManager("SampleApp.Resource1", typeof(SampleApp.Resource1).Assembly);
...
```

You can also use the `AddSystemResourceManager<TResource>()` overload version instead.

```csharp
// Program.cs
...
using SampleApp;
...
builder.Services.AddSystemResourceManager<Resource1>();
...
```

After doing that, the resource strings for the DataAnnotations validation error messages will be retrieved from the resource you specified at first. If the resource string with the specified key doesn't exist in the resource you specified, it will be retrieved from the resource that is before of you injected it.

You can call the `AddSystemsResourceManager()` extension method multiple with each different resource to inject it. The last injected resource is the most high-priority resource for retrieving resource strings.

## Example

1. Create a new Blazor application project.

2. Create a model class on your Blazor app project like below.

```cs
using System.ComponentModel.DataAnnotations;
public class ValidationTestModel
{
    [Required]
    public string RequiredField { get; set; } = "";
}
```

3. Implement a form on a Razor component on your Blazor app project.

```razor
<EditForm Model="_Model">
    <DataAnnotationsValidator/>
    <InputText @bind-Value="_Model.RequiredField" />
    <ValidationSummary />
    <button type="submit">Submit</button>
</EditForm>

@code {
    private ValidationTestModel _Model = new();
}
```

4. Prepare `Resource1.resx` and `Resource1.ja.resx` resource files on your Blazor project. Make the `Resource1.ja.resx` to be below.

Name | Value
-----|------
RequiredAttribute_ValidationError	| フィールド {0} は必須です。	

5. Inject the `Resource1` resource as a system resource.

```csharp
// Program.cs
...
using SampleApp;
...
builder.Services.AddSystemResourceManager<Resource1>();
...
```

6. Finally, you will see localized validation error messages on your Blazor app, like this.

![](https://raw.githubusercontent.com/jsakamoto/Toolbelt.SystemResourceManager/main/.assets/fig.001.png)

## Release Note

[Release notes](https://github.com/jsakamoto/Toolbelt.SystemResourceManager/blob/main/RELEASE-NOTES.txt)

## License

[Mozilla Public License Version 2.0](https://github.com/jsakamoto/Toolbelt.SystemResourceManager/blob/main/LICENSE)
