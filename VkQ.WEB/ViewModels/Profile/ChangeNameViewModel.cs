﻿using System.ComponentModel.DataAnnotations;

namespace VkQ.WEB.ViewModels.Profile;

public class ChangeNameViewModel
{
    [Required(ErrorMessage = "Поле не должно быть пустым")]
    [Display(Name = "Введите имя пользователя")]
    [RegularExpression("^[a-zA-Zа-яА-Я0-9_ ]{3,20}$",
        ErrorMessage = "Имя пользователя должно содержать от 3 до 20 символов")]
    public string Name { get; set; } = null!;
}