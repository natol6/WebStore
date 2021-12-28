using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Domain.People;

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
        [RegularExpression(@"(([А-ЯЁ][а-яё]+)|([A-Z][a-z]+))?", ErrorMessage = "Ошибка формата")]
        public string Patronymic { get; set; }
        
        [Display(Name = "Возраст")]
        [Range(18, 80, ErrorMessage ="Возраст должен быть в пределах 18 - 80 лет")]
        public int Age { get; set; }
        
        [Display(Name = "Должность")]
        [Required(ErrorMessage = "Ввод должности обязателен")]
        public string Position { get; set; }
        
        [Display(Name = "Дата приема на работу")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")]
        [Required(ErrorMessage = "Ввод даты обязателен")]
        [RegularExpression(@"((0[1-9])|([12][0-9])|(3[01]))[.]((0[1-9])|(1[0-2]))[.](20[0-9][0-9])", ErrorMessage = "Дата должна быть в формате: дд.мм.гггг")]
        public DateOnly DateOfEmployment { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        
    }
}
