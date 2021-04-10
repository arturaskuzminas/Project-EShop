using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Laikrodziai.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Laikrodziai.Controllers
{
    public class KlientasController : Controller
    {
        public ActionResult Index()
        {
            KlientasRepository context = HttpContext.RequestServices.GetService(typeof(KlientasRepository)) as KlientasRepository;
            return View(context.getKlientai());
        }
    }
}