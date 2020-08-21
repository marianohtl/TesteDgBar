using DgBar.Domain.Entities;
using DgBar.Domain.Interfaces;
using DgBar.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace DgBar.Domain.Services
{
    public class OrderManageService : IOrderManageService
    {
        private readonly IOrderSheetRepository _repositoryOrder;
        private readonly IMenuRepository _repositoryMenu;

        public OrderManageService(IOrderSheetRepository repositoryOrder, IMenuRepository repositoryMenu)
        {
            _repositoryOrder = repositoryOrder;
            _repositoryMenu = repositoryMenu;
        }

        public List<Menu> GetItensMenu()
        {
            try
            {
                var result = _repositoryMenu.GetAllItems();
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }



        public SheetOrder RegistryOrder(int? idSheetOrder, int? idItem)
        {
            try
            {

                SheetOrder sheetOrderItem = _repositoryOrder.GetItemByIdAndOrder(idItem, idSheetOrder);
                Menu menu = _repositoryMenu.GetById(idItem);
                SheetOrder newOrderSheet = new SheetOrder();

                if (menu != null)
                {
                    if (sheetOrderItem != null)
                    {
                        if(sheetOrderItem.IdMenu == 3 && sheetOrderItem.Amount >= 3)
                        {
                            return sheetOrderItem;
                        }
                        sheetOrderItem.Amount += 1;
                        _repositoryOrder.Update(sheetOrderItem);
                        return sheetOrderItem;
                    }
                    else
                    {
                        newOrderSheet.IdOrder = idSheetOrder;
                        newOrderSheet.IdMenu = idItem;
                        newOrderSheet.Amount = 1;
                        _repositoryOrder.Save(newOrderSheet);
                    }
                }

                return newOrderSheet;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }


        public OrderViewModel GenerateNote(int id)
        {
            try
            {
                List<SheetOrder> orders = _repositoryOrder.GetAllOrdersById(id);

                OrderViewModel note = new OrderViewModel();
                note.Discount = 0;
                note.TotalPrice = 0;
                note.FinalPrice = 0;


                int amountBeer = 0;
                int amountWater = 0;
                int amountConhaque = 0;
                foreach (SheetOrder details in orders)
                {

                    if (details.IdMenu != null)
                    {
                        var item = _repositoryMenu.GetById(details.IdMenu);

                        note.Items.Add(item);

                        switch (details.IdMenu)
                        {

                            case 1:
                                amountBeer = details.Amount;
                                double result = amountBeer / 5;
                                var amoutDesc = (int) Math.Ceiling(result);
                                var discountBeer = amoutDesc * item.Price;
                                note.Discount += discountBeer;
                                break;
                            case 2:
                                amountConhaque = details.Amount;
                                break;
                            case 4:
                                amountWater = details.Amount;
                                break;

                        }

                        note.TotalPrice += details.Amount * item.Price;
                    }
                }

                if (amountConhaque >= 3 && amountBeer >= 2 && amountWater > 0) 
                {
                    note.Discount += 70;
                }
                note.FinalPrice = note.TotalPrice - note.Discount;
                

                return note;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }

        }



        public bool ResetOrder(int id)
        {
            try
            {
                var orders = _repositoryOrder.GetAllOrdersById(id);
                foreach (SheetOrder details in orders)
                {
                    var result = _repositoryOrder.Delete(details.IdOrder);
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

    }
}
