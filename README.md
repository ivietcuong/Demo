## Demo

<img src="demo_blazor_wpf_uwp.png" alt="NuGet Downloads" />
My first project was a Silverlight application where we used technologies such as Entity Framework, Prism, WCF, and various design patterns. Since then, I have continually deepened my knowledge of design patterns and software architecture.

Most of the projects I worked on were based on WPF, so I frequently used Prism and Entity Framework. However, for the past three years, I haven't worked with WPF. Instead, I’ve been developing web applications using Blazor Server. While I enjoy working with web technologies, I do miss building WPF applications.

In recent years, Microsoft has introduced many advancements and improvements - such as .NET 6, .NET 7, Blazor, MAUI, and built-in Dependency Injection. This made me wonder: how does application development work today without using Prism? Prism supports WPF, MAUI, and Xamarin, but not web applications. I was curious to see how far I could get using only Microsoft libraries and frameworks.

That curiosity led me to create this demo project, aimed at exploring modern development patterns without relying on third-party frameworks like Prism.
## Project Overview
#### The Core Layer holds the business model, which includes:
- A Point class.
- A Point repository.
- A IPointService, to get Point-list from Repository.
- A IMathSercive to calculate point list.
 
#### The Infrastructure projects contain the implementations of Repository (different imlemetations for json, xml or EF Core), of Service...etc.
#### The user interface layers are the entry point for the application (WPF, MAUI, Web)

Applied Technologies: 
- Clean Architecture
- .NET Standard 2.0 for Core project. It contains Entities classes, Interface and Service, the other Project use this Library project.
- .NET 7.0 for Infrastructure, which implements Core.
- MVVM Toolkit, Microsoft ServiceCollection
- NLog, OxyPlot, Plotly for Logging and Plotting.
- WPF, MAUI, Blazor Server.
## WPF
<span/><img src="demo_wpf.png" width="50%" height="50%" />

The application is built using a modular architecture. There are separate modules for JSON and XML, each implementing different logic - for example, retrieving data from XML or JSON sources, or using different implementations of the MathService to demonstrate flexibility. Each module also includes its own views.

The main page of the application functions as a shell, into which all modules are integrated. This modular structure makes the application easy to extend and maintain. While Prism offers more advanced support for modularity, using Microsoft’s ServiceCollection also works quite well for this purpose.

### MAUI
<span/><p><img src="demo_uwp.png" width="50%" height="50%" /> <img src="android.png" width="20%" height="20%" /></p>

There is a single shared view for both Android and Windows platforms. They use a common shared project that contains the same controls and logic implementations, enabling consistent behavior and appearance across platforms.

## Blazor
<span/><p><img src="blazors.jpeg" width="50%" height="50%" /></p>

The solution consists of UI applications and a shared project. The shared project contains reusable controls and logic, making it easy to maintain and extend across different platforms. The UI layer is implemented as a Blazor Server application. If needed, the architecture allows for easy extension with a separate API application to support broader scenarios.

## Conclusion

Apart from logging and plotting (where I used third-party libraries), the entire project is built using Microsoft frameworks — and it works well. Of course, the choice of tools and architecture always depends on the project context: whether it's small or large, requires rapid development, or has long - term maintainability needs. Still, this demo shows that a clean, modular, and maintainable architecture can be achieved using only Microsoft's native tools and libraries.

## How to run
#### WPF start Demo.Net.WpfApp
#### MAUI start Demo.Net.Maui.UIAPP
#### Web start Demo.Net.Blazor.UIAPP

## License

MIT
