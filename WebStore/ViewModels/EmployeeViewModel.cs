using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStore.ViewModels
{
    public class EmployeeViewModel
    {
        [HiddenInput(DisplayValue=false)]
        public int Id { get; set; }
        [Display(Name="Фамилия")]
        [Required(ErrorMessage ="Ввод фамилии обязателен")]
        [StringLength(50,MinimumLength =2, ErrorMessage ="Длина должна быть от 2 до 50 символов")]
        [RegularExpression(@"([А-ЯЁ][а-яё]+)|([A-Z][a-z]+)", ErrorMessage ="Ошибка формата")]
        public string LastName { get; set; }
        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Ввод имени обязателен")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Длина должна быть от 2 до 50 символов")]
        [RegularExpression(@"([А-ЯЁ][а-яё]+)|([A-Z][a-z]+)", ErrorMessage = "Ошибка формата")]
        public string FirstName { get; set; }
        [Display(Name = "Отчество")]
        [StringLength(50, ErrorMessage = "Длина не должна превышать 50 символов")]
        [RegularExpression(@"([А-ЯЁ][а-яё]+)|([A-Z][a-z]+)", ErrorMessage = "Ошибка формата")]
        public string Patronymic { get; set; }
        [Display(Name = "Возраст")]
        [Range(18, 80, ErrorMessage ="Возраст должен быть в пределах 18 - 80 лет")]
        public int Age { get; set; }
        [Display(Name = "Должность")]
        [Required(ErrorMessage = "Ввод должности обязателен")]
        
        public string Position { get; set; }
        [Display(Name = "Дата приема на работу")]
        [Required(ErrorMessage = "Ввод даты обязателен")]
        public string DateOfEmployment { get; set; }
    }
}
