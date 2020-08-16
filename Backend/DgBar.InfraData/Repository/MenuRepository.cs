using DgBar.Domain.Entities;
using DgBar.InfraData.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DgBar.InfraData.Repository
{
    public class MenuRepository
    {

        private readonly BarDGContext _context;

        public MenuRepository(BarDGContext context)
        {
            this._context = context;
        }

        public Menu GetById(int Id)
        {
            return _context.Menu.Where(x => x.Id == Id).FirstOrDefault();
        }

    }
}
