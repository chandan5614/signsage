using FluentValidation;
using API.DTOs.Document;

namespace API.Validators
{
    public class DocumentValidator : AbstractValidator<CreateDocumentDto>
    {
        public DocumentValidator()
        {
            RuleFor(d => d.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");

            RuleFor(d => d.Description)
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

            RuleFor(d => d.UserId)
                .NotEmpty().WithMessage("UserId is required.");
        }
    }
}
