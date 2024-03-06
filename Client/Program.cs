using BaseLibrary.Entities;
using Blazored.LocalStorage;
using Client;
using Client.ApplicationStates;
using ClientLibrary.Helpers;
using ClientLibrary.Services.Contracts;
using ClientLibrary.Services.Implementations;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Fast.Components.FluentUI;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddTransient<CustomHtppHandler>();
builder.Services.AddHttpClient("SystemApiClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7140");
}).AddHttpMessageHandler<CustomHtppHandler>();

builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<GetHttpClient>();
builder.Services.AddScoped<LocalStorageService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped<IUserAccountService, UserAccountServices>();

#region General Department / Department / Branch
builder.Services.AddScoped<IGenericService<GeneralDepartment>, GenericServiceImplementation<GeneralDepartment>>();
builder.Services.AddScoped<IGenericService<Department>, GenericServiceImplementation<Department>>();
builder.Services.AddScoped<IGenericService<Branch>, GenericServiceImplementation<Branch>>();
#endregion

#region Country / City / Town
builder.Services.AddScoped<IGenericService<Country>, GenericServiceImplementation<Country>>();
builder.Services.AddScoped<IGenericService<City>, GenericServiceImplementation<City>>();
builder.Services.AddScoped<IGenericService<Town>, GenericServiceImplementation<Town>>();
#endregion

#region Employee
builder.Services.AddScoped<IGenericService<Employee>, GenericServiceImplementation<Employee>>();
#endregion

builder.Services.AddScoped<AllState>();

builder.Services.AddFluentUIComponents();

await builder.Build().RunAsync();
