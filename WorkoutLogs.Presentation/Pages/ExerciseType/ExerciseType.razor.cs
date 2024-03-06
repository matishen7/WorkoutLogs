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
using WorkoutLogs.Presentation.Services.Base;

namespace WorkoutLogs.Presentation.Pages.ExerciseType
{
    public partial class ExerciseType
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IExerciseTypeService ExerciseTypeService { get; set; }

        public string Message { get; set; } = string.Empty;

        public ICollection<ExerciseTypeDto> ExerciseTypes { get; set; } = new List<ExerciseTypeDto>();

        private void OptionClicked(int exerciseTypeId)
        {
            NavigationManager.NavigateTo($"/session/{exerciseTypeId}");
        }

        protected async Task LoadExerciseTypes()
        {
            ExerciseTypes = await ExerciseTypeService.GetAllExerciseTypes();
        }

        protected override async Task OnInitializedAsync()
        {
            await LoadExerciseTypes();
        }
    }
}