using ControlzEx.Theming;
using Oclock.Model;
using Oclock.Services;
using Prism.Commands;
using Prism.Mvvm;
using Reactive.Bindings;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Windows;

namespace Oclock.ViewModels
{
	public class MainWindowViewModels : BindableBase
	{
		private SettingsService _settingsService = new SettingsService();

		//public ReactiveProperty<bool> IsTimeDisplayed { get; set; } = new ReactiveProperty<bool>(true);
		public ReactiveProperty<bool> IsDisplay { get;  } = new ReactiveProperty<bool>();
		public ReactiveProperty<bool> IsDarkTheme { get;  } = new ReactiveProperty<bool>();

		
		public ReactiveCollection<string> ListWorldCurrent { get; set; } = new ReactiveCollection<string>();

		public ReactiveCollection<DigitalOclockViewModels> DigitalOclockViewModels { get; }

		public DelegateCommand<CancelEventArgs> WindowClosingCommand { get; }
		//private readonly IThemeManager _themeManager;

		public DelegateCommand OpenNewWindowCommand { get; }
		public DelegateCommand ToggleDarkModeCommand { get; }

		public DelegateCommand ToggleDisplayCommand { get; }

		//public MainWindowViewModels(IThemeManager themeManager)
		//{


		//	_themeManager = themeManager;

		//	//OpenNewWindowCommand = new DelegateCommand(OpenNewWindow);
		//	ToggleDarkModeCommand = new DelegateCommand(ToggleDarkMode);
		//}

		public MainWindowViewModels()
		{
			
			LoadSettings();
			ToggleDarkModeCommand = new DelegateCommand(ToggleDarkMode);
			OpenNewWindowCommand = new DelegateCommand(OpenNewWindow);
			WindowClosingCommand = new DelegateCommand<CancelEventArgs>(OnWindowClosing);


			//var settings = _settingsService.LoadSettings();
			//int count = 0;
			//ListWorldCurrent.Subscribe(tx => {
			//	count = 0;
			//	foreach (var world in settings.ListWorldCurrent)
			//	{
			//		ListWorldCurrent[count] = world;
			//		count++;
			//	}
			//	count = 0;
			//});

			//Init 3 clock
			DigitalOclockViewModels = new ReactiveCollection<DigitalOclockViewModels>()
			{
				new DigitalOclockViewModels(ListWorldCurrent[0]),
				new DigitalOclockViewModels(ListWorldCurrent[1]),
				new DigitalOclockViewModels(ListWorldCurrent[2])
			};
		
		
			
		}

		private void OpenNewWindow()
		{
			SaveSettings();
			MainWindow newWindow = new MainWindow();
			newWindow.Owner = Application.Current.MainWindow;
			newWindow.Show();
		}

		private void ToggleDarkMode()
		{
			// Toggle the value of IsDarkTheme
			//IsDarkTheme.Value = !IsDarkTheme.Value;
			if(IsDarkTheme.Value )
			{
				Console.WriteLine(true);
				ThemeManager.Current.ChangeTheme(Application.Current.MainWindow, "Dark.Blue");
			}
			else
			{
				Console.WriteLine(false);
				ThemeManager.Current.ChangeTheme(Application.Current.MainWindow, "Light.Blue");
			}
			// Change theme based on the value of IsDarkTheme
			//var newTheme = IsDarkTheme.Value ? "Dark.Blue" : "Light.Blue";
			//_themeManager.ChangeTheme(newTheme);
		}

		//public interface IThemeManager
		//{
		//	void ChangeTheme(string themeName);
		//}

		//public class ThemeManagerService : IThemeManager
		//{
		//	public void ChangeTheme(string themeName)
		//	{
		//		ThemeManager.Current.ChangeTheme(Application.Current.MainWindow, themeName);
		//	}
		//}
		private void OnWindowClosing(CancelEventArgs e)
		{
			// Xử lý logic khi cửa sổ đang đóng ở đây
			// Ví dụ: Lưu cài đặt
			SaveSettings();
		}
		public void LoadSettings()
		{

			var settings = _settingsService.LoadSettings();
			//init first start app.
			if (settings.ListWorldCurrent.Count != 3)
			{
				IsDisplay.Value = true;
				IsDarkTheme.Value = false;
				ListWorldCurrent = new ReactiveCollection<string>()
				{
					"Tokyo Standard Time",
					"Central Standard Time",
					"SE Asia Standard Time"
				};
			}
			else
			{
				IsDisplay.Value = settings.IsDisplay;
				IsDarkTheme.Value = settings.IsDarkTheme;
				ListWorldCurrent.Clear();
				if (settings.ListWorldCurrent != null)
				{
					foreach (var world in settings.ListWorldCurrent)
					{
						ListWorldCurrent.Add(world);
					}
				}
			
			}

		

			
		}

		




		

		public void SaveSettings()
		{
			var list = DigitalOclockViewModels
					.Select(item => item.SelectedItem.ToString().Split(':')[1].TrimEnd('}'))
					.ToList();
			var settings = new AppSettings
			{
				IsDisplay = IsDisplay.Value,
				IsDarkTheme = IsDarkTheme.Value,

				ListWorldCurrent = list// Convert ReactiveCollection to List

				  


		};
			_settingsService.SaveSettings(settings);
		
		}
	}
}
