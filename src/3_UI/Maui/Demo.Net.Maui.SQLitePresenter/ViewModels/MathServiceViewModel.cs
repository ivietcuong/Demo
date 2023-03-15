using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

using Demo.Net.Maui.Shared.Services;
using Demo.NetStandard.Core.Services;

using Microsoft.Extensions.Logging;

using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;

using Point = Demo.NetStandard.Core.Entities.Point;

namespace Demo.Net.Maui.SQLitePresenter.ViewModels
{
	public partial class MathServiceViewModel : ObservableRecipient, INotifyDataErrorInfo
	{
		private readonly Dictionary<string, List<string>> _errors = new();
		private readonly IPointService _pointService;
		protected ILogger? Logger;

		[ObservableProperty]
		private double _coefficientA = 1;

		[ObservableProperty]
		private double _coefficientB = 2;

		[ObservableProperty]
		private double _coefficientC;

		[ObservableProperty]
		private IMathService? _mathService;

		[ObservableProperty]
		private ObservableCollection<Point>? _points;

		public string? Name
		{
			get => MathService?.Name;
		}

		public string? Message
		{
			get
			{
				var result = string.Join(Environment.NewLine, GetErrors().Cast<string>().Select(e => e));
				return result;
			}
		}

		public bool HasErrors => _errors.Any();

		public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

		public MathServiceViewModel(IPointService pointService)
		{
			IsActive = true;
			_pointService = pointService;
		}

		public IEnumerable<Point> Calculate(IEnumerable<Point> points)
		{
			if (MathService == null)
				return points;

			return MathService.Calculate(points, CoefficientA, CoefficientB, CoefficientB);
		}

		partial void OnCoefficientAChanged(double value)
		{
			var result = MathService?.Validate(value, CoefficientB, CoefficientC);

			if (string.IsNullOrEmpty(result))
				_errors.Remove(nameof(CoefficientA));
			else
				_errors.Add(nameof(CoefficientA), new List<string>() { result });

			CalculateCommand.NotifyCanExecuteChanged();
			ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nameof(CoefficientA)));
			OnPropertyChanged(nameof(Message));
		}

		partial void OnCoefficientBChanged(double value)
		{
			var result = MathService?.Validate(CoefficientA, value, CoefficientC);

			if (string.IsNullOrEmpty(result))
				_errors.Remove(nameof(CoefficientB));
			else
				_errors.Add(nameof(CoefficientA), new List<string>() { result });

			CalculateCommand.NotifyCanExecuteChanged();
			ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nameof(CoefficientB)));
			OnPropertyChanged(nameof(Message));
		}

		protected override void OnActivated()
		{
			Messenger.Register(this, (MessageHandler<MathServiceViewModel, MathServiceChangedMessage>)(async (r, m) =>
			{
				MathService = m.Value;
				await Reset();
			}));
		}

		private async Task Reset()
		{
			CoefficientA = 1;
			CoefficientB = 2;
			CoefficientC = 0;

			var points = await _pointService.GetPointListAsync();
            var meanvalue = points.Count() / 2;
            Points = new ObservableCollection<Point>(points.Select(p => new Point() { X = p.X - meanvalue, Y = p.Y }));

			OnPropertyChanged(nameof(Name));
			CalculateCommand.NotifyCanExecuteChanged();
		}

		protected override void OnDeactivated()
		{
			Messenger.Unregister<MathServiceChangedMessage>(this);
		}

		[RelayCommand(CanExecute = nameof(CanExecute))]
		private void Calculate()
		{
			if (MathService != null)
				Points = new ObservableCollection<Point>(MathService.Calculate(Points, CoefficientA, CoefficientB, CoefficientC));
		}

		private bool CanExecute()
		{
			return MathService != null && !HasErrors;
		}

		public IEnumerable GetErrors(string? propertyName = null)
		{
			if (!string.IsNullOrEmpty(propertyName))
				return _errors[propertyName];

			var errors = _errors.Values.SelectMany(e => e);
			return errors;
		}
	}
}
