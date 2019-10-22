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
    public class EmpresaController : Controller
    {

        [HttpPost]
        [Route("Create")]
        public ApiResponse Create(Empresa empresa)
        {

            ApiResponse resp = this.ValidateEmpresa(empresa);
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
                DAOBase<Empresa> daoA = new DAOBase<Empresa>();
                daoA.Insert(empresa);

                resp.Success = true;
                resp.data = empresa;

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
        public ApiResponse Update(int id, Empresa empresa)
        {
            ApiResponse resp = this.ValidateEmpresa(empresa);
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
                resp.ErrorList.Add("Empresa nao informado");
                return resp;
            }

            DAOBase<Empresa> dao = new DAOBase<Empresa>();
            Empresa n = dao.GetById(id);
            if (n == null)
            {
                resp.Success = false;
                resp.ErrorList = new List<string>();
                resp.ErrorList.Add("Nivel de empresa: " + id + " informado inexistente");
                return resp;
            }

            // campos
            
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
                DAOBase<Empresa> daoA = new DAOBase<Empresa>();
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
                DAOBase<Empresa> daoA = new DAOBase<Empresa>();
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
                resp.ErrorList.Add("Nivel Empresa nao informado");
                return resp;
            }

            DAOBase<Empresa> dao = new DAOBase<Empresa>();
            Empresa n = dao.GetById(id);
            if (n == null)
            {
                resp.Success = false;
                resp.ErrorList = new List<string>();
                resp.ErrorList.Add("Nivel de Empresa: " + id + " informado inexistente");
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

        private ApiResponse ValidateEmpresa(Empresa empresa)
        {
            ApiResponse resp = new ApiResponse();
            
            if (empresa == null)
            {
                resp.Success = false;
                resp.ErrorList = new List<string>();
                resp.ErrorList.Add("Conteudo vazio");
                return resp;
            }

            if (string.IsNullOrEmpty(empresa.s_nomefantasia))
            {
                resp.Success = false;
                resp.ErrorList = new List<string>();
                resp.ErrorList.Add("Nome fantasia nao informado");
                return resp;
            }

            if (string.IsNullOrEmpty(empresa.s_cnpj))
            {
                resp.Success = false;
                resp.ErrorList = new List<string>();
                resp.ErrorList.Add("CNPJ nao informado");
                return resp;
            }

            if (empresa.id_empresa_pai != null)
            {
                if ((int)empresa.id_empresa_pai <= 0)
                {
                    resp.Success = false;
                    resp.ErrorList = new List<string>();
                    resp.ErrorList.Add("Empresa pai invalida");
                    return resp;
                }

                DAOBase<Empresa> dao = new DAOBase<Empresa>();
                Empresa n = dao.GetById((int)empresa.id_empresa_pai);
                if (n == null)
                {
                    resp.Success = false;
                    resp.ErrorList = new List<string>();
                    resp.ErrorList.Add("Empresa pai: " + empresa.id_empresa_pai + " informado inexistente");
                    return resp;
                }
            }

            if (empresa.id_tipo_empresa_pai != null)
            {
                if ((int)empresa.id_tipo_empresa_pai <= 0)
                {
                    resp.Success = false;
                    resp.ErrorList = new List<string>();
                    resp.ErrorList.Add("Empresa pai invalida");
                    return resp;
                }

                DAOBase<TipoEmpresaPai> dao = new DAOBase<TipoEmpresaPai>();
                TipoEmpresaPai n = dao.GetById((int)empresa.id_tipo_empresa_pai);
                if (n == null)
                {
                    resp.Success = false;
                    resp.ErrorList = new List<string>();
                    resp.ErrorList.Add("Tipo empresa pai: " + empresa.id_tipo_empresa_pai + " informado inexistente");
                    return resp;
                }
            }

            if (empresa.id_usuario_participante != null)
            {
                if ((int)empresa.id_usuario_participante <= 0)
                {
                    resp.Success = false;
                    resp.ErrorList = new List<string>();
                    resp.ErrorList.Add("Empresa pai invalida");
                    return resp;
                }

                DAOBase<Usuario> dao = new DAOBase<Usuario>();
                Usuario n = dao.GetById((int)empresa.id_usuario_participante);
                if (n == null)
                {
                    resp.Success = false;
                    resp.ErrorList = new List<string>();
                    resp.ErrorList.Add("Tipo empresa pai: " + empresa.id_usuario_participante + " informado inexistente");
                    return resp;
                }
                else
                {
                    DAOBase<TipoUsuario> daoTU = new DAOBase<TipoUsuario>();
                    TipoUsuario tuParti = daoTU.Get(w => w.s_codigo == "PAR");
                    if(tuParti!=null)
                    {
                        if(n.id_tipo_usuario != tuParti.id)
                        {
                            resp.Success = false;
                            resp.ErrorList = new List<string>();
                            resp.ErrorList.Add("Usuario informado como correspondete não é um correspondente");
                            return resp;
                        }
                    }
                    else
                    {
                        resp.Success = false;
                        resp.ErrorList = new List<string>();
                        resp.ErrorList.Add("Entidade corresponde não encontrado no sistema");
                        return resp;
                    }
                }
            }

            resp.Success = true;
            return resp;
        }
    }
}