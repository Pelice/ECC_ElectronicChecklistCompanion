using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using NLog;
using NLog.Web;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

try
{
	var builder = WebApplication.CreateBuilder(args);

	builder.Services.AddRazorPages();
	builder.Services.AddServerSideBlazor();

	// NLog: Setup NLog for Dependency injection
	builder.Logging.ClearProviders();
	builder.Host.UseNLog();

	var app = builder.Build();

	app.UseStaticFiles();

	app.UseRouting();

	app.MapBlazorHub();
	app.MapFallbackToPage("/_Host");

	app.Run();

	logger.Debug("init complete");
}
catch (Exception exception)
{
	// NLog: catch setup errors
	logger.Error(exception, "Stopped program because of exception");
	throw;
}
finally
{
	// Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
	NLog.LogManager.Shutdown();
}