﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BusinessFlus;
using EntityFlus;

namespace FlusBankWeb.Controllers
{
    public class BanksOperationController : Controller
    {
        // GET: BanksOperations
        public ActionResult Index()
        {
            return View(BanksOperationsBL.BanksOperationsList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(BanksOperation BankOperation)
        {
            try
            {
                BankOperation.Operation = BankOperation.Id+1;
                await BanksOperationsBL.Create(BankOperation);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(BankOperation);
            }
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            BanksOperation operation = BanksOperationsBL.Details(id);
            return View(operation);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(BanksOperation editOperation)
        {
            try
            {
                await BanksOperationsBL.Edit(editOperation);
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("", "No se pudo editar");
                return View(editOperation);
            }
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            BanksOperation operation = BanksOperationsBL.Details(id);
            return View(operation);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                ModelState.AddModelError("", "Se necesita seleccionar una operación");
                return RedirectToAction("Index");
            }
            BanksOperation operation = BanksOperationsBL.Details(id.Value);
            return View(operation);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await BanksOperationsBL.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("", "No se puedo eliminar la operación");
                return View(id);
            }
        }

    }
}