using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLogs.Application.Persistence;

namespace WorkoutLogs.Application.Contracts.Features.Sessions.Commands
{
    public class CreateSessionCommandValidator : AbstractValidator<CreateSessionCommand>
    {
        private readonly IMemberRepository _memberRepository;

        public CreateSessionCommandValidator(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository ?? throw new ArgumentNullException(nameof(memberRepository));

            RuleFor(x => x.MemberId)
                .GreaterThan(0).WithMessage("MemberId must be greater than 0")
                .MustAsync(MemberExists).WithMessage("Member does not exist.");
        }

        private async Task<bool> MemberExists(int memberId, CancellationToken cancellationToken)
        {
            return await _memberRepository.Exists(memberId);
        }
    }

}
