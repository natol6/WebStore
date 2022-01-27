using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Domain.References;

namespace WebStore.Domain.DTO.Employees
{
    public static class PositionClassDTOMapper
    {
        public static PositionClassDTO? ToDTO(this PositionClass pos) => pos is null
            ? null
            : new PositionClassDTO
            {
                Id = pos.Id,
                Name = pos.Name,
            };

        public static PositionClass? FromDTO(this PositionClassDTO? pos) => pos is null
            ? null
            : new PositionClass
            {
                Id = pos.Id,
                Name = pos.Name,
            };

        public static IEnumerable<PositionClassDTO?> ToDTO(this IEnumerable<PositionClass?> positions) => positions.Select(ToDTO);
        public static IEnumerable<PositionClass?> FromDTO(this IEnumerable<PositionClassDTO?> positions) => positions.Select(FromDTO);
    }
}
