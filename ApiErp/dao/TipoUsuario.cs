using Dapper.Contrib.Extensions;
using System;

[Table("tb_tipo_usuario")]
public class TipoUsuario
{
    public long id { get; set; }
    public string s_descricao { get; set; }
    public string s_codigo { get; set; }
    public int id_alcada { get; set; }
}