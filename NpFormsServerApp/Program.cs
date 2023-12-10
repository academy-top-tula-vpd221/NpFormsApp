var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", async (HttpContext context) =>
{
    //context.Response.Cookies.Append("user", "joe");
    //context.Response.Cookies.Append("password", "qwerty");

    context.Request.Cookies.TryGetValue("login", out string? login);
    context.Request.Cookies.TryGetValue("email", out string? email);
    context.Request.Cookies.TryGetValue("age", out string? age);
    context.Request.Cookies.TryGetValue("name", out string? name);

    await context.Response.WriteAsync($"You cookies values: {login}, {email}, {age}, {name}");
});

app.MapPost("/data", async (HttpContext context) => 
{
    var form = context.Request.Form;
    string? name = form["name_user"];
    string? email = form["email_user"];
    string? age = form["age_user"];

    await context.Response.WriteAsync($"Name: {name}, Email: {email}, Age: {age}");
});

app.MapPost("/stream", async (HttpContext context) =>
{
    var uploadDir = $"{Directory.GetCurrentDirectory()}/upload";
    Directory.CreateDirectory(uploadDir);

    string fileName = $"file-{Guid.NewGuid().ToString()}";

    using(var stream = new FileStream($"{uploadDir}/{fileName}.dat", FileMode.Create))
    {
        await context.Request.Body.CopyToAsync(stream);
    }

    await context.Response.WriteAsync("Data save");
});

app.MapPost("/bytes", async (HttpContext context) =>
{
    using StreamReader reader = new StreamReader(context.Request.Body);
    string message = await reader.ReadToEndAsync();

    await context.Response.WriteAsync($"Message: {message}");
});

app.MapPost("/upload", async (HttpContext context) =>
{
    var uploadDir = $"{Directory.GetCurrentDirectory()}/upload";
    Directory.CreateDirectory(uploadDir);

    IFormFileCollection files = context.Request.Form.Files;

    foreach(var file in files)
    {
        string filesPath = $"{uploadDir}/{file.FileName}";

        using(var stream = new FileStream(filesPath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }
    }

    await context.Response.WriteAsync("files saved");
});

app.MapPost("/multi", async (HttpContext context) =>
{
    var form = context.Request.Form;

    string? name = form["name_user"];
    string? age = form["age_user"];

    IFormFileCollection files = form.Files;

    var uploadDir = $"{Directory.GetCurrentDirectory()}/upload";
    Directory.CreateDirectory(uploadDir);

    foreach (var file in files)
    {
        string filesPath = $"{uploadDir}/{file.FileName}";

        using (var stream = new FileStream(filesPath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }
    }

    await context.Response.WriteAsync($"files saved. userdate: {name}, {age}");

});

app.Run();
