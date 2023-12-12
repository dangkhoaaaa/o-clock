﻿using Oclock.ViewModels;
using Prism.Ioc;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using static Oclock.ViewModels.MainWindowViewModels;

namespace Oclock
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : PrismApplication
	{
		protected override Window CreateShell()
		{
			var w = Container.Resolve<MainWindow>();
			return w;
		}

		protected override void RegisterTypes(IContainerRegistry containerRegistry)
		{
			containerRegistry.Register<MainWindowViewModels>();
			//containerRegistry.RegisterSingleton<IThemeManager, ThemeManagerService>();
			containerRegistry.RegisterForNavigation<MainWindow, MainWindowViewModels>();
			//	containerRegistry.Register<DigitalOclockViewModels>();
		}
	}
}
