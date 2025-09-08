using HMS.CommonMethod_Class;
using HMS.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();
builder.Services.AddScoped<DatabaseMethod>();
builder.Services.AddScoped<AdminDashboardActions>();
builder.Services.AddScoped<AppointmentActions>();
builder.Services.AddScoped<DepartmentActions>();
builder.Services.AddScoped<DoctorActions>();
builder.Services.AddScoped<DoctorDepartmentActions>();
builder.Services.AddScoped<PatientActions>();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();
app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Admin}/{action=Login}/{id?}");

app.Run();
