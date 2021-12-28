using WebStore.ViewModels;
using WebStore.Domain.People;
using WebStore.Domain.Entities;
using WebStore.Domain.References;

namespace WebStore.Data
{
    public static class TestData
    {
        public static List<Employee> Employees { get; } = new()
        {
            new Employee { Id = 1, LastName = "Иванов", FirstName = "Иван", Patronymic = "Иванович", Age = 28, Position = new PositionClass { Id = 3, PositionName = "Старший продавец-консультант" }, DateOfEmployment = new DateOnly(2019, 06, 15) },
            new Employee { Id = 2, LastName = "Петров", FirstName = "Петр", Patronymic = "Петрович", Age = 23, Position = new PositionClass { Id = 4, PositionName = "Главный бухгалтер" }, DateOfEmployment = new DateOnly(2018, 10, 28) },
            new Employee { Id = 3, LastName = "Баширов", FirstName = "Руслан", Patronymic = "Михайлович", Age = 32, Position = new PositionClass { Id = 5, PositionName = "Директор" }, DateOfEmployment = new DateOnly(2017, 03, 03) },
            new Employee { Id = 4, LastName = "Сидоров", FirstName = "Сидор", Patronymic = "Сидорович", Age = 18, Position = new PositionClass { Id = 2, PositionName = "Продавец-консультант" }, DateOfEmployment = new DateOnly(2020, 11, 25) },
        };
        public static List<PositionClass> Positions { get; } = new()
        {
            new PositionClass { Id = 1, PositionName = "Менеджер доставки" },
            new PositionClass { Id = 2, PositionName = "Продавец-консультант" },
            new PositionClass { Id = 3, PositionName = "Старший продавец-консультант" },
            new PositionClass { Id = 4, PositionName = "Главный бухгалтер" },
            new PositionClass { Id = 5, PositionName = "Директор" },
       
        };
        public static IEnumerable<Section> Sections { get; } = new[]
        {
              new Section { Id = 1, Name = "Спорт", Order = 0 },
              new Section { Id = 2, Name = "Nike", Order = 0, Parent = new Section { Id = 1, Name = "Спорт", Order = 0 } },
              new Section { Id = 3, Name = "Under Armour", Order = 1, Parent = new Section { Id = 1, Name = "Спорт", Order = 0 } },
              new Section { Id = 4, Name = "Adidas", Order = 2, Parent = new Section { Id = 1, Name = "Спорт", Order = 0 } },
              new Section { Id = 5, Name = "Puma", Order = 3, Parent = new Section { Id = 1, Name = "Спорт", Order = 0 } },
              new Section { Id = 6, Name = "ASICS", Order = 4, Parent = new Section { Id = 1, Name = "Спорт", Order = 0 } },
              new Section { Id = 7, Name = "Для мужчин", Order = 1 },
              new Section { Id = 8, Name = "Fendi", Order = 0, Parent = new Section { Id = 7, Name = "Для мужчин", Order = 1 } },
              new Section { Id = 9, Name = "Guess", Order = 1, Parent = new Section { Id = 7, Name = "Для мужчин", Order = 1 } },
              new Section { Id = 10, Name = "Valentino", Order = 2, Parent = new Section { Id = 7, Name = "Для мужчин", Order = 1 } },
              new Section { Id = 11, Name = "Диор", Order = 3, Parent = new Section { Id = 7, Name = "Для мужчин", Order = 1 } },
              new Section { Id = 12, Name = "Версачи", Order = 4, Parent = new Section { Id = 7, Name = "Для мужчин", Order = 1 } },
              new Section { Id = 13, Name = "Армани", Order = 5, Parent = new Section { Id = 7, Name = "Для мужчин", Order = 1 } },
              new Section { Id = 14, Name = "Prada", Order = 6, Parent = new Section { Id = 7, Name = "Для мужчин", Order = 1 } },
              new Section { Id = 15, Name = "Дольче и Габбана", Order = 7, Parent = new Section { Id = 7, Name = "Для мужчин", Order = 1 } },
              new Section { Id = 16, Name = "Шанель", Order = 8, Parent = new Section { Id = 7, Name = "Для мужчин", Order = 1 } },
              new Section { Id = 17, Name = "Гуччи", Order = 9, Parent = new Section { Id = 7, Name = "Для мужчин", Order = 1 } },
              new Section { Id = 18, Name = "Для женщин", Order = 2 },
              new Section { Id = 19, Name = "Fendi", Order = 0, Parent = new Section { Id = 18, Name = "Для женщин", Order = 2 } },
              new Section { Id = 20, Name = "Guess", Order = 1, Parent = new Section { Id = 18, Name = "Для женщин", Order = 2 } },
              new Section { Id = 21, Name = "Valentino", Order = 2, Parent = new Section { Id = 18, Name = "Для женщин", Order = 2 } },
              new Section { Id = 22, Name = "Dior", Order = 3, Parent = new Section { Id = 18, Name = "Для женщин", Order = 2 } },
              new Section { Id = 23, Name = "Versace", Order = 4, Parent = new Section { Id = 18, Name = "Для женщин", Order = 2 } },
              new Section { Id = 24, Name = "Для детей", Order = 3 },
              new Section { Id = 25, Name = "Мода", Order = 4 },
              new Section { Id = 26, Name = "Для дома", Order = 5 },
              new Section { Id = 27, Name = "Интерьер", Order = 6 },
              new Section { Id = 28, Name = "Одежда", Order = 7 },
              new Section { Id = 29, Name = "Сумки", Order = 8 },
              new Section { Id = 30, Name = "Обувь", Order = 9 },
        };

