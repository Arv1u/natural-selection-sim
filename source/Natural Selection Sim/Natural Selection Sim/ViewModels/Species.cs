using LiveChartsCore.SkiaSharpView;
using Natural_Selection_Sim.MVVM;
using System.Collections.ObjectModel;
using System.Timers;
using System.Xml.Linq;
namespace Natural_Selection_Sim.ViewModels
{
    public class Species : PropertyChangedBase
    {
        private LineChartViewModel LineChartVM { get; set; }
        public readonly ObservableCollection<int> values = new() { };
        public LineSeries<int> Series { get; set; }
        public Species(string name, LineChartViewModel lineChartVM, int startPop, double startBirth)
        {
            LineChartVM = lineChartVM;
            Name = name;
            BirthRateStart = startBirth;
            PopulationStart = startPop;
            IsEnabled = true;
        }
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged(); }
        }
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
            }
        }
        public void AddValue(int value)
        {
            values.Add(value);
        }
        private int population;
        public int Population
        {
            get { return population; }
            set
            {
                population = value;
                values.Add(value);
                OnPropertyChanged();
            }
        }
        private double mutationPerc;
        public double MutationPerc
        {
            get { return mutationPerc; }
            set
            {
                mutationPerc = value;
                OnPropertyChanged();
            }
        }
        public void Start()
        {
            Series = new()
            {
                Name = this.Name,
                Values = values,
                Fill = null,
                GeometrySize = 0,
                LineSmoothness = 0,
                IsVisibleAtLegend = true,
            };
            LineChartVM.AddSeries(Series);
            
            Population = PopulationStart;
            BirthRate = BirthRateStart;
        }
        public void Update()
        {
            Random rand = new();
            int random = rand.Next(-10, 11);
            Population += random;
        }
        public void Reset()
        {
            LineChartVM.Series.Remove(Series);
            Population = 0;
            BirthRate = 0;
            values.Clear();
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
        private double birthRate;
        public double BirthRate
        {
            get
            {
                return birthRate;
            }
            set
            {
                birthRate = value;
                OnPropertyChanged();
            }
        }
    }
}
