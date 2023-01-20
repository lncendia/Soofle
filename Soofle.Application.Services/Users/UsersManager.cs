﻿using Microsoft.AspNetCore.Identity;
using Soofle.Application.Abstractions.Users.DTOs;
using Soofle.Application.Abstractions.Users.Entities;
using Soofle.Application.Abstractions.Users.Exceptions;
using Soofle.Application.Abstractions.Users.ServicesInterfaces;
using Soofle.Domain.Abstractions.UnitOfWorks;
using Soofle.Domain.Specifications;
using Soofle.Domain.Specifications.Abstractions;
using Soofle.Domain.Users.Entities;
using Soofle.Domain.Users.Ordering;
using Soofle.Domain.Users.Specification;
using Soofle.Domain.Users.Specification.Visitor;

namespace Soofle.Application.Services.Users;

public class UsersManager : IUsersManager
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<UserData> _userManager;
    private readonly IPasswordHasher<UserData> _passwordHasher;
    private readonly IPasswordValidator<UserData> _passwordValidator;

    public UsersManager(IUnitOfWork unitOfWork, UserManager<UserData> userManager,
        IPasswordHasher<UserData> passwordHasher, IPasswordValidator<UserData> passwordValidator)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _passwordHasher = passwordHasher;
        _passwordValidator = passwordValidator;
    }

    public async Task<List<UserShortDto>> FindAsync(SearchQuery query)
    {
        ISpecification<User, IUserSpecificationVisitor>? spec = null;
        if (!string.IsNullOrEmpty(query.Email)) spec = new UserByEmailSpecification(query.Email);
        if (!string.IsNullOrEmpty(query.Name))
        {
            spec = spec == null
                ? new UserByNameSpecification(query.Name)
                : new AndSpecification<User, IUserSpecificationVisitor>(spec, new UserByNameSpecification(query.Name));
        }

        var users = await _unitOfWork.UserRepository.Value.FindAsync(spec, new UserByNameOrder(), (query.Page - 1) * 30,
            30);
        return users.Select(x => new UserShortDto(x.Name, x.Email, x.Id)).ToList();
    }

    public async Task<UserDto> GetAsync(Guid userId)
    {
        var user = await _unitOfWork.UserRepository.Value.GetAsync(userId);
        if (user == null) throw new UserNotFoundException();
        return new UserDto(user.Name, user.Email, user.Id, user.Subscription?.ExpirationDate);
    }

    public async Task EditAsync(EditUserDto editData)
    {
        var user = await _unitOfWork.UserRepository.Value.GetAsync(editData.Id);
        if (user == null) throw new UserNotFoundException();
        var userApplication = await _userManager.FindByEmailAsync(user.Email);
        if (userApplication == null) throw new UserNotFoundException();
        if (!string.IsNullOrEmpty(editData.Email))
        {
            user.Email = editData.Email;
            var result = await _userManager.SetEmailAsync(userApplication, editData.Email);
            if (!result.Succeeded) throw new UserAlreadyExistException();
            userApplication.EmailConfirmed = true;
            await _userManager.UpdateAsync(userApplication);
        }

        if (!string.IsNullOrEmpty(editData.Username))
        {
            user.Name = editData.Username;
            await _userManager.SetUserNameAsync(userApplication, editData.Username);
        }

        await _unitOfWork.UserRepository.Value.UpdateAsync(user);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task ChangePasswordAsync(string email, string password)
    {
        var userApplication = await _userManager.FindByEmailAsync(email);
        if (userApplication == null) throw new UserNotFoundException();
        var result = await _passwordValidator.ValidateAsync(_userManager, userApplication, password);
        if (!result.Succeeded) throw new InvalidPasswordFormatException();
        userApplication.PasswordHash = _passwordHasher.HashPassword(userApplication, password);
        await _userManager.UpdateAsync(userApplication);
    }

    public async Task AddSubscribeAsync(Guid userId, TimeSpan timeSpan)
    {
        var user = await _unitOfWork.UserRepository.Value.GetAsync(userId);
        if (user == null) throw new UserNotFoundException();
        user.AddSubscription(timeSpan);
        await _unitOfWork.UserRepository.Value.UpdateAsync(user);
        await _unitOfWork.SaveChangesAsync();
    }
}