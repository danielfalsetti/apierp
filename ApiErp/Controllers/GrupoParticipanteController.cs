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
    public class GrupoParticipanteController : ControllerBase
    {

        [HttpPost]
        [Route("Create")]
        public ApiResponse Create(GrupoParticipante grupoParticipante)
        {
            ApiResponse resp = new ApiResponse();
            if(grupoParticipante == null)
            {
                resp.Success = false;
                resp.ErrorList = new List<string>();
                resp.ErrorList.Add("Conteudo vazio");
                return resp;
            }

            if(string.IsNullOrEmpty(grupoParticipante.s_descricao)==true)
            {
                resp.Success = false;
                resp.ErrorList = new List<string>();
                resp.ErrorList.Add("Descricao nao informada");
                return resp;
            }

            try
            {
                DAOBase<GrupoParticipante> daoA = new DAOBase<GrupoParticipante>();
                daoA.Insert(grupoParticipante);
                
                resp.Success = true;
                resp.data = grupoParticipante;

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
        public ApiResponse Update(int id, GrupoParticipante grupoParticipante)
        {
            ApiResponse resp = new ApiResponse();
            if (grupoParticipante == null)
            {
                resp.Success = false;
                resp.ErrorList = new List<string>();
                resp.ErrorList.Add("Conteudo vazio");
                return resp;
            }

            if (string.IsNullOrEmpty(grupoParticipante.s_descricao) == true)
            {
                resp.Success = false;
                resp.ErrorList = new List<string>();
                resp.ErrorList.Add("Descricao nao informada");
                return resp;
            }

            DAOBase<GrupoParticipante> daoA = new DAOBase<GrupoParticipante>();
            GrupoParticipante a = daoA.GetById(id);
            if (a == null)
            {
                resp.Success = false;
                resp.ErrorList = new List<string>();
                resp.ErrorList.Add("GrupoParticipante: " + grupoParticipante + " informada inexistente");
                return resp;
            }

            // atriuicao dos valores
            a.s_descricao = grupoParticipante.s_descricao;

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
                DAOBase<GrupoParticipante> daoA = new DAOBase<GrupoParticipante>();
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
                DAOBase<GrupoParticipante> daoA = new DAOBase<GrupoParticipante>();
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
                resp.ErrorList.Add("GrupoParticipante nao informada");
                return resp;
            }
            
            DAOBase<GrupoParticipante> daoAlacada = new DAOBase<GrupoParticipante>();
            GrupoParticipante a = daoAlacada.GetById(id);
            if (a == null)
            {
                resp.Success = false;
                resp.ErrorList = new List<string>();
                resp.ErrorList.Add("GrupoParticipante: " + id + " informada inexistente");
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
