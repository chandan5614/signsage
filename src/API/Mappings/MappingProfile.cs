using AutoMapper;
using API.DTOs.AuditTrail;
using API.DTOs.Customer;
using API.DTOs.Document;
using API.DTOs.Signature;
using API.DTOs.Template;
using API.DTOs.User;
using Core.Entities;

namespace API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // AuditTrail mappings
            CreateMap<AuditTrail, AuditTrailDto>();
            CreateMap<AuditTrailDto, AuditTrail>();
            CreateMap<CreateAuditTrailDto, AuditTrail>();

            // Customer mappings
            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerDto, Customer>();
            CreateMap<CreateCustomerDto, Customer>();
            CreateMap<UpdateCustomerDto, Customer>();

            // Document mappings
            CreateMap<Document, DocumentDto>();
            CreateMap<DocumentDto, Document>();
            CreateMap<CreateDocumentDto, Document>();
            CreateMap<UpdateDocumentDto, Document>();

            // Signature mappings
            CreateMap<Signature, SignatureDto>();
            CreateMap<SignatureDto, Signature>();
            CreateMap<CreateSignatureDto, Signature>();

            // Template mappings
            CreateMap<Template, TemplateDto>();
            CreateMap<TemplateDto, Template>();
            CreateMap<CreateTemplateDto, Template>();
            CreateMap<UpdateTemplateDto, Template>();

            // User mappings
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<CreateUserDto, User>();
            CreateMap<UpdateUserDto, User>();

            // Role mappings
            CreateMap<Role, RoleDto>();
            CreateMap<RoleDto, Role>();
            CreateMap<CreateRoleDto, Role>();
        }
    }
}
