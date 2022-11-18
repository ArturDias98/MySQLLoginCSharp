using Caliburn.Micro;
using Login.Commom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Threading;
using Color = System.Drawing.Color;

namespace Login.ViewModels.UserControls
{
    public class LoggerViewModel : Screen
    {
        private Brush _panelBackground;
        private string _message;

        private readonly DispatcherTimer messageTimer;
        private bool isShowing;
        private Queue<LoggerModel> loggerReport;
        public LoggerViewModel()
        {
            loggerReport = new Queue<LoggerModel>();

            Message = string.Empty;

            messageTimer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromMilliseconds(250),
            };
            messageTimer.Tick += MessageTimer_Tick;
            messageTimer.Start();

        }

        private async void MessageTimer_Tick(object sender, EventArgs e)
        {
            await ShowMessages();

        }
        private async Task ShowMessages()
        {
            if (isShowing)
            {
                return;
            }

            if (loggerReport.Count > 0 &&
                loggerReport.TryDequeue(out var report))
            {
                isShowing = true;
                var color = report.Success ? Color.LightBlue : Color.OrangeRed;
                await UpdateMessages(report.Message, color);
                isShowing = false;
            }
        }
        private async Task UpdateMessages(string message, Color displayColor)
        {
            ChangeMessage(message, displayColor, displayColor);
            await Task.Delay(3000);
            for (int i = 255 - 1; i >= 0; i -= 5)
            {
                ChangeMessageColor(Color.FromArgb(i, displayColor), Color.FromArgb(i, displayColor));
                await Task.Delay(8);
            }
            ChangeMessage(string.Empty, Color.Empty, Color.Empty);
        }
        private void ChangeMessage(string message, Color labelColor, Color statusColor)
        {
            Message = message;
            ChangeMessageColor(labelColor, statusColor);
        }
        private void ChangeMessageColor(Color labelColor, Color statusColor)
        {
            PanelBackGround = new SolidColorBrush(System.Windows.Media.Color.FromArgb(statusColor.A, statusColor.R, statusColor.G, statusColor.B));
        }

        public void Update(LoggerModel model)
        {
            loggerReport.Enqueue(model);
        }

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                NotifyOfPropertyChange(() => Message);
            }
        }

        public Brush PanelBackGround
        {
            get { return _panelBackground; }
            set
            {
                _panelBackground = value;
                NotifyOfPropertyChange(nameof(PanelBackGround));
            }
        }
    }
}
