#r "PresentationCore"
#r "PresentationFramework"
#r "WindowsBase"
#r "System.Xaml"
#r "System.Xml"

#load OwinBasedSchemeHandler.csx

using System.Windows;
using System.Threading;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;

using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

using CefSharp;
using CefSharp.Wpf;

public static class Utilities
{
        public static void RunInSTAThread(ThreadStart threadStart)
        {
                var thread = new Thread(threadStart);
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
                thread.Join();
        }
}

public class App : Application 
{ 
    protected override void OnStartup(StartupEventArgs e)
	{
	CEF.Initialize(new Settings());
            CEF.RegisterScheme("scriptcs", new OwinBasedRequestHandler());

            WebView webView = new WebView("scriptcs://server/Welcome", new BrowserSettings());

            webView.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "IsBrowserInitialized")
                    webView.ShowDevTools();
            };
            
	    
		var window = new Window
		{
			Content = webView
		};

		window.Show();
	}
}

Utilities.RunInSTAThread(() => new App().Run());