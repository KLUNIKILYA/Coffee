using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Coffee.Pages.Rent
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public RentRequest Input { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // ТУТ ЛОГИКА ОТПРАВКИ:
            // 1. Отправить письмо администратору (EmailService)
            // 2. Или сохранить заявку в БД

            // Показываем сообщение об успехе
            TempData["SuccessMessage"] = "Спасибо! Ваша заявка принята. Мы свяжемся с вами в течение часа.";

            // Сбрасываем форму
            ModelState.Clear();
            Input = new RentRequest();

            return Page();
        }

        public class RentRequest
        {
            [Required(ErrorMessage = "Как к вам обращаться?")]
            public string Name { get; set; }

            [Required(ErrorMessage = "Укажите телефон для связи")]
            [Phone(ErrorMessage = "Некорректный номер")]
            public string Phone { get; set; }

            [Required(ErrorMessage = "Выберите желаемую дату")]
            public DateTime? Date { get; set; }

            public string EventType { get; set; } // Лекция, Воркшоп, Праздник

            public string Comment { get; set; }
        }
    }
}