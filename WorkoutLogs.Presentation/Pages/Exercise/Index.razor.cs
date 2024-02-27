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
using System.Text.RegularExpressions;

namespace WorkoutLogs.Presentation.Pages.Exercise
{
    public partial class Index
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IExerciseService ExerciseService { get; set; }

        public ICollection<ExerciseVM> Exercises { get; private set; }
        public string Message { get; set; } = string.Empty;

        protected void CreateExercise()
        {
            NavigationManager.NavigateTo("/Exercises/Create");
        }

        protected void GetByGroupId(int groupId)
        {
            NavigationManager.NavigateTo($"/Exercise/byGroupId/{groupId}");
        }


        protected override async Task OnInitializedAsync()
        {
            Exercises = await ExerciseService.GetByGroupIdAsync(17);
        }
    }
}