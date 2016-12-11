using Hack121.App_Start.AutoMapper;
using Hack121.Business.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hack121.Mvc.ViewModels
{
    public class UniversityModel : IMapFrom<University>
    {
        [ScaffoldColumn(false)]
        public virtual string Id { get; set; }

        public virtual string Edrpou { get; set; }

        public virtual string Title { get; set; }

        public virtual decimal YearBudget { get; set; }

        public virtual string ShortName { get; set; }
    }
}
