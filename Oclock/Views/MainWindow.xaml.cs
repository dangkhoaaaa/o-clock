using ControlzEx.Theming;
using MahApps.Metro.Controls;
using Oclock.UserControll;
using Oclock.ViewModels;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls.Primitives;


namespace Oclock
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : MetroWindow
	{
		//DigitalOclock _digitalOclock = new DigitalOclock();

		public static MainWindowViewModels _mainWindowViewModels;
		//private DigitalOclockViewModels viewModel;
		
		public MainWindow()
		{
			InitializeComponent();
			//_mainWindowViewModels = new MainWindowViewModels();
			


			
			//this.DataContext = _mainWindowViewModels;
			
			//clock1.DataContext = new DigitalOclockViewModels(_digitalOclockViewModels.ListWorldCurrent[0], 0);
		
			//clock2.DataContext = new DigitalOclockViewModels(_digitalOclockViewModels.ListWorldCurrent[1],1);
			//clock3.DataContext = new DigitalOclockViewModels(_digitalOclockViewModels.ListWorldCurrent[2],2);
		}

	
		public MainWindow(MainWindowViewModels mainWindowViewModels)
		{
			InitializeComponent();
			this.DataContext= mainWindowViewModels;
		}
		private void Button_Click(object sender, RoutedEventArgs e)
		{
			//CallBackWindow.OpenTab();
			MainWindow newWindow = new MainWindow(_mainWindowViewModels);
			newWindow.Owner = this;
			//newWindow.Left = this.Left + 35; 
			//newWindow.Top = this.Top + 35;
			newWindow.Show();
		}

	
		////Dark Mode
		//private void IsDarkTheme_Checked(object sender, RoutedEventArgs e)
		//{
		//	ThemeManager.Current.ChangeTheme(this, "Dark.Blue");
		//	_mainWindowViewModels.IsDarkTheme.Value = true;
		//}

		////Light Mode
		//private void IsDarkTheme_Unchecked(object sender, RoutedEventArgs e)
		//{
		//	ThemeManager.Current.ChangeTheme(this, "Light.Blue");
		//	_mainWindowViewModels.IsDarkTheme.Value = false;
		//}


		//Close this app --> Save File
		protected override void OnClosing(CancelEventArgs e)
		{

			//_mainWindowViewModels.SaveSettings();
			base.OnClosing(e);
		}
	}

}
