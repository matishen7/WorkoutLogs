using global::System;
using global::System.Collections.Generic;
using global::System.Linq;
using global::System.Threading.Tasks;
using global::Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.JSInterop;
using WorkoutLogs.Presentation;
using WorkoutLogs.Presentation.Shared;
using WorkoutLogs.Presentation.Contracts;
using WorkoutLogs.Presentation.Models.Exercise;

namespace WorkoutLogs.Presentation.Pages.Session
{
    public partial class Index
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public ISessionService SessionService { get; set; }

        public int SessionId { get; private set; }
        public string Message { get; set; } = string.Empty;

        protected void CreateSession()
        {
            NavigationManager.NavigateTo("/Session/Create");
        }

        protected void EndSession(int id)
        {
            NavigationManager.NavigateTo($"/Session/End/{id}");
        }
    }
}