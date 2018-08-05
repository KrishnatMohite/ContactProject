﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataObject;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System.Configuration;

namespace ContactsProject.Controllers
{
    public class ContactsController : Controller
    {
        string APIBaseURL = ConfigurationManager.AppSettings["WebAPIURL"];
        // GET: Contacts
        public ActionResult Index()
        {
            List<Contact> _lstContacts = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(APIBaseURL);
                //HTTP GET
                var responseTask = client.GetAsync("Contacts");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var Response = result.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    _lstContacts = JsonConvert.DeserializeObject<List<Contact>>(Response);
                }
                else //web api sent error response 
                {
                    //log response status here..
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(_lstContacts);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return View(contact);
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(APIBaseURL);
                //HTTP GET
                StringContent content = new StringContent(JsonConvert.SerializeObject(contact), Encoding.UTF8, "application/json");
                var responseTask = client.PostAsync("Contacts", content);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var Response = result.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    int id = JsonConvert.DeserializeObject<int>(Response);
                }
                else //web api sent error response 
                {
                    //log response status here..
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            Contact contact = new Contact();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(APIBaseURL);
                //HTTP GET
                var responseTask = client.GetAsync("Contacts?id=" + id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var Response = result.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    contact = JsonConvert.DeserializeObject<Contact>(Response);
                }
                else //web api sent error response 
                {
                    //log response status here..
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(contact);
        }

        [HttpPost]
        public ActionResult Edit(Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return View(contact);
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(APIBaseURL);
                //HTTP GET
                StringContent content = new StringContent(JsonConvert.SerializeObject(contact), Encoding.UTF8, "application/json");
                var responseTask = client.PutAsync("Contacts?id=" + contact.Id, content);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var Response = result.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    int id = JsonConvert.DeserializeObject<int>(Response);
                }
                else //web api sent error response 
                {
                    //log response status here..
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            Contact contact = new Contact();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(APIBaseURL);
                //HTTP GET
                var responseTask = client.GetAsync("Contacts?id=" + id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var Response = result.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    contact = JsonConvert.DeserializeObject<Contact>(Response);
                }
                else //web api sent error response 
                {
                    //log response status here..
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(contact);
        }

        [HttpPost]
        public ActionResult Delete(Contact contact)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(APIBaseURL);
                //HTTP GET
                //StringContent content = new StringContent(JsonConvert.SerializeObject(contact), Encoding.UTF8, "application/json");
                var responseTask = client.DeleteAsync("Contacts?id=" + contact.Id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var Response = result.Content.ReadAsStringAsync().Result;
                }
                else //web api sent error response 
                {
                    //log response status here..
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}