﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using my_books.ActionResults;
using my_books.Data.Models;
using my_books.Data.Services;
using my_books.Data.ViewModels;
using my_books.Exceptions;
using System;

namespace my_books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private PublishersService _publishersService;
        private readonly ILogger<PublishersController> _logger;

        /*public PublishersController(PublishersService publishersService)
        {
            _publishersService = publishersService;
        }*/

        public PublishersController(PublishersService publishersService, ILogger<PublishersController> logger)
        {
            _publishersService = publishersService;
            _logger = logger;
        }

        /*[HttpPost("add-publisher")]
        public IActionResult AddBook([FromBody] PublisherVM publisher)
        {
            _publishersService.AddPublisher(publisher);
            return Ok();
        }*/

        /*[HttpGet("get-all-publishers")]
        public IActionResult GetAllPublishers()
        {
            try
            {
                var _result = _publishersService.GetAllPublishers();

                return Ok(_result);
            }
            catch(Exception e)
            {
                return BadRequest("Sorry, we could not load the publishers");
            }
        }*/

        [HttpGet("get-all-publishers")]
        public IActionResult GetAllPublishers(string sortBy, string searchString, int pageNumber)
        {
            // throw new Exception("This is an exception thrown from GetAllPublishers()");

            try
            {
                _logger.LogInformation("This is just a log in GetAllPublishers()");

                var _result = _publishersService.GetAllPublishers(sortBy, searchString, pageNumber);

                return Ok(_result);
            }
            catch (Exception e)
            {
                return BadRequest("Sorry, we could not load the publishers");
            }
        }

        // HTTP Response Status Codes
        [HttpPost("add-publisher")]
        public IActionResult AddPublisher([FromBody] PublisherVM publisher)
        {
            try
            {
                var newPublisher = _publishersService.AddPublisher(publisher);
                return Created(nameof(AddPublisher), newPublisher);
            }
            catch(PublisherNameException ex)
            {
                return BadRequest($"{ex.Message}, Publisher name: {ex.PublisherName}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /*[HttpGet("get-publisher-by-id/{id}")]
        public IActionResult GetPublisherById(int id)
        {
            throw new Exception("This is an exception that will be handled by middleware");

            var _response = _publishersService.GetPublisherById(id);

            if(_response != null)
            {
                return Ok(_response);
            }
            else
            {
                return NotFound();
            }
        }*/

        /*[HttpGet("get-publisher-by-id/{id}")]
        public Publisher GetPublisherById(int id)
        {
            // throw new Exception("This is an exception that will be handled by middleware");

            var _response = _publishersService.GetPublisherById(id);

            if (_response != null)
            {
                // return Ok(_response);
                return _response;
            }
            else
            {
                return null;
                // return NotFound();
            }
        }*/

        /*[HttpGet("get-publisher-by-id/{id}")]
        public ActionResult<Publisher> GetPublisherById(int id)
        {
            throw new Exception("This is an exception that will be handled by middleware");

            var _response = _publishersService.GetPublisherById(id);

            if (_response != null)
            {
                //return Ok(_response);

                // Here show us an error due to it expected to Publisher Type instead Book Type
                //var newBook = new Book();
                //return newBook;

                return _response;
            }
            else
            {
                return NotFound();
            }
        }*/

        /*[HttpGet("get-publisher-by-id/{id}")]
        public CustomActionResult GetPublisherById(int id)
        {
            // throw new Exception("This is an exception that will be handled by middleware");

            var _response = _publishersService.GetPublisherById(id);

            if (_response != null)
            {
                //return Ok(_response);

                // Here show us an error due to it expected to Publisher Type instead Book Type
                //var newBook = new Book();
                //return newBook;

                var _responseObj = new CustomActionResultVM()
                {
                    Publisher = _response
                };

                return new CustomActionResult(_responseObj);

                //return _response;
            }
            else
            {
                var _responseObj = new CustomActionResultVM()
                {
                    Exception = new Exception("This is comiing from publishers controller")
                };

                return new CustomActionResult(_responseObj);

                //return NotFound();
            }
        }*/

        [HttpGet("get-publisher-by-id/{id}")]
        public IActionResult GetPublisherById(int id)
        {
            var _response = _publishersService.GetPublisherById(id);

            if (_response != null)
            {
                return Ok(_response);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("get-publisher-books-with-authors/{id}")]
        public IActionResult GetPublisherData(int id)
        {
            var _response = _publishersService.GetPublisherData(id);

            return Ok(_response);
        }

        [HttpDelete("delete-publisher-by-id/{id}")]
        public IActionResult DeletePublisherById(int id)
        {
            try
            {
                _publishersService.DeletePublisherById(id);
                return Ok();
            }
            
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
            /*try
            {
                int x1 = 1;
                int x2 = 0;

                int result = x1 / x2;

                _publishersService.DeletePublisherById(id);
                return Ok();
            }
            catch(ArithmeticException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            finally
            {
                string stopHere = "";
            }*/
            /*catch (Exception ex)
            {
                return BadRequest("Your custom message");
            }*/
        }
    }
}
