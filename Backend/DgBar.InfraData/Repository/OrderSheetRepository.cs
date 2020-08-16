using DgBar.Domain.Entities;
using DgBar.Domain.Interfaces;
using DgBar.InfraData.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DgBar.InfraData.Repository
{
    public class OrderSheetRepository : IOrderSheetRepository
    {
          private readonly BarDGContext _context;

          public OrderSheetRepository(BarDGContext context)
          {
              this._context = context;
          }

          public SheetOrder GetById(int Id)
          {
              return _context.SheetOrder.Where(x => x.Id == Id).FirstOrDefault();
          }


          public SheetOrder Save(SheetOrder sheetOrder)
          {
              _context.Add(sheetOrder);
              _context.SaveChanges();
              return sheetOrder;
          }

          public SheetOrder Update(SheetOrder sheetOrder)
          {
              var _sheetOrder = _context.SheetOrder.Where(e => e.Id == sheetOrder.Id).FirstOrDefault();
              if (_sheetOrder != null)
              {
                  _sheetOrder.IdMenu = sheetOrder.IdMenu;
                  _sheetOrder.Amount = sheetOrder.Amount;
                  _sheetOrder.Discount = sheetOrder.Discount;
                  _context.Entry(_sheetOrder).State = EntityState.Modified;
                  _context.SaveChanges();
              }
              return _sheetOrder;
          }


          public bool Delete(int idSheetOrder)
          {
              var orderItem = _context.SheetOrder.Where(i => i.Id == idSheetOrder).FirstOrDefault();
              if (idSheetOrder != null)
              {
                  _context.Entry(orderItem).State = EntityState.Deleted;
                  _context.SaveChanges();
                  return true;
              }
              return false;
          }
    }
}
