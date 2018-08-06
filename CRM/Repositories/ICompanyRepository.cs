﻿using CRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Repositories
{
    public interface ICompanyRepository : IRepository<Company>
    {
        Company GetFirst();
    }
}
