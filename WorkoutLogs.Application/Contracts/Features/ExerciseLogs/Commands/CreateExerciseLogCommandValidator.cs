using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLogs.Application.Persistence;

namespace WorkoutLogs.Application.Contracts.Features.ExerciseLogs.Commands
{
    public class CreateExerciseLogCommandValidator : AbstractValidator<CreateExerciseLogCommand>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IDifficultyRepository _difficultyRepository;
        private readonly IExerciseRepository _exerciseRepository;
        public CreateExerciseLogCommandValidator(IMemberRepository memberRepository, 
            IDifficultyRepository difficultyRepository, 
            IExerciseRepository exerciseRepository)
        {
            _memberRepository = memberRepository;
            _difficultyRepository = difficultyRepository;
            _exerciseRepository = exerciseRepository;

            RuleFor(x => x.MemberId).NotEmpty().MustAsync(MemberExists).WithMessage("Member does not exist.");
            RuleFor(x => x.SessionId).NotEmpty().WithMessage("SessionId is required.");
            RuleFor(x => x.ExerciseId).NotEmpty().WithMessage("ExerciseId is required.").MustAsync(ExerciseExists).WithMessage("Exercise does not exist."); ;
            RuleFor(x => x.Sets).GreaterThan(0).WithMessage("Sets must be greater than 0.");
            RuleFor(x => x.Reps).GreaterThan(0).WithMessage("Reps must be greater than 0.");
            RuleFor(x => x.Weight).GreaterThan(0).WithMessage("Weight must be greater than 0.");
            RuleFor(x => x.DifficultyId).NotEmpty().WithMessage("DifficultyId is required.").MustAsync(DifficultyExists).WithMessage("Difficulty does not exist."); ;
        }
        private async Task<bool> MemberExists(int id, CancellationToken cancellationToken)
        {
            return await _memberRepository.MemberExists(id, cancellationToken);
        }

        private async Task<bool> ExerciseExists(int id, CancellationToken cancellationToken)
        {
            return await _exerciseRepository.ExerciseExists(id, cancellationToken);
        }

        private async Task<bool> DifficultyExists(int id, CancellationToken cancellationToken)
        {
            return await _difficultyRepository.DifficultyExists(id, cancellationToken);
        }
    }

}
