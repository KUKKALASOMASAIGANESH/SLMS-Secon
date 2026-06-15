using AutoMapper;
using SLMS.DOL.Entities;
using SLMS.Shared.DTOs.Category;
using SLMS.Shared.DTOs.LibraryResource;
using SLMS.Shared.DTOs.InventoryItem;
using SLMS.Shared.DTOs.Request;
using SLMS.Shared.DTOs.Employee;
using SLMS.Shared.DTOs.User;
using SLMS.Shared.DTOs.BookIssue;
using SLMS.Shared.DTOs.Role;
using SLMS.Shared.DTOs.Permission;
using SLMS.Shared.DTOs.RolePermission;

namespace SLMS.WebAPI.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Category

        CreateMap<CategoryCreateDto, Category>();

        CreateMap<Category, CategoryResponseDto>();


        // Library Resource

        CreateMap<LibraryResourceCreateDto, LibraryResource>();

        CreateMap<LibraryResource, LibraryResourceResponseDto>();

        CreateMap<LibraryResourceUpdateDto, LibraryResource>();


        // Inventory Item

        CreateMap<InventoryItemCreateDto, InventoryItem>();

        CreateMap<InventoryItem, InventoryItemResponseDto>();

        CreateMap<InventoryItemUpdateDto, InventoryItem>();

        //Employee
      

        CreateMap<EmployeeCreateDto, Employee>();

        CreateMap<Employee, EmployeeResponseDto>();

        CreateMap<EmployeeUpdateDto, Employee>();


        // Request

        CreateMap<RequestCreateDto, Request>();

        CreateMap<Request, RequestResponseDto>();

        CreateMap<RequestUpdateDto, Request>();

        // User

        CreateMap<UserCreateDto, User>();

        CreateMap<User, UserResponseDto>();

        CreateMap<UserUpdateDto, User>();

        // Book Issue

        CreateMap<BookIssueCreateDto, BookIssue>();

        CreateMap<BookIssue, BookIssueResponseDto>();

        CreateMap<BookIssueUpdateDto, BookIssue>();

        // Role
        CreateMap<RoleCreateDto, Role>();

        CreateMap<RoleUpdateDto, Role>();

        CreateMap<Role, RoleResponseDto>();

        //Permission
        CreateMap<PermissionCreateDto, Permission>();

        CreateMap<PermissionUpdateDto, Permission>();

        CreateMap<Permission, PermissionResponseDto>();

        //RolePermission
        CreateMap<RolePermissionCreateDto,RolePermission>();

        CreateMap<RolePermission,RolePermissionResponseDto>();
    }
}