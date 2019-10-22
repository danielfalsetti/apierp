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
    public class UsuarioController : Controller
    {

        [HttpPost]
        [Route("Create")]
        public ApiResponse Create(Usuario usuario)
        {

            ApiResponse resp = this.ValidateUsuario(usuario);
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
                DAOBase<Usuario> daoA = new DAOBase<Usuario>();
                daoA.Insert(usuario);

                resp.Success = true;
                resp.data = usuario;

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
        public ApiResponse Update(int id, Usuario usuario)
        {
            ApiResponse resp = this.ValidateUsuario(usuario);
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
                resp.ErrorList.Add("Usuario nao informado");
                return resp;
            }

            DAOBase<Usuario> dao = new DAOBase<Usuario>();
            Usuario n = dao.GetById(id);
            if (n == null)
            {
                resp.Success = false;
                resp.ErrorList = new List<string>();
                resp.ErrorList.Add("Nivel de usuario: " + id + " informado inexistente");
                return resp;
            }

            // atribuicoes de campos
            n.b_emailseg = usuario.b_emailseg;
            n.b_email_ssl = usuario.b_email_ssl;
            n.id_nivel_usuario = usuario.id_nivel_usuario;
            n.id_tipo_usuario = usuario.id_tipo_usuario;
            n.n_emailporta = usuario.n_emailporta;
            n.s_emailcopia = usuario.s_emailcopia;
            n.s_emailhost = usuario.s_emailhost;
            n.s_emaillogin = usuario.s_emaillogin;
            n.s_emailnome = usuario.s_emailnome;
            n.s_emailsenha = usuario.s_emailsenha;
            n.s_nome = usuario.s_nome;
            n.b_usuario_pf = usuario.b_usuario_pf;
            n.s_cpf_cnpj = usuario.s_cpf_cnpj;
            n.id_tipo_participante = usuario.id_tipo_participante;
            n.id_grupo_participante = usuario.id_grupo_participante;
            
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
                DAOBase<Usuario> daoA = new DAOBase<Usuario>();
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
                DAOBase<Usuario> daoA = new DAOBase<Usuario>();
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

            DAOBase<Usuario> dao = new DAOBase<Usuario>();
            Usuario n = dao.GetById(id);
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

        private ApiResponse ValidateUsuario(Usuario usuario)
        {
            ApiResponse resp = new ApiResponse();
            if (usuario == null)
            {
                resp.Success = false;
                resp.ErrorList = new List<string>();
                resp.ErrorList.Add("Conteudo vazio");
                return resp;
            }

            if (string.IsNullOrEmpty(usuario.s_nome) == true)
            {
                resp.Success = false;
                resp.ErrorList = new List<string>();
                resp.ErrorList.Add("Nome nao informada");
                return resp;
            }

            if (usuario.s_nome.Length > 100)
            {
                resp.Success = false;
                resp.ErrorList = new List<string>();
                resp.ErrorList.Add("Nome informado possui mais que 100 caracters");
                return resp;
            }

            if (usuario.id_nivel_usuario <= 0)
            {
                resp.Success = false;
                resp.ErrorList = new List<string>();
                resp.ErrorList.Add("Nivel usuario nao informado");
                return resp;
            }

            DAOBase<NivelUsuario> dao = new DAOBase<NivelUsuario>();
            if (dao.GetById(usuario.id_nivel_usuario) == null)
            {
                resp.Success = false;
                resp.ErrorList = new List<string>();
                resp.ErrorList.Add("Nivel Usuário: " + usuario.id_nivel_usuario+ " informado inexistente");
                return resp;
            }

            if (usuario.id_tipo_usuario <= 0)
            {
                resp.Success = false;
                resp.ErrorList = new List<string>();
                resp.ErrorList.Add("Tipo usuario nao informado");
                return resp;
            }

            DAOBase<TipoUsuario> daoTU = new DAOBase<TipoUsuario>();
            if (daoTU.GetById(usuario.id_tipo_usuario) == null)
            {
                resp.Success = false;
                resp.ErrorList = new List<string>();
                resp.ErrorList.Add("Tipo Usuário: " + usuario.id_tipo_usuario + " informado inexistente");
                return resp;
            }

            if(string.IsNullOrEmpty(usuario.s_emailhost)==false)
            {
                if(usuario.s_emailhost.Length > 100)
                {
                    resp.Success = false;
                    resp.ErrorList = new List<string>();
                    resp.ErrorList.Add("Emailhost informado possui mais que 100 caracteres");
                    return resp;
                }
            }
            //

            if (string.IsNullOrEmpty(usuario.s_emailnome) == false)
            {
                if (usuario.s_emailnome.Length > 100)
                {
                    resp.Success = false;
                    resp.ErrorList = new List<string>();
                    resp.ErrorList.Add("Emailnome informado possui mais que 100 caracteres");
                    return resp;
                }
            }
            //

            if (string.IsNullOrEmpty(usuario.s_emaillogin) == false)
            {
                if (usuario.s_emaillogin.Length > 100)
                {
                    resp.Success = false;
                    resp.ErrorList = new List<string>();
                    resp.ErrorList.Add("Email login informado possui mais que 100 caracteres");
                    return resp;
                }
            }
            //

            if (string.IsNullOrEmpty(usuario.s_emailsenha) == false)
            {
                if (usuario.s_emailsenha.Length > 100)
                {
                    resp.Success = false;
                    resp.ErrorList = new List<string>();
                    resp.ErrorList.Add("Email senha informado possui mais que 100 caracteres");
                    return resp;
                }
            }
            //

            if (string.IsNullOrEmpty(usuario.s_cpf_cnpj) == false)
            {
                if (usuario.s_cpf_cnpj.Length > 30)
                {
                    resp.Success = false;
                    resp.ErrorList = new List<string>();
                    resp.ErrorList.Add("CPF/CNPJ informado possui mais que 30 caracteres");
                    return resp;
                }
            }
            //

            if(usuario.id_grupo_participante != null)
            {
                DAOBase<GrupoParticipante> daoGP = new DAOBase<GrupoParticipante>();
                if (daoGP.GetById((int)usuario.id_grupo_participante) == null)
                {
                    resp.Success = false;
                    resp.ErrorList = new List<string>();
                    resp.ErrorList.Add("Grupo Participante: " + usuario.id_grupo_participante + " informado inexistente");
                    return resp;
                }
            }

            if (usuario.id_tipo_participante != null)
            {
                DAOBase<GrupoParticipante> daoGP = new DAOBase<GrupoParticipante>();
                if (daoGP.GetById((int)usuario.id_tipo_participante) == null)
                {
                    resp.Success = false;
                    resp.ErrorList = new List<string>();
                    resp.ErrorList.Add("Tipo Participante: " + usuario.id_tipo_participante + " informado inexistente");
                    return resp;
                }
            }


            resp.Success = true;
            return resp;
        }
    }
}