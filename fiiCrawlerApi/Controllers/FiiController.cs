﻿using fiiCrawlerApi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;

namespace fiiCrawlerApi.Controllers
{
    public class FiiController : Controller
    {
        [HttpGet]
        //[EnableCors()]
        [Route("getListaResumo/")]
        //https://localhost:44304/Fii/getListaResumo/
        public ActionResult getListaResumo()
        {
            try
            {
                /*
                 * CORS !_________________________________________
                 * 
                 * Headers necessários para evitar bloqueios do CORS.
                 * Neste caso são enviados headers específicos para permitir
                 * o acesso vindo de todas as origens externas a da api (https://localhost:44304/)
                 * e para o acesso ao tipo de método requisitado.
                */
                HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                HttpContext.Response.Headers.Add("Access-Control-Allow-Methods", "GET");

                var crawler = new fiiCrawlerApi.WebScraper.Crawler();
                var cache = new fiiCrawlerApi.Cache.GerenciadorDeCache();

                List<Fii> dados = new List<Fii>();                
                dados = cache.RetornarDadosDeCache().Result;                                                   
                
                HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                ContentResult res200 = new ContentResult();
                res200.Content = JsonConvert.SerializeObject(dados);
                res200.ContentType = "application/json";                
                return res200;
            }
            catch (Exception ex)
            {
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}