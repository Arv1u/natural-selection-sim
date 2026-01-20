using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using Natural_Selection_Sim.MVVM;
using SkiaSharp;
using System.Collections.ObjectModel;
namespace Natural_Selection_Sim.ViewModels
{
    public class SpeciesData : PropertyChangedBase
    {
        private readonly SimulationViewModel simulationVM;
        private LineChartViewModel LineChartVM { get; set; }

        private readonly ObservableCollection<int> populationTrend = new() { };

        public LineSeries<int>? Series { get; set; }

        private readonly SKColor color;

        #region Properties

        private bool isEnabled;
        public bool IsEnabled
        {
            get
            {
                return isEnabled;
            }
            set
            {
                isEnabled = value;
                OnPropertyChanged(nameof(IsEnabled));
                simulationVM.StartCommand.RaiseCanExecuteChanged();
            }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set 
            { 
                name = value; 
                OnPropertyChanged();
            }
        }

        private int populationStart;
        public int PopulationStart
        {
            get
            {
                return populationStart;
            }
            set
            {
                populationStart = value;
                OnPropertyChanged();
            }
        }

        private int populationCurrent;
        public int PopulationCurrent
        {
            get { return populationCurrent; }
            set
            {
                populationCurrent = value;
                populationTrend.Add(value);
                OnPropertyChanged();
            }
        }

        private double birthRateStart;
        public double BirthRateStart
        {
            get
            {
                return birthRateStart;
            }
            set
            {
                birthRateStart = value;
                OnPropertyChanged();
            }
        }

        private double birthRateAvg;
        public double BirthRateAvg
        {
            get
            {
                return birthRateAvg;
            }
            set
            {
                birthRateAvg = value;
                OnPropertyChanged();
            }
        }

        private double deathRateStart;
        public double DeathRateStart
        {
            get
            {
                return deathRateStart;
            }
            set
            {
                deathRateStart = value;
                OnPropertyChanged(nameof(DeathRateStart));
            }
        }

        private double deathRateAvg;
        public double DeathRateAvg
        {
            get
            {
                return deathRateAvg;
            }
            set
            {
                deathRateAvg = value;
                OnPropertyChanged(nameof(DeathRateAvg));
            }
        }

        private double mutationRateStart;
        public double MutationRateStart
        {
            get
            {
                return mutationRateStart;
            }
            set
            {
                mutationRateStart = value;
                OnPropertyChanged();
            }
        }

        private double mutationRateAvg;
        public double MutationRateAvg
        {
            get { return mutationRateAvg; }
            set
            {
                mutationRateAvg = value;
                OnPropertyChanged();
            }
        }

        private int speedStart;
        public int SpeedStart
        {
            get
            {
                return speedStart;
            }
            set
            {
                speedStart = value;
                OnPropertyChanged(nameof(SpeedStart));
            }
        }

        private int speedAvg;
        public int SpeedAvg
        {
            get
            {
                return speedAvg;
            }
            set
            {
                speedAvg = value;
                OnPropertyChanged(nameof(SpeedAvg));
            }
        }

        private int sizeStart;
        public int SizeStart
        {
            get
            {
                return sizeStart;
            }
            set
            {
                sizeStart = value;
                OnPropertyChanged(nameof(SizeStart));
            }
        }

        private int sizeAvg;
        public int SizeAvg
        {
            get
            {
                return sizeAvg;
            }
            set
            {
                sizeAvg = value;
                OnPropertyChanged(nameof(SizeAvg));
            }
        }

        #endregion
        public SpeciesData(string name,SKColor color, LineChartViewModel lineChartVM, SimulationViewModel simulationVM)
        {
            LineChartVM = lineChartVM;
            Name = name;
            this.color = color;
            this.simulationVM = simulationVM;
            IsEnabled = true;

            SetDefaultStartData();
        }
        private void SetDefaultStartData()
        {
            PopulationStart = 33;
            BirthRateStart = 0.5;
            DeathRateStart = 0.5;
            MutationRateStart = 0.5;
            SpeedStart = 5;
            SizeStart = 5;
        }
        /// <summary>
        /// Adds a line graph of this species' population trend to the cartesian chart.
        /// </summary>
        public void Start()
        {
            Series = new()
            {
                Name = this.Name,
                Values = populationTrend,
                Fill = null,
                GeometrySize = 0,
                LineSmoothness = 0,
                IsVisibleAtLegend = true,
                Stroke = new SolidColorPaint(color)
            };
            LineChartVM.AddSeries(Series);    
        }
        public void Update(int newPopulation, double newBirthRateAvg, double newDeathRateAvg, double newMutationRateAvg, int newSpeedAvg, int newSizeAvg)
        {
            PopulationCurrent = newPopulation;
            BirthRateAvg = newBirthRateAvg;
            DeathRateAvg = newDeathRateAvg;
            MutationRateAvg = newMutationRateAvg;
            SpeedAvg = newSpeedAvg;
            SizeAvg = newSizeAvg;
        }
        public void UpdateDummyData()
        {
            
            Random rand = new Random();
            
            double newBirthRateAvg = rand.NextDouble() * 0.5 + 0.1; // Random birth rate between 0.1 and 0.6
            double newDeathRateAvg = rand.NextDouble() * 0.5 + 0.1; // Random death rate between 0.1 and 0.6
            double newMutationRateAvg = rand.NextDouble() * 0.5 + 0.1; // Random mutation rate between 0.1 and 0.6
            int newSpeedAvg = rand.Next(1, 10); // Random speed between 1 and 10
            int newSizeAvg = rand.Next(5, 15); // Random size between 5 and 15

            // Update the properties
            PopulationCurrent += rand.Next(-10, +11);
            BirthRateAvg = newBirthRateAvg;
            DeathRateAvg = newDeathRateAvg;
            MutationRateAvg = newMutationRateAvg;
            SpeedAvg = newSpeedAvg;
            SizeAvg = newSizeAvg;
        }
        public void Reset()
        {
            LineChartVM.Series.Remove(Series);
            populationTrend.Clear();

            PopulationCurrent = 0;
            BirthRateAvg = 0;
            DeathRateAvg = 0;
            MutationRateAvg = 0;
            SpeedAvg = 0;
            SizeAvg = 0;
        }
    }
}
