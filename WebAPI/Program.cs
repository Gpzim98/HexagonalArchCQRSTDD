public class APIConfiguration
{
	private WebApplicationBuilder _builder;
	public APIConfiguration(string[] args, Action<IServiceCollection> options)
	{
		var dir = AppDomain.CurrentDomain.BaseDirectory;
		Directory.SetCurrentDirectory(dir);

		_builder = WebApplication.CreateBuilder(
			new WebApplicationOptions
			{
				ApplicationName = "WebAPI"
			});

		options.Invoke(_builder.Services);

		_builder.Services.AddControllers();
	}

	public Task RunAsync()
	{
		var app = _builder.Build();
		app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
		app.MapControllers();
		return app.RunAsync();
	}
}