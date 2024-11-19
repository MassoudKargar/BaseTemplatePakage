using BaseTemplate.Endpoints.API.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureServices().ConfigurePipeline().Run();
