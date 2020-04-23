﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using PRM.ViewModel;
using Size = System.Windows.Size;

namespace PRM.View
{
    /// <summary>
    /// TabWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TabWindow : Window
    {
        public VmTabWindow Vm;
        public TabWindow(string token)
        {
            InitializeComponent();
            Vm = new VmTabWindow(token);
            DataContext = Vm;

            //Closed += (sender, args) => { MessageBox.Show("Tab win close"); };
            BtnClose.Click += (sender, args) =>
            {
                var _vm = ((VmTabWindow) this.DataContext);
                if (_vm?.Items.Count > 1)
                {
                    // TODO 销毁 SelectedItem
                    _vm.Items.Remove(_vm.SelectedItem);
                }
                else
                {
                    Close();
                }
            };
            BtnCloseAll.Click += (sender, args) => Close();
            BtnMaximize.Click += (sender, args) => this.WindowState = (this.WindowState == WindowState.Normal) ? WindowState.Maximized : WindowState.Normal;
            BtnMinimize.Click += (sender, args) => { this.WindowState = WindowState.Minimized; };
        }

        /// <summary>
        /// DragMove
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void System_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }


        private void TabablzControl_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.DataContext is VmTabWindow _vm
                && _vm?.SelectedItem != null)
            {
                // TODO set ICOm
                this.Title = _vm.SelectedItem.Header.ToString() + " - " + "PRemoteM";
            }
        }

        public Thickness TabContentBorder { get; } = new Thickness(2 ,0 ,2 ,2);
        public Size GetTabContentSize()
        {
            return new Size()
            {
                Width = TabablzControl.ActualWidth - TabContentBorder.Left - TabContentBorder.Right,
                Height = TabablzControl.ActualHeight - 30 - 2 - 1,
            };
        }
    }
}