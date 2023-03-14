using CommunityToolkit.Mvvm.ComponentModel;

using Demo.NetStandard.Core.Services;

using Microsoft.Extensions.Logging;

namespace Demo.Net.Maui.SQLitePresenter.ViewModels
{
	public partial class MathServiceViewModel : ObservableValidator
	{
		protected ILogger? Logger;

		public string? Name
		{
			get => MathService?.Name;
			set
			{
				if (MathService == null)
					return;

				MathService.Name = value;
				OnPropertyChanged();
			}
		}
		public string Message
		{
			get => string.Join(Environment.NewLine, GetErrors().Select(e => e.ErrorMessage));
		}
		public string? Description
		{
			get => MathService?.Description;
			set
			{
				if (MathService == null)
					return;

				MathService.Description = value;
				OnPropertyChanged();
			}
		}
		public IMathService? MathService
		{
			get;
			protected set;
		}
		public virtual double CoefficientA
		{
			get;
			set;
		}
		public virtual double CoefficientB
		{
			get;
			set;
		}
		public virtual double CoefficientC
		{
			get;
			set;
		}
		public virtual IEnumerable<NetStandard.Core.Entities.Point> Calculate(IEnumerable<NetStandard.Core.Entities.Point> points)
		{
			if (MathService == null)
				return points;

			return MathService.Calculate(points, CoefficientA, CoefficientB, CoefficientB);
		}
	}
}
