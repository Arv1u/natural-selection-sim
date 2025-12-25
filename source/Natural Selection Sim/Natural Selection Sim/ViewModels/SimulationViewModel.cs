using Natural_Selection_Sim.MVVM;
using System.Timers;
namespace Natural_Selection_Sim.ViewModels
{
    public class SimulationViewModel : PropertyChangedBase
    {

		static public LineChartViewModel LineChartViewModel { get; } = new();
		public Species Herbivore { get; } = new("Herbivore", LineChartViewModel,1000,0.1);
		public Species Omnivore { get; } = new("Omnivore", LineChartViewModel, 2, 0.2);
		public Species Carnivore { get; } = new("Carnivore", LineChartViewModel, 3, 0.3);
		private bool _isRunning => timer.Enabled;
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

		public SimulationViewModel()
		{
			timer.AutoReset = true;
			timer.Elapsed += OnTimerTick;


			StartCommand = new RelayCommand(_ => StartSimulation());
			PauseCommand = new RelayCommand(_ => PauseSimulation());
			ResetCommand = new RelayCommand(_ => ResetSimulation());
        }

		
		private void OnTimerTick(Object? source, ElapsedEventArgs e)
		{
			Herbivore.Update();
			Carnivore.Update();
			Omnivore.Update();
			CurrentTimeStep++;
		}
		private void StartSimulation()
		{
			if (timer.Enabled)
				return;
			timer.Enabled = true;
			
			if (Herbivore.IsEnabled)
			{
				Herbivore.Start();
			}
			if (Omnivore.IsEnabled)
			{

				Omnivore.Start();
			}
			if (Carnivore.IsEnabled)
			{
				Carnivore.Start();
			}
		}
		private void PauseSimulation()
		{
			timer.Enabled = !timer.Enabled;
		}
		private void ResetSimulation()
		{
			LineChartViewModel.Reset();
			Herbivore.Reset();
			Carnivore.Reset();
			Omnivore.Reset();
			CurrentTimeStep = 0;
			timer.Enabled = false;
		}
	}
}
