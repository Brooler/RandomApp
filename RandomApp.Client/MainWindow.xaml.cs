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
        private static Mutex _mutex = new Mutex();

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
                _mutex.WaitOne(TimeSpan.FromSeconds(1));

                Model.Number = Task.Run(async () => await _dataService.GetRandomNumber(), new CancellationTokenSource(900).Token).Result;

                _mutex.ReleaseMutex();
            }
            catch (Exception ex)
            {
                _timer.Stop();
                _mutex.Dispose();

                MessageBox.Show(ex.Message, "Something went wrong!");
            }
        }
    }
}
