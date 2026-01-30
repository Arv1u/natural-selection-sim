using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using Natural_Selection_Sim.MVVM;
using SkiaSharp;
using System.Collections.ObjectModel;
using System.Diagnostics;
namespace Natural_Selection_Sim.ViewModels
{
    /// <summary>
    /// Contains all data displayed by the UI for an individual species.
    /// </summary>
    public class SpeciesData : PropertyChangedBase
    {
        // reference needed to notify SimulationViewModel when IsEnabled has changed
        private readonly SimulationViewModel simulationVM; 
        private LineChartViewModel LineChartVM { get; set; }

        /// <summary>
        /// History of the population trend. 1 index = 1 timestep.
        /// </summary>
        private readonly ObservableCollection<int> populationTrend = new() { };

        /// <summary>
        /// Object the SkiaSharp package uses to display graphs in cartesian charts.
        /// </summary>
        public LineSeries<int> Series { get; set; }

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

                // Notify start button that execute conditions have changed.
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

                Debug.WriteLine("Staring Pop: " + value);
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
        public bool isDead = false;
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
            isDead = false;
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
            PopulationCurrent = PopulationStart;
        }
        /// <summary>
        /// Updates properties with current simulation data.
        /// </summary>
        public void Update(int newPopulation, double newBirthRateAvg, double newDeathRateAvg, double newMutationRateAvg, int newSpeedAvg, int newSizeAvg)
        {
            // Skip update if species is dead or not enabled.
            if (isDead || !IsEnabled)
            {
                return;
            }

            // Check if species goes extinct with the new pop-change.
            if (PopulationCurrent + newPopulation <= 0)
            {
                isDead = true;
                PopulationCurrent = 0;
                return;
            }

            // Update UI-bound properties with new values
            PopulationCurrent = newPopulation;
            BirthRateAvg = newBirthRateAvg;
            DeathRateAvg = newDeathRateAvg;
            MutationRateAvg = newMutationRateAvg;
            SpeedAvg = newSpeedAvg;
            SizeAvg = newSizeAvg;
        }
        /// <summary>
        /// Clears all saved data.
        /// </summary>
        public void Reset()
        {
            LineChartVM.Series.Remove(Series);

            // Current pop has to be set to 0 BEFORE clearing populationTrend. Else a single 0 will be left in pop-trend.
            PopulationCurrent = 0;
            populationTrend.Clear();

            SetDefaultStartData();

            BirthRateAvg = 0;
            DeathRateAvg = 0;
            MutationRateAvg = 0;
            SpeedAvg = 0;
            SizeAvg = 0;
        }
        ///// <summary>
        ///// Updates properties with dummy data for testing purposes.
        ///// </summary>
        //public void UpdateDummyData() 
        //{
        //    if (isDead)
        //    {
        //        return;
        //    }
        //    Random rand = new Random();
            
        //    double newBirthRateAvg = Math.Round(rand.NextDouble() * 0.5 + 0.1,2); 
        //    double newDeathRateAvg = Math.Round(rand.NextDouble() * 0.5 + 0.1, 2); 
        //    double newMutationRateAvg = Math.Round(rand.NextDouble() * 0.5 + 0.1, 2); 
        //    int newSpeedAvg = rand.Next(1, 10); 
        //    int newSizeAvg = rand.Next(5, 15); 
        //    int randPopChange = rand.Next(-10, +11);
        //    if(PopulationCurrent + randPopChange <= 0)
        //    {
        //        isDead = true;
        //        PopulationCurrent = 0;
        //        return;
        //    }
        //    PopulationCurrent += randPopChange;
        //    BirthRateAvg = newBirthRateAvg;
        //    DeathRateAvg = newDeathRateAvg;
        //    MutationRateAvg = newMutationRateAvg;
        //    SpeedAvg = newSpeedAvg;
        //    SizeAvg = newSizeAvg;
        //}
    }
}
