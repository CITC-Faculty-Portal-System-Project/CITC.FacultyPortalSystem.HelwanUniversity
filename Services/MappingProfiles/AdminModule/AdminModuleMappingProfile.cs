using Domain.Entities.IdentityModule.Authorization;
using Domain.Entities.IdentityModule.Users;

namespace Services.MappingProfiles.AdminModule
{
    public class AdminModuleMappingProfile : Profile
    {
        public AdminModuleMappingProfile()
        {

            CreateMap<User, UserShowForAdminResponseDTO>()
                    .ForMember(dest => dest.Roles, opt => opt.Ignore())
                    .ForMember(dest => dest.Permissions, opt => opt.Ignore())

                    .ForMember(dest => dest.RolePermissions, opt => opt.MapFrom(src =>
                       src.Roles!.SelectMany(ur => ur.Role.Permissions!.Select(rp => rp.Permission))
                        .Distinct()
                        .Select(p => new PermissionResponseDTO
                        {
                            Code = p.Code,
                            DisplayName = p.DisplayName,
                            Description = p.Description,
                            Type = (Shared.Enums.IdentityModule.PermissionType)p.Type
                        })
                        .ToList()))


                     .ForMember(dest => dest.Permissions, opt => opt.MapFrom(src =>
                        src.Permissions!.Select(up => up.Permission)
                        .Distinct()
                        .Select(p => new PermissionResponseDTO
                        {
                            Code = p.Code,
                            DisplayName = p.DisplayName,
                            Description = p.Description,
                            Type = (Shared.Enums.IdentityModule.PermissionType)p.Type
                        })
                        .ToList()))

                    .ForMember(dest => dest.Roles,
                    opt => opt.MapFrom(src => src.Roles.Select(r => r.Role.Name).ToList()));

            
            CreateMap<Permission, PermissionResponseDTO>();
            CreateMap<Role, RoleResponseDTO>();
            CreateMap<UserAddDTO, User>()
                .ForMember(dest => dest.Permissions, opt => opt.Ignore())
                .ForMember(dest => dest.Roles, opt => opt.Ignore());
            
            CreateMap<PermissionResponseDTO, Permission>();
            CreateMap<UserEditDTO, UserAddDTO>();




        }

    }
}
