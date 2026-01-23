using Natural_Selection_Sim.MVVM;
using SkiaSharp;
using System.Diagnostics;

namespace Natural_Selection_Sim.ViewModels
{
    public class SimulationViewModel : PropertyChangedBase
    {
		// Property backing the UI's linechart
		public LineChartViewModel LineChartViewModel { get; } = new();

		// Simulated data for each individual species.
		public SpeciesData Herbivore { get; } 
		public SpeciesData Omnivore { get; } 
		public SpeciesData Carnivore { get; } 

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
		private int availableFood;
		public int AvailableFood
		{
			get
			{
				return availableFood;
			}
			set
			{
				availableFood = value;
				OnPropertyChanged();
			}
		}
		// Relaycommands bind to the Command button property. They are executed on button click and define whether the button is active or not.
		public RelayCommand StartCommand { get; } 
		public RelayCommand PauseCommand { get; }
		public RelayCommand ResetCommand { get; }

		private readonly int defaultAvailableFood = 100;
		public SimulationViewModel()
		{
            StartCommand = new RelayCommand(_ => StartSimulation(),_ => Herbivore!.IsEnabled || Omnivore!.IsEnabled || Carnivore!.IsEnabled);
			PauseCommand = new RelayCommand(_ => PauseSimulation());
			ResetCommand = new RelayCommand(_ => ResetSimulation(), _ => !IsRunning);

			Herbivore = new("Herbivore", SKColors.Green, LineChartViewModel, this);
			Omnivore = new("Omnivore", SKColors.Orange, LineChartViewModel, this);
			Carnivore = new("Carnivore", SKColors.Red, LineChartViewModel, this);

			AvailableFood = defaultAvailableFood;

			IsRunning = false;
        }
		/// <summary>
		/// Method executed to simulate one timestep.
		/// </summary>
		private void Run() // call this method to simulate one time step
		{
			//Herbivore.Update();
			//Omnivore.Update();
			//Carnivore.Update();
			//call these with appropiately calculated data to update the UI
		}
		/// <summary>
		/// Executed when the start button is pressed.
		/// </summary>
		private void StartSimulation()
		{
			IsRunning = true ;
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
		}
		/// <summary>
		/// Executed when pressing the pause button.
		/// </summary>
		private void PauseSimulation()
		{
            Debug.WriteLine("Pausecmd run");
            IsRunning = false;
        }
		/// <summary>
		/// Executed when the reset button is pressed.
		/// </summary>
        private void ResetSimulation()
		{
            Debug.WriteLine("Resetcmd run");
			LineChartViewModel.Reset();
			Herbivore.Reset();
			Carnivore.Reset();
			Omnivore.Reset();
			AvailableFood = defaultAvailableFood;
		}
		private void RunningRaiseExecuteChanged()
		{
            ResetCommand.RaiseCanExecuteChanged();
        }
    }
}