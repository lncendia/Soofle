﻿namespace VkQ.Domain.Links.Exceptions;

public class LinkAlreadyConfirmedException:Exception
{
    public LinkAlreadyConfirmedException():base("Link already confirmed")
    {
    }
}