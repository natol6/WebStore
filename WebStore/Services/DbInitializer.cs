using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.Data;
using WebStore.Services.Interfaces;

namespace WebStore.Services
{
    public class DbInitializer : IDbInitializer
    {
        private readonly WebStoreDB _db;
        private readonly ILogger<DbInitializer> _logger;
        public DbInitializer(WebStoreDB db, ILogger<DbInitializer> Logger)
        {
            _db = db;
            _logger = Logger;
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

            var sections_pool = TestData.Sections;
            var brands_pool = TestData.Brands;
            var products_pool = TestData.Products;
            foreach(var section in sections_pool)
            {
                section.Id = 0;
                if(section.Parent is not null)
                    section.Parent.Id = 0;
            }
            foreach (var brand in brands_pool)
            {
                brand.Id = 0;
            }
            foreach (var product in products_pool)
            {
                product.Id = 0;
                product.Section.Id = 0;
                if(product.Section.Parent is not null)
                    product.Section.Parent.Id = 0;
                if (product.Brand is not null)
                    product.Brand.Id = 0;
                
            }

            await using (await _db.Database.BeginTransactionAsync(Cancel))
            {
                await _db.Sections.AddRangeAsync(sections_pool, Cancel);
                await _db.Brands.AddRangeAsync(brands_pool, Cancel);
                await _db.Products.AddRangeAsync(products_pool, Cancel);

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
            var employees_pool = TestData.Employees;
            var positions_pool = TestData.Positions;
            foreach (var employee in employees_pool)
            {
                employee.Id = 0;
                employee.Position.Id = 0;
            }
            foreach (var position in positions_pool)
            {
                position.Id = 0;
            }

            await using var transaction = await _db.Database.BeginTransactionAsync(Cancel);

            await _db.Positions.AddRangeAsync(positions_pool, Cancel);
            await _db.Employees.AddRangeAsync(employees_pool, Cancel);

            await _db.SaveChangesAsync(Cancel);

            await transaction.CommitAsync(Cancel);
            _logger.LogInformation("Инициализация сотрудников выполнена успешно");
        }
    }
}
