using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiErp.dao;
using Microsoft.AspNetCore.Mvc;

namespace ApiErp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AlcadaController : ControllerBase
    {

        [HttpPost]
        [Route("Create")]
        public ApiResponse Create(Alcada alcada)
        {
            ApiResponse resp = new ApiResponse();
            if(alcada == null)
            {
                resp.Success = false;
                resp.ErrorList = new List<string>();
                resp.ErrorList.Add("Conteudo vazio");
                return resp;
            }

            if(string.IsNullOrEmpty(alcada.s_descricao)==true)
            {
                resp.Success = false;
                resp.ErrorList = new List<string>();
                resp.ErrorList.Add("Descricao nao informada");
                return resp;
            }

            try
            {
                DAOBase<Alcada> daoA = new DAOBase<Alcada>();
                daoA.Insert(alcada);
                
                resp.Success = true;
                resp.data = alcada;

                return resp;
            }
            catch (Exception e)
            {
                resp.Success = false;
                resp.ErrorList = new List<string>();
                resp.ErrorList.Add(e.Message);
                return resp;
            }

            
        }

        [HttpPost("{id}")]
        public ApiResponse Update(int id, Alcada alcada)
        {
            ApiResponse resp = new ApiResponse();
            if (alcada == null)
            {
                resp.Success = false;
                resp.ErrorList = new List<string>();
                resp.ErrorList.Add("Conteudo vazio");
                return resp;
            }

            if (string.IsNullOrEmpty(alcada.s_descricao) == true)
            {
                resp.Success = false;
                resp.ErrorList = new List<string>();
                resp.ErrorList.Add("Descricao nao informada");
                return resp;
            }

            DAOBase<Alcada> daoA = new DAOBase<Alcada>();
            Alcada a = daoA.GetById(id);
            if (a == null)
            {
                resp.Success = false;
                resp.ErrorList = new List<string>();
                resp.ErrorList.Add("Alcada: " + alcada + " informada inexistente");
                return resp;
            }

            // atriuicao dos valores
            a.s_descricao = alcada.s_descricao;

            try
            {              
                daoA.Update(a);

                resp.Success = true;
                resp.data = a;

                return resp;
            }
            catch (Exception e)
            {
                resp.Success = false;
                resp.ErrorList = new List<string>();
                resp.ErrorList.Add(e.Message);
                return resp;
            }


        }

        [HttpGet("{id}")]
        public ApiResponse Get(int id)
        {
            ApiResponse resp = new ApiResponse();

            try
            {
                DAOBase<Alcada> daoA = new DAOBase<Alcada>();
                resp.data = daoA.GetById(id);
                resp.Success = true;
                return resp;
            }
            catch (Exception e)
            {
                resp.Success = false;
                resp.ErrorList = new List<string>();
                resp.ErrorList.Add(e.Message);
                return resp;
            }
        }

        [HttpGet]
        [Route("GetAll")]
        public ApiResponse GetAll()
        {

            ApiResponse resp = new ApiResponse();

            try
            {
                DAOBase<Alcada> daoA = new DAOBase<Alcada>();
                resp.data = daoA.GetAll();
                resp.Success = true;
                return resp;
            }
            catch (Exception e)
            {
                resp.Success = false;
                resp.ErrorList = new List<string>();
                resp.ErrorList.Add(e.Message);
                return resp;
            }
        }

        [HttpDelete("{id}")]
        public ApiResponse Delete(int id)
        {
            ApiResponse resp = new ApiResponse();

            if (id <= 0)
            {
                resp.Success = false;
                resp.ErrorList = new List<string>();
                resp.ErrorList.Add("Alcada nao informada");
                return resp;
            }
            
            DAOBase<Alcada> daoAlacada = new DAOBase<Alcada>();
            Alcada a = daoAlacada.GetById(id);
            if (a == null)
            {
                resp.Success = false;
                resp.ErrorList = new List<string>();
                resp.ErrorList.Add("Alcada: " + id + " informada inexistente");
                return resp;
            }

            try
            {
                daoAlacada.Delete(a);
                resp.Success = true;

                return resp;
            }
            catch (Exception e)
            {
                resp.Success = false;
                resp.ErrorList = new List<string>();
                resp.ErrorList.Add(e.Message);
                return resp;
            }
        }
    }
}
