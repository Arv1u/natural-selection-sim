using Natural_Selection_Sim.MVVM;
using System.Timers;
namespace Natural_Selection_Sim.ViewModels
{
    public class MainWindowViewModel : PropertyChangedBase
    {

		public LineChartViewModel LineChartViewModel { get; }
		private int timeStepsPerSecond;

		public int TimeStepsPerSecond
		{
			get { return timeStepsPerSecond; }
			set 
			{
				timeStepsPerSecond = value;
				int secondInMillis = 1000;
				timer.Interval = secondInMillis / value; // update timer interval on changed slider
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
		private System.Timers.Timer timer = new System.Timers.Timer(1000);

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
		public int HerbivorePopulation
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
            LineChartViewModel.AddData(new ChartData("Series2"));
            HerbivorePopulation = 0;
			data2 = 0;
			CurrentTimeStep = 0;
        }
		private int data2;
        private void OnTimerTick(Object? source, ElapsedEventArgs e)
		{
			Random rnd = new();
            HerbivorePopulation += rnd.Next(-10, 11);
			data2 += rnd.Next(-10, 11);
            LineChartViewModel.ChartData[0].AddValue(HerbivorePopulation);
            LineChartViewModel.ChartData[1].AddValue(data2);
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
