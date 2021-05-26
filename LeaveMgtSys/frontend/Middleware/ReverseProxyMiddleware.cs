using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace frontend.Middleware
{
    public class ReverseProxyMiddleware
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private readonly RequestDelegate _nextMiddleware;
        private readonly IConfiguration _configuration;

        public ReverseProxyMiddleware(RequestDelegate nextMiddleware, IConfiguration configuration)
        {
            _nextMiddleware = nextMiddleware;
            _configuration = configuration;
        } 
        public async Task Invoke(HttpContext context)
        {
            IFormFile formFile = null;
            string requestUrl = context.Request.Path.Value.ToLower();
            string requestPath = requestUrl.Split('/')[requestUrl.Split('/').Length - 1];
            if (requestPath != "1" && requestPath != "1")
            {
                using (var reader = new StreamReader(context.Request.Body))
                {
                    try
                    {
                        var body = reader.ReadToEndAsync();
                        if (!string.IsNullOrEmpty(body.Result))
                        {
                            EncryptDecryptProvider encryptDecrypt = new EncryptDecryptProvider();
                            string str = encryptDecrypt.Decrypt(body.Result.ToString());
                            //var reverseString = Reverse(body.Result.ToString());
                            //var str = Encoding.UTF8.GetString(Convert.FromBase64String(reverseString));
                            context.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(str));
                            
                        }
                    }
                    catch (Exception ex)
                    {
                        await context.Response.WriteAsync(ex.Message.ToString());
                        return;
                    }
                }
            }
            else
            {
                try
                {
                    if (requestPath == "1" || requestPath == "1")
                    {
                        formFile = context.Request.Form.Files[0];
                    }

                }
                catch (Exception) { }
            }

            var targetUri = BuildTargetUri(context.Request);

            if (targetUri != null)
            {
                try
                {
                    if (context.Request.Headers.Count > 0)
                    {
                        context.Request.Headers.Remove("Origin");
                        context.Request.Headers.Remove("Sec-Fetch-Site");
                        context.Request.Headers.Remove("Sec-Fetch-Mode");
                        context.Request.Headers.Remove("Sec-Fetch-Dest");
                    }
                }
                catch (Exception ex)
                {
                }
                string serilize = "";
                try
                {
                    using (var reader = new StreamReader(context.Request.Body))
                    {
                        var body = reader.ReadToEndAsync();
                        serilize = body.Result;
                    }
                }
                catch (Exception ex)
                {
                    await context.Response.WriteAsync(ex.Message.ToString());
                    return;
                }

                try
                {
                    try
                    {
                        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", context.Request.Headers["Authorization"].ToString().Replace("Bearer ", ""));
                        var ip = context.Connection.RemoteIpAddress.ToString();
                        _httpClient.DefaultRequestHeaders.Remove("X-Forwarded-For");
                        _httpClient.DefaultRequestHeaders.Add("X-Forwarded-For", Convert.ToString(ip));
                    }
                    catch (Exception ex)
                    {
                    }

                    if (context.Request.Method.ToLower() == "post")
                    {
                        if (requestPath == "1" || requestPath == "1")
                        {
                            try
                            {
                                var file = formFile.OpenReadStream(); //context.Request.Form.Files[0].OpenReadStream(); 
                                HttpContent fileStreamContent = new StreamContent(file);
                                fileStreamContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                                { FileName = "\"" + context.Request.Form.Files[0].FileName + "\"", };
                                fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue("multipart/form-data");

                                var type = context.Request.Form["fileType"].ToString();

                                using (var formData = new MultipartFormDataContent())
                                {
                                    formData.Add(fileStreamContent, "file");
                                    formData.Add(new StringContent(type), "fileType");
                                    var response = await _httpClient.PostAsync(targetUri.AbsoluteUri, formData);
                                    var data = response.Content.ReadAsStringAsync().Result;
                                    byte[] bytes = Encoding.UTF8.GetBytes(data);
                                    await context.Response.Body.WriteAsync(bytes, 0, bytes.Length);
                                    formFile = null;
                                    return;
                                }
                            }
                            catch (ObjectDisposedException ex)
                            {
                            }

                        }
                        else
                        {
                            using (var responseString = await _httpClient.PostAsync(targetUri.AbsoluteUri, new StringContent(serilize, Encoding.UTF8, "application/json")))
                            {
                                var data = responseString.Content.ReadAsStringAsync().Result;
                                byte[] bytes = Encoding.UTF8.GetBytes(data);
                                await context.Response.Body.WriteAsync(bytes, 0, bytes.Length);
                                return;
                            }
                        }
                    }
                    else
                    {
                        using (var responseString = await _httpClient.GetAsync(targetUri.AbsoluteUri))
                        {
                            var data = responseString.Content.ReadAsStringAsync().Result;
                            byte[] bytes = Encoding.UTF8.GetBytes(data);
                            await context.Response.Body.WriteAsync(bytes, 0, bytes.Length);
                            return;
                        }
                    } 
                }
                catch (Exception ex)
                {
                   await context.Response.WriteAsync(ex.Message.ToString());
                }

                return;

            }
            await _nextMiddleware(context);
        }
        private Uri BuildTargetUri(HttpRequest request)
        {
            Uri targetUri = new Uri(_configuration.GetSection("AppSettings:Endpoint").Value + request.Path.Value);
            return targetUri;
        }
    }

}
