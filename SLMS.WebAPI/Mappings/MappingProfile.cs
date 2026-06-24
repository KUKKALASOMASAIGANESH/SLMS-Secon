using AutoMapper;

using SLMS.DOL.Entities;

using SLMS.Shared.DTOs.Department;
using SLMS.Shared.DTOs.Inventory;
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
using SLMS.Shared.DTOs.BookReturn;


using SLMS.Shared.DTOs.Shelf;

namespace SLMS.WebAPI.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Department
        CreateMap<DepartmentCreateDto, Department>();
        CreateMap<DepartmentUpdateDto, Department>();
        CreateMap<Department, DepartmentResponseDto>();

        // Inventory
        CreateMap<InventoryItem, InventoryItemDto>();
        CreateMap<CreateInventoryItemDto, InventoryItem>();
        CreateMap<UpdateInventoryItemDto, InventoryItem>();

        // Category
        CreateMap<CategoryCreateDto, Category>();
        CreateMap<Category, CategoryResponseDto>();

        // Library Resource
        CreateMap<LibraryResourceCreateDto, LibraryResource>();
        CreateMap<LibraryResource, ShelfResourceDto>();
        //shelf
        //CreateMap<LibraryResource, LibraryResourceResponseDto>();
        CreateMap<LibraryResource, LibraryResourceResponseDto>()
     .ForMember(
         dest => dest.ShelfName,
         opt => opt.MapFrom(
             src => src.Shelf != null
                 ? src.Shelf.ShelfName
                 : null))
     .ForMember(
         dest => dest.CategoryName,
         opt => opt.MapFrom(
             src => src.Category != null
                 ? src.Category.Name
                 : string.Empty));
        CreateMap<LibraryResourceUpdateDto, LibraryResource>();

        // Inventory Item
        CreateMap<InventoryItemCreateDto, InventoryItem>();
        CreateMap<InventoryItem, InventoryItemResponseDto>();
        CreateMap<InventoryItemUpdateDto, InventoryItem>();

        // Employee
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
        CreateMap<BookIssue, BookIssueResponseDto>()
     .ForMember(
         dest => dest.EmployeeName,
         opt => opt.MapFrom(
             src => src.Employee.FullName))
     .ForMember(
         dest => dest.BookTitle,
         opt => opt.MapFrom(
             src => src.LibraryResource.Title));


        CreateMap<BookIssueUpdateDto, BookIssue>();

        // Role
        CreateMap<RoleCreateDto, Role>();
        CreateMap<RoleUpdateDto, Role>();
        CreateMap<Role, RoleResponseDto>();

        // Permission
        CreateMap<PermissionCreateDto, Permission>();
        CreateMap<PermissionUpdateDto, Permission>();
        CreateMap<Permission, PermissionResponseDto>();

        // Role Permission
        CreateMap<RolePermissionCreateDto, RolePermission>();
        CreateMap<RolePermission, RolePermissionResponseDto>();

        // Book Return
        CreateMap<BookReturnCreateDto, BookReturn>();
        CreateMap<BookReturn, BookReturnResponseDto>();
        CreateMap<BookReturnUpdateDto, BookReturn>();

        //Shelf
        CreateMap<Shelf, ShelfResponseDto>();
        CreateMap<ShelfCreateDto, Shelf>();
        CreateMap<ShelfUpdateDto, Shelf>();


    }
}
