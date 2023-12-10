using System.Net;

HttpClient client = new();
string server = "https://localhost:7173/";
Uri uri = new Uri(server);

/*
 * FORM SEND
 * 
Dictionary<string, string> formData = new()
{
    ["name_user"] = "Bobby",
    ["email_user"] = "bobby@gmail.com",
    ["age_user"] = "25",
};

HttpContent content = new FormUrlEncodedContent(formData);
using var response = await client.PostAsync(server + "data", content);

string text = await response.Content.ReadAsStringAsync();
Console.WriteLine(text);
*/

/*
 * STREAM SEND
 * 
string fileName = @"D:\cars.png";

using var stream  = File.OpenRead(fileName);
StreamContent content = new StreamContent(stream);

using var response = await client.PostAsync(server + "stream", content);
string text = await response.Content.ReadAsStringAsync();
Console.WriteLine(text);
*/

/*
 * BYTE ARRAY SEND
 * 
string text = "HTML-формы требуются для сбора данных от посетителей сайта. Например, при регистрации на Uber, Netflix или Facebook пользователь вводит свое имя, почту и пароль.\nПримечание: В примерах ниже уже заданы CSS стили, поэтому они отличаются от того, что получится у вас. CSS файлы можно скачать отсюда:";

byte[] buffer = System.Text.Encoding.UTF8.GetBytes(text);
var content = new ByteArrayContent(buffer);

using var response = await client.PostAsync(server + "bytes", content);

string textResponse = await response.Content.ReadAsStringAsync();
Console.WriteLine(textResponse);
*/

/*
 * ONE FILE SEND
 * 
string filePath = @"D:\cars.png";

using var filesContent = new MultipartFormDataContent();
//var fileStream = new StreamContent(File.OpenRead(filePath));
byte[] fileBytes = await File.ReadAllBytesAsync(filePath);
var fileByteContetnt = new ByteArrayContent(fileBytes);

//fileStream.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");  
fileByteContetnt.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");

//filesContent.Add(fileStream, name: "file", fileName: "cars.png");
filesContent.Add(fileByteContetnt, name: "file", fileName: "cars_bytes.png");

using var response = await client.PostAsync(server + "upload", filesContent);

var text = await response.Content.ReadAsStringAsync();
Console.WriteLine(text);
*/

/*
 * MULTI FILES SEND
 * 
 * 
string[] files = new[]
{
    @"D:\cars.png",
    @"D:\circs.jpg"
};

using var filesContent = new MultipartFormDataContent();

foreach(var file in files)
{
    var fileName = Path.GetFileName(file);
    var fileStream = new StreamContent(File.OpenRead(file));

    filesContent.Add(fileStream, name: "file", fileName: fileName);
}

using var response = await client.PostAsync(server + "upload", filesContent);

var text = await response.Content.ReadAsStringAsync();
Console.WriteLine(text);
*/

/*
 * MULTI DATA SEND
 * 
using var datesContent = new MultipartFormDataContent();

datesContent.Add(new StringContent("Bobby"), name: "name_user");
datesContent.Add(new StringContent("35"), name: "age_user");

string[] files = new[]
{
    @"D:\cars.png",
    @"D:\circs.jpg"
};

foreach (var file in files)
{
    var fileName = Path.GetFileName(file);
    var fileStream = new StreamContent(File.OpenRead(file));

    datesContent.Add(fileStream, name: "file", fileName: fileName);
}

using var response = await client.PostAsync(server + "multi", datesContent);

var text = await response.Content.ReadAsStringAsync();
Console.WriteLine(text);

*/

// cookie in request

Cookie loginCookie = new("login", "sammy");
Cookie emailCookie = new("email", "sam@gmail.com");
Cookie ageCookie = new("age", "21");

CookieContainer cookieContainer = new CookieContainer();
cookieContainer.Add(uri, loginCookie);
cookieContainer.Add(uri, emailCookie);
cookieContainer.Add(uri, ageCookie);

cookieContainer.SetCookies(uri, "name=Sam");

client.DefaultRequestHeaders.Add("cookie", cookieContainer.GetCookieHeader(uri));

using var response = await client.GetAsync(uri);
string text = await response.Content.ReadAsStringAsync();
Console.WriteLine(text);



// cookie in response
//using var response = await client.GetAsync(uri);

//CookieContainer cookies = new CookieContainer();

//foreach(var cookieKey in response.Headers.GetValues("Set-Cookie"))
//    cookies.SetCookies(uri, cookieKey);


//foreach(Cookie cookie in cookies.GetCookies(uri))
//    Console.WriteLine($"key: {cookie.Name} - {cookie.Value}");



