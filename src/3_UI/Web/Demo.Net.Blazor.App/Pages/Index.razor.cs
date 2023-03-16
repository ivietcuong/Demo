using Demo.Net.Blazor.Shared;

using Microsoft.AspNetCore.Components;

using System.Diagnostics;

namespace Demo.Net.Blazor.App.Pages
{
    public class Person
    {
        public string Name { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"{Title} - {Name}";
        }
    }
    public partial class Index : ComponentBase, IWorkspace
    {
        public Type Type { get => GetType(); }
        public string Name { get => nameof(Index); }
        public Dictionary<string, object?> Parameters { get; } = new Dictionary<string, object?>();


        public Index()
        {
            People = new List<Person>();
            People.Add(new Person() { Title = "Ela 01", Name = "Velden 01" });
            People.Add(new Person() { Title = "Ela 02", Name = "Velden 02" });
            People.Add(new Person() { Title = "Ela 03", Name = "Velden 03" });
            People.Add(new Person() { Title = "Ela 04", Name = "Velden 04" });
        }
        public List<Person> People { get; set; }

        private void OnSelectionChanged(Person person)
        {
            Debug.WriteLine(person);
        }
    }
}
