using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Reflection;
using WorkoutLogs.Presentation;
using WorkoutLogs.Presentation.Contracts;
using WorkoutLogs.Presentation.Services;
using WorkoutLogs.Presentation.Services.Base;
using AutoMapper;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddHttpClient<IClient, Client>(client => client.BaseAddress = new Uri("https://localhost:7260"));
builder.Services.AddScoped<IExerciseService, ExerciseService>();
builder.Services.AddScoped<ISessionService, SessionService>();
builder.Services.AddScoped<IExerciseLogService, ExerciseLogService>();
builder.Services.AddScoped<IExerciseTypeService, ExerciseTypeService>();
builder.Services.AddScoped<IExerciseGroupService, ExerciseGroupService>();
builder.Services.AddScoped<IDifficultyService, DifficultyService>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

await builder.Build().RunAsync();
