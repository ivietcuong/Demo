
using CommunityToolkit.Mvvm.ComponentModel;

using Demo.Net.Maui.UIApp.Models;
using Demo.Net.Maui.UIApp.Views;

namespace Demo.Net.Maui.UIApp
{
	public partial class MainPageViewModel : ObservableObject
	{
		[ObservableProperty]
		private List<Person> _people = new List<Person>();

		[ObservableProperty]
		private Person _selectedPerson;

		[ObservableProperty]
		private object _workspace;

		public MainPageViewModel()
		{

			People = new List<Person>()
			{
				new Person() { Name = "Steve", Age = 21, Location = "USA"  },
				new Person() { Name = "John", Age = 37, Location = "USA"  },
				new Person() { Name = "Tom", Age = 42 ,Location = "UK" } ,
				new Person() { Name = "Lucas", Age = 39 ,Location = "Germany" } ,
				new Person() { Name = "Jane", Age = 30 ,Location = "UK" }
			};

			Workspace = new HomeView();
		}
		partial void OnSelectedPersonChanged(Person value)
		{
		}
	}
}
