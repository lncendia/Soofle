﻿namespace Soofle.Application.Abstractions.Users.Exceptions;

public class InvalidPasswordFormatException : Exception
{
    public InvalidPasswordFormatException() : base($"Specified password is invalid")
    {
    }
}