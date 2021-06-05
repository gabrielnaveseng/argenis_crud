using Microsoft.AspNetCore.Mvc;
using Argenis.CRUD.Borders.Shared;
using System;

namespace Argenis.CRUD.Api.Models
{
    public interface IActionResultConverter
    {
        IActionResult Convert<Tin, Tout>(UseCaseResponse<Tin> response, Func<Tin?, Tout?>? converter = null) where Tin : class where Tout : class;
        IActionResult Convert<Tin>(UseCaseResponse<Tin> response) where Tin : class;
    }
}
