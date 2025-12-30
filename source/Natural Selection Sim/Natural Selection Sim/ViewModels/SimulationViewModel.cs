using Natural_Selection_Sim.MVVM;
using SkiaSharp;
using System.Diagnostics;

namespace Natural_Selection_Sim.ViewModels
{
    public class SimulationViewModel : PropertyChangedBase
    {
		static public LineChartViewModel LineChartViewModel { get; } = new();
		public SpeciesData Herbivore { get; } = new("Herbivore",SKColors.Green, LineChartViewModel);
		public SpeciesData Omnivore { get; } = new("Omnivore", SKColors.Orange, LineChartViewModel);
		public SpeciesData Carnivore { get; } = new("Carnivore", SKColors.Red, LineChartViewModel);

		private int timeStepsPerSecond;

		public int TimeStepsPerSecond
		{
			get { return timeStepsPerSecond; }
			set 
			{
				timeStepsPerSecond = value;
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
		private bool isRunning;
		public bool IsRunning
		{
			get
			{
				return isRunning;
			}
			set
			{
				if (value == isRunning)
					return;

				isRunning = value;
				OnPropertyChanged(nameof(IsRunning));
				RunningRaiseExecuteChanged();
			}
		}
		public RelayCommand StartCommand { get; } 
		public RelayCommand PauseCommand { get; }
		public RelayCommand ResetCommand { get; }

		public SimulationViewModel()
		{
			StartCommand = new RelayCommand(_ => StartSimulation());
			PauseCommand = new RelayCommand(_ => PauseSimulation());
			ResetCommand = new RelayCommand(_ => ResetSimulation(), _ => !IsRunning);
			IsRunning = false;
        }
		private void Run() // call this method to simulate one time step
		{
			//Herbivore.Update();
			//Omnivore.Update();
			//Carnivore.Update();
			//call these with appropiately calculated data to update the UI
		}
		private void StartSimulation()
		{
			Debug.WriteLine("Startcmd run");
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
			
			IsRunning = true ;
		}
		private void PauseSimulation()
		{
            Debug.WriteLine("Pausecmd run");
            IsRunning = false;
        }
        private void ResetSimulation()
		{
            Debug.WriteLine("Resetcmd run");
			LineChartViewModel.Reset();
			Herbivore.Reset();
			Carnivore.Reset();
			Omnivore.Reset();
		}
		private void RunningRaiseExecuteChanged()
		{
            ResetCommand.RaiseCanExecuteChanged();

        }
    }
}
