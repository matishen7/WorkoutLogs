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
using WorkoutLogs.Presentation.Services;
using WorkoutLogs.Presentation.Services.Base;

namespace WorkoutLogs.Presentation.Pages.Session
{
    public partial class Index
    {
        [Parameter]
        public int exerciseTypeId { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public ISessionService SessionService { get; set; }

        [Inject]
        public IExerciseGroupService ExerciseGroupService { get; set; }

        [Inject]
        public IExerciseLogService ExerciseLogService { get; set; }

        public int CurrentSessionId { get; private set; }
        public string Message { get; set; } = string.Empty;

        public ICollection<ExerciseTypeDto> ExerciseTypes { get; set; } = new List<ExerciseTypeDto>();
        public ICollection<ExerciseGroupDto> ExerciseGroups { get; set; } = new List<ExerciseGroupDto>();
        public ICollection<ExerciseLogDto> ExerciseLogs { get; set; } = new List<ExerciseLogDto>();

        protected async Task CreateSession()
        {
            if (CurrentSessionId != 0)
            {
                Message = "You need to end current session before creating new workout session.";
                return;
            }

            var createSessionResponse = await SessionService.CreateSession(2, CancellationToken.None);
            if (createSessionResponse.Success == true)
            {
                CurrentSessionId = createSessionResponse.Data;
                Message = "New workout session created. Enjoy!";
            }
            else
            {
                CurrentSessionId = 0;
                Message = "Something went wrong with creating workout session! Try again later..";
            }
        }

        protected async Task EndSession()
        {
            if (CurrentSessionId == 0) { Message = "Please, create new workout session first!"; return; }
            var endSessionResponse = await SessionService.EndSession(CurrentSessionId, CancellationToken.None);
            if (endSessionResponse.Success == true)
            {
                CurrentSessionId = 0;
                Message = "Workout session ended. Thanks!";
            }
            else
            {
                Message = "Something went wrong with ending workout session! Try again later..";
            }
        }

        protected async Task LoadExerciseGroups(int exerciseTypeId)
        {
            ExerciseGroups = await ExerciseGroupService.GetExerciseGroupsAsync(exerciseTypeId);
        }

        protected override async Task OnInitializedAsync()
        {
            await CreateSession();
            if (exerciseTypeId != 0) await LoadExerciseGroups(exerciseTypeId);
        }
    }
}