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
    public class TipoEmpresaPaiController : ControllerBase
    {

        [HttpPost]
        [Route("Create")]
        public ApiResponse Create(TipoEmpresaPai tipoEmpresaPai)
        {
            ApiResponse resp = new ApiResponse();
            if(tipoEmpresaPai == null)
            {
                resp.Success = false;
                resp.ErrorList = new List<string>();
                resp.ErrorList.Add("Conteudo vazio");
                return resp;
            }

            if(string.IsNullOrEmpty(tipoEmpresaPai.s_descricao)==true)
            {
                resp.Success = false;
                resp.ErrorList = new List<string>();
                resp.ErrorList.Add("Descricao nao informada");
                return resp;
            }

            try
            {
                DAOBase<TipoEmpresaPai> daoA = new DAOBase<TipoEmpresaPai>();
                daoA.Insert(tipoEmpresaPai);
                
                resp.Success = true;
                resp.data = tipoEmpresaPai;

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
        public ApiResponse Update(int id, TipoEmpresaPai tipoEmpresaPai)
        {
            ApiResponse resp = new ApiResponse();
            if (tipoEmpresaPai == null)
            {
                resp.Success = false;
                resp.ErrorList = new List<string>();
                resp.ErrorList.Add("Conteudo vazio");
                return resp;
            }

            if (string.IsNullOrEmpty(tipoEmpresaPai.s_descricao) == true)
            {
                resp.Success = false;
                resp.ErrorList = new List<string>();
                resp.ErrorList.Add("Descricao nao informada");
                return resp;
            }

            DAOBase<TipoEmpresaPai> daoA = new DAOBase<TipoEmpresaPai>();
            TipoEmpresaPai a = daoA.GetById(id);
            if (a == null)
            {
                resp.Success = false;
                resp.ErrorList = new List<string>();
                resp.ErrorList.Add("TipoEmpresaPai: " + tipoEmpresaPai + " informada inexistente");
                return resp;
            }

            // atriuicao dos valores
            a.s_descricao = tipoEmpresaPai.s_descricao;

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
                DAOBase<TipoEmpresaPai> daoA = new DAOBase<TipoEmpresaPai>();
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
                DAOBase<TipoEmpresaPai> daoA = new DAOBase<TipoEmpresaPai>();
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
                resp.ErrorList.Add("TipoEmpresaPai nao informada");
                return resp;
            }
            
            DAOBase<TipoEmpresaPai> daoAlacada = new DAOBase<TipoEmpresaPai>();
            TipoEmpresaPai a = daoAlacada.GetById(id);
            if (a == null)
            {
                resp.Success = false;
                resp.ErrorList = new List<string>();
                resp.ErrorList.Add("TipoEmpresaPai: " + id + " informada inexistente");
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
