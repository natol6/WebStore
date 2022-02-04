using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Domain.Entities;

namespace WebStore.Interfaces.Services
{
    /// <summary>Хранилище данных корзины</summary>
    public interface ICartStore
    {
        /// <summary>Объект корзины</summary>
        public Cart Cart { get; set; }
    }
}
