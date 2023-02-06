public class APIConfiguration
{
	private WebApplicationBuilder _builder;
	public APIConfiguration(string[] args, Action<IServiceCollection> options)
	{
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
		app.UseAuthorization();
		app.MapControllers();
		return app.RunAsync();
	}
}