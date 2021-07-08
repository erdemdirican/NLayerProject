using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLayerProject.Core.Services;
using NLayerProject.WebUI.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLayerProject.WebUI.Filters
{
    public class CategoryNotFoundFilter : ActionFilterAttribute
    {
        private readonly ICategoryService _categoryService;
        public CategoryNotFoundFilter(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            int id = (int)context.ActionArguments.Values.FirstOrDefault();
            var category = await _categoryService.GetByIdAsync(id);

            if (category != null)
            {
                await next();
            }
            else
            {
                ErrorDto errorDto = new ErrorDto();

                errorDto.Errors.Add($"Id'si {id} olan kategori veritabanında bulunamadı.");

                context.Result = new RedirectToActionResult("Error", "Home", errorDto);
            }
        }
    }
}
