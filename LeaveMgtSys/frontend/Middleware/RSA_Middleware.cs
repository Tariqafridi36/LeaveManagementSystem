using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace frontend.Middleware
{
    public class RSA_Middleware
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private readonly RequestDelegate _nextMiddleware;
        private readonly IConfiguration _configuration;

        public RSA_Middleware(RequestDelegate nextMiddleware, IConfiguration configuration)
        {
            _nextMiddleware = nextMiddleware;
            _configuration = configuration;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                using (var reader = new StreamReader(context.Request.Body))
                {
                    var body = reader.ReadToEndAsync();
                    if (!string.IsNullOrEmpty(body.Result))
                    {
                        EncryptDecryptProvider encryptDecrypt = new EncryptDecryptProvider();
                        string str = encryptDecrypt.Decrypt(body.Result.ToString());
                        context.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(str));

                    }
                }

                var targetUri = BuildTargetUri(context.Request);

                if (targetUri != null)
                {

                    if (context.Request.Headers.Count > 0)
                    {
                        context.Request.Headers.Remove("Origin");
                        context.Request.Headers.Remove("Sec-Fetch-Site");
                        context.Request.Headers.Remove("Sec-Fetch-Mode");
                        context.Request.Headers.Remove("Sec-Fetch-Dest");
                    }

                    string serilize = "";

                    using (var reader = new StreamReader(context.Request.Body))
                    {
                        var body = reader.ReadToEndAsync();
                        serilize = body.Result;
                    }


                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", context.Request.Headers["Authorization"].ToString().Replace("Bearer ", ""));
                    var ip = context.Connection.RemoteIpAddress.ToString();
                    _httpClient.DefaultRequestHeaders.Remove("X-Forwarded-For");
                    _httpClient.DefaultRequestHeaders.Add("X-Forwarded-For", Convert.ToString(ip));


                    if (context.Request.Method.ToLower() == "post")
                    {
                        using (var responseString = await _httpClient.PostAsync(targetUri.AbsoluteUri, new StringContent(serilize, Encoding.UTF8, "application/json")))
                        {
                            var data = responseString.Content.ReadAsStringAsync().Result;
                            byte[] bytes = Encoding.UTF8.GetBytes(data);
                            await context.Response.Body.WriteAsync(bytes, 0, bytes.Length);
                            return;
                        }

                    }
                    if (context.Request.Method.ToLower() == "delete")
                    {
                        using (var responseString = await _httpClient.DeleteAsync(targetUri.AbsoluteUri))
                        {
                            var data = responseString.Content.ReadAsStringAsync().Result;
                            byte[] bytes = Encoding.UTF8.GetBytes(data);
                            await context.Response.Body.WriteAsync(bytes, 0, bytes.Length);
                            return;
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
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync(ex.Message.ToString());
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
