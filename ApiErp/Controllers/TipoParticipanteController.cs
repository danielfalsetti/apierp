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
    public class TipoParticipanteController : ControllerBase
    {

        [HttpPost]
        [Route("Create")]
        public ApiResponse Create(TipoParticipante tipoParticipante)
        {
            ApiResponse resp = new ApiResponse();
            if(tipoParticipante == null)
            {
                resp.Success = false;
                resp.ErrorList = new List<string>();
                resp.ErrorList.Add("Conteudo vazio");
                return resp;
            }

            if(string.IsNullOrEmpty(tipoParticipante.s_descricao)==true)
            {
                resp.Success = false;
                resp.ErrorList = new List<string>();
                resp.ErrorList.Add("Descricao nao informada");
                return resp;
            }

            try
            {
                DAOBase<TipoParticipante> daoA = new DAOBase<TipoParticipante>();
                daoA.Insert(tipoParticipante);
                
                resp.Success = true;
                resp.data = tipoParticipante;

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
        public ApiResponse Update(int id, TipoParticipante tipoParticipante)
        {
            ApiResponse resp = new ApiResponse();
            if (tipoParticipante == null)
            {
                resp.Success = false;
                resp.ErrorList = new List<string>();
                resp.ErrorList.Add("Conteudo vazio");
                return resp;
            }

            if (string.IsNullOrEmpty(tipoParticipante.s_descricao) == true)
            {
                resp.Success = false;
                resp.ErrorList = new List<string>();
                resp.ErrorList.Add("Descricao nao informada");
                return resp;
            }

            DAOBase<TipoParticipante> daoA = new DAOBase<TipoParticipante>();
            TipoParticipante a = daoA.GetById(id);
            if (a == null)
            {
                resp.Success = false;
                resp.ErrorList = new List<string>();
                resp.ErrorList.Add("TipoParticipante: " + tipoParticipante + " informada inexistente");
                return resp;
            }

            // atriuicao dos valores
            a.s_descricao = tipoParticipante.s_descricao;

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
                DAOBase<TipoParticipante> daoA = new DAOBase<TipoParticipante>();
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
                DAOBase<TipoParticipante> daoA = new DAOBase<TipoParticipante>();
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
                resp.ErrorList.Add("TipoParticipante nao informada");
                return resp;
            }
            
            DAOBase<TipoParticipante> daoAlacada = new DAOBase<TipoParticipante>();
            TipoParticipante a = daoAlacada.GetById(id);
            if (a == null)
            {
                resp.Success = false;
                resp.ErrorList = new List<string>();
                resp.ErrorList.Add("TipoParticipante: " + id + " informada inexistente");
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
