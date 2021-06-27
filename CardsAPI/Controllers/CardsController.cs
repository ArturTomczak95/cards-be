using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using CardsAPI.Helpers;
using CardsAPI.Models;
using DataAccess.BusinessLogic;
using DataAccess.Models.Dtos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CardsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CardsController : ControllerBase
    {
        private static int CardValidityInYears = 3;
        private static long CardNumberMinValue = 11111111111111111;
        private static long CardNumberMaxValue = 99999999999999999;

        private readonly CardTypeProcessor _processor;
        private readonly ILogger<CardsController> _logger;

        public CardsController(IConfiguration config, ILogger<CardsController> logger)
        {
            string cardsTaskDbcConnectionString = config.GetConnectionString("CardsTaskDB");
            _processor = new CardTypeProcessor(cardsTaskDbcConnectionString);
            _logger = logger;
        }

        [HttpGet("/api/cards")]
        public IEnumerable<UserCardDto> GetCards()
        {
            return _processor.GetUserCards();
        }

        [HttpPost("/api/card")]
        public ActionResult AddCard(UserCardBaseDto userCardBase)
        {
            ResponseDto response = new ResponseDto(500, "An error during saving item to database occurred");
            try
            {
                userCardBase.CardNumber = RandomValuesHelper.LongRandom(CardNumberMinValue, CardNumberMaxValue);
                userCardBase.ValidThru = DateTime.Now.AddYears(CardValidityInYears);

                string message = CardsHelper.IsUserCardBaseDtoInvalid(userCardBase);
                if (!string.IsNullOrEmpty(message))
                {
                    response = new ResponseDto(400, message);
                    return StatusCode(response.Status, response);
                }

                int affectedRows = _processor.InsertUserCard(userCardBase);
                return affectedRows != 0
                    ? StatusCode(200, new ResponseDto(200, ""))
                    : StatusCode(response.Status, response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error during saving item to database occurred.", ex);
                return StatusCode(response.Status, response);
            }
        }

        [HttpPut("/api/card/{userCardId}")]
        public ActionResult UpdateUserName(UserCardBaseDto userCardBase, int userCardId)
        {
            ResponseDto response = new ResponseDto(500, "An error during updating item in database occurred");
            try
            {
                if (string.IsNullOrEmpty(userCardBase.LastName))
                {
                    return StatusCode(400, new
                    {
                        status = 400,
                        message = "Last name cannot be empty"
                    });
                }

                int affectedRows = _processor.UpdateUserLastName(userCardBase, userCardId);

                return affectedRows != 0 ? StatusCode(200, new ResponseDto(200, "")) : StatusCode(response.Status, response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error during updating item in database occurred, userCardID: {userCardId}", ex);
                return StatusCode(response.Status, response);
            }
        }

        [HttpDelete("/api/card/{userCardId}")]
        public ActionResult RemoveCard(int userCardId)
        {
            ResponseDto response = new ResponseDto(500, "An error during deleting item in database occurred");

            try
            {
                int affectedRows = _processor.DeleteUserCardWithCard(userCardId);

                return affectedRows != 0 ? StatusCode(200, new ResponseDto(200, "")) : StatusCode(response.Status, response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error during removing item in database occurred, userCardID: {userCardId}", ex);
                return StatusCode(response.Status, response);
            }
        }
    }
}
