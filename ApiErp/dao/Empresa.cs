using Dapper.Contrib.Extensions;
using System;

[Table("tb_empresa")]
public class Empresa
{
    public long id { get; set; }
    public string s_nomefantasia { get; set; }
    public string s_inscest { get; set; }
    public string s_cnpj { get; set; }
    public string s_nfecertificado { get; set; }
    public string s_nfeenviopath { get; set; }
    public string s_nfecancpath { get; set; }
    public string s_nfeinupath { get; set; }
    public string s_nfeconspath { get; set; }
    public string s_logo { get; set; }
    public bool b_ambiente_nfe_prodducao { get; set; }
    public bool b_ambiente_nfse_prodducao { get; set; }
    public string s_emailhost { get; set; }
    public string s_emailnome { get; set; }
    public string s_emaillogin { get; set; }
    public string s_emailsenha { get; set; }
    public string s_emailssl { get; set; }
    public string s_emailseg { get; set; }
    public string s_emailcopia { get; set; }
    public string s_emailporta { get; set; }
    public string s_nfsecertificado { get; set; }
    public string s_nfseenviopath { get; set; }
    public Nullable<int> id_usuario_participante { get; set; }
    public Nullable<int> id_empresa_pai { get; set; }
    public Nullable<int> id_tipo_empresa_pai { get; set; }
}