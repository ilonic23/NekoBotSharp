# NekoBotSharp

A library that allows you to interact with the NekoBot API in C#.
[API Documentation](https://docs.nekobot.xyz/)

Now it only supports getting an image. ( not generating )

Getting image URL:
```csharp
using System;
using NekoBotSharp;

class Program {
    static async Task Main(string[] args) {
        string url = await NekoBotSharp.GetImageUrl(NekoImageTypes.Neko);
        Console.WriteLine(url);
    }
}
```
