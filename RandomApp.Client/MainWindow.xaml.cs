using System;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using RandomApp.Client.Interfaces;
using RandomApp.Client.Models;
using Timer = System.Timers.Timer;

namespace RandomApp.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IDataService _dataService;
        private readonly Timer _timer;

        public RandomModel Model { get; set; }

        public MainWindow(IDataService dataService)
        {
            _dataService = dataService;

            InitializeComponent();

            Model = new RandomModel();

            _timer = new Timer
            {
                AutoReset = true,
                Interval = TimeSpan.FromSeconds(1).TotalMilliseconds
            };

            _timer.Elapsed += TimerOnElapsed;

            _timer.Start();

            DataContext = Model;
        }

        private void TimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                Model.Number = Task.Run(async () => await _dataService.GetRandomNumber(), new CancellationTokenSource(900).Token).Result;
            }
            catch (Exception ex)
            {
                _timer.Stop();

                MessageBox.Show(ex.Message, "Something went wrong!");
            }
        }
    }
}
