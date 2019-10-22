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
    public class NivelUsuarioController : Controller
    {

        [HttpPost]
        [Route("Create")]
        public ApiResponse Create(NivelUsuario nivelUsuario)
        {

            ApiResponse resp = this.ValidateNivelUsuario(nivelUsuario);
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
                DAOBase<NivelUsuario> daoA = new DAOBase<NivelUsuario>();
                daoA.Insert(nivelUsuario);

                resp.Success = true;
                resp.data = nivelUsuario;

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
        public ApiResponse Update(int id, NivelUsuario nivelUsuario)
        {
            ApiResponse resp = this.ValidateNivelUsuario(nivelUsuario);
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

            DAOBase<NivelUsuario> dao = new DAOBase<NivelUsuario>();
            NivelUsuario n = dao.GetById(id);
            if (n == null)
            {
                resp.Success = false;
                resp.ErrorList = new List<string>();
                resp.ErrorList.Add("Nivel de usuario: " + id + " informado inexistente");
                return resp;
            }

            // atribuicoes de campos
            n.id_alcada = nivelUsuario.id_alcada;
            n.s_codigo = nivelUsuario.s_codigo;
            n.s_descricao = nivelUsuario.s_descricao;

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
                DAOBase<NivelUsuario> daoA = new DAOBase<NivelUsuario>();
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
                DAOBase<NivelUsuario> daoA = new DAOBase<NivelUsuario>();
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

            DAOBase<NivelUsuario> dao = new DAOBase<NivelUsuario>();
            NivelUsuario n = dao.GetById(id);
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

        private ApiResponse ValidateNivelUsuario(NivelUsuario nivelUsuario)
        {
            ApiResponse resp = new ApiResponse();
            if (nivelUsuario == null)
            {
                resp.Success = false;
                resp.ErrorList = new List<string>();
                resp.ErrorList.Add("Conteudo vazio");
                return resp;
            }

            if (string.IsNullOrEmpty(nivelUsuario.s_codigo) == true)
            {
                resp.Success = false;
                resp.ErrorList = new List<string>();
                resp.ErrorList.Add("Codigo nao informada");
                return resp;
            }

            if (nivelUsuario.s_codigo.Length > 20)
            {
                resp.Success = false;
                resp.ErrorList = new List<string>();
                resp.ErrorList.Add("Codigo informado possui mais que 20 caracters");
                return resp;
            }

            if (string.IsNullOrEmpty(nivelUsuario.s_descricao) == true)
            {
                resp.Success = false;
                resp.ErrorList = new List<string>();
                resp.ErrorList.Add("Descricao nao informada");
                return resp;
            }

            if (nivelUsuario.s_descricao.Length > 200)
            {
                resp.Success = false;
                resp.ErrorList = new List<string>();
                resp.ErrorList.Add("Descricao informado possui mais que 200 caracters");
                return resp;
            }

            if (nivelUsuario.id_alcada <= 0)
            {
                resp.Success = false;
                resp.ErrorList = new List<string>();
                resp.ErrorList.Add("Alcada nao informada");
                return resp;
            }

            DAOBase<Alcada> daoAlacada = new DAOBase<Alcada>();
            if (daoAlacada.GetById(nivelUsuario.id_alcada) == null)
            {
                resp.Success = false;
                resp.ErrorList = new List<string>();
                resp.ErrorList.Add("Alcada: " + nivelUsuario.id_alcada + " informada inexistente");
                return resp;
            }

            resp.Success = true;
            return resp;
        }
    }
}