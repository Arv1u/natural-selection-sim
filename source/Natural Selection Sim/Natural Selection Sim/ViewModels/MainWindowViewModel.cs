using Natural_Selection_Sim.MVVM;
using System.Timers;
namespace Natural_Selection_Sim.ViewModels
{
    public class MainWindowViewModel : PropertyChangedBase
    {
		private LineChartViewModel lineChartViewModel;

		public LineChartViewModel LineChartViewModel
		{
			get { return lineChartViewModel; }
			set { lineChartViewModel = value; }
		}
		private int timeStepsPerSecond;

		public int TimeStepsPerSecond
		{
			get { return timeStepsPerSecond; }
			set 
			{ 
				timeStepsPerSecond = value;
				timer.Interval = 1000 / value;
				OnPropertyChanged();
			}
		}
		private int currentTimeStep;

		public int CurrentTimeStep
		{
			get { return currentTimeStep; }
			set 
			{ 
				currentTimeStep = value;
				OnPropertyChanged();
			}
		}
		public RelayCommand StartCommand { get; } 
		public RelayCommand PauseCommand { get; }
		public RelayCommand ResetCommand { get; }
		public System.Timers.Timer timer = new System.Timers.Timer(1000);

		public MainWindowViewModel()
		{
			LineChartViewModel = new();
			timer.AutoReset = true;
			timer.Elapsed += OnTimerTick;

			SetupChartData();

			StartCommand = new RelayCommand(_ => StartSimulation());
			PauseCommand = new RelayCommand(_ => PauseSimulation());
			ResetCommand = new RelayCommand(_ => ResetSimulation());
        }
		private int data;

		public int Data
		{
			get { return data; }
			set { data = value;
				OnPropertyChanged();
			}
		}
		private void SetupChartData()
		{
			LineChartViewModel.Reset();
            LineChartViewModel.AddData(new ChartData("Series1"));
			Data = 0;
        }
        private void OnTimerTick(Object? source, ElapsedEventArgs e)
		{
			Random rnd = new();
			Data += rnd.Next(-10, 11);
            LineChartViewModel.ChartData[0].AddValue(Data);
			CurrentTimeStep++;
        }
		private void StartSimulation()
		{
			timer.Enabled = true;
		}
		private void PauseSimulation()
		{
			timer.Enabled = false;
		}
		private void ResetSimulation()
		{
			SetupChartData();
		}
	}
}
