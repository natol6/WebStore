using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Services.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Components
{
    public class SectionsViewComponent : ViewComponent
    {
        private readonly IProductData _ProductData;
        public SectionsViewComponent(IProductData productData) => _ProductData = productData;
        public IViewComponentResult Invoke()
        {
            var sections = _ProductData.GetSections();
            var parent_sections = sections.Where(s => s.Parent is null);
            var parent_sections_view = new List<SectionViewModel>();
            //var parent_sections_view = parent_sections
            //    .Select(s => new SectionViewModel
            //    {
            //        Id = s.Id,
            //        Name = s.Name,
            //        Order = s.Order,
            //    })
                //.ToList();
            foreach(var parent_section in parent_sections)
            {
                var par_sec_view = new SectionViewModel
                {
                    Id = parent_section.Id,
                    Name = parent_section.Name,
                    Order = parent_section.Order,
                };
                var childs = sections.Where(s => s.Parent == parent_section);
                foreach(var child_section in childs)
                {
                    par_sec_view.ChildSections.Add(new SectionViewModel
                    {
                        Id = child_section.Id,
                        Name= child_section.Name,
                        Order= child_section.Order,
                        Parent = par_sec_view,
                    });
                }
                par_sec_view.ChildSections.Sort((a, b) => Comparer<int>.Default.Compare(a.Order, b.Order));
                parent_sections_view.Add(par_sec_view);
            }
            parent_sections_view.Sort((a, b) => Comparer<int>.Default.Compare(a.Order, b.Order));
            return View(parent_sections_view);
        }
        
    }
}
