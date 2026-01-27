using Natural_Selection_Sim.MVVM;
using SkiaSharp;

namespace Natural_Selection_Sim.ViewModels
{
    public class SimulationViewModel : PropertyChangedBase
    {
		static public LineChartViewModel LineChartViewModel { get; } = new();
		public SpeciesData Herbivore { get; } = new("Herbivore",SKColors.Green, LineChartViewModel);
		public SpeciesData Omnivore { get; } = new("Omnivore", SKColors.Orange, LineChartViewModel);
		public SpeciesData Carnivore { get; } = new("Carnivore", SKColors.Red, LineChartViewModel);
		private SimulationController simulation;
		private SimulationStats stats;

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
		public RelayCommand StartCommand { get; } 
		public RelayCommand PauseCommand { get; }
		public RelayCommand ResetCommand { get; }

		public SimulationViewModel()
		{
			StartCommand = new RelayCommand(_ => StartSimulation());
			PauseCommand = new RelayCommand(_ => PauseSimulation());
			ResetCommand = new RelayCommand(_ => ResetSimulation());
        }
		private void Run() // call this method to simulate one time step
		{

			simulation.Step();
			stats= simulation.GetStats();


			Herbivore.Update(stats.Herbivores.Count,Math.Round(stats.Herbivores.AvgBirthRate,2),Math.Round(stats.Herbivores.AvgDeathRate,2),Math.Round(stats.Herbivores.AvgMutationRate,2),Convert.ToInt32(stats.Herbivores.AvgSpeed),Convert.ToInt32(stats.Herbivores.AvgSize));
			Omnivore.Update(stats.Omnivores.Count,Math.Round(stats.Omnivores.AvgBirthRate,2),Math.Round(stats.Omnivores.AvgDeathRate,2),Math.Round(stats.Omnivores.AvgMutationRate,2),Convert.ToInt32(stats.Omnivores.AvgSpeed),Convert.ToInt32(stats.Omnivores.AvgSize));
			Carnivore.Update(stats.Carnivores.Count,Math.Round(stats.Carnivores.AvgBirthRate,2),Math.Round(stats.Carnivores.AvgDeathRate,2),Math.Round(stats.Carnivores.AvgMutationRate,2),Convert.ToInt32(stats.Carnivores.AvgSpeed),Convert.ToInt32(stats.Carnivores.AvgSize));
			//call these with appropiately calculated data to update the UI
		}
		private void StartSimulation()
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
			var config = new SimulationConfig
{
    PlantsPerStep = 50,//Hier fehlt noch der UI Teil zum einstellen
    Herbivore = new SpeciesConfig { StartCount = Herbivore.PopulationStart, BirthRate =Herbivore.BirthRateStart, DeathRate =Herbivore.DeathRateStart, MutationRate = Herbivore.MutationRateStart, Speed = Herbivore.SpeedStart, Size = Herbivore.SizeStart },
    Omnivore = new SpeciesConfig { StartCount = Omnivore.PopulationStart, BirthRate =Omnivore.BirthRateStart, DeathRate =Omnivore.DeathRateStart, MutationRate = Omnivore.MutationRateStart, Speed = Omnivore.SpeedStart, Size = Omnivore.SizeStart },
    Carnivore = new SpeciesConfig { StartCount = Carnivore.PopulationStart, BirthRate =Carnivore.BirthRateStart, DeathRate =Carnivore.DeathRateStart, MutationRate = Carnivore.MutationRateStart, Speed = Carnivore.SpeedStart, Size = Carnivore.SizeStart}

	while(true)
	{
		Run();
		System.Threading.Thread.Sleep(1000 / TimeStepsPerSecond);
};

simulation = new SimulationController(config);
stats=new SimulationStats();

		}
		private void PauseSimulation()
		{

		}
		private void ResetSimulation()
		{
			LineChartViewModel.Reset();
			Herbivore.Reset();
			Carnivore.Reset();
			Omnivore.Reset();
		}
	}
}
