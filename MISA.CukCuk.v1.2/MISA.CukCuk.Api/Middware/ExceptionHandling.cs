using Microsoft.AspNetCore.Http;
using MISA.Core.Exceptions;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace MISA.CukCuk.Api.Middware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode status;
            string message;
            var response = new
            {
                devMsg = exception.Message,
                userMsg = "Có lỗi xảy ra vui lòng liên hệ MISA",
                MISACode = "002",
                Data = exception.Data
            };

            if(exception is CustomerException)
            {
                response = new
                {
                    devMsg = exception.Message,
                    userMsg = "Có lỗi xảy ra vui lòng liên hệ MISA",
                    MISACode = "001",
                    Data = exception.Data
                };
                context.Response.StatusCode = 400;
            }
            else
            {
                response = new
                {
                    devMsg = exception.Message,
                    userMsg = "Có lỗi xảy ra vui lòng liên hệ MISA",
                    MISACode = "002",
                    Data = exception.Data
                };
                context.Response.StatusCode = 500;

            }

            var stackTrace = String.Empty;
            message = exception.Message;
            var exceptionType = exception.GetType();
            var result = JsonSerializer.Serialize(response);
            context.Response.ContentType = "application/json";
            
            return context.Response.WriteAsync(result);
        }
    }
}
