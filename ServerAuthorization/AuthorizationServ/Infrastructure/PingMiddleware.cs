﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace ServerAuthorization.Infrastructure
{
    public class PingMiddleware
    {
        private RequestDelegate nextDelegate;
        private readonly IConfiguration config;
        public PingMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            nextDelegate = next;
            config = configuration;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            //Ping x = new Ping();
            //PingReply reply = x.Send("64.233.165.100", 100);

            //if (reply.Status== IPStatus.Success)
            //{
            //    await nextDelegate.Invoke(httpContext);
            //}
            //else
            //{
            //    httpContext.Response.StatusCode = 400; //Bad Request                
            //    await httpContext.Response.WriteAsync("Server is not available");
            //    return;
            //}
            var client = new TcpClient();
            string[] address = config["IPchat"].Split(":");
            try
            {
                client.ConnectAsync(address[0], Convert.ToInt32(address[1])).Wait(10000);
                await nextDelegate.Invoke(httpContext);
            }
            catch
            {
                httpContext.Response.StatusCode = 503; //Bad Request                
                await httpContext.Response.WriteAsync("Server is not available");
            }



        }
    }
}
