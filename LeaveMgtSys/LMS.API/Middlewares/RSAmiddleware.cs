using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace LMS.API.Middlewares
{
    public class RSAmiddleware
    {
        
        private readonly RequestDelegate _nextMiddleware; 
        public RSAmiddleware(RequestDelegate nextMiddleware)
        {
            _nextMiddleware = nextMiddleware; 
        }

        public async Task Invoke(HttpContext context)
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
                            context.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(str)); 
                        }
                    }
                    catch (Exception ex)
                    {
                        await context.Response.WriteAsync(ex.Message.ToString());
                        return;
                    }
                } 

            await _nextMiddleware(context);
        } 
    }
}
