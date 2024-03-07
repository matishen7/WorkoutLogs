using global::Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using WorkoutLogs.Presentation.Contracts;
using WorkoutLogs.Presentation.Models.Exercise;
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
        [Inject]

        public IExerciseService ExerciseService { get; set; }

        public int CurrentSessionId { get; private set; }
        public string Message { get; set; } = string.Empty;

        public ICollection<ExerciseTypeDto> ExerciseTypes { get; set; } = new List<ExerciseTypeDto>();
        public ICollection<ExerciseGroupDto> ExerciseGroups { get; set; } = new List<ExerciseGroupDto>();
        public ICollection<ExerciseLogDto> ExerciseLogs { get; set; } = new List<ExerciseLogDto>();
        public ICollection<ExerciseVM> Exercises { get; set; } = new List<ExerciseVM>();
        private ExerciseLogDto exerciseLog = new ExerciseLogDto();

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
        private async Task ShowConfirmation()
        {
            bool confirmed = await JSRuntime.InvokeAsync<bool>("confirm", "Are you sure you want to end the session?");

            if (confirmed)
            {
                await EndSession();
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

        public int SelectedExerciseGroupId { get; set; }

        protected async Task ExerciseGroupChanged(ChangeEventArgs e)
        {
            if (int.TryParse(e.Value.ToString(), out int selectedId))
            {
                SelectedExerciseGroupId = selectedId;
                Exercises.Clear();
                Exercises = await ExerciseService.GetByGroupIdAsync(SelectedExerciseGroupId);
            }
            else
            {
                SelectedExerciseGroupId = 0;
            }
        }

        public int SelectedExerciseId { get; set; }
        public string CurrentExerciseTutorial { get; set; } = string.Empty;

        protected async Task ExerciseChanged(ChangeEventArgs e)
        {
            if (int.TryParse(e.Value.ToString(), out int selectedId))
            {
                SelectedExerciseId = selectedId;
                CurrentExerciseTutorial = Exercises.FirstOrDefault(x => x.Id == selectedId).TutorialUrl;
            }
            else
            {
                SelectedExerciseId = 0;
            }
        }

        protected async Task SubmitForm()
        {
            var createExerciseLog = new CreateExerciseLogCommand()
            {
                MemberId = 2,
                DifficultyId = exerciseLog.DifficultyId,
                AdditionalNotes = (exerciseLog.AdditionalNotes == null)? string.Empty : exerciseLog.AdditionalNotes,
                ExerciseId = exerciseLog.ExerciseId,
                Reps = exerciseLog.Reps,
                SessionId = CurrentSessionId,
                Sets = exerciseLog.Sets,
                Weight = exerciseLog.Weight,
            };

            try
            {
                var exerciseLog = await ExerciseLogService.CreateExerciseLog(createExerciseLog);
                Message = "You successfully logged exercise.";
            }
            catch (Exception)
            {
                Message = "Something went wrong with logging the exercise.";
            }
        }
    }
}