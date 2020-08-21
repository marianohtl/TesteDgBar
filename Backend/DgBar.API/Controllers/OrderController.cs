using DgBar.Domain.Entities;
using DgBar.Domain.Interfaces;
using DgBar.Domain.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DgBar.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private readonly IOrderManageService _OrderManageService;
        

        public OrderController(IOrderManageService orderManageService)
        {
            _OrderManageService = orderManageService;
        }


        /// <summary>
        /// Returns all items registered in the menu.
        /// </summary>
        /// <returns> The list of menu items.</returns>
        [HttpGet("Menu")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<SheetOrderViewModel> GetMenu()
        {
            var _menu = _OrderManageService.GetItensMenu();
            if (_menu != null)
            {
                return Ok(_menu);
            }
            else
            {
                return NoContent();
            }
        }

        /// <summary>
        /// This method registers an item in the order;
        /// </summary>
        /// <param name="order"> menu item id, order number id</param>
        /// <returns>the registration of the menu in the order</returns>
        [HttpPost("ResgistryOrder")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<SheetOrderViewModel> RegistryOrder([FromBody] SheetOrderViewModel order)
        {
            var _sheetOrder = _OrderManageService.RegistryOrder(order.IdOrder, order.IdMenu);
            SheetOrderViewModel _sheetOrderViewModel = new SheetOrderViewModel(_sheetOrder);
            if (_sheetOrderViewModel != null)
            {
                return Ok(_sheetOrderViewModel);
            }
            else
            {
                return NoContent();
            }
        }

        /// <summary>
        /// This method generates a note with: items, discount and total values;
        /// </summary>
        /// <param name="order">Order number id.</param>
        /// <returns>A note </returns>
        [HttpPost("GenerateNote")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<OrderViewModel> GenerateNote([FromBody] OrderViewModel order)
        {
            var _note = _OrderManageService.GenerateNote(order.Id);

            if (_note != null)
            {
                return Ok(_note);
            }
            else
            {
                return NoContent();
            }
        }

        /// <summary>
        /// This method resets all records in the database.
        /// </summary>
        /// <param name="id">order id</param>
        /// <returns>Response 200, with message or error.</returns>
        [HttpDelete("ResetOrder/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult ResetOrder(int id)
        {
            var result = _OrderManageService.ResetOrder(id);

            if (result)
            {
                return Ok("Deletado com sucesso!");
            }
            else
            {
                return NoContent();
            }
        }
    }
}
