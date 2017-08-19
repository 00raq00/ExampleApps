using FizzWare.NBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace WebApplication6.Controllers
{
    public class AjaxController : Controller
    {
      static  IList<PersonalDetail> fakeList = Builder<PersonalDetail>.CreateListOfSize(100)
            .All().With(c => c.Name = Faker.Name.FullName())
            .With(c => c.EMail = Faker.Internet.Email()).Build();
      
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult SearchData(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                return PartialView(fakeList);
            }

            Thread.Sleep(2000);

            var data = fakeList.Where(f =>
            f.Name.ToLower().Contains(keyword.ToLower())).ToList();
            return PartialView(data);
        }
    }
}