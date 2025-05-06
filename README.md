## Demo

<img src="demo_blazor_wpf_uwp.png" alt="NuGet Downloads" />
My first project was a Silverlight application where we used technologies such as Entity Framework, Prism, WCF, and various design patterns. Since then, I have continually deepened my knowledge of design patterns and software architecture.

Most of the projects I worked on were based on WPF, so I frequently used Prism and Entity Framework. However, for the past three years, I haven't worked with WPF. Instead, I’ve been developing web applications using Blazor Server. While I enjoy working with web technologies, I do miss building WPF applications.

In recent years, Microsoft has introduced many advancements and improvements—such as .NET 6, .NET 7, Blazor, MAUI, and built-in Dependency Injection. This made me wonder: how does application development work today without using Prism? Prism supports WPF, MAUI, and Xamarin, but not web applications. I was curious to see how far I could get using only Microsoft libraries and frameworks.

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

It is modular built. Json module and Xml module, each of them implements different logic (for instance, get data from xml, json, or different implementation for calculate from MathServer, just to demonstrate) and has own Views. The Main Page of Application is just a Shell, where all the module are put together. It is easy to extend and maintain. With Prism it is better to implement, but with ServiceCollection it works quite well.
### MAUI
<span/><p><img src="demo_uwp.png" width="50%" height="50%" /> <img src="android.png" width="20%" height="20%" /></p>

There is just one View for both Plattform Adroid, Windows. They are using the common Shared project, with the same Controls and the Logic's implementations.
## Blazor
<span/><p><img src="blazors.jpeg" width="50%" height="50%" /></p>

There are UI App and Share projects. The shared project holds the Controls, so it is reuseable. The UI is a Blazor Server, if it is nessessary, it can be extended with API app. 

## Conclusion

Except Logging and Plotting I just have been using Microsoft Frameworks and it works well. It is depending on project, it is small, it is big, it is quickly to be done...etc.

## How to run
#### WPF start Demo.Net.WpfApp
#### MAUI start Demo.Net.Maui.UIAPP
#### Web start Demo.Net.Blazor.UIAPP

## License

MIT
