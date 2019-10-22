using Dapper.Contrib.Extensions;
using System;

[Table("tb_tipo_empresa_pai")]
public class TipoEmpresaPai
{
    public long id { get; set; }
    public string s_descricao { get; set; }
}