        public static IEnumerable<Brand> Brands { get; } = new[]
        {
            new Brand { Id = 1, Name = "Acne", Order = 0 },
            new Brand { Id = 2, Name = "Grune Erde", Order = 1 },
            new Brand { Id = 3, Name = "Albiro", Order = 2 },
            new Brand { Id = 4, Name = "Ronhill", Order = 3 },
            new Brand { Id = 5, Name = "Oddmolly", Order = 4 },
            new Brand { Id = 6, Name = "Boudestijn", Order = 5 },
            new Brand { Id = 7, Name = "Rosch creative culture", Order = 6 },
        };

        public static IEnumerable<Product> Products { get; } = new[]
        {
            new Product { Id = 1, Name = "Белое платье", Price = 1025, ImageUrl = "product1.jpg", Order = 0
                , Section =  new Section { Id = 2, Name = "Nike", Order = 0, Parent = new Section { Id = 1, Name = "Спорт", Order = 0 } }
                , Brand = new Brand { Id = 1, Name = "Acne", Order = 0 } },
            new Product { Id = 2, Name = "Розовое платье", Price = 1025, ImageUrl = "product2.jpg", Order = 1
                , Section =  new Section { Id = 2, Name = "Nike", Order = 0, Parent = new Section { Id = 1, Name = "Спорт", Order = 0 } }
                , Brand = new Brand { Id = 1, Name = "Acne", Order = 0 } },
            new Product { Id = 3, Name = "Красное платье", Price = 1025, ImageUrl = "product3.jpg", Order = 2
                , Section =  new Section { Id = 2, Name = "Nike", Order = 0, Parent = new Section { Id = 1, Name = "Спорт", Order = 0 } }
                , Brand = new Brand { Id = 1, Name = "Acne", Order = 0 } },
            new Product { Id = 4, Name = "Джинсы", Price = 1025, ImageUrl = "product4.jpg", Order = 3
                , Section =  new Section { Id = 2, Name = "Nike", Order = 0, Parent = new Section { Id = 1, Name = "Спорт", Order = 0 } }
                , Brand = new Brand { Id = 1, Name = "Acne", Order = 0 } },
            new Product { Id = 5, Name = "Лёгкая майка", Price = 1025, ImageUrl = "product5.jpg", Order = 4
                , Section =  new Section { Id = 2, Name = "Nike", Order = 0, Parent = new Section { Id = 1, Name = "Спорт", Order = 0 } }
                , Brand = new Brand { Id = 2, Name = "Grune Erde", Order = 1 } },
            new Product { Id = 6, Name = "Лёгкое голубое поло", Price = 1025, ImageUrl = "product6.jpg", Order = 5
                , Section =  new Section { Id = 2, Name = "Nike", Order = 0, Parent = new Section { Id = 1, Name = "Спорт", Order = 0 } }
                , Brand = new Brand { Id = 1, Name = "Acne", Order = 0 } },
            new Product { Id = 7, Name = "Платье белое", Price = 1025, ImageUrl = "product7.jpg", Order = 6
                , Section =  new Section { Id = 2, Name = "Nike", Order = 0, Parent = new Section { Id = 1, Name = "Спорт", Order = 0 } }
                , Brand = new Brand { Id = 1, Name = "Acne", Order = 0 } },
            new Product { Id = 8, Name = "Костюм кролика", Price = 1025, ImageUrl = "product8.jpg", Order = 7
                , Section = new Section { Id = 25, Name = "Мода", Order = 4 }
                , Brand = new Brand { Id = 1, Name = "Acne", Order = 0 } },
            new Product { Id = 9, Name = "Красное китайское платье", Price = 1025, ImageUrl = "product9.jpg", Order = 8
                , Section = new Section { Id = 25, Name = "Мода", Order = 4 }
                , Brand = new Brand { Id = 1, Name = "Acne", Order = 0 } },
            new Product { Id = 10, Name = "Женские джинсы", Price = 1025, ImageUrl = "product10.jpg", Order = 9
                , Section = new Section { Id = 25, Name = "Мода", Order = 4 }
                , Brand = new Brand { Id = 3, Name = "Albiro", Order = 2 } },
            new Product { Id = 11, Name = "Джинсы женские", Price = 1025, ImageUrl = "product11.jpg", Order = 10
                , Section = new Section { Id = 25, Name = "Мода", Order = 4 }
                , Brand = new Brand { Id = 3, Name = "Albiro", Order = 2 } },
            new Product { Id = 12, Name = "Летний костюм", Price = 1025, ImageUrl = "product12.jpg", Order = 11
                , Section = new Section { Id = 25, Name = "Мода", Order = 4 }
                , Brand = new Brand { Id = 3, Name = "Albiro", Order = 2 } },
        };

    }
}
