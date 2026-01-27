using Natural_Selection_Sim.MVVM;
using SkiaSharp;
using System.Diagnostics;
using System.Windows.Threading;

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
        private SimulationController simulation;
        private SimulationStats stats;
        private int timeStepsPerSecond;

        public int TimeStepsPerSecond
        {
            get { return timeStepsPerSecond; }
            set
            {
                simulationTimer.Interval = TimeSpan.FromMilliseconds(1000.0 / value);
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
                simulationTimer.IsEnabled = value;
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

        private bool isReset;
        public bool IsReset
        {
            get
            {
                return isReset;
            }
            set
            {
                isReset = value;
                OnPropertyChanged();
            }
        }

        private readonly int defaultAvailableFood = 100;
        private readonly DispatcherTimer simulationTimer;
        public SimulationViewModel()
        {
            StartCommand = new RelayCommand(_ => StartSimulation(), _ => Herbivore!.IsEnabled || Omnivore!.IsEnabled || Carnivore!.IsEnabled);
            PauseCommand = new RelayCommand(_ => PauseSimulation());
            ResetCommand = new RelayCommand(_ => ResetSimulation(), _ => !IsRunning);

            Herbivore = new("Herbivore", SKColors.Green, LineChartViewModel, this);
            Omnivore = new("Omnivore", SKColors.Orange, LineChartViewModel, this);
            Carnivore = new("Carnivore", SKColors.Red, LineChartViewModel, this);

            AvailableFood = defaultAvailableFood;
            IsReset = true;
            IsRunning = false;
            simulationTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1),
                IsEnabled = false
            };

            simulationTimer.Tick += Run;
        }
        /// <summary>
        /// Method executed to simulate one timestep.
        /// </summary>
        private void Run(object? sender, EventArgs e) // call this method to simulate one time step
        {
            if (Herbivore.isDead && Carnivore.isDead && Omnivore.isDead)
            {
                PauseCommand.Execute(null);
                return;
            }
            simulation.Step();
            stats = simulation.GetStats();

            if (Herbivore.IsEnabled)
            {

            Herbivore.Update(stats.Herbivores.Count, Math.Round(stats.Herbivores.AvgBirthRate, 2), Math.Round(stats.Herbivores.AvgDeathRate, 2), Math.Round(stats.Herbivores.AvgMutationRate, 2), Convert.ToInt32(stats.Herbivores.AvgSpeed), Convert.ToInt32(stats.Herbivores.AvgSize));
            }
            if (Omnivore.IsEnabled)
            {

            Omnivore.Update(stats.Omnivores.Count, Math.Round(stats.Omnivores.AvgBirthRate, 2), Math.Round(stats.Omnivores.AvgDeathRate, 2), Math.Round(stats.Omnivores.AvgMutationRate, 2), Convert.ToInt32(stats.Omnivores.AvgSpeed), Convert.ToInt32(stats.Omnivores.AvgSize));
            }
            if (Carnivore.IsEnabled)
            {
            Carnivore.Update(stats.Carnivores.Count, Math.Round(stats.Carnivores.AvgBirthRate, 2), Math.Round(stats.Carnivores.AvgDeathRate, 2), Math.Round(stats.Carnivores.AvgMutationRate, 2), Convert.ToInt32(stats.Carnivores.AvgSpeed), Convert.ToInt32(stats.Carnivores.AvgSize));
            }

            //call these with appropiately calculated data to update the UI
            CurrentTimeStep++;
        }
        /// <summary>
        /// Executed when the start button is pressed.
        /// </summary>
        private void StartSimulation()
        {
            Debug.WriteLine("Starting Simulation...");

            if (IsReset)
            {
                StartPopulations();
                IsReset = false;
            }
            var config = new SimulationConfig
            {
                PlantsPerStep = AvailableFood,//Hier fehlt noch der UI Teil zum einstellen
                Herbivore = new SpeciesConfig { StartCount = Herbivore.PopulationStart, BirthRate = Herbivore.BirthRateStart, DeathRate = Herbivore.DeathRateStart, MutationRate = Herbivore.MutationRateStart, Speed = Herbivore.SpeedStart, Size = Herbivore.SizeStart },
                Omnivore = new SpeciesConfig { StartCount = Omnivore.PopulationStart, BirthRate = Omnivore.BirthRateStart, DeathRate = Omnivore.DeathRateStart, MutationRate = Omnivore.MutationRateStart, Speed = Omnivore.SpeedStart, Size = Omnivore.SizeStart },
                Carnivore = new SpeciesConfig { StartCount = Carnivore.PopulationStart, BirthRate = Carnivore.BirthRateStart, DeathRate = Carnivore.DeathRateStart, MutationRate = Carnivore.MutationRateStart, Speed = Carnivore.SpeedStart, Size = Carnivore.SizeStart }
            };

            simulation = new SimulationController(config);
            stats = new SimulationStats();
            IsRunning = true;

        }
        private void StartPopulations()
        {
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
            Debug.WriteLine("Pausing simulation...");
            IsRunning = false;
        }
        /// <summary>
        /// Executed when the reset button is pressed.
        /// </summary>
        private void ResetSimulation()
        {
            Debug.WriteLine("Resetting simulation...");

            LineChartViewModel.Reset();
            Herbivore.Reset();
            Carnivore.Reset();
            Omnivore.Reset();
            CurrentTimeStep = 0;
            AvailableFood = defaultAvailableFood;
            IsReset = true;
        }
        private void RunningRaiseExecuteChanged()
        {
            ResetCommand.RaiseCanExecuteChanged();
        }
    }
}