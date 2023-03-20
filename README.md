## Demo

<img src="demo_blazor_wpf_uwp.png" alt="NuGet Downloads" />
My first Project was a Silverlight application, we used Entity Framwork, Prism, WCF and Design Pattern. I always have been deeping my knowledge in Design Pattern and Softwarearchitect since. I used to develop with Prism, and EF, because the most Project were in WPF. For about three years I have not developed any thing with WPF. I've been working with Web application (Blazor-Server) and missing a little bit working with WPF.

There are so much Changes, Improvement like .NET 6, .NET 7, Blazor, MAUI and DependencyInjection from Microsoft. I was wondering, how did it work without using Prism. (Prism suppports WPF, MAUI, Xamarin, but not Web application) and how did it work when I just used Libraries or Framework, which just come from Microsoft. That's why, I have made this Demo, just to see, how it works. 

Demo application has:
- A Point class.
- A Point repository.
- A IPointService, to get Point-list from Repository.
- A IMathSercive to calculate point list.
 
The apps get a list of point (from different sources (json, xml or database)), calculate it (with different implementing of MathService) and show it with diffrent UI (WPF, MAUI, or Web). 

Applied Technologies: 
- Clean Architecture
- .NET Standard 2.0 for Core project. It contains Entities classes, Interface and Service, the other Project use this Library project.
- .NET 7.0 for Infrastructure, which implements Core.
- MVVM Toolkit, Microsoft ServiceCollection
- NLog, OxyPlot, Plotly for Logging and Plotting.
- WPF, MAUI, Blazor server.
## WPF
<span/><img src="demo_wpf.png" width="50%" height="50%" />
Markdown is awesome. It’s a fantastic way to write content quickly without the overhead of formatting with a WYSIWYG editor
## MAUI
<span/><p><img src="demo_uwp.png" width="50%" height="50%" /> <img src="android.png" width="20%" height="20%" /></p>
Markdown is awesome. It’s a fantastic way to write content quickly without the overhead of formatting with a WYSIWYG editor
## Blazor
<span/><p><img src="blazors.jpeg" width="50%" height="50%" /></p>
Markdown is awesome. It’s a fantastic way to write content quickly without the overhead of formatting with a WYSIWYG editor
