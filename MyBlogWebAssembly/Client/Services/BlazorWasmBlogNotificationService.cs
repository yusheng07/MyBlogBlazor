using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using MyBlog.Data.Models;
using MyBlog.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlogWebAssembly.Client.Services
{
    public class BlazorWasmBlogNotificationService : IBlogNotificationService, IAsyncDisposable
    {
        NavigationManager _navigationManager;

        private HubConnection _hubConnection;

        public Action<BlogPost> BlogPostChanged { get; set; }

        public BlazorWasmBlogNotificationService(NavigationManager navigationManager)
        {
            _navigationManager = navigationManager;

            _hubConnection = new HubConnectionBuilder().AddJsonProtocol(options => {
                options.PayloadSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
                options.PayloadSerializerOptions.PropertyNamingPolicy = null;
            })
                .WithUrl(navigationManager.ToAbsoluteUri("/BlogNotificationHub"))
            .Build();

            //configure the hub connection to listen for [BlogPostChanged] event
            _hubConnection.On<BlogPost>("BlogPostChanged", post =>
            {
                BlogPostChanged?.Invoke(post);
            });

            //start the connection
            //since constructor can't be async, won't await this method
            _hubConnection.StartAsync(); 
        }

        public async Task SendNotification(BlogPost post)
        {
            await _hubConnection.SendAsync("SendNotification",post);
        }

        public async ValueTask DisposeAsync()
        {
            await _hubConnection.DisposeAsync();
        }
    }
}
