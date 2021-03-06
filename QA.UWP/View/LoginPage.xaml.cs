﻿using QA.UWP.Core;
using QA.UWP.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace QA.UWP.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        INavigatable vm;
        public LoginPage()
        {
            this.InitializeComponent();
            vm = DataContext as INavigatable;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            vm.NavigateFrom(e);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            vm.NavigateTo(e);
        }
    }
}
