#r "System.Net.Http"

using System;
using System.IO;
using CefSharp;
using Microsoft.Owin.Testing;
using Owin;

internal class OwinBasedRequestHandler : ISchemeHandler, ISchemeHandlerFactory
{
private readonly TestServer m_Server;

public OwinBasedRequestHandler()
{
    m_Server = TestServer.Create(app => {
	    app.UseErrorPage(); // See Microsoft.Owin.Diagnostics
		app.UseWelcomePage("/Welcome"); // See Microsoft.Owin.Diagnostics
		app.Run(context =>
		{
		     return context.Response.WriteAsync("<html>Hello world using OWIN TestServer</html>");
		});
    });
}

public bool ProcessRequestAsync(IRequest request, SchemeHandlerResponse response,
    OnRequestCompletedHandler requestCompletedCallback)
{
    if (request.Url.StartsWith("chrome"))
	return false;

    Uri uri = new Uri(request.Url);
    
    var owinResponse = m_Server.HttpClient.GetAsync(uri.LocalPath).Result;

    Stream result = owinResponse.Content.ReadAsStreamAsync().Result;
    
    response.MimeType = "text/html";
    response.ResponseStream = result;

    requestCompletedCallback();

    return true;
}

public ISchemeHandler Create()
{
    return new OwinBasedRequestHandler();
}
}