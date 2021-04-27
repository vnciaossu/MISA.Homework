using Microsoft.AspNetCore.Http;
using MISA.Core.Exceptions;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace MISA.CukCuk.Api.Middware
{
    public class ErrorHandling
    {
        private readonly RequestDelegate _requestDelegate;

        public ErrorHandling(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _requestDelegate(context);
            }
            catch (System.Exception ex)
            {
                await HandleExeptionAsync(context, ex);
            }
        }

        private Task HandleExeptionAsync(HttpContext context, Exception ex)
        {
            HttpStatusCode statusCode;

            var response = new
            {
                devMsg = ex.Message,
                userMsg = "Có lỗi xảy ra vui lòng liên hệ MISA",
                MISACode = "002",
                Data = ex.Data
            };
            context.Response.StatusCode = 500;

            if (ex is CustomerException)
            {
                response = new
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi xảy ra vui lòng liên hệ MISA",
                    MISACode = "003",
                    Data = ex.Data
                };
                context.Response.StatusCode = 400;
            }


            var result = JsonSerializer.Serialize(response);
            context.Response.ContentType = "application/json";

            return context.Response.WriteAsync(result);
        }
    }
}