
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Timers;


namespace Oclock.ViewModels
{
	public class DigitalOclockViewModels 
	{
		//public ReactiveProperty<bool> IsDisplay { get; set; } 
		public ReactiveProperty<DateTime> TimeNow { get; } = new ReactiveProperty<DateTime>();

		public ReactiveProperty<string> SelectedItem { get; }

	
		private Timer updateTimer;
		public Dictionary<string, string> ZonesDictionary { get; } = new Dictionary<string, string>

{
	{ "日本", "Tokyo Standard Time" },
	{ "アメリカ", "Central Standard Time" },
	{ "タイ", "SE Asia Standard Time" },
	{ "インド", "India Standard Time" },
	{ "中国", "China Standard Time" }
};

		public DigitalOclockViewModels(string ZoneFile)
		{
			//_mainwindow = new MainWindowViewModels();
			// _mainwindow.IsDisplay.Subscribe(text =>
			// {
			//	 //IsDisplay.Value = true;
			//	 Console.WriteLine("Controlll	" + _mainwindow.IsDisplay.Value);
			// });
			//IsDisplay=new ReactiveProperty<bool>(true);
			
			SelectedItem =new ReactiveProperty<string>(ZoneFile);
			SelectedItem.Subscribe(text =>
			{
				//_mainwindow = new MainWindowViewModels();
				//_mainwindow.ListWorldCurrent[0] = text;
				//_mainwindow.SaveSettings();
				//_mainwindow.Temp.Value = "" + _mainwindow.Temp.Value;
				StartClock(text);

			});
		}

		
	

		// check mul task 
		//private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

		////Start Time (while 0.5 second => respon)
		//private async void StartClock(string timeZoneId)
		//{
		//	//  cancel the task if have
		//	cancellationTokenSource.Cancel();
		//	cancellationTokenSource = new CancellationTokenSource();

		//	while (true)
		//	{
		//		// check request cancel
		//		if (cancellationTokenSource.Token.IsCancellationRequested)
		//		{
		//			break;
		//		}

		//		TimeNow.Value = GetLocalTime(timeZoneId);

		//		try
		//		{
		//			await Task.Delay(500, cancellationTokenSource.Token);
		//		}
		//		catch (TaskCanceledException)
		//		{

		//			break;
		//		}
		//	}
		//}
		private void StartClock(string timeZoneId)
		{
			if (updateTimer != null)
			{
				updateTimer.Stop();
				updateTimer.Elapsed -= TimerElapsed; // Unsubscribe the previous event to avoid multiple subscriptions
			}

			updateTimer = new Timer(100); // Set the interval to 100 ms
			updateTimer.Elapsed += (sender, args) => TimerElapsed(timeZoneId);
			updateTimer.AutoReset = true; // This makes the timer repeat its action
			updateTimer.Start();
		}

		private void TimerElapsed(string timeZoneId)
		{
			TimeNow.Value = GetLocalTime(timeZoneId);
		}
		private void TimerElapsed(object sender, ElapsedEventArgs e)
		{
			TimeNow.Value = GetLocalTime(SelectedItem.Value); // Use SelectedItem.Value to get the timeZoneId
		}
		//Get Realtime by TimeZoneID
		private DateTime GetLocalTime(string timeZoneId)
		{
			if (timeZoneId != null)
			{
				var timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
				return TimeZoneInfo.ConvertTime(DateTime.Now, timeZone);
			}
			else
			{
				return DateTime.Now;
			}
		}
	}
	
}
