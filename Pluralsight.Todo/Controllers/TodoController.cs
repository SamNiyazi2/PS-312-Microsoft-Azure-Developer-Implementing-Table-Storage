using Pluralsight.Todo.Models;
using Pluralsight.Todo.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pluralsight.Todo.Controllers
{
    public class TodoController : Controller
    {


        ITodoRepository repository;// = new TodoRepository();



        public TodoController(ITodoRepository _repository)
        {


            string test = "defaults";
            //  repository = new TodoRepository(MvcApplication.ps312AzureTableConnectionString_azureTable);
            repository = _repository;


        }

        // GET: Todo
        public ActionResult Index(IndexPageModel model)
        {
            if (model == null)
            {
                model = new IndexPageModel();

            }

            if (model.todoModel == null)
            {
                if (!isSamePageCall(Request))
                {

                    if (this.Session["CompletionSelectionOption"] != null) model.CompletionSelectionOption = (EnumCompletionSelectionOption)this.Session["CompletionSelectionOption"];
                    if (this.Session["IncludeOnlyVacationEntries"] != null) model.IncludeOnlyVacationEntries = (bool)this.Session["IncludeOnlyVacationEntries"];

                    if (this.Session["AzureTableOptionSelected"] != null) model.AzureTableOptionSelected = (EnumAzureTableTypes)this.Session["AzureTableOptionSelected"];

                }

            }


            repository.AzureTableTypes = model.AzureTableOptionSelected;










            try
            {

                repository.DO_TodoRepository();
            }
            catch (Exception)
            {

                return View(model);

                throw;
            }

















         //   repository.DO_TodoRepository();


            var entities = repository.All(model.CompletionSelectionOption, model.IncludeOnlyVacationEntries);

            this.Session["CompletionSelectionOption"] = model.CompletionSelectionOption;
            this.Session["IncludeOnlyVacationEntries"] = model.IncludeOnlyVacationEntries;
            this.Session["AzureTableOptionSelected"] = model.AzureTableOptionSelected;


            var models = entities.Select(x => new TodoModel
            {
                Id = x.RowKey,
                Group = x.PartitionKey,
                Content = x.Content,
                Due = x.Due,
                Completed = x.Completed,
                CompletedDate = x.CompletedDate,
                Timestamp = x.Timestamp
            });

            model.todoModel = models;



            return View(model);

        }


        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(TodoModel model)
        {
            if (ModelState.IsValid)
            {

                ///var repository = new TodoRepository();

                repository.CreateOrUpdate(new TodoEntity
                {
                    PartitionKey = model.Group,
                    RowKey = Guid.NewGuid().ToString(),
                    Content = model.Content,
                    Due = model.Due
                });

                return RedirectToAction("Index");
            }

            return View(model);

        }

        public ActionResult ConfirmDelete(string id, string group)
        {
            ///var repository = new TodoRepository();
            var item = repository.Get(group, id);

            return View("Delete", new TodoModel
            {
                Id = item.RowKey,
                Group = item.PartitionKey,
                Content = item.Content,
                Due = item.Due,
                Completed = item.Completed,
                Timestamp = item.Timestamp
            });
        }

        [HttpPost]
        public ActionResult Delete(string id, string group)
        {
            ///var repository = new TodoRepository();
            var item = repository.Get(group, id);
            repository.Delete(item);

            return RedirectToAction("Index");
        }

        public ActionResult Edit(string id, string group)
        {
            ///var repository = new TodoRepository();
            var item = repository.Get(group, id);

            return View(new TodoModel
            {
                Id = item.RowKey,
                Group = item.PartitionKey,
                Content = item.Content,
                Due = item.Due,
                Completed = item.Completed,
                Timestamp = item.Timestamp
            });
        }

        [HttpPost]
        public ActionResult Edit(TodoModel model)
        {
            if (ModelState.IsValid)
            {
                ///var repository = new TodoRepository();
                var item = repository.Get(model.Group, model.Id);


                if (!item.Completed && model.Completed)
                {
                    item.CompletedDate = DateTime.Now;
                }

                item.Completed = model.Completed;
                item.Content = model.Content;
                item.Due = model.Due;


                repository.CreateOrUpdate(item);

                return RedirectToAction("Index");
            }
            return View(model);

        }


        bool isSamePageCall(HttpRequestBase request)
        {
            if (Request.UrlReferrer == null) return true;
            var temp1 = Request.UrlReferrer.ToString().Split('/');
            if (temp1.Length < 4) return true;
            if (temp1[3] == "" || temp1[3].StartsWith("?")) return true;

            return false;

        }


    }
}