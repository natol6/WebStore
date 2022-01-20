using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using WebStore.DAL.Context;
using WebStore.Data;
using WebStore.Domain.Entities.Identity;
using WebStore.Interfaces.Services;

namespace WebStore.Services.Services
{
    public class DbInitializer : IDbInitializer
    {
        private readonly WebStoreDB _db;

        private readonly ILogger<DbInitializer> _logger;

        private readonly UserManager<User> _UserManager;

        private readonly RoleManager<Role> _RoleManager;

        public DbInitializer(WebStoreDB db, ILogger<DbInitializer> Logger, UserManager<User> UserManager, RoleManager<Role> RoleManager)
        {
            _db = db;
            _logger = Logger;
            _UserManager = UserManager;
            _RoleManager = RoleManager;
        }

        public async Task<bool> RemoveAsinc(CancellationToken Cancel = default)
        {
            var result = await _db.Database.EnsureDeletedAsync(Cancel).ConfigureAwait(false);

            if (result)
                _logger.LogInformation("Удаление БД выполнено успешно");
            else
                _logger.LogInformation("Удаление БД не требуется (отсутствует)");

            return result;
        }

        public async Task InitializeAsync(bool RemoveBefore = false, CancellationToken Cancel = default)
        {
            _logger.LogInformation("Инициализация БД...");

            if (RemoveBefore)
                await RemoveAsinc(Cancel).ConfigureAwait(false);

            var pending_migrations = await _db.Database.GetPendingMigrationsAsync(Cancel);
            if (pending_migrations.Any())
            {
                _logger.LogInformation("Выполнение миграции БД...");

                await _db.Database.MigrateAsync(Cancel).ConfigureAwait(false);

                _logger.LogInformation("Выполнение миграции БД выполнено успешно");
            }

            await InitializeProductsAsync(Cancel).ConfigureAwait(false);

            await InitializeEmployeesAsync(Cancel).ConfigureAwait(false);

            await InitializeIdentityAsync(Cancel).ConfigureAwait(false);

            _logger.LogInformation("Инициализация БД выполнена успешно");
        }

        private async Task InitializeProductsAsync(CancellationToken Cancel = default)
        {
            if (_db.Sections.Any())
            {
                _logger.LogInformation("Инициализация тестовых данных о товарах не требуется");
                return;
            }

            _logger.LogInformation("Инициализация тестовых данных о товарах ...");

            var sections_pool = TestData.Sections.ToDictionary(s => s.Id);
            var brands_pool = TestData.Brands.ToDictionary(b => b.Id);

            foreach (var child_section in TestData.Sections.Where(s => s.ParentId is not null))
                child_section.Parent = sections_pool[(int)child_section.ParentId!];

            foreach (var product in TestData.Products)
            {
                product.Section = sections_pool[product.SectionId];
                if (product.BrandId is { } brand_id)
                    product.Brand = brands_pool[brand_id];

                product.Id = 0;
                product.SectionId = 0;
                product.BrandId = null;
            }

            foreach (var section in TestData.Sections)
            {
                section.Id = 0;
                section.ParentId = null;
            }

            foreach (var brand in TestData.Brands)
                brand.Id = 0;
            await using (await _db.Database.BeginTransactionAsync(Cancel))
            {
                await _db.Sections.AddRangeAsync(TestData.Sections, Cancel);
                await _db.Brands.AddRangeAsync(TestData.Brands, Cancel);
                await _db.Products.AddRangeAsync(TestData.Products, Cancel);
                await _db.SaveChangesAsync(Cancel);

                await _db.Database.CommitTransactionAsync(Cancel);
            }
            _logger.LogInformation("Инициализация тестовых данных товаров выполнена успешно");
        }

        private async Task InitializeEmployeesAsync(CancellationToken Cancel)
        {
            if (await _db.Employees.AnyAsync(Cancel))
            {
                _logger.LogInformation("Инициализация сотрудников не требуется");
                return;
            }

            _logger.LogInformation("Инициализация сотрудников...");

            var positions_pool = TestData.Positions.ToDictionary(s => s.Id);

            foreach (var employee in TestData.Employees)
            {
                employee.Position = positions_pool[employee.PositionId];

                employee.Id = 0;
                employee.PositionId = 0;
            }

            foreach (var position in TestData.Positions)
            {
                position.Id = 0;
            }

            await using var transaction = await _db.Database.BeginTransactionAsync(Cancel);

            await _db.Positions.AddRangeAsync(TestData.Positions, Cancel);
            await _db.Employees.AddRangeAsync(TestData.Employees, Cancel);

            await _db.SaveChangesAsync(Cancel);

            await transaction.CommitAsync(Cancel);
            _logger.LogInformation("Инициализация сотрудников выполнена успешно");
        }

        private async Task InitializeIdentityAsync(CancellationToken Cancel)
        {
            _logger.LogInformation("Инициализация данных системы Identity");

            var timer = Stopwatch.StartNew();

            async Task CheckRole(string RoleName)
            {
                if (await _RoleManager.RoleExistsAsync(RoleName))
                    _logger.LogInformation("Роль {0} существует в БД. {1} c", RoleName, timer.Elapsed.TotalSeconds);
                else
                {
                    _logger.LogInformation("Роль {0} не существует в БД. {1} c", RoleName, timer.Elapsed.TotalSeconds);

                    await _RoleManager.CreateAsync(new Role { Name = RoleName });

                    _logger.LogInformation("Роль {0} создана. {1} c", RoleName, timer.Elapsed.TotalSeconds);
                }
            }

            await CheckRole(Role.Administrators);
            await CheckRole(Role.Users);

            if (await _UserManager.FindByNameAsync(User.Administrator) is null)
            {
                _logger.LogInformation("Пользователь {0} отсутствует в БД. Создаю... {1} c", User.Administrator, timer.Elapsed.TotalSeconds);

                var admin = new User
                {
                    UserName = User.Administrator,
                };

                var creation_result = await _UserManager.CreateAsync(admin, User.DefaultAdminPassword);
                if (creation_result.Succeeded)
                {
                    _logger.LogInformation("Пользователь {0} создан успешно. Наделяю его правами администратора... {1} c", User.Administrator, timer.Elapsed.TotalSeconds);

                    await _UserManager.AddToRoleAsync(admin, Role.Administrators);

                    _logger.LogInformation("Пользователь {0} наделён правами администратора. {1} c", User.Administrator, timer.Elapsed.TotalSeconds);
                }
                else
                {
                    var errors = creation_result.Errors.Select(err => err.Description);
                    _logger.LogError("Учётная запись администратора не создана. Ошибки:{0}", string.Join(", ", errors));

                    throw new InvalidOperationException($"Невозможно создать пользователя {User.Administrator} по причине: {string.Join(", ", errors)}");
                }
            }

            _logger.LogInformation("Данные системы Identity успешно добавлены в БД за {0} c", timer.Elapsed.TotalSeconds);
        }
    }
}
