using Dapper.Contrib.Extensions;
using System;

[Table("tb_usuario")]
public class Usuario
{
    public long id { get; set; }
    public string s_nome { get; set; }
    public string s_senha { get; set; }
    public int id_nivel_usuario { get; set; }
    public int id_tipo_usuario { get; set; }
    public string s_emailhost { get; set; }
    public string s_emailnome { get; set; }
    public string s_emaillogin { get; set; }
    public string s_emailsenha { get; set; }
    public bool b_email_ssl { get; set; }
    public bool b_emailseg { get; set; }
    public string s_emailcopia { get; set; }
    public int n_emailporta { get; set; }
    public bool b_usuario_pf { get; set; }
    public string s_cpf_cnpj { get; set; }
    public Nullable<int> id_grupo_participante { get; set; }
    public Nullable<int> id_tipo_participante { get; set; }
}