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
    public class TipoUsuarioController : Controller
    {

        [HttpPost]
        [Route("Create")]
        public ApiResponse Create(TipoUsuario tipoUsuario)
        {

            ApiResponse resp = this.ValidateTipoUsuario(tipoUsuario);
            if(resp.Success == false)
            {
                return resp;
            }
            else
            {
                resp = new ApiResponse();
            }
           
            try
            {
                DAOBase<TipoUsuario> daoA = new DAOBase<TipoUsuario>();
                daoA.Insert(tipoUsuario);

                resp.Success = true;
                resp.data = tipoUsuario;

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
        public ApiResponse Update(int id, TipoUsuario tipoUsuario)
        {
            ApiResponse resp = this.ValidateTipoUsuario(tipoUsuario);
            if (resp.Success == false)
            {
                return resp;
            }
            else
            {
                resp = new ApiResponse();
            }

            if (id <= 0)
            {
                resp.Success = false;
                resp.ErrorList = new List<string>();
                resp.ErrorList.Add("Nivel usuario nao informado");
                return resp;
            }

            DAOBase<TipoUsuario> dao = new DAOBase<TipoUsuario>();
            TipoUsuario n = dao.GetById(id);
            if (n == null)
            {
                resp.Success = false;
                resp.ErrorList = new List<string>();
                resp.ErrorList.Add("Nivel de usuario: " + id + " informado inexistente");
                return resp;
            }

            // atribuicoes de campos
            n.id_alcada = tipoUsuario.id_alcada;
            n.s_codigo = tipoUsuario.s_codigo;
            n.s_descricao = tipoUsuario.s_descricao;

            try
            {
                dao.Update(n);
                resp.Success = true;
                resp.data = n;

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
                DAOBase<TipoUsuario> daoA = new DAOBase<TipoUsuario>();
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
                DAOBase<TipoUsuario> daoA = new DAOBase<TipoUsuario>();
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
                resp.ErrorList.Add("Nivel usuario nao informado");
                return resp;
            }

            DAOBase<TipoUsuario> dao = new DAOBase<TipoUsuario>();
            TipoUsuario n = dao.GetById(id);
            if (n == null)
            {
                resp.Success = false;
                resp.ErrorList = new List<string>();
                resp.ErrorList.Add("Nivel de usuario: " + id + " informado inexistente");
                return resp;
            }

            try
            {
                dao.Delete(n);
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

        private ApiResponse ValidateTipoUsuario(TipoUsuario tipoUsuario)
        {
            ApiResponse resp = new ApiResponse();
            if (tipoUsuario == null)
            {
                resp.Success = false;
                resp.ErrorList = new List<string>();
                resp.ErrorList.Add("Conteudo vazio");
                return resp;
            }

            if (string.IsNullOrEmpty(tipoUsuario.s_codigo) == true)
            {
                resp.Success = false;
                resp.ErrorList = new List<string>();
                resp.ErrorList.Add("Codigo nao informada");
                return resp;
            }

            if (tipoUsuario.s_codigo.Length > 20)
            {
                resp.Success = false;
                resp.ErrorList = new List<string>();
                resp.ErrorList.Add("Codigo informado possui mais que 20 caracters");
                return resp;
            }

            if (string.IsNullOrEmpty(tipoUsuario.s_descricao) == true)
            {
                resp.Success = false;
                resp.ErrorList = new List<string>();
                resp.ErrorList.Add("Descricao nao informada");
                return resp;
            }

            if (tipoUsuario.s_descricao.Length > 200)
            {
                resp.Success = false;
                resp.ErrorList = new List<string>();
                resp.ErrorList.Add("Descricao informado possui mais que 200 caracters");
                return resp;
            }

            if (tipoUsuario.id_alcada <= 0)
            {
                resp.Success = false;
                resp.ErrorList = new List<string>();
                resp.ErrorList.Add("Alcada nao informada");
                return resp;
            }

            DAOBase<Alcada> daoAlacada = new DAOBase<Alcada>();
            if (daoAlacada.GetById(tipoUsuario.id_alcada) == null)
            {
                resp.Success = false;
                resp.ErrorList = new List<string>();
                resp.ErrorList.Add("Alcada: " + tipoUsuario.id_alcada + " informada inexistente");
                return resp;
            }

            resp.Success = true;
            return resp;
        }
    }
}