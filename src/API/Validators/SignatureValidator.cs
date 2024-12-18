using FluentValidation;
using API.DTOs.Signature;

namespace API.Validators
{
    public class SignatureValidator : AbstractValidator<CreateSignatureDto>
    {
        public SignatureValidator()
        {
            RuleFor(s => s.SignatureContent)
                .NotEmpty().WithMessage("Signature content is required.");

            RuleFor(s => s.DocumentId)
                .NotEmpty().WithMessage("DocumentId is required.");

            RuleFor(s => s.UserId)
                .NotEmpty().WithMessage("UserId is required.");
        }
    }
}
