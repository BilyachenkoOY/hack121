using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Hach121.ViewModels
{
    public class BaseViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public virtual string Id { get; set; }

    }
}
