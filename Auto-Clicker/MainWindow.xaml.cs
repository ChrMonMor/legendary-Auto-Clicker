using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Auto_Clicker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick); 
            KeyDown += new KeyEventHandler(MainWindow_KeyDown);
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.D1)
            {
                dispatcherTimer.Stop();
                stop = true;
            }
            if (e.Key == Key.D2)
            {
                dispatcherTimer.Start();
                stop = false;
            }
        }

        [DllImport("user32.dll")]
        static extern void mouse_event(int dwFlags, int dx, int dy, int dwdata, int dwextrainfo);

        //  DispatcherTimer setup
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();


        public enum mouseeventflags
        {
            leftDown = 2,
            leftUp = 4,
        }
        
        public void leftclick(Point p)
        {
            mouse_event((int)(mouseeventflags.leftDown), (int)p.X, (int)p.Y, 0, 0);
            mouse_event((int)(mouseeventflags.leftUp), (int)p.X, (int)p.Y, 0, 0);
        }

        bool stop = true;
        
        private void button_Click(object sender, RoutedEventArgs e)
        {
            stop = (stop) ? false : true;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, int.Parse(textBox.Text));
            dispatcherTimer.IsEnabled = true;
            dispatcherTimer_Tick(sender, e);
            if (!stop) dispatcherTimer.Start();
            if (stop) dispatcherTimer.Stop();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            Mouse.Capture(this);
            Point pointToWindow = Mouse.GetPosition(this);
            Point pointToScreen = PointToScreen(pointToWindow);
            Mouse.Capture(null);
            leftclick(new Point(pointToScreen.X, pointToScreen.Y));
        }

        int click = 0;

        private void buttonClickMe_Click(object sender, RoutedEventArgs e)
        {
            click++;
            labelClicks.Content = click;
        }
    }
}
