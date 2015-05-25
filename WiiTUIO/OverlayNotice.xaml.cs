﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WiiTUIO.Output.Handlers.Touch;
using WiiTUIO.Provider;

namespace WiiTUIO
{
    /// <summary>
    /// Interaction logic for OverlayNotice.xaml
    /// </summary>
    public partial class OverlayNotice : UserControl
    {

        Timer hideTimer;

        public OverlayNotice(string message, int wiimoteID, int timeout)
        {
            InitializeComponent();
            this.noticeMessage.Text = ""+message;

            Color bordercolor = CursorColor.getColor(wiimoteID);
            //bordercolor.ScA = 0.5f;
            bordercolor.R = (byte)(bordercolor.R * 0.8);
            bordercolor.G = (byte)(bordercolor.G * 0.8);
            bordercolor.B = (byte)(bordercolor.B * 0.8);
            this.noticeBorder.Background = new SolidColorBrush(bordercolor);

            this.hideTimer = new Timer();
            this.hideTimer.Interval = timeout;
            this.hideTimer.AutoReset = true;
            this.hideTimer.Elapsed += hideTimer_Elapsed;
            this.hideTimer.Start();
        }

        private void hideTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.fadeOut();
        }

        private void noticeBorder_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.fadeOut();
        }

        private void fadeOut()
        {
            Dispatcher.BeginInvoke(new Action(delegate()
            {
                DoubleAnimation pAnimation = UIHelpers.createDoubleAnimation(0, 1000, false);
                pAnimation.FillBehavior = FillBehavior.HoldEnd;
                pAnimation.Completed += delegate(object sender, EventArgs pEvent)
                {
                    UIHelpers.animateCollapse(this, true);
                };
                this.BeginAnimation(FrameworkElement.OpacityProperty, pAnimation, HandoffBehavior.SnapshotAndReplace);
            
            }), null);
        }
    }
}
